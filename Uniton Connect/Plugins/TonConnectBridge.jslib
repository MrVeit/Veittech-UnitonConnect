const tonConnectBridge = {
    $tonConnect: {
        isAvailableSDK: function()
        {
            if (!window.tonConnectUI)
            {
                console.error(`[UNITON CONNECT] TonConnectUI is not initialized`);

                return false;
            }

            return true;
        },

        init: function(manifestUrl, dAppUrl, callback)
        {
                const url = UTF8ToString(manifestUrl);
                const appUrl = UTF8ToString(dAppUrl);

                console.log(`[UNITON CONNECT] Claimed init credentials, url: ${url}, appUrl: ${appUrl}`);

                window.tonConnectUI = new TON_CONNECT_UI.TonConnectUI(
                {
                    manifestUrl: url,
                });

                if (!tonConnect.isAvailableSDK())
                {
                    console.warn(`[UNITON CONNECT] Library entity is not exist, something wrong...`);

                    dynCall('vi', callback, [0]);

                    return;
                }

                console.log(`[UNITON CONNECT] Sdk successfully initialized`);

                dynCall('vi', callback, [1]);
        },
        
        initTonWeb: function()
        {
            window.tonWeb = new TonWeb();

            if (!window.tonWeb)
            {
                console.error(`[UNITON CONNECT] Failed to create Ton Web`);

                return;
            }

            console.log(`[UNITON CONNECT] Ton Web successfully created`);
        },

        openModal: async function(callback)
        {
            try
            {
                await window.tonConnectUI.openModal();

                console.log(`[UNITON CONNECT] Modal window successfully opened`);

                dynCall('vi', callback, [1]);
            }
            catch (eror)
            {
                console.error(`[UNITON CONNECT] Failed to open modal window`);

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

                console.log(`[UNITON CONNECT] Wallet successfully disconnected`);

                dynCall('vi', callback, [statusPtr]);

                _free(statusPtr);
            }
            catch (eror)
            {
                const statusPtr = allocate(
                    intArrayFromString("500"), 'i8', ALLOC_NORMAL);

                console.error(`[UNITON CONNECT] Failed to disconnect active wallet`);

                dynCall('vi', callback, [statusPtr]);

                _free(statusPtr);
            }
        },

        subscribeToStatusChanged: function(callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                console.warn(`[UNITON CONNECT] Sdk is not available, ` +
                    `listening wallet connection event stopped`)
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
                    console.log(`Parsed is connected status: ` +
                        `${JSON.stringify(window.tonConnectUI.connected)}`)

                    console.log(`[UNITON CONNECT] Wallet successfully connected, data: ${walletInfo}`);

                    dynCall('vi', callback, [walletPtr]);

                    _free(walletPtr);

                    return;
                }

                console.log(`[UNITON CONNECT] Wallet is disconnected`);

                dynCall('vi', callback, [0]);
            });
        },

        unsubscribeToStatusChanged: function()
        {
            if (window.unsubscribeToStatusChange)
            {
                window.unsubscribeToStatusChange();

                window.unsubscribeToStatusChange = null;

                console.log(`[UNITON CONNECT] Listening wallet status change unsubscribed`)
            }
        },

        subscribeToRestoreConnection: function(manifestUrl, dAppUrl, callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                console.warn(`[UNITON CONNECT] Sdk is not initialized, restoring connection cancelled`);

                dynCall('vi', callback, [0]);

                return;
            }

            window.tonConnectUI.connectionRestored.then(restored =>
            {
                if (restored)
                {
                    console.log(`[UNITON CONNECT] Wallet connection successfully restored, wallet:` +
                        `${JSON.stringify(window.tonConnectUI.wallet)},` +
                        `${JSON.stringify(window.tonConnectUI.walletInfo)}`);

                    dynCall('vi', callback, [1]);

                    return;
                }

                console.warn(`[UNITON CONNECT] Wallet connection was not restored`);

                dynCall('vi', callback, [0]);
            });
        },

        handleTransactionError: function(error, callback)
        {
            console.error(`[UNITON CONNECT] Failed to validate transaction:` +
                `${error.message || 'UNKNOWN ERROR'}`);

            const errorPtr = allocate(intArrayFromString(
                error.message || "Transaction failed"), 'i8', ALLOC_NORMAL);
            
            dynCall('vi', callback, [errorPtr]);

            _free(errorPtr);
        },

        sendTransaction: async function(nanoInTon, recipientAddress, callback) 
        {
            if (!tonConnect.isAvailableSDK()) 
            {
                console.warn(`[UNITON CONNECT] SDK is not initialized, sending transactions not available`);
                const nullPtr = allocate(intArrayFromString("null"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [nullPtr]);

                _free(nullPtr);

                return;
            }

            const transactionData = {
                validUntil: Math.floor(Date.now() / 1000) + 60,
                messages: [{ 
                    address: UTF8ToString(recipientAddress), 
                    amount: UTF8ToString(nanoInTon) 
            }]};

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

                const tonWeb = window.tonWeb;
                
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
                tonConnect.handleTransactionError(error, callback);
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
        tonConnect.sendTransaction(nanoInTon, recipientAddress, callback);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);