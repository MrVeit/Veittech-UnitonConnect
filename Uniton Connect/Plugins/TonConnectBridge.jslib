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

        isAvailableTonConnect: function()
        {
            if (!window.tonConnectUI)
            {
                console.error(`[UNITON CONNECT] Ton Connect UI is not initialized`);

                return false;
            }

            return true;
        },

        isAvailableTonWeb: function()
        {
            if (!window.tonWeb)
            {
                console.error(`[UNITON CONNECT] Library 'Ton Web' is not exist`);

                return false;
            }

            return true;
        },

        isInitialized: function()
        {
            if (!tonConnect.isAvailableTonConnect())
            {
                return false;
            }

            if (!tonConnect.isAvailableTonWeb())
            {
                return false;
            }

            return true;
        },

        parseAddress: function(address)
        {
            const correctAddress = UTF8ToString(address);

            const parsedAddress = new window.tonWeb.utils.Address(correctAddress);

            return parsedAddress;
        },

        init: function(manifestUrl, callback)
        {
            const url = UTF8ToString(manifestUrl);

            window.tonConnectUI = new TON_CONNECT_UI.TonConnectUI(
            {
                manifestUrl: url,
            });

            if (!tonConnect.isAvailableTonConnect())
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
            if (!tonConnect.isInitialized())
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

                    console.log(`[UNITON CONNECT] Parsed account: ` +
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
            if (!tonConnect.isInitialized())
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

        getTonTransactionPayload: async function(
            nanoInTon, recipientAddress, message)
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

        sendAsssetsTransaction: async function(
            itemAddress, gasFeeAmount, payload, callback)
        {
            if (!tonConnect.isInitialized())
            {
                const errorMessage = tonConnect.allocString("SDK is not initialized");

                dynCall('vi', callback, [errorMessage]);

                _free(errorMessage);

                return;
            }

            const tonWeb = window.tonWeb;

            const senderOrNftAddress = UTF8ToString(itemAddress);
            const gasFee = UTF8ToString(gasFeeAmount);
            const transactionPayload = UTF8ToString(payload);

            console.log(`[UNITON CONNECT] Parsed assets transaction `+
                `payload: ${transactionPayload}`);

            try
            {
                transactionData = {
                    validUntil: Math.floor(Date.now() / 1000) + 360,
                    messages: [
                    {  
                        address: senderOrNftAddress, 
                        amount: gasFee,
                        payload: transactionPayload
                    }
                ]};

                console.log(`[UNITON CONNECT] Parsed assets transaction `+
                    `data: ${JSON.stringify(transactionData)}`);

                const result = await window.tonConnectUI.sendTransaction(transactionData, 
                {
                    modals: ['before', 'success', 'error'],
                    notifications: ['before', 'success', 'error']
                });
            
                if (!result || !result.boc)
                {
                    const emptyPtr = tonConnect.allocString("EMPTY_BOC");

                    console.error(`[UNITON CONNECT] No BOC returned from assets transaction`);

                    dynCall('vi', callback, [emptyPtr]);

                    _free(emptyPtr);

                    return;
                }
                
                let claimedBoc = result.boc;

                const hashBase64 = await tonConnect.convertBocToHashBase64(claimedBoc);
                const hashPtr = tonConnect.allocString(hashBase64);

                console.log(`[UNITON CONNECT] Parsed assets transaction `+
                    `hash: ${JSON.stringify(hashBase64)}`);

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

        sendTonTransaction: async function(
            nanoInTon, recipientAddress, message, callback) 
        {
            if (!tonConnect.isInitialized()) 
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

                    console.error(`[UNITON CONNECT] No BOC returned from toncoin transaction`);

                    dynCall('vi', callback, [emptyPtr]);

                    _free(emptyPtr);

                    return;
                }
                
                let claimedBoc = result.boc;

                const hashBase64 = await tonConnect.convertBocToHashBase64(claimedBoc);
                const hashPtr = tonConnect.allocString(hashBase64);

                console.log(`[UNITON CONNECT] Parsed toncoin transaction `+
                    `hash: ${JSON.stringify(hashBase64)}`);

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

        convertBocToHashBase64: async function(claimedBoc)
        {
            const bocBytes = tonWeb.utils.base64ToBytes(claimedBoc);

            const bocCellBytes = await tonWeb.boc.Cell.oneFromBoc(bocBytes).hash();
            const hashBase64 = tonWeb.utils.bytesToBase64(bocCellBytes);

            return hashBase64;
        },

        toBounceable: function(address, valueClaimed)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            const parsedAddress = tonConnect.parseAddress(address);
            const bouceableAddress = parsedAddress.toString(true, true, true, false);

            console.log(`[UNITON CONNECT] Address ${parsedAddress} converted `+
                `to bounceable format: ${bouceableAddress}`);

            const addressPtr = tonConnect.allocString(bouceableAddress);

            dynCall('vi', valueClaimed, [addressPtr]);

            _free(addressPtr);
        },

        toNonBounceable: function(address, valueClaimed)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            const parsedAddress = tonConnect.parseAddress(address);
            const nonBouceableAddress = parsedAddress.toString(true, true, false, false);

            console.log(`[UNITON CONNECT] Address ${parsedAddress} converted to `+
                `non bounceable format: ${nonBouceableAddress}`);

            const addressPtr = tonConnect.allocString(nonBouceableAddress);

            dynCall('vi', valueClaimed, [addressPtr]);

            _free(addressPtr);
        },

        toHex: function(address, valueClaimed)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            const parsedAddress = tonConnect.parseAddress(address);
            const hexAddress = parsedAddress.toString(false);

            console.log(`[UNITON CONNECT] Address ${parsedAddress} `+
                `converted to hex/raw format: ${hexAddress}`);

            const addressPtr = tonConnect.allocString(hexAddress);

            dynCall('vi', valueClaimed, [addressPtr]);

            _free(addressPtr);
        },

        isUserFriendly: function(address)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            return tonConnect.parseAddress(address).isUserFriendly;
        },

        isBounceable: function(address)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            return tonConnect.parseAddress(address).isBounceable;
        },

        isTestOnly: function(address)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            return tonConnect.parseAddress(address).isTestOnly;
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
        tonConnect.subscribeToTransactionEvents(
            successCallback, errorCallback);
    },

    UnSubscribeToTransactionEvents: function()
    {
        tonConnect.unsubscribeToTransactionEvents();
    },

    SendTonTransaction: function(
        nanoInTon, recipientAddress, callback)
    {
        tonConnect.sendTonTransaction(nanoInTon,
            recipientAddress, "CLEAR", callback);
    },

    SendTonTransactionWithMessage: function(
        nanoInTon, recipientAddress, message, callback)
    {
        tonConnect.sendTonTransaction(nanoInTon,
            recipientAddress, message, callback);
    },

    SendTransactionWithPayload: function(
        targetAddress, gasFee, payload, callback)
    {
        tonConnect.sendAsssetsTransaction(
            targetAddress, gasFee, payload, callback);
    },

    ToBounceableAddress: function(address, valueClaimed)
    {
        return tonConnect.toBounceable(address, valueClaimed);
    },

    ToNonBounceableAddress: function(address, valueClaimed)
    {
        return tonConnect.toNonBounceable(address, valueClaimed);
    },

    ToHexAddress: function(address, valueClaimed)
    {
        return tonConnect.toHex(address, valueClaimed);
    },

    IsUserFriendlyAddress: function(address)
    {
        return tonConnect.isUserFriendly(address);
    },

    IsBounceableAddress: function(address)
    {
        return tonConnect.isBounceable(address);
    },

    IsTestnetAddress: function(address)
    {
        return tonConnect.isTestOnly(address);
    }
};

autoAddDeps(tonConnectBridge, '$tonConnect');
mergeInto(LibraryManager.library, tonConnectBridge);