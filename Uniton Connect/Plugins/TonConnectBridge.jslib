const tonConnectBridge = {
    $tonConnect: {
        init: function(manifestUrl, dAppUrl, callback)
        {
            try
            {
                const url = UTF8ToString(manifestUrl);
                const appUrl = UTF8ToString(dAppUrl);

                window.tonConnectUI = new TON_CONNECT_UI.TonConnectUI(
                {
                    manifestUrl: url
                });

                window.tonConnectUI.uiOptions =
                {
                    twaReturnUrl: appUrl,
                    language: 'ru',
                    uiPreferences: { theme: THEME.DARK }
                };

                Runtime.dynCall('vi', callback, [1]);
            }
            catch (error)
            {
                console.error(`[UNITON CONNECT] Failed to initialize Uniton Connect.`);

                Runtime.dynCall('vi', callback, [0]);
            }
        },

        openModal: async function(callback)
        {
            try
            {
                await window.tonConnectUI.openModal();

                Runtime.dynCall('vi', callback, [1]);

                console.log(`[UNITON CONNECT] Connect modal opened`);
            }
            catch (eror)
            {
                Runtime.dynCall('vi', callback, [0]);

                console.error(`[UNITON CONNECT] Failed to open connect modal`);
            }
        },

        disconnect: async function(callback)
        {
            try
            {
                await window.tonConnectUI.disconnect();

                const statusPtr = allocate(
                    intArrayFromString("200"), 'i8', ALLOC_NORMAL);

                Runtime.dynCall('vi', callback, [statusPtr]);

                console.log(`[UNITON CONNECT] Wallet successfully disconnected`);
            }
            catch (eror)
            {
                const statusPtr = allocate(
                    intArrayFromString("500"), 'i8', ALLOC_NORMAL);

                Runtime.dynCall('vi', callback, [statusPtr]);

                console.error(`[UNITON CONNECT] Failed to disconnect active wallet`);
            }
        },

        subscribeToStatusChanged: function(callback)
        {
            window.unsubscribeToStatusChange = window
                .tonConnectUI.onStatusChange((wallet) =>
            {
                if (wallet)
                {
                    const walletInfo = JSON.stringify(wallet);
                    const walletPtr = allocate(
                        intArrayFromString(walletInfo), 'i8', ALLOC_NORMAL);

                    Runtime.dynCall('vi', callback, [walletPtr]);

                    console.log(`Parsed wallet: ${window.tonConnectUI.wallet}`);
                    console.log(`Parsed wallet info: ${window.tonConnectUI.walletInfo}`);
                    console.log(`Parsed account: ${window.tonConnectUI.account}`);
                    console.log(`Parsed is connected status: ${window.tonConnectUI.connected}`)

                    console.log(`[UNITON CONNECT] Wallet successfully connected, data: ${walletInfo}`);
                }
                else
                {
                    Runtime.dynCall('vi', callback, [0]);

                    console.log(`[UNITON CONNECT] Wallet disconnected.`);
                }
            });
        },

        unsubscribeToStatusChanged: function()
        {
            if (window.unsubscribeToStatusChange)
            {
                window.unsubscribeToStatusChange();

                window.unsubscribeToStatusChange = null;
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
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);