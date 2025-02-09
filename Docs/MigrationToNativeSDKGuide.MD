# Migrating from 0.2.* to 0.2.9.5+

By migrating from [based TonSdk.NET](https://github.com/continuation-team/TonSdk.NET) to the [native JS SDK Ton Connect UI](https://github.com/ton-connect/sdk/tree/main/packages/ui), a number of changes have been made to the functionality of the SDK. The following shows how the library functions are implemented in a working environment before and after the API change.

## **SDK INITIALIZATION**

Before
```c#
public sealed class OldInitExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private List_WalletConfig> _loadedWallets;

    public IReadOnlyList<WalletConfig> LoadedWallets => _loadedWallets;

    private void OnDisable()
    {
        _unitonConnect.OnInitialized -= SdkInitialized;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnInitialized += SdkInitialized;

        _unitonConnect.LoadWalletsConfigs(
          ProjectStorageConsts.TEST_SUPPORTED_WALLETS_LINK, (walletsConfigs) =>
        {
            CreateWalletsList(walletsConfigs);
        });

        _unitonConnect.Initialize();
    }

    private void CreateWalletsList(IReadOnlyList<WalletConfig> walletsConfigs)
    {
        var walletsConfigs = WalletConnectUtils.GetSupportedWalletsListForUse(wallets);

        Debug.Log($"Created {walletsConfigs.Capacity} wallets");

        _loadedWallets = walletsConfigs;

        Debug.Log($"List of available wallet configurations:
            {JsonConvert.SerializeObject(walletsConfigs)}"); 
    }

    private void SdkInitialized()
    {
        Debug.Log("Sdk has been successfully initialized, " +
              "you can test the functionality ^-^");
    }
}
```

After
```c#
public sealed class NewInitExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnNativeInitialized -= SdkInitialized;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeInitialized += SdkInitialized;

        _unitonConnect.Initialize();
    }

    private void SdkInitialized(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("Sdk has been successfully initialized, " + 
              "you can test the functionality ^-^");
        }
    }
}
```

## **CONNECT WALLET**

Before
```c#
public sealed class OldConnectWalletExample: MonoBehaviour
{
    [SerializeField, Space] private OldInitExample _initExample;
    [SerializeField, Space] private Button _connectTestWallet;
    [SerializeField] private RawImage _qrCodePlace;

    private UnitonConnectSDK _unitonConnect;

    private WalletConfig _currentWalletConfig;

    private string _connectUrl;

    public event Action<WalletConfig> OnWalletConnected;

    private void OnDisable()
    {
        _unitonConnect.OnInitialized -= SdkInitialized;
        _unitonConnect.OnWalletConnectionFinished -= WalletConnectionFinished;
        _unitonConnect.OnWalletConnectionFailed -= WalletConnectionFailed; 

        _connectTestWallet.onClick.RemoveListener(Connect);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnInitialized += SdkInitialized;
        _unitonConnect.OnWalletConnectionFinished += WalletConnectionFinished;
        _unitonConnect.OnWalletConnectionFailed += WalletConnectionFailed; 

        _connectTestWallet.interactable = false;

        _connectTestWallet.onClick.AddListener(Connect);
    }

    private async void Connect()
    {
        if (_currentWalletConfig == null ||
            string.isNullOrEmpty(_connectUrl))
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        if (WalletConnectUtils.HasHttpBridge(_currentWalletConfig))
        {
            _unitonConnect.ConnectHttpBridgeWalletViaDeeplink(
                _currentWalletConfig, _connectUrl);

            Debug.Log("Start connection via Mobile App/TMA");
        }

        if (WalletConnectUtils.HasJSBridge(_currentWalletConfig))
        {
            await _unitonConnect.ConnectJavaScriptBridgeWalletViaDeeplink(
                _currentWalletConfig);

            Debug.Log("Start connection via Browser Extension");
        }
    }

    private async Task GenerateConnectionElements(
            WalletConfig connectedConfig)
    {
        _currentWalletConfig = connectedConfig;

        _connectUrl = await _unitonConnect.GenerateConnectURL(connectedConfig);
        _qrCodePlace.texture = WalletVisualUtils.GetQRCodeFromUrl(_connectUrl);
    }

    private async void SdkInitialized()
    {
        _connectTestWallet.interactable = true;

        await GenerateConnectionElements(_initExample.LoadedWallets[0]);
    }

    private void WalletConnectionFinished(Wallet connectedWallet)
    {
        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning($"Wallet is not connected");

            return;
        }
  
        var successConnectMessage = $"Wallet is connected, " +
               $"full address: {wallet.Account.Address}, \n" +
               $"Platform: {wallet.Device.Platform}, " +
               $"Name: {wallet.Device.AppName}, " +
               $"Version: {wallet.Device.AppVersion}";
        var shortWalletAddress = WalletVisualUtils.ProcessWalletAddress(
               wallet.Account.Address.ToString(AddressType.Base64), 6);

        Debug.Log($"Connected wallet short address: {shortWalletAddress}");

        if (_initExample.LoadedWallets != null)
        {
            var connectedWalletConfig = WalletConnectUtils.GetConfigOfSpecifiedWallet(
              _initExample.LoadedWallets, connectedWallet.Device.AppName);

            _currentWalletConfig = connectedWalletConfig;

            OnWalletConnected?.Invoke(_currentWalletConfig);
        }
    }

    private void WalletConnectionFailed(string errorMessage)
    {
        Debug.LogError($"Failed to connect " +
          $"the wallet due to the following reason: {message}");
    }
}
```

After
```c#
public sealed class NewConnectWalletExample: MonoBehaviour
{
    [SerializeField, Space] private Button _connectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _connectButton.onClick.RemoveListener(Connect);

        _unitonConnect.OnNativeInitialized -= SdkInitialized;

        _unitonConnect.OnNativeWalletConnectionFinished -= WalletConnectionFinished;
        _unitonConnect.OnNativeWalletConnectionFailed -= WalletConnectionFailed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _connectButton.interactable = false;
        _connectButton.onClick.AddListener(Connect);

        _unitonConnect.OnNativeInitialized += SdkInitialized;

        _unitonConnect.OnNativeWalletConnectionFinished += WalletConnectionFinished;
        _unitonConnect.OnNativeWalletConnectionFailed += WalletConnectionFailed;
    }

    private void Connect()
    {
        if (!_unitonConnect.IsInitialized)
        {
            Debug.LogWarning("Sdk is not initialized, connection canceled");
  
            return;
        }

        _unitonConnect.Connect();
    }

    private void SdkInitialized(bool isSuccess)
    {
        _connectButton.interactable = isSuccess;
    }

    private void WalletConnectionFinished(NewWalletConfig wallet)
    {
        var userAddress = wallet.Address;

        var successConnectMessage = $"Wallet is connected, " +
            $"Address: {WalletConnectUtils.GetNonBounceableAddress(userAddress), \n}" +
            $"Public Key: {wallet.PublicKey}";
        var shortAddress = WalletVisualUtils.ProcessWalletAddress(userAddress, 6);

        Debug.Log(successConnectMessage);
        Debug.Log($"Parsed short address: {shortAddress}");
    }

    private void WalletConnectionFailed(string errorMessage)
    {
        Debug.LogError("Failed to connect wallet, reason: {errorMessage}");
    }
}
```

## **RESTORE WALLET CONNECTION**

Before
```c#
public sealed class OldRestoreConnectionExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private WalletConfig _loadedWalletConfig;

    private void OnDisable()
    {
        _unitonConnect.OnWalletConnectionRestored -= WalletConnectionRestored;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnWalletConnectionRestored += WalletConnectionRestored;
    }

    private void WalletConnectionRestored(bool isRestored,
        WalletConfig loadedWalletConfig)
    {
        if (!isRestored)
        {
            Debug.LogWarning("Wallet connection was not restored");

            return;
        }

        _loadedWalletConfig = loadedWalletConfig;

        Debug.Log($"Wallet connection restored, " + 
           $"loaded address: {_unitonConnect.GetWalletAddress()}");
    }
}
```

After
```c#
public sealed class NewRestoreConnectionExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnNativeWalletConnectionRestored -= WalletConnectionRestored;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeWalletConnectionRestored += WalletConnectionRestored;
    }

    private void WalletConnectionRestored(bool isRestored)
    {
        if (!isRestored)
        {
            Debug.LogWarning("Wallet connection was not restored");

            return;
        }

        Debug.Log($"Wallet connection restored, " +
            $"loaded address: {_unitonConnect.Wallet.ToNonBounceable()}");
    }
}
```

## **DISCONNECT WALLET**

Before
```c#
public sealed class OldDisconnectWalletExample : MonoBehaviour
{
    [SerializeField, Space] private Button _disconnectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnWalletDisconnected -= WalletDisconnected;

        _disconnectButton.onClick.RemoveListener(Disconnect);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnWalletDisconnected += WalletDisconnected;

        _disconnectButton.interactable = true;

        _disconnectButton.onClick.AddListener(Disconnect);
    }

    private async void Disconnect()
    {
        await _unitonConnect.DisconnectWallet();
    }

    private void WalletDisconnected()
    {
        _disconnectButton.interactable = false;

        Debug.Log($"Wallet successfully disconnected, " + 
            $"status: {_unitonConnect.IsWalletConnected}");
    }
}
```

After
```c#
public sealed class NewDisconnectWalletExample : MonoBehaviour
{
    [SerializeField, Space] private Button _disconnectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnNativeWalletDisconnected -= WalletDisconnected;

        _disconnectButton.onClick.RemoveListener(Disconnect);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeWalletDisconnected += WalletDisconnected;

        _disconnectButton.interactable = true;

        _disconnectButton.onClick.AddListener(Disconnect);
    }

    private void Disconnect()
    {
        _unitonConnect.Disconnect();
    }

    private void WalletDisconnected(bool isSuccess)
    {
        if (!isSuccess)
        {
            Debug.LogWarning("Failed to disconnect wallet, " +
                "something wrong...");

            return;
        }

        _disconnectButton.interactable = false;

        Debug.Log($"Wallet successfully disconnected, " + 
            $"status: {_unitonConnect.IsWalletConnected}");
    }
}
```

## **LOADING TON BALANCE**

Before
```c#
public sealed class OldTonBalanceLoaderExample: MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI _balanceBar;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnTonBalanceClaimed -= TonBalanceClaimed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        _unitonConnect.OnTonBalanceClaimed += TonBalanceClaimed;

        _unitonConnect.UpdateTonBalance();
    }

    private void TonBalanceClaimed(decimal balance)
    {
        Debug.Log($"Loaded ton balance: {balance}");

        _balanceBar.text = $"{balance} TON";
    }
}
```

After
```c#
public sealed class NewTonBalanceLoaderExample: MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI _balanceBar;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnTonBalanceClaimed -= TonBalanceClaimed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        _unitonConnect.OnTonBalanceClaimed += TonBalanceClaimed;

        _unitonConnect.LoadBalance(ClassicTokenTypes.Toncoin);
    }

    private void TonBalanceClaimed(decimal balance)
    {
        Debug.Log($"Loaded ton balance: {balance}");

        _balanceBar.text = $"{balance} TON";
    }
}
```

## **SENDING TONCOIN**

Before
```c#
public sealed class OldTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;
    [SerializeField, Space] private OldConnectWalletExample _connectExample;

    private UnitonConnectSDK _unitonConnect;

    private WalletConfig _connectedConfig;

    private void OnDisable()
    {
        _connectExample.OnWalletConnected -= WalletConfigClaimed;
        _unitonConnect.OnSendingTonFinished -= TonSendingFinished;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _connectExample.OnWalletConnected += WalletConfigClaimed;
        _unitonConnect.OnSendingTonFinished += TonSendingFinished;

        _sendTransctionButton.onClick.AddListener(Send);
    }

    private async void Send()
    {
        if (_connectedConfig == null)
        {
            Debug.LogWarning("Wallet was not connected");

            return;
        }

        await _unitonConnect.SendTon(_connectedConfig,
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_", (double)10.0f);
    }

    private void WalletConfigClaimed(WalletConfig config)
    {
        _connectedConfig = config;
    }

    private void TonSendingFinished(
        SendTransactionResult? result, bool isSuccess)
    {
        if (!isSuccess || !result.HasValue)
        {
            Debug.LogError("Failed to send transaction for possible reasons:" +
                    " not enough funds or unsuccessful connection to the wallet");

            return;
        }

        Debug.Log($"Ton transaction sended, boc: {result.Boc}");
    }
}
```

After
```c#
public sealed class NewTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;

    private UnitonConnectSDK _unitonConnect;

    private string _latestTransactionHash;

    private void OnDisable()
    {
        _unitonConnect.OnNativeSendingTonFinished -= TransactionSendingFinished;
        _unitonConnect.OnNativeTransactionConfirmed -= TonTransactionConfirmed;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeSendingTonFinished += TransactionSendingFinished;
        _unitonConnect.OnNativeTransactionConfirmed += TonTransactionConfirmed;

        _sendTransctionButton.onClick.AddListener(Send);
    }

    private void Send()
    {
        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet was not connected");

            return;
        }

        _unitonConnect.SendTransaction(ClassicTokenTypes.Toncoin,
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_", 
            (double)10.0f, "Ton Foundation is scam!");
    }

    private void TransactionSendingFinished(string transactionHash)
    {
        _latestTransactionHash = transactionHash;

        Debug.Log($"Ton transaction sended, hash: {transactionHash}");
    }

    private void TonTransactionConfirmed(
        SuccessTransactionData transactionData)
    {
        Debug.Log($"Ton transaction {_latestTransactionHash} " +
            $"confirmed with status: {transactionData.IsSuccess}");

        var status = transactionData.IsSuccess;
            var newBalance = transactionData.EndBalance.FromNanoton();
            var fee = transactionData.TotalFees.FromNanoton();
            var sendedAmount = transactionData.OutMessages[0].Value.FromNanoton();
            var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
            var convertedAddress = WalletConnectUtils.GetNonBounceableAddress(recipientAddress);
            var message = transactionData.OutMessages[0].DecodedBody.MessageText;

        var transactionDetails = $"Loaded transaction data: \n" +
                $"STATUS: {transactionData.IsSuccess},\n" +
                $"HASH: {_latestTransactionHash},\n" +
                $"NEW BALANCE: {newBalance} TON,\n" +
                $"FEE: {fee} TON,\n" +
                $"SENDED AMOUNT: {sendedAmount} TON,\n" +
                $"RECIPIENT ADDRESS: {convertedAddress},\n" +
                $"MESSAGE: {message}";

        Debug.Log(transactionDetails);
    }
}
```

## **EDITING BUILD TEMPLATE**

If you are using your own project build template, you will need to edit the `index.html` file to add Java Script references to these two libraries.

```html
<script src="https://unpkg.com/@tonconnect/ui@2.0.9/dist/tonconnect-ui.min.js"></script>
<script src="https://unpkg.com/tonweb@0.0.66/dist/tonweb.js"></script>
```

You can add it to `<head>` or `<body>` - it doesn't make any difference how early you put it and where you put it, as it will work fine.

For example, the `Uniton Connect` template for assembly looks like this now:
```html
<!DOCTYPE html>
<html lang="en-us">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>Uniton Connect Demo</title>
    <link rel="shortcut icon" href="TemplateData/favicon.ico">
    <link rel="stylesheet" href="TemplateData/style.css">
    <link rel="manifest" href="manifest.webmanifest">
    <script src="https://telegram.org/js/telegram-web-app.js"></script>
    <script src="https://unpkg.com/@tonconnect/ui@2.0.9/dist/tonconnect-ui.min.js"></script>
    <script src="https://unpkg.com/tonweb@0.0.66/dist/tonweb.js"></script>
  </head>
  <body>
    <div id="unity-container">
      <canvas id="unity-canvas" width=1080 height=1920 tabindex="-1"></canvas>
      <div id="unity-loading-bar">
        <div id="unity-logo"></div>
        <div id="unity-progress-bar-empty">
          <div id="unity-progress-bar-full"></div>
        </div>
      </div>
      <div id="unity-warning"> </div>
    </div>
    <script src="index.js"></script>
  </body>
</html>
```

**IMPORTANT:** All methods marked as `Obsolute` will be removed or hidden from public access (this applies to utilities and other things that will only be used inside the SDK) after a couple of library version updates.
On this occasion, backwards compatibility is still temporarily preserved for a smooth transition between versions, but not forever, sorry for the inconvenience :0

# Migrating from 0.2.9.5 to 0.5.0

For the global `0.5.0 update`, the logic of basic functions has been `artially redone`, including the names of some events.
Below is shown the past and current implementation of the functionality that has undergone changes:

## **SDK INITIALIZATION**

Before
```c#
public sealed class OldInitExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnNativeInitialized -= SdkInitialized;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeInitialized += SdkInitialized;

        _unitonConnect.Initialize();
    }

    private void SdkInitialized(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("Sdk has been successfully initialized, " + 
              "you can test the functionality ^-^");
        }
    }
}
```

After
```c#
public sealed class NewInitExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.‎OnInitiliazed -= SdkInitialized;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.‎OnInitiliazed += SdkInitialized;

        _unitonConnect.Initialize();
    }

    private void SdkInitialized(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("Sdk has been successfully initialized, " + 
              "you can test the functionality ^-^");
        }
    }
}
```

## **CONNECT WALLET**

Before
```c#
public sealed class OldConnectWalletExample: MonoBehaviour
{
    [SerializeField, Space] private Button _connectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _connectButton.onClick.RemoveListener(Connect);

        _unitonConnect.OnNativeInitialized -= SdkInitialized;

        _unitonConnect.OnNativeWalletConnectionFinished -= WalletConnectionFinished;
        _unitonConnect.OnNativeWalletConnectionFailed -= WalletConnectionFailed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _connectButton.interactable = false;
        _connectButton.onClick.AddListener(Connect);

        _unitonConnect.OnNativeInitialized += SdkInitialized;

        _unitonConnect.OnNativeWalletConnectionFinished += WalletConnectionFinished;
        _unitonConnect.OnNativeWalletConnectionFailed += WalletConnectionFailed;
    }

    private void Connect()
    {
        if (!_unitonConnect.IsInitialized)
        {
            Debug.LogWarning("Sdk is not initialized, connection canceled");
  
            return;
        }

        _unitonConnect.Connect();
    }

    private void SdkInitialized(bool isSuccess)
    {
        _connectButton.interactable = isSuccess;
    }

    private void WalletConnectionFinished(NewWalletConfig wallet)
    {
        var userAddress = wallet.Address;

        var successConnectMessage = $"Wallet is connected, " +
            $"Address: {WalletConnectUtils.GetNonBounceableAddress(userAddress), \n}" +
            $"Public Key: {wallet.PublicKey}";
        var shortAddress = WalletVisualUtils.ProcessWalletAddress(userAddress, 6);

        Debug.Log(successConnectMessage);
        Debug.Log($"Parsed short address: {shortAddress}");
    }

    private void WalletConnectionFailed(string errorMessage)
    {
        Debug.LogError("Failed to connect wallet, reason: {errorMessage}");
    }
}
```

After
```c#
public sealed class NewConnectWalletExample: MonoBehaviour
{
    [SerializeField, Space] private Button _connectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _connectButton.onClick.RemoveListener(Connect);

        _unitonConnect.‎OnInitiliazed -= SdkInitialized;

        _unitonConnect.OnWalletConnected -= WalletConnectionFinished;
        _unitonConnect.OnWalletConnectFailed -= WalletConnectionFailed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _connectButton.interactable = false;
        _connectButton.onClick.AddListener(Connect);

        _unitonConnect.‎OnInitiliazed += SdkInitialized;

        _unitonConnect.OnWalletConnected += WalletConnectionFinished;
        _unitonConnect.OnWalletConnectFailed += WalletConnectionFailed;
    }

    private void Connect()
    {
        if (!_unitonConnect.IsInitialized)
        {
            Debug.LogWarning("Sdk is not initialized, connection canceled");
  
            return;
        }

        _unitonConnect.Connect();
    }

    private void SdkInitialized(bool isSuccess)
    {
        _connectButton.interactable = isSuccess;
    }

    private void WalletConnectionFinished(WalletConfig  wallet)
    {
        var userAddress = wallet.Address;

        var successConnectMessage = $"Wallet is connected, " +
            $"Address: {userAddress}, Public Key: {wallet.PublicKey}";

        var shortAddress = _unitonConnect.Wallet.ToShort(6);

        Debug.Log(successConnectMessage);
        Debug.Log($"Parsed short address: {shortAddress}");
    }

    private void WalletConnectionFailed(string errorMessage)
    {
        Debug.LogError("Failed to connect wallet, reason: {errorMessage}");
    }
}
```

## **RESTORE WALLET CONNECTION**

Before
```c#
public sealed class OldRestoreConnectionExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnNativeWalletConnectionRestored -= WalletConnectionRestored;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeWalletConnectionRestored += WalletConnectionRestored;
    }

    private void WalletConnectionRestored(bool isRestored)
    {
        if (!isRestored)
        {
            Debug.LogWarning("Wallet connection was not restored");

            return;
        }

        Debug.Log($"Wallet connection restored, " +
            $"loaded address: {_unitonConnect.Wallet.ToNonBounceable()}");
    }
}
```

After
```c#
public sealed class NewRestoreConnectionExample: MonoBehaviour
{
    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnWalletConnectRestored -= WalletConnectionRestored;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnWalletConnectRestored += WalletConnectionRestored;
    }

    private void WalletConnectionRestored(bool isRestored)
    {
        if (!isRestored)
        {
            Debug.LogWarning("Wallet connection was not restored");

            return;
        }

        Debug.Log($"Wallet connection restored, " +
            $"loaded address: {_unitonConnect.Wallet.ToNonBounceable()}");
    }
}
```

## **DISCONNECT WALLET**

Before
```c#
public sealed class OldDisconnectWalletExample : MonoBehaviour
{
    [SerializeField, Space] private Button _disconnectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnNativeWalletDisconnected -= WalletDisconnected;

        _disconnectButton.onClick.RemoveListener(Disconnect);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeWalletDisconnected += WalletDisconnected;

        _disconnectButton.interactable = true;

        _disconnectButton.onClick.AddListener(Disconnect);
    }

    private void Disconnect()
    {
        _unitonConnect.Disconnect();
    }

    private void WalletDisconnected(bool isSuccess)
    {
        if (!isSuccess)
        {
            Debug.LogWarning("Failed to disconnect wallet, " +
                "something wrong...");

            return;
        }

        _disconnectButton.interactable = false;

        Debug.Log($"Wallet successfully disconnected, " + 
            $"status: {_unitonConnect.IsWalletConnected}");
    }
}
```

After
```c#
public sealed class NewDisconnectWalletExample : MonoBehaviour
{
    [SerializeField, Space] private Button _disconnectButton;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnWalletDisconnected -= WalletDisconnected;

        _disconnectButton.onClick.RemoveListener(Disconnect);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnWalletDisconnected += WalletDisconnected;

        _disconnectButton.interactable = true;

        _disconnectButton.onClick.AddListener(Disconnect);
    }

    private void Disconnect()
    {
        _unitonConnect.Disconnect();
    }

    private void WalletDisconnected(bool isSuccess)
    {
        if (!isSuccess)
        {
            Debug.LogWarning("Failed to disconnect wallet, " +
                "something wrong...");

            return;
        }

        _disconnectButton.interactable = false;

        Debug.Log($"Wallet successfully disconnected, " + 
            $"status: {_unitonConnect.IsWalletConnected}");
    }
}
```

## **LOADING TON BALANCE**

Before
```c#
public sealed class OldTonBalanceLoaderExample: MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI _balanceBar;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnTonBalanceClaimed -= TonBalanceClaimed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        _unitonConnect.OnTonBalanceClaimed += TonBalanceClaimed;

        _unitonConnect.LoadBalance(ClassicTokenTypes.Toncoin);
    }

    private void TonBalanceClaimed(decimal balance)
    {
        Debug.Log($"Loaded ton balance: {balance}");

        _balanceBar.text = $"{balance} TON";
    }
}
```

After
```c#
public sealed class NewTonBalanceLoaderExample: MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI _balanceBar;

    private UnitonConnectSDK _unitonConnect;

    private void OnDisable()
    {
        _unitonConnect.OnTonBalanceClaimed -= TonBalanceClaimed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        _unitonConnect.OnTonBalanceClaimed += TonBalanceClaimed;

        _unitonConnect.LoadBalance();
    }

    private void TonBalanceClaimed(decimal balance)
    {
        Debug.Log($"Loaded ton balance: {balance}, in nano: {balance.ToNanoton()}");

        _balanceBar.text = $"{balance} TON";
    }
}
```

## **SENDING TONCOIN**

Before
```c#
public sealed class OldTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;

    private UnitonConnectSDK _unitonConnect;

    private string _latestTransactionHash;

    private void OnDisable()
    {
        _unitonConnect.OnNativeSendingTonFinished -= TransactionSendingFinished;
        _unitonConnect.OnNativeTransactionConfirmed -= TonTransactionConfirmed;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnNativeSendingTonFinished += TransactionSendingFinished;
        _unitonConnect.OnNativeTransactionConfirmed += TonTransactionConfirmed;

        _sendTransctionButton.onClick.AddListener(Send);
    }

    private void Send()
    {
        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet was not connected");

            return;
        }

        _unitonConnect.SendTransaction(ClassicTokenTypes.Toncoin,
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_", 
            (double)10.0f, "Ton Foundation is scam!");
    }

    private void TransactionSendingFinished(string transactionHash)
    {
        _latestTransactionHash = transactionHash;

        Debug.Log($"Ton transaction sended, hash: {transactionHash}");
    }

    private void TonTransactionConfirmed(
        SuccessTransactionData transactionData)
    {
        Debug.Log($"Ton transaction {_latestTransactionHash} " +
            $"confirmed with status: {transactionData.IsSuccess}");

        var status = transactionData.IsSuccess;
            var newBalance = transactionData.EndBalance.FromNanoton();
            var fee = transactionData.TotalFees.FromNanoton();
            var sendedAmount = transactionData.OutMessages[0].Value.FromNanoton();
            var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
            var convertedAddress = WalletConnectUtils.GetNonBounceableAddress(recipientAddress);
            var message = transactionData.OutMessages[0].DecodedBody.MessageText;

        var transactionDetails = $"Loaded transaction data: \n" +
                $"STATUS: {transactionData.IsSuccess},\n" +
                $"HASH: {_latestTransactionHash},\n" +
                $"NEW BALANCE: {newBalance} TON,\n" +
                $"FEE: {fee} TON,\n" +
                $"SENDED AMOUNT: {sendedAmount} TON,\n" +
                $"RECIPIENT ADDRESS: {convertedAddress},\n" +
                $"MESSAGE: {message}";

        Debug.Log(transactionDetails);
    }
}
```

After
```c#
public sealed class NewTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;

    private UnitonConnectSDK _unitonConnect;

    private string _latestTransactionHash;

    private void OnDisable()
    {
        _unitonConnect.OnTonTransactionSended -= TransactionSendingFinished;
        _unitonConnect.OnTonTransactionConfirmed -= TonTransactionConfirmed;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnTonTransactionSended += TransactionSendingFinished;
        _unitonConnect.OnTonTransactionConfirmed += TonTransactionConfirmed;

        _sendTransctionButton.onClick.AddListener(Send);
    }

    private void Send()
    {
        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet was not connected");

            return;
        }

        var creatorAddress = "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";
        var message = "Made by Uniton Connect";

        _unitonConnect.SendTransaction(creatorAddress, (decimal)10.0f, message);
    }

    private void TransactionSendingFinished(string transactionHash)
    {
        _latestTransactionHash = transactionHash;

        Debug.Log($"Ton transaction sended, hash: {transactionHash}");
    }

    private void TonTransactionConfirmed(
        SuccessTransactionData transactionData)
    {
        Debug.Log($"Ton transaction {_latestTransactionHash} " +
            $"confirmed with status: {transactionData.IsSuccess}");

        var status = transactionData.IsSuccess;
            var newBalance = transactionData.EndBalance.FromNanoton();
            var fee = transactionData.TotalFees.FromNanoton();
            var sendedAmount = transactionData.OutMessages[0].Value.FromNanoton();
            var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
            var convertedAddress = WalletConnectUtils.GetNonBounceableAddress(recipientAddress);
            var message = transactionData.OutMessages[0].DecodedBody.MessageText;

        var transactionDetails = $"Loaded transaction data: \n" +
                $"STATUS: {transactionData.IsSuccess},\n" +
                $"HASH: {_latestTransactionHash},\n" +
                $"NEW BALANCE: {newBalance} TON,\n" +
                $"FEE: {fee} TON,\n" +
                $"SENDED AMOUNT: {sendedAmount} TON,\n" +
                $"RECIPIENT ADDRESS: {convertedAddress},\n" +
                $"MESSAGE: {message}";

        Debug.Log(transactionDetails);
    }
}
```
