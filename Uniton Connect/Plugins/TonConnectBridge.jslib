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

        getJettonTransactionPayload: async function(nanoInJetton,
            recipient, jettonMasterAddress, nanoInTonForGas, message)
        {
            const tonWeb = window.tonWeb;

            const recipientAddress = new tonWeb.Address(recipient);
            const masterAddress = new tonWeb.Address(jettonMasterAddress);

            let forwardPayload = new tonWeb.boc.Cell();

            if (message)
            {
                forwardPayload.bits.writeUint(0, 32);
                forwardPayload.bits.writeString(message);
            }

            let transferBody = new tonWeb.boc.Cell();
            
            transferBody.bits.writeUint(0xf8a7ea5, 32);
            transferBody.bits.writeUint(Date.now(), 64);
            transferBody.bits.writeCoins(nanoInJetton); // JETTON AMOUNT FOR TRANSFER
            transferBody.bits.writeAddress(recipientAddress);
            transferBody.bits.writeAddress(recipientAddress);
            transferBody.bits.writeBit(0);
            transferBody.bits.writeCoins(tonWeb.utils.toNano("1"));

            if (message)
            {
                transferBody.bits.writeBit(1);
                transferBody.refs.push(forwardPayload);
            }
            else
            {
                transferBody.bits.writeBit(0);
            }

            let payload = tonWeb.utils.bytesToBase64(await transferBody.toBoc());

            const transactionData = {
                validUntil: Math.floor(Date.now() / 1000) + 360,
                messages: [
                {
                    address: masterAddress.toString(true),
                    amount: nanoInTonForGas, /// GAS FEE
                    payload: payload,
                }
            ]};

            return transactionData;
        },

        sendJettonTransaction: async function(nanoInTon,
            recipientAddress, jettonMasterAddress, 
            nanoInTonForGas, message, callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                const nullPtr = tonConnect.allocString("null");

                dynCall('vi', callback, [nullPtr]);

                _free(nullPtr);

                return;
            }

            const tonWeb = window.tonWeb;

            const jettonAmount = UTF8ToString(nanoInTon);
            const gasFee = UTF8ToString(nanoInTonForGas);
            const address = UTF8ToString(recipientAddress);
            const masterAddress = UTF8ToString(jettonMasterAddress);

            let payloadMessage= UTF8ToString(message);

            console.log(`Received jetton transaction data: 
                amount: ${jettonAmount}, gas fee: ${gasFee}, recipient address: ${address},
                master jetton address: ${masterAddress}`);

            if (payloadMessage === "CLEAR")
            {
                payloadMessage = null;
            }

            const transactionData = await tonConnect.getJettonTransactionPayload(
                jettonAmount, address, masterAddress, gasFee, payloadMessage);

            console.log(`Loaded transaction payload: ${JSON.stringify(transactionData)}`);

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

                    console.error(`[UNITON CONNECT] No BOC returned from jetton transaction`);

                    dynCall('vi', callback, [emptyPtr]);

                    _free(emptyPtr);

                    return;
                }
                
                let claimedBoc = result.boc;

                const bocBytes = tonWeb.utils.base64ToBytes(claimedBoc);
                const bocCellBytes = await tonWeb.boc.Cell.oneFromBoc(bocBytes).hash();
                const hashBase64 = tonWeb.utils.bytesToBase64(bocCellBytes);

                console.log(`[UNITON CONNECT] Parsed jetton transaction hash: ${hashBase64}`);

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

    SendJettonTransaction: function(nanoInTon, masterAddress, 
        recipientAddress, gasFee, callback)
    {
        tonConnect.sendJettonTransaction(nanoInTon, recipientAddress, 
            masterAddress, gasFee, "CLEAR", callback);
    },

    SendJettonTransactionWithMessage: function(nanoInTon, masterAddress, 
        recipientAddress, gasFee, message, callback)
    {
        tonConnect.sendJettonTransaction(nanoInTon, recipientAddress, 
            masterAddress, gasFee, message, callback);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);