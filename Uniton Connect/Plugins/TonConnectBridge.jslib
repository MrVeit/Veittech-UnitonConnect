const tonConnectBridge = {

    // Class definition

    $tonConnect: {
        allocString: function (stringData)
        {
            let ptr;

            if (typeof allocate === 'undefined')
            {
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
                console.error(`[Uniton Connect] Ton Connect UI is not initialized`);

                return false;
            }

            return true;
        },

        isAvailableTonWeb: function()
        {
            if (!window.tonWeb)
            {
                console.error(`[Uniton Connect] Library 'Ton Web' is not exist`);

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

        parseSignMessageSignature: function(
            walletMessage, messageSignFailed)
        {
            let messageEntity = null;

            try
            {
                messageEntity = JSON.parse(walletMessage);
            }
            catch (error)
            {
                var errorPtr = tonConnect.allocString("INVALID_JSON");

                console.error(`[Uniton Connect] Failed to parse sign `+
                    `message object, reasonn: ${error}`);

                {{{ makeDynCall('vi', 'messageSignFailed') }}}(errorPtr);

                _free(errorPtr);

                return;
            }

            const signTypes =
            {
                text:
                {
                    type: "text",
                    text: messageEntity.text,
                    network: messageEntity.network,
                    from: messageEntity.from
                },
                binary:
                {
                    type: "binary",
                    bytes: messageEntity.bytes,
                    network: messageEntity.network,
                    from: messageEntity.from
                },
                cell:
                {
                    type: "cell",
                    schema: messageEntity.schema,
                    cell: messageEntity.cell,
                    network: messageEntity.network,
                    from: messageEntity.from
                }
            };

            const currentType = messageEntity.type;
            const targetSignature = signTypes[currentType];

            if (!targetSignature)
            {
                var invalidTypePtr = tonConnect.allocString("UNSUPPORTED_SIGN_TYPE");

                {{{ makeDynCall('vi', 'messageSignFailed') }}}(invalidTypePtr);

                _free(invalidTypePtr);

                return null;
            }

            return targetSignature;
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
                {{{ makeDynCall('vi', 'callback') }}}(0);

                return;
            }

            {{{ makeDynCall('vi', 'callback') }}}(1);
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

                {{{ makeDynCall('vi', 'callback') }}}(1);
            }
            catch (error)
            {
                {{{ makeDynCall('vi', 'callback') }}}(0);
            }
        },

        closeModal: function(callback)
        {
            try
            {
                window.tonConnectUI.closeModal();

                {{{ makeDynCall('vi', 'callback') }}}(1);
            }
            catch (error)
            {
                {{{ makeDynCall('vi', 'callback') }}}(0);
            }
        },

        disconnect: async function(callback)
        {
            try
            {
                await window.tonConnectUI.disconnect();

                const statusPtr = tonConnect.allocString("200");

                {{{ makeDynCall('vi', 'callback') }}}(statusPtr);

                _free(statusPtr);
            }
            catch (error)
            {
                const statusPtr = tonConnect.allocString("500");

                {{{ makeDynCall('vi', 'callback') }}}(statusPtr);

                _free(statusPtr);
            }
        },

        signData: async function(textData, messageSignFailed)
        {
            if (!tonConnect.isInitialized())
            {
                const errorPtr = tonConnect.allocString("NOT_INITIALIZED");
            
                {{{ makeDynCall('vi', 'messageSignFailed') }}}(errorPtr);

                _free(errorPtr);

                return;
            }

            const message = UTF8ToString(textData);

            console.log(`[Uniton Connect] Parsed wallet message for sign: ${message}`);

            const signData = tonConnect.parseSignMessageSignature(message, messageSignFailed);

            if (!signData)
            {
                return;
            }

            console.log(`Final wallet message for sign: ${JSON.stringify(signData)}`);

            try
            {
                const payload = await window.tonConnectUI.signData(signData);

                const signedPayload = JSON.stringify(payload);

                console.log(`[Uniton Connect] Wallet message successfully sign, payload: ${signedPayload}`)
            }
            catch (error)
            {
                const errorDescription = error.message || error;

                console.error(`Failed to sign wallet data, reason: ${errorDescription}`);

                const errorPtr = tonConnect.allocString(errorDescription);

                {{{ makeDynCall('vi', 'messageSignFailed') }}}(errorPtr);

                _free(errorPtr);
            }
        },

        getModalState: function(valueClaimed)
        {
            const stateEntity = window.tonConnectUI.modalState;
            
            const state = JSON.stringify(stateEntity);
            const statePtr = tonConnect.allocString(state);

            console.log(`[Uniton Connect] Current modal state: ${state}`);

            {{{ makeDynCall('vi', 'valueClaimed') }}}(statePtr);

            _free(statePtr);
        },

        subscribeToStatusChanged: function(callback)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            window.unsubscribeFromStatusChange = window.
                tonConnectUI.onStatusChange((wallet) =>
            {
                if (wallet)
                {
                    const walletInfo = JSON.stringify(window.tonConnectUI.account);
                    const walletPtr = tonConnect.allocString(walletInfo);

                    console.log(`[Uniton Connect] Parsed wallet account: ${walletInfo}`);
    
                    {{{ makeDynCall('vi', 'callback') }}}(walletPtr);

                    _free(walletPtr);

                    return;
                }

                const statusPtr = tonConnect.allocString("CONNECT_FAILED");

                {{{ makeDynCall('vi', 'callback') }}}(statusPtr);

                _free(statusPtr);
            });
        },

        unsubscribeFromStatusChanged: function()
        {
            if (window.unsubscribeFromStatusChange)
            {
                window.unsubscribeFromStatusChange();

                window.unsubscribeFromStatusChange = null;

                console.log(`[Uniton Connect] Unsubscribed from 'wallet-status-changed' event`);
            }
        },

        subscribeToModalState: function(modalStateCallback)
        {
            window.unsubsribeFromModalState = window.
                tonConnectUI.onModalStateChange((state) =>
            {
                const stateInfo = JSON.stringify(state);
                const statePtr = tonConnect.allocString(stateInfo);

                console.log(`[Uniton Connect] Claimed 'modal-state-changed' `+
                    `event data, status: ${stateInfo}`);

                {{{ makeDynCall('vi', 'modalStateCallback') }}}(statePtr);

                _free(statePtr);

            });
        },

        unsubscribeFromModalState: function()
        {
            if (window.unsubsribeFromModalState)
            {
                window.unsubsribeFromModalState();

                window.unsubsribeFromModalState = null;

                console.log(`[Uniton Connect] Unsubscribed from 'modal-state-changed' event`);
            }
        },

        subscribeToRestoreConnection: function(callback)
        {
            if (!tonConnect.isInitialized())
            {
                {{{ makeDynCall('vi', 'callback') }}}(0);

                return;
            }

            window.tonConnectUI.connectionRestored.then(restored =>
            {
                if (restored)
                {
                    {{{ makeDynCall('vi', 'callback') }}}(1);

                    return;
                }

                {{{ makeDynCall('vi', 'callback') }}}(0);
            });
        },

        subscribeToTransactionEvents: function(
            successCallback, errorCallback)
        {
            const signedHandler = (event) =>
            {
                const data = event.detail;

                console.log(`[Uniton Connect] Transaction signed:`, data);

                const signedData = JSON.stringify(data);
                const signedPtr = tonConnect.allocString(signedData);

                {{{ makeDynCall('vi', 'successCallback') }}}(signedPtr);

                _free(signedPtr);
            };

            const failedHandler = (event) =>
            {
                const data = event.detail;

                console.warn(`[Uniton Connect] Transaction signing failed:`, data);

                const failedData = JSON.stringify(data);
                const failedPtr = tonConnect.allocString(failedData);

                {{{ makeDynCall('vi', 'errorCallback') }}}(failedPtr);

                _free(failedPtr);
            };

            window.addEventListener('ton-connect-ui-transaction-signed', signedHandler);
            window.addEventListener('ton-connect-ui-transaction-signing-failed', failedHandler);

            window._tonConnectTransactionSignedHandler = signedHandler;
            window._tonConnectTransactionFailedHandler = failedHandler;

            console.log('[Uniton Connect] Subscribed to transaction events');
        },

        unsubscribeFromTransactionEvents: function()
        {
            if (window._tonConnectTransactionSignedHandler)
            {
                window.removeEventListener('ton-connect-ui-transaction-signed',
                    window._tonConnectTransactionSignedHandler);
                
                delete window._tonConnectTransactionSignedHandler;

                console.log(`[Uniton Connect] Unsubscribed from 'transaction-signed' event`);
            }

            if (window._tonConnectTransactionFailedHandler)
            {
                window.removeEventListener('ton-connect-ui-transaction-signing-failed',
                    window._tonConnectTransactionFailedHandler);

                delete window._tonConnectTransactionSignedHandler;

                console.log(`[Uniton Connect] Unsubsribed from 'transaction-signing-failed' event`);
            }
        },

        subscribeToWalletMessageSigned: function(
            successSign, failedSign)
        {
            if (!tonConnect.isInitialized())
            {
                return;
            }

            const signedHandler = (event) =>
            {
                const data = event.detail.signed_data;
                
                console.log(`[Uniton Connect] Wallet message signed`, data);

                const signedData = JSON.stringify(data);
                const signedPtr = tonConnect.allocString(signedData);

                {{{ makeDynCall('vi', 'successSign') }}}(signedPtr);

                _free(signedPtr);
            };

            const failedHandler = (event) =>
            {
                const data = event.detail;

                console.warn(`[Uniton Connect] Failed to sign wallet message`, data);

                const failedData = JSON.stringify(data);
                const failedPtr = tonConnect.allocString(failedData);

                {{{ makeDynCall('vi', 'failedSign') }}}(failedPtr);

                _free(failedPtr);
            };

            window.addEventListener('ton-connect-ui-sign-data-request-completed', signedHandler);
            window.addEventListener('ton-connect-ui-sign-data-request-failed', failedHandler);

            window._tonConnectMessageSignedHandler = signedHandler;
            window._tonConnectMessageSignFailedHandler = failedHandler;

            console.log('[Uniton Connect] Subscribed to message sign events');
        },

        unsubscribeFromWalletMessageSigned: function()
        {
            if (window._tonConnectMessageSignedHandler)
            {
                window.removeEventListener('ton-connect-ui-sign-data-request-completed',
                    window._tonConnectMessageSignedHandler);
                
                delete window._tonConnectMessageSignedHandler;

                console.log(`[Uniton Connect] Unsubscribed from 'wallet-message-signed' event`);
            }

            if (window._tonConnectMessageSignFailedHandler)
            {
                window.removeEventListener('ton-connect-ui-sign-data-request-failed',
                    window._tonConnectMessageSignFailedHandler);

                delete window._tonConnectMessageSignFailedHandler;

                console.log(`[Uniton Connect] Unsubsribed from 'wallet-message-sign-failed' event`);
            }
        },

        getTonTransactionPayload: async function(
            nanoInTon, recipientAddress, message)
        {
            const tonWeb = window.tonWeb;

            var transactionData;

            if (!message || message === "CLEAR")
            {
                transactionData =
                {
                    validUntil: Math.floor(Date.now() / 1000) + 60,
                    messages:
                    [
                        {  
                            address: recipientAddress, 
                            amount: nanoInTon
                        }
                    ]
                };

                return transactionData;
            }

            let cellBuilder = new tonWeb.boc.Cell();

            cellBuilder.bits.writeUint(0, 32);
            cellBuilder.bits.writeString(message);
                
            let payload = tonWeb.utils.bytesToBase64(await cellBuilder.toBoc());

            transactionData =
            {
                validUntil: Math.floor(Date.now() / 1000) + 60,
                messages:
                [
                    {  
                        address: recipientAddress, 
                        amount: nanoInTon,
                        payload: payload
                    },
                ]
            };

            return transactionData;
        },

        sendAsssetsTransaction: async function(itemAddress,
            gasFeeAmount, payload, callback)
        {
            if (!tonConnect.isInitialized())
            {
                const errorMessage = tonConnect.allocString("SDK is not initialized");

                {{{ makeDynCall('vi', 'callback') }}}(errorMessage);

                _free(errorMessage);

                return;
            }

            const tonWeb = window.tonWeb;

            const senderOrNftAddress = UTF8ToString(itemAddress);
            const gasFee = UTF8ToString(gasFeeAmount);
            const transactionPayload = UTF8ToString(payload);

            console.log(`[Uniton Connect] Parsed assets transaction `+
                `payload: ${transactionPayload}`);

            try
            {
                transactionData =
                {
                    validUntil: Math.floor(Date.now() / 1000) + 360,
                    messages:
                    [
                        {  
                            address: senderOrNftAddress, 
                            amount: gasFee,
                            payload: transactionPayload
                        }
                    ]
                };

                console.log(`[Uniton Connect] Parsed assets transaction `+
                    `data: ${JSON.stringify(transactionData)}`);

                const result = await window.tonConnectUI.sendTransaction(transactionData, 
                {
                    modals: ['before', 'success', 'error'],
                    notifications: ['before', 'success', 'error']
                });
            
                if (!result || !result.boc)
                {
                    const emptyPtr = tonConnect.allocString("EMPTY_BOC");

                    console.error(`[Uniton Connect] No BOC returned from assets transaction`);

                    {{{ makeDynCall('vi', 'callback') }}}(emptyPtr);

                    _free(emptyPtr);

                    return;
                }
                
                let claimedBoc = result.boc;

                const hashBase64 = await tonConnect.convertBocToHashBase64(claimedBoc);
                const hashPtr = tonConnect.allocString(hashBase64);

                console.log(`[Uniton Connect] Parsed assets transaction `+
                    `hash: ${JSON.stringify(hashBase64)}`);

                {{{ makeDynCall('vi', 'callback') }}}(hashPtr);

                _free(hashPtr);
            }
            catch (error)
            {
                const errorPtr = tonConnect.allocString("");
            
                {{{ makeDynCall('vi', 'callback') }}}(errorPtr);

                _free(errorPtr);
            }
        },

        sendTonTransaction: async function(nanoInTon,
            recipientAddress, message, callback) 
        {
            if (!tonConnect.isInitialized()) 
            {
                const nullPtr = tonConnect.allocString("null");

               {{{ makeDynCall('vi', 'callback') }}}(nullPtr);

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

                    console.error(`[Uniton Connect] No BOC returned from toncoin transaction`);

                    {{{ makeDynCall('vi', 'callback') }}}(emptyPtr);

                    _free(emptyPtr);

                    return;
                }
                
                let claimedBoc = result.boc;

                const hashBase64 = await tonConnect.convertBocToHashBase64(claimedBoc);
                const hashPtr = tonConnect.allocString(hashBase64);

                console.log(`[Uniton Connect] Parsed toncoin transaction `+
                    `hash: ${JSON.stringify(hashBase64)}`);

                {{{ makeDynCall('vi', 'callback') }}}(hashPtr);

                _free(hashPtr);
            }
            catch (error)
            {
                const errorPtr = tonConnect.allocString("");

                {{{ makeDynCall('vi', 'callback') }}}(errorPtr);

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

            console.log(`[Uniton Connect] Address ${parsedAddress} converted `+
                `to bounceable format: ${bouceableAddress}`);

            const addressPtr = tonConnect.allocString(bouceableAddress);

            {{{ makeDynCall('vi', 'valueClaimed') }}}(addressPtr);

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

            console.log(`[Uniton Connect] Address ${parsedAddress} converted to `+
                `non bounceable format: ${nonBouceableAddress}`);

            const addressPtr = tonConnect.allocString(nonBouceableAddress);

            {{{ makeDynCall('vi', 'valueClaimed') }}}(addressPtr);

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

            console.log(`[Uniton Connect] Address ${parsedAddress} `+
                `converted to hex/raw format: ${hexAddress}`);

            const addressPtr = tonConnect.allocString(hexAddress);

            {{{ makeDynCall('vi', 'valueClaimed') }}}(addressPtr);

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

    SubscribeToModalState: function(callback)
    {
        tonConnect.subscribeToModalState(callback);
    },

    UnsubscribeFromModalState: function()
    {
        tonConnect.unsubscribeFromModalState();
    },

    SubscribeToStatusChange: function(callback)
    {
        tonConnect.subscribeToStatusChanged(callback);
    },

    UnsubscribeFromStatusChange: function() 
    {
        tonConnect.unsubscribeFromStatusChanged();
    },

    SubscribeToRestoreConnection: function(callback)
    {
        tonConnect.subscribeToRestoreConnection(callback);
    },

    SubscribeToTransactionEvents: function(
        successCallback, errorCallback)
    {
        tonConnect.subscribeToTransactionEvents(
            successCallback, errorCallback);
    },

    UnsubscribeFromTransactionEvents: function()
    {
        tonConnect.unsubscribeFromTransactionEvents();
    },

    SubscribeToWalletMessageSigned: function(
        successCallback, errorCallback)
    {
        tonConnect.subscribeToWalletMessageSigned(
            successCallback, errorCallback);
    },

    UnsubscribeFromWalletMessageSigned: function()
    {
        tonConnect.unsubscribeFromWalletMessageSigned();
    },

    SendTonTransaction: function(
        nanoInTon, recipientAddress, callback)
    {
        tonConnect.sendTonTransaction(nanoInTon,
            recipientAddress, "CLEAR", callback);
    },

    SendTonTransactionWithMessage: function(nanoInTon,
        recipientAddress, message, callback)
    {
        tonConnect.sendTonTransaction(nanoInTon,
            recipientAddress, message, callback);
    },

    SendTransactionWithPayload: function(targetAddress,
        gasFee, payload, callback)
    {
        tonConnect.sendAsssetsTransaction(
            targetAddress, gasFee, payload, callback);
    },

    SignData: function(message, messageSignFailed)
    {
        tonConnect.signData(message, messageSignFailed);
    },

    GetModalState: function(callback)
    {
        tonConnect.getModalState(callback);
    },

    ToBounceableAddress: function(address, valueClaimed)
    {
        tonConnect.toBounceable(address, valueClaimed);
    },

    ToNonBounceableAddress: function(address, valueClaimed)
    {
        tonConnect.toNonBounceable(address, valueClaimed);
    },

    ToHexAddress: function(address, valueClaimed)
    {
        tonConnect.toHex(address, valueClaimed);
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