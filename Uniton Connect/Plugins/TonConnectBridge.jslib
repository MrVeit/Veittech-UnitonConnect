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

        sendTransaction: async function(nanoInTon, 
            recipientAddress, callback)
        {
            const transationData = 
            {
                validUntil: Math.floor(Date.now() / 1000) + 60,
                messages: [{ 
                    address: UTF8ToString(recipientAddress), 
                    amount: UTF8ToString(nanoInTon) 
                }]
            };

            try
            {
                if (!tonConnect.isAvailableSDK())
                {
                    console.warn(`[UNITON CONNECT] Sdk is not initialized, sending transactions not available`);

                    return;
                }
                
                const result = await window.tonConnectUI.sendTransaction(transationData);

                console.log(`[UNITON CONNECT] Response for transaction sended, result: ${result}`);

                if (result) 
                {
                    const bocPtr = allocate(intArrayFromString(result.boc || ""), 'i8', ALLOC_NORMAL);

                    console.log(`[UNITON CONNECT] Transaction sent successfully, BOC: ${result.boc}`);

                    dynCall('vi', callback, [bocPtr]);

                    _free(bocPtr);

                    return;
                } 

                const emptyPtr = allocate(intArrayFromString(""), 'i8', ALLOC_NORMAL);

                console.error(`[UNITON CONNECT] Transaction sent, but no BOC returned`);

                dynCall('vi', callback, [emptyPtr]);

                _free(emptyPtr);
            }
            catch (error)
            {
                console.error(`[UNITON CONNECT] Failed to send transaction, reason: ${error.message}`);

                const nullPtr = allocate(intArrayFromString("null"), 'i8', ALLOC_NORMAL);

                dynCall('vi', callback, [nullPtr]);

                _free(nullPtr);
            }
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
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);