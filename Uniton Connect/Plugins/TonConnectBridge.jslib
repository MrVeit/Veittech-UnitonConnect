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

        convertBocToHash: function(BoC, callback)
        {
            window.tonWeb = new TonWeb();
            const tonWeb = window.tonWeb;
            
            tonWeb.boc.Cell.oneFromBoc(tonWeb.utils.base64ToBytes(Boc)).hash()
            .then((bocCellBytes) =>
            {
                const hashBase64 = tonWeb.utils.base64ToBytes(bocCellBytes);

                console.log(`[UNITON CONNECT] Parsed transaction hash: ${hashBase64}`);

                const hashPtr = allocate(intArrayFromString(hashBase64), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [hashPtr]);
            })
            .catch((error) =>
            {
                console.error(`[UNITON CONNECT] Failed to parse transaction hash: ${error.message}`);

                const errorPtr = allocate(intArrayFromString(
                    error.message || "Parsing failed"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [errorPtr]);

                _free(errorPtr);
            });
        },

        sendTransaction: async function(nanoInTon, 
            recipientAddress, callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                console.warn(`[UNITON CONNECT] Sdk is not initialized,` +
                    `sending transactions not available`);

                const nullPtr = allocate(intArrayFromString("null"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [nullPtr]);

                _free(nullPtr);

                return;
            }

            const transationData = 
            {
                validUntil: Math.floor(Date.now() / 1000) + 60,
                messages: [{ 
                    address: UTF8ToString(recipientAddress), 
                    amount: UTF8ToString(nanoInTon) 
                }]
            };

            await window.tonConnectUI.sendTransaction(transationData).then((result) =>
            {
                console.log(`[UNITON CONNECT] Parsed transaction result: ${JSON.stringify(result)}`);

                if (result && result.boc) 
                {
                    const bocPtr = allocate(intArrayFromString(result.boc), 'i8', ALLOC_NORMAL);

                    console.log(`[UNITON CONNECT] Transaction sent successfully, BOC: ${result.boc}`);

                    dynCall('vi', callback, [bocPtr]);

                    _free(bocPtr);

                    return;
                }

                const emptyPtr = allocate(intArrayFromString("EMPTY_BOC"), 'i8', ALLOC_NORMAL);

                console.error(`[UNITON CONNECT] Transaction sent, but no BOC returned`);

                dynCall('vi', callback, [emptyPtr]);

                _free(emptyPtr);
            })
            .catch((error) =>
            {
                console.error(`[UNITON CONNECT] Failed to send transaction: ${error.message}`);

                const errorPtr = allocate(intArrayFromString(
                    error.message || "Transaction failed"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [errorPtr]);

                _free(errorPtr);
            });
        }
    },

    Init: function(manifestUrl, dAppUrl, callback)
    {
        tonConnect.init(manifestUrl, dAppUrl, callback);
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
    },

    ConvertBocToHash: function(Boc, callback)
    {
        tonConnect.convertBocToHash(Boc, callback);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);