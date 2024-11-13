const tonConnectBridge = {
    $tonConnect: {
        isAvailableSDK: function()
        {
            if (!window.tonConnectUI)
            {
                console.error(`Ton Connect UI is not initialized`);

                return false;
            }

            return true;
        },

        init: function(manifestUrl, dAppUrl, callback)
        {
                const url = UTF8ToString(manifestUrl);
                const appUrl = UTF8ToString(dAppUrl);

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
            catch (eror)
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
            catch (eror)
            {
                dynCall('vi', callback, [0]);
            }
        },

        disconnect: async function(callback)
        {
            try
            {
                await window.tonConnectUI.disconnect();

                const statusPtr = allocate(
                    intArrayFromString("200"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [statusPtr]);

                _free(statusPtr);
            }
            catch (eror)
            {
                const statusPtr = allocate(
                    intArrayFromString("500"), 'i8', ALLOC_NORMAL);

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
                    const walletPtr = allocate(
                        intArrayFromString(walletInfo), 'i8', ALLOC_NORMAL);

                    console.log(`Parsed account: ` +
                        `${JSON.stringify(window.tonConnectUI.account)}`);

                    dynCall('vi', callback, [walletPtr]);

                    _free(walletPtr);

                    return;
                }

                dynCall('vi', callback, [0]);
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

        subscribeToRestoreConnection: function(manifestUrl, dAppUrl, callback)
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

        getTransactionPayload: async function(nanoInTon, 
            recipientAddress, message)
        {
            const tonWeb = window.tonWeb;

            var transactionData;

            if (!messagePayload || messagePayload === "CLEAR")
            {
                transactionData = {
                    validUntil: Math.floor(Date.now() / 1000) + 60,
                    messages: [
                    {  
                        address: recipientAddress, 
                        amount: nanoInTon
                    }
                ]};

                console.log(`[UNITON CONNECT] Created payload without message`);

                return transactionData;
            }

            console.log(`Message for payload: ${messagePayload}`);

            let cellBuilder = new tonWeb.boc.Cell();

            cellBuilder.bits.writeUint(0, 32);
            cellBuilder.bits.writeString(message);
                
            let payload = tonWeb.utils.bytesToBase64(await cellBuilder.toBoc());

            console.log(`[UNITON CONNECT] Created transaction message payload: ${payload}`);

            transactionData = {
                validUntil: Math.floor(Date.now() / 1000) + 60,
                messages: [
                {  
                    address: recipientAddress, 
                    amount: nanoInTon,
                    payload: payload
                }
            ]};

            console.log(`Transaction data for send: ${JSON.stringify(transactionData)}`);

            return transactionData;
        },

        sendTransaction: async function(nanoInTon, 
            recipientAddress, message, callback) 
        {
            if (!tonConnect.isAvailableSDK()) 
            {
                console.warn(`[UNITON CONNECT] SDK is not initialized, sending transactions not available`);
                const nullPtr = allocate(intArrayFromString("null"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [nullPtr]);

                _free(nullPtr);

                return;
            }

            const tonWeb = window.tonWeb;

            const nanotons = UTF8ToString(nanoInTon);
            const address = UTF8ToString(recipientAddress);
            const payloadMessage = UTF8ToString(message);

            const transactionData = await tonConnect.getTransactionPayload(
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
                    const emptyPtr = allocate(intArrayFromString("EMPTY_BOC"), 'i8', ALLOC_NORMAL);

                    console.error(`[UNITON CONNECT] No BOC returned from transaction`);

                    dynCall('vi', callback, [emptyPtr]);

                    _free(emptyPtr);

                    return;
                }

                console.log(`[UNITON CONNECT] Parsed boc after send transaction: ${result.boc}`);
                
                let claimedBoc = result.boc;

                console.log(`[UNITON CONNECT] Boc after send transaction: ${claimedBoc}`);

                const bocBytes = tonWeb.utils.base64ToBytes(claimedBoc);

                console.log(`[UNITON CONNECT] Parsed boc bytes: ${bocBytes}`);

                const bocCellBytes = await tonWeb.boc.Cell.oneFromBoc(bocBytes).hash();

                console.log(`[UNITON CONNECT] Parsed boc cell bytes: ${bocCellBytes}`);

                const hashBase64 = tonWeb.utils.bytesToBase64(bocCellBytes);

                console.log(`[UNITON CONNECT] Parsed transaction hash: ${hashBase64}`);

                const hashPtr = allocate(intArrayFromString(hashBase64), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [hashPtr]);

                _free(hashPtr);
            }
            catch (error)
            {
                console.error(`[UNITON CONNECT] Failed to validate transaction, reason: ${error.message}`);

                const errorPtr = allocate(intArrayFromString(""), 'i8', ALLOC_NORMAL);
            
                dynCall('vi', callback, [errorPtr]);

                _free(errorPtr);
            }
        }
    },

    Init: function(manifestUrl, dAppUrl, callback)
    {
        tonConnect.init(manifestUrl, dAppUrl, callback);
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

    SubscribeToRestoreConnection: function(
        manifestUrl, dAppUrl, callback)
    {
        tonConnect.subscribeToRestoreConnection(
            manifestUrl, dAppUrl, callback);
    },

    SendTransaction: function(nanoInTon, recipientAddress, callback)
    {
        tonConnect.sendTransaction(nanoInTon, recipientAddress, "CLEAR", callback);
    },

    SendTransactionWithMessage: function(nanoInTon, 
        recipientAddress, message, callback)
    {
        tonConnect.sendTransaction(nanoInTon, recipientAddress, message, callback);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);