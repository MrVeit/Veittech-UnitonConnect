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
            try
            {
                const url = UTF8ToString(manifestUrl);
                const appUrl = UTF8ToString(dAppUrl);

                window.tonConnectUI = new TON_CONNECT_UI.TonConnectUI(
                {
                    manifestUrl: url,
                    uiPreferences: { theme: THEME.DARK }
                });

                console.log(`Library entity state: ${window.tonConnectUI}`);

                if (!tonConnect.isAvailableSDK())
                {
                    console.warn(`Something wrong, library entity is not exist`);

                    dynCall('vi', callback, [0]);

                    return;
                }

                window.tonConnectUI.uiOptions = { twaReturnUrl: appUrl };

                console.log(`[UNITON CONNECT] Sdk successfully initialized`);

                dynCall('vi', callback, [1]);
            }
            catch (error)
            {
                console.error(`[UNITON CONNECT] Failed to initialize Uniton Connect.`);

                dynCall('vi', callback, [0]);
            }
        },

        openModal: async function(callback)
        {
            try
            {
                if (!tonConnect.isAvailableSDK())
                {
                    console.warn(`[UNITON CONNECT] Opening modal canceled, sdk is not initialized`);

                    dynCall('vi', callback, [0]);

                    return;
                }

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
                if (!tonConnect.isAvailableSDK())
                {
                    console.warn(`[UNITON CONNECT] Disconnect is not available, sdk is not initialized`);

                    return;
                }

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
                return;
            }

            window.unsubscribeToStatusChange = window
                .tonConnectUI.onStatusChange((wallet) =>
            {
                if (wallet)
                {
                    const walletInfo = JSON.stringify(wallet);
                    const walletPtr = allocate(
                        intArrayFromString(walletInfo), 'i8', ALLOC_NORMAL);

                    console.log(`Parsed wallet: ` +
                        `${JSON.stringify(window.tonConnectUI.wallet)}`);
                    console.log(`Parsed wallet info: ` +
                        `${JSON.stringify(window.tonConnectUI.walletInfo)}`);
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

        subscribeToRestoreConnection: function(callback)
        {
            if (!tonConnect.isAvailableSDK())
            {
                console.warn(`[UNITON CONNECT] Sdk is not loaded, restore connection cancelled`);

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

    SubscribeToRestoreConnection: function(callback)
    {
        tonConnect.subscribeToRestoreConnection(callback);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);