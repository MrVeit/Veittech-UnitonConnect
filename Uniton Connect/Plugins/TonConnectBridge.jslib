const tonConnectBridge = {
    $tonConnect: {
        allocString: function (stringData)
        {
            let ptr;

            if (typeof allocate === 'undefined')
            {
                console.log(`[UNITON CONNECT] Detected Unity version 2023+`);

                const length = lengthBytesUTF8(stringData) + 1;

                ptr = _malloc(length);

                stringToUTF8(stringData, ptr, length);

                return ptr;
            }

            return allocate(intArrayFromString(stringData), 'i8', ALLOC_NORMAL);
        },

        isAvailableSDK: function()
        {
            if (!window.tonConnectUI)
            {
                console.error(`Ton Connect UI is not initialized`);

                return false;
            }

            return true;
        },

        init: function(manifestUrl, callback)
        {
                const url = UTF8ToString(manifestUrl);

                window.tonConnectUI = new TON_CONNECT_UI.TonConnectUI(
                {
                    manifestUrl: url,
                });

                if (!tonConnect.isAvailableSDK())
                {
                    dynCall('vi', callback, [0]);

                    return;
                }

                dynCall('vi', callback, [1]);
        },
        
        initTonWeb: function()
        {
            window.tonWeb = new TonWeb();
        },

        openModal: async function(callback)
        {
            try
            {
                await window.tonConnectUI.openModal();

                dynCall('vi', callback, [1]);
            }
            catch (error)
            {
                dynCall('vi', callback, [0]);
            }
        },

        closeModal: function(callback)
        {
            try
            {
                window.tonConnectUI.closeModal();

                dynCall('vi', callback, [1]);
            }
            catch (error)
            {
                dynCall('vi', callback, [0]);
            }
        },

        disconnect: async function(callback)
        {
            try
            {
                await window.tonConnectUI.disconnect();

                const statusPtr = tonConnect.allocString("200");

                dynCall('vi', callback, [statusPtr]);

                _free(statusPtr);
            }
            catch (error)
            {
                const statusPtr = tonConnect.allocString("500");

                dynCall('vi', callback, [statusPtr]);

                _free(statusPtr);
            }
        },

        subscribeToStatusChanged: function(callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                return;
            }

            window.unsubscribeToStatusChange = window
                .tonConnectUI.onStatusChange((wallet) =>
            {
                if (wallet)
                {
                    const walletInfo = JSON.stringify(window.tonConnectUI.account);
                    const walletPtr = tonConnect.allocString(walletInfo);

                    console.log(`Parsed account: ` +
                        `${JSON.stringify(window.tonConnectUI.account)}`);

                    dynCall('vi', callback, [walletPtr]);

                    _free(walletPtr);

                    return;
                }

                const statusPtr = tonConnect.allocString("CONNECT_FAILED");

                dynCall('vi', callback, [statusPtr]);

                _free(statusPtr);
            });
        },

        unsubscribeToStatusChanged: function()
        {
            if (window.unsubscribeToStatusChange)
            {
                window.unsubscribeToStatusChange();

                window.unsubscribeToStatusChange = null;
            }
        },

        subscribeToRestoreConnection: function(callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                dynCall('vi', callback, [0]);

                return;
            }

            window.tonConnectUI.connectionRestored.then(restored =>
            {
                if (restored)
                {
                    dynCall('vi', callback, [1]);

                    return;
                }

                dynCall('vi', callback, [0]);
            });
        },

        subscribeToTransactionEvents: function(
            successCallback, errorCallback)
        {
            const signedHandler = (event) =>
            {
                console.log(`[UNITON CONNECT] Transaction signed:`, event.detail);

                const signedData = JSON.stringify(event.detail);
                const signedPtr = tonConnect.allocString(signedData);

                dynCall('vi', successCallback, [signedPtr]);

                _free(signedPtr);
            };

            const failedHandler = (event) =>
            {
                console.warn(`[UNITON CONNECT] Transaction signing failed:`, event.detail);

                const failedData = JSON.stringify(event.detail);
                const failedPtr = tonConnect.allocString(failedData);

                dynCall('vi', errorCallback, [failedPtr]);

                _free(failedPtr);
            };

            window.addEventListener('ton-connect-ui-transaction-signed', signedHandler);
            window.addEventListener('ton-connect-ui-transaction-signing-failed', failedHandler);

            window._tonConnectTransactionSignedHandler = signedHandler;
            window._tonConnectTransactionFailedHandler = failedHandler;

            console.log('[UNITON CONNECT] Subscribed to transaction events');
        },

        unsubscribeToTransactionEvents: function()
        {
            if (window._tonConnectTransactionSignedHandler)
            {
                window.removeEventListener('ton-connect-ui-transaction-signed',
                    window._tonConnectTransactionSignedHandler);
                
                delete window._tonConnectTransactionSignedHandler;

                console.log(`[UNITON CONNECT] Unsubscribed from transaction-signed event`);
            }

            if (window._tonConnectTransactionFailedHandler)
            {
                window.removeEventListener('ton-connect-ui-transaction-signing-failed',
                    window._tonConnectTransactionFailedHandler);

                delete window._tonConnectTransactionSignedHandler;

                console.log(`[UNITON CONNECT] Unsubsribed from transaction-signing-failed event`);
            }
        },

        getTonTransactionPayload: async function(nanoInTon, 
            recipientAddress, message)
        {
            const tonWeb = window.tonWeb;

            var transactionData;

            if (!message || message === "CLEAR")
            {
                transactionData = {
                    validUntil: Math.floor(Date.now() / 1000) + 60,
                    messages: [
                    {  
                        address: recipientAddress, 
                        amount: nanoInTon
                    }
                ]};

                return transactionData;
            }

            let cellBuilder = new tonWeb.boc.Cell();

            cellBuilder.bits.writeUint(0, 32);
            cellBuilder.bits.writeString(message);
                
            let payload = tonWeb.utils.bytesToBase64(await cellBuilder.toBoc());

            transactionData = {
                validUntil: Math.floor(Date.now() / 1000) + 60,
                messages: [
                {  
                    address: recipientAddress, 
                    amount: nanoInTon,
                    payload: payload
                }
            ]};

            return transactionData;
        },

        getJettonWalletAddress: async function(address, ownerAddress)
        {
            try
            {
                const tonWeb = window.tonWeb;

                const jettonMaster = new tonWeb.utils.Address(jettonMaster);
                const owner = new tonWeb.utils.Address(ownerAddress);

                const method = 'get_wallet_address';
                const stack = [
                    ['tvmSlice', owner.toSlice()],
                ];

                const result = await tonWeb.provider.call(
                    jettonMaster.toString(), method, stack);

                if (!result || !result.stack || result.stack.length === 0)
                {
                    throw new Error("Empty result from Jetton Master contract");
                }  

                const walletAddress = result.stack[0][1].toString();

                console.log(`Jetton Wallet address: ${walletAddress}`);

                return walletAddress;
            }
            catch (error)
            {
                console.error(`Failed to get Jetton Wallet address`, error);
            }
        },

        sendJettonTransaction: async function(amount, 
            senderAddress, recipientAddress, callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                const errorMessage = tonConnect.allocString("SDK is not initialized");

                dynCall('vi', callback, [errorMessage]);

                _free(errorMessage);

                return;
            }

            try
            {
                const tonWeb = window.tonWeb;

                const JETTON_MASTER_ADDRESS = "0:b113a994b5024a16719f69139328eb759596c38a25f59028b146fecdc3621dfe";
                const forwardTonAmount = "0.05";
                const forwardAmountInNano = tonWeb.utils.toNano(forwardTonAmount);

                const amountInNano = tonWeb.utils.toNano(UTF8ToString(amount));
                const sender = new tonWeb.utils.Address(UTF8ToString(senderAddress));
                const recipient = new tonWeb.utils.Address(UTF8ToString(recipientAddress));
                const jettonMaster = new tonWeb.utils.Address(JETTON_MASTER_ADDRESS);

                const senderJettonWallet = await tonConnect.getJettonWalletAddress(
                    jettonMaster.toString(), sender.toString());

                const body = tonWeb.utils
                    .beginCell()
                    .storeUint(0xf8a7ea5, 32)
                    .storeUint(0, 64)
                    .storeCoins(amountInNano)
                    .storeAddress(recipient)
                    .storeAddress(sender)
                    .storeUint(0, 1)
                    .storeCoins(forwardAmountInNano)
                    .storeUint(0, 1)
                    .endCell();

                const transaction = {
                    validUntil: Math.floor(Date.now() / 1000) + 360,
                    messages: [
                        {
                            address: senderJettonWallet,
                            amount: forwardAmountInNano.toString(),
                            payload: body.toBoc().toString("base64"),
                        },
                    ],
                };

                const result = await window.tonConnectUI.sendTransaction(transaction,
                {
                    modals: ['before', 'success', 'error']
                });

                console.log(`Jetton Transaction sent successfully:`, result);

                const resultPtr = tonConnect.allocString(JSON.stringify(result));

                console.log(`Result Jetton transaction: ${resultPtr}`);

                dynCall("vi", callback, [resultPtr]);

                _free(resultPtr);
            }
            catch (error)
            {
                console.error(`Jetton Transaction Error:`, error);

                const errorPtr = tonConnect.allocString("TRANSACTION_FAILED");

                dynCall('vi', callback, [errorPtr]);

                _free(errorPtr);
            }
        },

        sendTransaction: async function(nanoInTon, 
            recipientAddress, message, callback) 
        {
            if (!tonConnect.isAvailableSDK()) 
            {
                const nullPtr = tonConnect.allocString("null");

                dynCall('vi', callback, [nullPtr]);

                _free(nullPtr);

                return;
            }

            const tonWeb = window.tonWeb;

            const nanotons = UTF8ToString(nanoInTon);
            const address = UTF8ToString(recipientAddress);
            const payloadMessage = UTF8ToString(message);

            const transactionData = await tonConnect.getTonTransactionPayload(
                nanotons, address, payloadMessage);

            try
            {
                const result = await window.tonConnectUI.sendTransaction(transactionData, 
                {
                    modals: ['before', 'success', 'error'],
                    notifications: ['before', 'success', 'error']
                });
            
                if (!result || !result.boc)
                {
                    const emptyPtr = tonConnect.allocString("EMPTY_BOC");

                    console.error(`[UNITON CONNECT] No BOC returned from transaction`);

                    dynCall('vi', callback, [emptyPtr]);

                    _free(emptyPtr);

                    return;
                }
                
                let claimedBoc = result.boc;

                const bocBytes = tonWeb.utils.base64ToBytes(claimedBoc);
                const bocCellBytes = await tonWeb.boc.Cell.oneFromBoc(bocBytes).hash();
                const hashBase64 = tonWeb.utils.bytesToBase64(bocCellBytes);

                console.log(`[UNITON CONNECT] Parsed transaction hash: ${hashBase64}`);

                const hashPtr = tonConnect.allocString(hashBase64);

                dynCall('vi', callback, [hashPtr]);

                _free(hashPtr);
            }
            catch (error)
            {
                const errorPtr = tonConnect.allocString("");
            
                dynCall('vi', callback, [errorPtr]);

                _free(errorPtr);
            }
        }
    },

    Init: function(manifestUrl, callback)
    {
        tonConnect.init(manifestUrl, callback);
    },

    InitTonWeb: function()
    {
        tonConnect.initTonWeb();
    },

    OpenModal: function(callback)
    {
        tonConnect.openModal(callback);
    },

    CloseModal: function(callback)
    {
        tonConnect.closeModal(callback);
    },

    Disconnect: function(callback)
    {
        tonConnect.disconnect(callback);
    },

    SubscribeToStatusChange: function(callback)
    {
        tonConnect.subscribeToStatusChanged(callback);
    },

    UnSubscribeToStatusChange: function() 
    {
        tonConnect.unsubscribeToStatusChanged();
    },

    SubscribeToRestoreConnection: function(callback)
    {
        tonConnect.subscribeToRestoreConnection(callback);
    },

    SubscribeToTransactionEvents: function(successCallback, errorCallback)
    {
        tonConnect.subscribeToTransactionEvents(successCallback, errorCallback);
    },

    UnSubscribeToTransactionEvents: function()
    {
        tonConnect.unsubscribeToTransactionEvents();
    },

    SendTransaction: function(nanoInTon, recipientAddress, callback)
    {
        tonConnect.sendTransaction(nanoInTon, recipientAddress, "CLEAR", callback);
    },

    SendTransactionWithMessage: function(nanoInTon, 
        recipientAddress, message, callback)
    {
        tonConnect.sendTransaction(nanoInTon, recipientAddress, message, callback);
    },

    SendJettonTransaction: function(amount,
        senderAddress, recipientAddress, callback)
    {
        tonConnect.sendJettonTransaction(amount, 
            senderAddress, recipientAddress, callback);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);