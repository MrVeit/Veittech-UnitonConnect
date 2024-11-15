# Migrating from version 0.2.* to 0.2.9.5

By migrating from C# based libraries for the .NET platform to the native Javascript SDK Ton Connect UI, a number of changes have been made to the functionality of the SDK. The following shows how the library functions are implemented in a working environment before and after the API change.

## **SDK INITIALIZATION:**

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
        Debug.Log("Sdk has been successfully initialized, 
              you can test the functionality ^-^");
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
            Debug.Log("Sdk has been successfully initialized, 
              you can test the functionality ^-^");
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
  
        var successConnectMessage = $"Wallet is connected, full account address:
              {wallet.Account.Address}, \n" +
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

```

After
```c#

```

## **SENDING TONCOIN**

Before
```c#

```

After
```c#

```

## **LOADING TON BALANCE**

Before
```c#

```

After
```c#

```

## **OTHER UTILS**

Before
```c#

```

After
```c#

```
