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

                console.log(`Lib entity state: ${window.tonConnectUI}`);

                window.tonConnectUI.uiOptions =
                {
                    twaReturnUrl: appUrl,
                    language: 'ru',
                    uiPreferences: { theme: THEME.DARK }
                };

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
                await window.tonConnectUI.openModal();

                console.log(`[UNITON CONNECT] Connect modal opened`);

                dynCall('vi', callback, [1]);
            }
            catch (eror)
            {
                console.error(`[UNITON CONNECT] Failed to open connect modal`);

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
            if (typeof TON_CONNECT_UI === 'undefined')
            {
                console.error('TON_CONNECT_UI is not loaded. Ensure the script is correctly linked.');
                
                return;
            }

            if (!window.tonConnectUI)
            {
                console.error('tonConnectUI entity is not loaded. Ensure the script is correctly linked.')

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

                    console.log(`Parsed wallet: ${window.tonConnectUI.wallet}`);
                    console.log(`Parsed wallet info: ${window.tonConnectUI.walletInfo}`);
                    console.log(`Parsed account: ${window.tonConnectUI.account}`);
                    console.log(`Parsed is connected status: ${window.tonConnectUI.connected}`)

                    console.log(`[UNITON CONNECT] Wallet successfully connected, data: ${walletInfo}`);

                    dynCall('vi', callback, [walletPtr]);

                    _free(walletPtr);
                }
                else
                {
                    console.log(`[UNITON CONNECT] Wallet is disconnected`);

                    dynCall('vi', callback, [0]);
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