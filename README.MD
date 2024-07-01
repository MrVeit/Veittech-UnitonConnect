# UNITON CONNECT

<p align="center">
 <img width="600px" src="Assets/sdkBackgroundLogo.png" alt="qr"/>
</p>

[![License](https://img.shields.io/github/license/mrveit/unity-ton-connect?color=318CE7&style=flat-square)](LICENSE)
[![Version](https://img.shields.io/github/package-json/v/mrveit/unity-ton-connect?color=318CE7&style=flat-square)](package.json)
[![Unity](https://img.shields.io/badge/Unity-2020.1+-2296F3.svg?color=318CE7&style=flat-square)](https://unity.com/releases/editor/archive)

**UNITON CONNECT** is a convenient solution for working with the Ton Connect ecosystem right in Unity and has the following features: authorization via TON wallets (QR code or via Deep Link), making transactions and much more.

# Technical Demo

You can test the SDK without installation on a demo app [in your browser](https://mrveit.github.io/Uniton-Connect/) or directly [in the Telegram bot](https://t.me/UnitonConnect_bot/dAppStart).

# Supported Platforms & Wallets

|                Wallet Provider                | WebGL Desktop | WebGL Mobile |
| --------------------------------------------- |   :-------:   |   :-------:  | 
| **Telegram Wallet**                           | ✔️           | ✔️           |
| **Ton Keeper**                                | ✔️           | ✔️           |
| **My Ton Wallet**                             | ✔️           | ✔️           |
| **Ton Wallet**                                | ⚠️           | ⚠️           |
| **xTon Wallet**                               | ⚠️           | ⚠️           |
| **Ton Hub**                                   | ⚠️           | ⚠️           |
| **Open Mask**                                 | ⚠️           | ⚠️           |
| **DeWallet**                                  | ⚠️           | ⚠️           |
| **Bitget Wallet**                             | ⚠️           | ⚠️           |
| **Safe Pal Wallet**                           | ⚠️           | ⚠️           |
 
<sub>✔️ Supported</sub> &nbsp; <sub>❌ Not Supported</sub> &nbsp; <sub>⚠️ In progress</sub>

# Dependencies

For the library to work correctly, the following dependencies **must be installed** in the project before use:
- **[WebGL Threading Patcher](https://github.com/VolodymyrBS/WebGLThreadingPatcher)** allows you to run asynchronous operations in a single thread in WebGL builds. 
Initially they do not work due to lack of multithreading support in them,
- **[Newtonsoft](https://www.youtube.com/watch?v=3H6xkl_EsvQ)** - modern solution for convenient work with json files.

# Installation

[Download the latest version of the SDK via the .unityPackage file here](https://github.com/MrVeit/unity-ton-connect/releases).

# Initialization

Before you can use all the features of the SDK, you must initialize it in **one of two available ways**.

#### Аutomatic initialization:
The `UnitonConnectSDK` component has an option called `Initialize On Awake`. When you activate it, the SDK will initialize automatically. You only need to subscribe to the necessary events and start working with it.

<p align="center">
 <img width="500px" src="Assets/sdkOverview.png" alt="qr"/>
</p>

#### Manual initialization:
Below is a test example of how it can look like.

```c#
public sealed class InitializationUsageTemplate : MonoBehaviour
{
    private UnitonConnectSDK _unitonSDK;
    
    private void OnDisable()
    {
        _unitonSDK.OnInitialized -= UnitonConnectSDKInitialized;        
    }

    private void Start()
    {
        _unitonSDK = UnitonConnectSDK.Instance;

        _unitonSDK.OnInitialized += UnitonConnectSDKInitialized;        

        _unitonSDK.Initialize();
    }

    private void UnitonConnectSDKInitialized()
    {
        Debug.Log("Success initialize Uniton Connect SDK");
    }
}
```

**At this point, you may encounter errors because your dApp application has not yet been configured.**

### Possible problems:

If you just want to test the SDK, activate the `Test Mode` option of the `UnitonConnectSDK` component. In this case, the SDK will initialize the library's test application.
In case you want your application name, its logo and a link to the project itself to be displayed when connecting to the wallets. You need to enter these data in the `dApp Config` window, which is located at the `Uniton Connect -> dApp Config` path.

<p align="center">
 <img width="500px" src="Assets/dAppConfigWindow.png" alt="qr"/>
</p>

Here you need to select your game/application logo, enter a title and provide a link to the site where it will be published.

### Set up a project for build:

The next step is to build the project in its current form. Before that you need to configure several parameters in the `Player Settings` window:
1. Install the custom `TC_Minimal` build template, which is available after importing the library into the project. It can be installed in `Build Settings -> Player Settings -> Resolution and Presentation -> WebGL Template`,
2. Then you should turn off the `Strip Engine Code` option, which is located in `Player Settings -> Othet Settings` and switch the `Managed Stripping Level` property type to **Minimal**, so that the library functionality is not cut out during unit build and it works correctly,
3. Now the final touches, in the `Player Settings -> Publish Settings` section you need to switch the `Enable Exception` property to any value except `Explicitly Thrown Exceptions Only`. Since in case of any errors in the build, you may **freeze or even crash** the project.

After successful build, **2 files will be created** in the build directory: `icon.png` and `dAppData.json`. They will contain all the information you entered earlier in the `dApp Config` window.
Now you can publish your project anywhere you want. After publishing your project, go back to the `dApp Config` window and specify the correct link for the SDK to work with your project correctly.

For test mode, **I recommend** using `Github Actions` with their `Static Page` feature. The tutorial for deploying a WebGL build to Unity can be found on the Internet, as there is no need to describe it here.

Now you can work with the SDK without any problems if you followed the second method. Anyway, this will be an additional guide when your project is ready to be published.

# Usage Template

Now it's time for some examples of how to use the Uniton Connect library API. 
After successful initialization, you can request from the SDK a list of available wallets for further use. 

P.S: At this point, the library links to the **official repository with a json file** that lists the currently available wallets in the `Ton Connect ecosystem`. In future updates, you will have the option **to filter this list and generate**, similar to the configuration of your dApp, a json file with the crypto wallets used in the project.

### Loading and processing wallet configurations:

An example of a method call and processing its result is shown below:
```c#
public sealed class InitializationUsageTemplate: MonoBehaviour
{
    private UnitonConnectSDK _unitonSDK;

    private List<WalletConfig> _loadedWallets;
    
    private void OnDisable()
    {
        _unitonSDK.OnInitialized -= UnitonConnectSDKInitialized;        
    }

    private void Start()
    {
        _unitonSDK = UnitonConnectSDK.Instance;

        _unitonSDK.OnInitialized += UnitonConnectSDKInitialized;  

        _unitonSDK.Initialize();

        _unitonSDK.GetWalletsConfigs(ProjectStorageConsts.
            START_TON_WALLETS_LINK, (walletsConfigs) =>
            {
                CreateWalletsList(walletsConfigs);
            });
    }

    private void CreateWalletsList(IReadOnlyList<WalletConfig> walletsConfigs)
    {
        var walletsConfigs = WalletConnectUtils.GetSupportedWalletsListForUse(wallets);

        Debug.Log($"Created {walletsConfigs.Capacity} wallets");

        _loadedWallets = walletsConfigs;

        Debug.Log($"List of available wallet configurations: {JsonConvert.SerializeObject(walletsConfigs)}"); 
    }

    private void UnitonConnectSDKInitialized()
    {
        Debug.Log("Success initialize Uniton Connect SDK");
    }
}
```

You can now process the filtered wallet configurations for later display in the UI, or cache them for use later.
All code available for use in the library is fully documented. If you have any questions when working with the library API,
you can always see the implementation of all available functions in the `Example scene`.

### Connecting the wallet to the application:

Now we need to add callbacks to get the connection status of the wallet in the app.

```c#
public sealed class ConnectionWalletUsageTemplate: MonoBehaviour
{
    [SerializeField, Space] private Button _connectTestWallet;
    [SerializeField, Space] private RawImage _qrCodePlace;

    private UnitonConnectSDK _unitonSDK;

    private List<WalletConfig> _loadedWallets;

    private WalletConfig _currentWalletForConnect;
    
    public WalletConfig LatestAuthorizedWallet { get; private set; }

    private stirng _connectURL;
    
    private void OnDisable()
    {
        _unitonSDK.OnInitialized -= UnitonConnectSDKInitialized;  

        _unitonSDK.OnWalletConnectionFinished -= WalletConnectionFinished;
        _unitonSDK.OnWalletConnectionFailed -= WalletConnectionFailed;  

        _connectTestWallet.onClick.RemoveListener(ConnectWallet);    
    }

    private void Start()
    {
        _connectTestWallet.onClick.AddListener(ConnectWallet);

        _unitonSDK = UnitonConnectSDK.Instance;

        _unitonSDK.OnInitialized += UnitonConnectSDKInitialized; 

        _unitonSDK.OnWalletConnectionFinished += WalletConnectionFinished;
        _unitonSDK.OnWalletConnectionFailed += WalletConnectionFailed; 

        _unitonSDK.Initialize();

        _unitonSDK.GetWalletsConfigs(ProjectStorageConsts.
            START_TON_WALLETS_LINK, (walletsConfigs) =>
            {
                CreateWalletsList(walletsConfigs);
            });
    }

    private void CreateWalletsList(IReadOnlyList<WalletConfig> wallets)
    {
        var walletsConfigs = WalletConnectUtils.GetSupportedWalletsListForUse(wallets);

        Debug.Log($"Created {walletsConfigs.Capacity} wallets");

        _loadedWallets = walletsConfigs;

        Debug.Log($"List of available wallet configurations: {JsonConvert.SerializeObject(walletsConfigs)}"); 

        GenerateQRCode();
    }

    private async void GenerateQRCode()
    {
        _currentWalletForConnect = _loadedWallets[0];

        _connectURL = await _unitonSDK.GenerateConnectURL(_currentWalletForConnect);
        _qrCodePlace.texture = WalletVisualUtils.GetQRCodeFromUrl(_connectURL);
    }

    private void ConnectWallet()
    {
        if (WalletConnectUtils.HasHttpBridge(_currentWalletForConnect))
        {
            _unitonSDK.ConnectHttpBridgeWalletViaDeepLink(_currentWalletForConnect, _connectURL);
        }
        else if (WalletConnectUtils.HasJSBridge(_currentWalletForConnect) && _unitonSDK.IsUseWebWallets)
        {
            _unitonSDK.ConnectJavaScriptBridgeWalletViaDeeplink(_currentWalletForConnect);
        }
    }

    private void UnitonConnectSDKInitialized()
    {
        Debug.Log("Success initialize Uniton Connect SDK");
    }

    private void WalletConnectionFinished(Wallet wallet)
    {
        if (UnitonConnectSDK.Instance.IsWalletConnected)
        {
           var successConnectMessage = $"Wallet is connected, full account address: {wallet.Account.Address}, \n" +
               $"Platform: {wallet.Device.Platform}, " +
               $"Name: {wallet.Device.AppName}, " +
               $"Version: {wallet.Device.AppVersion}";

           var shortWalletAddress = WalletVisualUtils.ProcessWalletAddress(
               wallet.Account.Address.ToString(AddressType.Base64), 6);

           Debug.Log($"Connected wallet short address: {shortWalletAddress}");

           if (_loadedWallets != null)
           {
                 LatestAuthorizedWallet = WalletConnectUtils.GetConfigOfSpecifiedWallet(
                   _loadedWallets, wallet.Device.AppName);

                 Debug.Log($"The current wallet in the list is detected: " +
                   $"{JsonConvert.SerializeObject(LatestAuthorizedWallet)}");
           }

           return;
        }

        Debug.LogWarning($"Connect status: {UnitonConnectSDK.Instance.IsWalletConnected}");
    }

    private void WalletConnectionFailed(string message)
    {
        Debug.LogError($"Failed to connect " +
          $"the wallet due to the following reason: {message}");
    }
}
```

In this example, we create a link to connect to the current wallet in the `GenerateQRCode()` method, and generate a QR code to display on the connection panel.
Then, in the `ConnectWallet()` method to which the button was subscribed, the current configuration is checked for a specific bridge and further connection is made when the connect button is clicked.

**P.S:** HTTP and JavaScript connection bridges are differentiated by the way the deep links are navigated to the cryptocurrency wallet application/extension in the browser.

After successful connection via QR code or deep link, you will have connection status messages displayed in the console.
Here you can process the connected wallet **to get its configuration** and use it when sending a transaction later.
To nicely display the wallet address, the SDK has a method `WalletVisualUtils.ProcessWalletAddress(string address, int value)` that allows only the first and last characters of the address to be displayed.

**IMPORTANT:**
The `UnitonConnectSDK` has a `RestoreConnectionOnAwake` option that restores the last automatically saved session to the wallet. If it is activated, you can subscribe to the result of this event `UnitonConnectSDK.OnWalletConnectionRestored` and process the result if the response is successful.

### Generation and sending of transaction:

After we plugged the wallet into the app. Now you can perform various operations with it.
At the moment (version 0.2.6), from the list of interactions with the wallet there is `only sending toncoin` to the desired address. Therefore, below we will show an example of implementing only this function.

**P.S:** When the wallet functionality in `SDK Uniton Connect` is updated, the examples in the documentation will be updated.

```c#
public sealed class TransactionUsageTemplate : MonoBehaviour
{
    [SerializeField, Space] private ConnectionWalletUsageTemplate _walletEntryPoint;
    [SerializeField, Space] private Button _sendTransaction;

    private const string CREATOR_WALLET_ADDRESS = "UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6";
    private const double START_TON_AMOUNT = 0.01f;

    private void Start()
    {
        UnitonConnectSDK.Instance.OnSendTransactionFinished += TransactionSuccessfullySended;

        _sendTransaction.onClick.AddListener(Send);
    }

    private void OnDisable()
    {
        UnitonConnectSDK.Instance.OnSendTransactionFinished -= TransactionSuccessfullySended;

        _sendTransaction.onClick.RemoveListener(Send);  
    }

    private async void Send()
    {
        await UnitonConnectSDK.Instance.SendTransaction(_walletEntryPoint.LatestAuthorizedWallet,
            CREATOR_WALLET_ADDRESS, START_TON_AMOUNT);
    }

    private void TransactionSuccessfullySended(bool isSuccess)
    {
        Debug.Log($"Transaction sending was completed with status: {isSuccess}");
    }
}
```

This example shows a test implementation of sending TonCoin to my TON address.
In the `UnitonConnectSDK.Instance.SendTransaction()` method you need to pass the configuration of the last authorized wallet, the recipient address and the quantity in `double`. 

**P.S:** The wallet configuration is used **to automatically redirect** when creating a transaction, for its subsequent confirmation.

# Source Code

**.NET Source Code:**
```
https://github.com/continuation-team/TonSdk.NET
```

# Donations

If you want to support my work you can send TonCoin to this address:
```
UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6
```

Thanks for your support!

# Support

[![Email](https://img.shields.io/badge/-gmail-090909?style=for-the-badge&logo=gmail)](https://mail.google.com/mail/?view=cm&fs=1&to=misster.veit@gmail.com)
[![Telegram](https://img.shields.io/badge/-Telegram-090909?style=for-the-badge&logo=telegram)](https://t.me/MrVeit)
