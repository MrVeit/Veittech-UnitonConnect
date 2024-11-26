# UNITON CONNECT

[![Unity](https://img.shields.io/badge/Unity-2020.1+-2296F3.svg?color=318CE7&style=flat-square&logo=Unity&logoColor=E0FFFF)](https://unity.com/releases/editor/archive)
[![Version](https://img.shields.io/github/package-json/v/mrveit/Veittech-UnitonConnect?color=318CE7&style=flat-square&logo=buffer&logoColor=E0FFFF)](package.json)
[![License](https://img.shields.io/github/license/mrveit/Veittech-UnitonConnect?color=318CE7&style=flat-square&logo=github&logoColor=E0FFFF)](LICENSE)
![Last commit](https://img.shields.io/github/last-commit/MrVeit/Veittech-UnitonConnect/main?color=318CE7&style=flat-square&logo=alwaysdata&logoColor=E0FFFF)
![Last release](https://img.shields.io/github/release-date/MrVeit/Veittech-UnitonConnect?color=318CE7&style=flat-square&logo=Dropbox&logoColor=E0FFFF)
![Downloads](https://img.shields.io/github/downloads/MrVeit/Veittech-UnitonConnect/total?color=318CE7&style=flat-square&logo=codeigniter&logoColor=E0FFFF)

<p align="left">
 <img width="600px" src="Assets/sdkBackgroundLogo.png" alt="qr"/>
</p>

**UNITON CONNECT** is a user-friendly solution for working with the TON ecosystem inside web applications on Unity. 

# **SDK Functionality:**

This reflects the functionality already added to the library as well as that planned on the roadmap.

**P.S:** the list of features can be expanded if the community needs them.

- [x] Connecting TON wallets,
- [x] Reading Toncoin balance,
- [ ] Reading Jettons balance,
- [x] Reading available NFT collections,
- [x] Sending Toncoin between wallets,
- [ ] Sending Jettons between wallets,
- [ ] Sending NFT collections between wallets,
- [ ] Swapping Jettons to Toncoin and reverse,
- [ ] Creating and mint Jettons,
- [ ] Creating and mint NFT collections,
- [ ] Burning available NFT collections,
- [x] Additional utilities,
 - [x] Filter nft collections by various filters: skam, collection address and so on,
 - [x] Conversion of the number of tokens to nanotons and vice versa,
 - [x] Conversion of wallet addresses to different formats and visualize them,
 - [ ] Getting current price of Toncoin and Jettons.

# Technical Demo

You can test the SDK without installation on a demo app [in your browser](https://mrveit.github.io/Veittech-UnitonConnect/) or directly [in the TMA (Telegram Mini App)](https://t.me/UnitonConnect_bot/dAppStart).

# Supported Wallets & Platforms

|                Wallet Provider                | WebGL Desktop | WebGL Mobile |
| --------------------------------------------- |   :--------:  |  :--------:  |
| **Telegram Wallet**                           | ✔️           | ✔️           |
| **Ton Keeper**                                | ✔️           | ✔️           |
| **My Ton Wallet**                             | ✔️           | ✔️           |
| **Ton Hub**                                   | ⚠️           | ⚠️           |
| **DeWallet (TMA)**                            | ⚠️           | ⚠️           |
| **Bitget Wallet**                             | ✔️           | ✔️           |
| **Bitget Web3 (TMA)**                         | ⚠️           | ✔️           |
| **Safe Pal Wallet**                           | ⚠️           | ⚠️           |
| **OKX Wallet**                                | ✔️           | ✔️           |
| **OKX Mini Wallet (TMA)**                     | ⚠️           | ⚠️           |
| **Hot Wallet (TMA)**                          | ❌           | ❌           |
| **Bybit Wallet**                              | ✔️           | ✔️           |
| **Gate Wallet**                               | ❌           | ⚠️           |
| **Binance Web3 Wallet**                       | ⚠️           | ✔️           |
| **Fintopio Wallet (TMA)**                     | ✔️           | ✔️           |
| **Open Mask (Web Extension)**                 | ✔️           | ❌           |
| **XTon Wallet (Web Extension)**               | ✔️           | ❌           |
| **Ton Wallet (Web Extension)**                | ✔️           | ❌           |

<sub>✔️ Supported</sub> &nbsp; <sub>❌ Not Supported</sub> &nbsp; <sub>⚠️ In progress</sub>

# Supported Wallets & Connection Types

|        Wallet Provider         | QR Code  | Deeplink |
| ------------------------------ | :------: | :------: |
| **Telegram Wallet**            | 💻+📱    | 💻+📱    |
| **Ton Keeper**                 | 💻+📱    | 💻+📱    |
| **My Ton Wallet**              | 💻+📱    | 💻+📱    |
| **Ton Hub**                    | 💻+📱    | 📱       |
| **DeWallet (TMA)**             | ❌       | 💻+📱   |
| **Bitget Wallet**              | 💻+📱    | 💻+📱   |
| **Bitget Web3 (TMA)**          | ❌       | 💻+📱   |
| **Safe Pal Wallet**            | ❌       | 💻+📱   |
| **OKX Wallet**                 | 💻+📱    | 💻+📱   |
| **OKX Mini Wallet (TMA)**      | ❌       | 💻+📱   |
| **Hot Wallet (TMA)**           | ❌       | ❌      |
| **Bybit Wallet**               | 💻+📱    | 💻+📱   |
| **Gate Wallet**                | ❌       | 📱      |
| **Binance Web3 Wallet**        | 💻+📱    | 📱      |
| **Fintopio Wallet (TMA)**      | 💻+📱    | 💻+📱   |
| **Open Mask (Web Extension)**  | ❌       | 💻     |
| **XTon Wallet (Web Extension)**| ❌       | 💻     |
| **Ton Wallet (Web Extension)** | ❌       | 💻     |

<sub>💻+📱 All Clients</sub> <sub>💻 Desktop Client</sub> &nbsp; <sub>📱 Mobile Client</sub> &nbsp; <sub>❌ Not Supported</sub>

# Supported Tokens & Available Func

|           Token         |   Status   |       Func       |
| ----------------------- | :--------: | :--------------: |
| **TON**                 | ✔️         | Balance, Send    |
| **NOT**                 | ⚠️         | ⚠️              |
| **GRAM**                | ⚠️         | ⚠️              |
| **DOGS**                | ⚠️         | ⚠️              |
| **NFT**                 | ✔️         | Balance, Filter |
| **Custom Jetton**       | ⚠️         | ⚠️              |
 
<sub>✔️ Supported</sub> &nbsp; <sub>❌ Not Supported</sub> &nbsp; <sub>⚠️ In progress</sub>

# Dependencies

For the library to work correctly, the following dependencies **must be installed** in the project before use:
- **[WebGL Threading Patcher](https://github.com/VolodymyrBS/WebGLThreadingPatcher)** allows you to run asynchronous operations in a single thread in WebGL builds. Initially they do not work due to lack of multithreading support in them,
- **[Newtonsoft](https://www.youtube.com/watch?v=3H6xkl_EsvQ)** - modern solution for convenient work with json files.

**IMPORTANT:** `WebGL Threading Patcher` will be removed in future releases as it is relevant for older `TonSdk .NET` based implementation.
Not supported in **Unity 2023 and above**, so only use the sdk in **NATIVE VERSION** where it is not needed.

# Migration

This section is for smooth migration between sdk versions, in case of a global API change.

- **[From 0.2.* to 0.2.9.5](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/Docs/MigrationToNativeSDKGuide.MD)**

# Installation

[Download the latest version of the SDK via the .unityPackage file here](https://github.com/MrVeit/unity-ton-connect/releases).

# Initialization

Before you can use all the features of the SDK, you must initialize it in **one of two available ways**.

#### Аutomatic initialization
The `UnitonConnectSDK` component has an option called `Initialize On Awake`. When you activate it, the SDK will initialize automatically. You only need to subscribe to the necessary events and start working with it.

<p align="center">
 <img width="500px" src="Assets/sdkOverview.png" alt="qr"/>
</p>

#### Manual initialization
Below is a test example of how it can look like.

```c#
public sealed class InitExample: MonoBehaviour
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

**At this point, you may encounter errors because your dApp application has not yet been configured.**

### Possible problems

If you just want to test the SDK, activate the `Test Mode` option of the `UnitonConnectSDK` component. In this case, the SDK will initialize the library's test application.
In case you want your application name, its logo and a link to the project itself to be displayed when connecting to the wallets. You need to enter these data in the `dApp Config` window, which is located at the `Uniton Connect -> dApp Config` path.

<p align="center">
 <img width="500px" src="Assets/dAppConfigWindow.png" alt="qr"/>
</p>

Here you need to select your game/application logo, enter a title and provide a link to the site where it will be published.

**IMPORTANT:** Starting with `version 0.2.8`, you need to provide a link to your own API server, which uploads NFT collection images `bypassing CORS` and converts from `webP` to `jpeg` format (png format is available, but its size will take longer to load).
To raise your own API server on `Node.js`, go to the [Backend Set Up](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/README.MD#backend-set-up) section.

Now you need to do the first build of the project to generate the application configuration from the editor to a json file. 
Go to [Build](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/README.MD#build) to customize the build settings. 
After the first build and publishing the project to `Github Pages` or `another test server`, you can continue working with the SDK without any problem if you followed the second method.

# Usage Template

Now it's time for some examples of how to use the `Uniton Connect` library API. 
All code available for use in the library is fully documented. If you have any questions when working with the library API,
you can always see the implementation of all available functions in the `Example scene`.

### Connecting wallet

Now we need to add callbacks to get the connection status of the wallet in the app.

```c#
public sealed class ConnectWalletExample: MonoBehaviour
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

**P.S:** Previously it was necessary to work with the type of connection to the wallet manually, determining the presence of this or that bridge in the loaded configuration.
Since **version 0.2.9.5**, all connection is done through a native window! Now you don't need to make connection windows, create wallets icon for connection - all this is handled by native JS SDK.

After clicking on the button and calling the `Connect()` method, a native window will open, displaying the wallets available for connection (depending on whether the web application is running on a PC or phone, their number will vary).
Once the selected wallet is successfully connected, you can beautifully display its address. The SDK has a method `WalletVisualUtils.ProcessWalletAddress(string address, int value)` that shows only the first and last characters of the address.

P.S: Starting from `version 0.2.8`, you can change the address format of a connected TON wallet.

Conversion to `HEX/RAW` format:
```c#
string baseAddress = "UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I";
string hexAddress = WalletConnectUtils.GetHEXAddress(baseAddress); 

// OR

string currentAddress = UnitonConnectSDK.Istance.Wallet.ToString();
string hexAddress = UnitonConnectSDK.Instance.Wallet.ToHex();

//result: 0:c1da9d221d87032b762ad647658a2b5115a38af4b2329d4824fb25cb65799cd9
```

Conversion to `Bounceable` format:
```c#
string baseAddress = "UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I";
string bounceableAddress = WalletConnectUtils.GetBounceableAddress(baseAddress); 

// OR

string hexAddress = UnitonConnectSDK.Instance.Wallet.ToBounceable();

//result: EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON
```

Convert to `Non Bounceable` format:
```c#
string baseAddress = "EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON";
string nonBounceableAddress = WalletConnectUtils.GetNonBounceableAddress(baseAddress); 

// OR

string hexAddress = UnitonConnectSDK.Instance.Wallet.ToNonBounceable();

//result: UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I
```

**IMPORTANT:** UnitonConnectSDK has an option that automatically restores the last saved wallet session. You can subscribe to the result of this event `UnitonConnectSDK.OnNativeWalletConnectionRestored` and handle the result via the boolean `isRestored`.

### Sending Toncoin

At the moment in `version 0.2.8`, from the list of interactions with the wallet there is `only sending Toncoin` to the desired address. 
Therefore, below we will show an example of implementing only this function.

**P.S:** When the wallet functionality in `SDK Uniton Connect` is updated, the examples in the documentation will be updated.

```c#
public sealed class TransactionSendingExample: MonoBehaviour
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

This example shows a test implementation of sending toncoins to my TON address.
In the `UnitonConnectSDK.Instance.SendTransaction()` method it is necessary to pass the recipient address, the number of TON in `double` and the transaction comment (optionally, you can not use it and send without it - the SDK provides it). 
If the transaction is successful, the `OnNativeSendingTonFinished` event will be called, where you can get its hash, which is parsed from Boc, which is passed to the native sdk.
In case the transaction failed to be sent, the `OnNativeTransactionSendingFailed` event will be called, where you can get a potential reason for the failed transaction.

**P.S:** to actually confirm the sending of the toncoin to the recipient's address, automatic processing of the transaction status in the blockchain has been added.
To get a detailed status with all transaction information, subscribe to the `OnNativeTransactionConfirmed` event.

**IMPORTANT:** transaction confirmation is done `in real time`, based on a snapshot of the blockchain via the Ton API. The processing time depends **on the speed of the TON blockchain.** You can adjust the processing time yourself (standard delay is 15 seconds).

### Loading NFT collections

Starting with `version 0.2.8`, the ability to obtain NFT collections from a connected wallet has been added.

Below you can see the implementation of downloading all NFT collections:

```c#
public sealed class LoadingNftsExample : MonoBehaviour
{
    [SerializeField, Space] private Button _loadAllNftCollections;
    [SerializeField] private Button _loadTargetNftCollection;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.NFT _nftStorage => _unitonConnect.UserAssets.Nft;

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _nftStorage.OnNftCollectionsClaimed += NftCollectionsClaimed;
        _nftStorage.OnTargetNftCollectionClaimed += TargetNftCollectionClaimed;

        _nftStorage.OnNftCollectionsNotFounded += NftCollectionsNotFounded; 

        _loadAllNftCollections.onClick.AddListener(LoadAll);
        _loadTargetNftCollection.onClick.AddListener(LoadTarget);
    }

    private void OnDestroy()
    {
        _nftStorage.OnNftCollectionsClaimed -= NftCollectionsClaimed;
        _nftStorage.OnTargetNftCollectionClaimed -= TargetNftCollectionClaimed;

        _nftStorage.OnNftCollectionsNotFounded -= NftCollectionsNotFounded; 

        _loadAllNftCollections.onClick.RemoveListener(LoadAll);
        _loadTargetNftCollection.onClick.RemoveListener(LoadTarget);
    }

    private void LoadAll()
    {
        _nftStorage.Load(10);
    }

    private void LoadTarget()
    {
        //Address of Lost Dogs collection 
        string collectionAddress = "EQAl_hUCAeEv-fKtGxYtITAS6PPxuMRaQwHj0QAHeWe6ZSD0" 

        _nftStorage.LoadTargetCollection(collectionAddress, 10);
    }

    private async void NftCollectionsClaimed(NftCollectionData collections)
    {
        if (collections.Items.Count == 0)
        {
            Debug.LogWarning("No NFT collections was found on the wallet");

            return;
        }

        List<NftViewData> nftVisual = new();

        foreach (var nft in collections.Items)
        {
             var iconUrl = nft.Get100x100ResolutionWebp();

             Debug.Log($"Claimed icon by urL: {iconUrl}");

             var nftIcon = await WalletVisualUtils.GetIconFromProxyServerAsync(iconUrl);
             var nftName = nft.Metadata.ItemName;

             var newNftView = new NftViewData()
             {
                 Icon = nftIcon,
                 Name = nftName
             };

             nftVisual.Add(newNftView);

             Debug.Log($"Created NFT view with name: {nftName}");
        }
    }

    private void TargetNftCollectionClaimed(NftCollectionData collection)
    {
         Debug.Log($"Loaded target nft collection:{collection.Items.Collection.Name}");
    }

    private void NftCollectionsNotFounded()
    {
         Debug.LogWarning("Nft collections not found");
    }
}
```

**IMPORTANT:** To download images from NFT collections, you cannot directly access the repository links because each collection has its own servers configured. 
So if you try to load a collection unsuccessfully, you will get an empty image due to CORS blocking the request.
To do this, you need to call the `await WalletVisualUtils.GetIconFromProxyServerAsync(iconUrl)` method, which calls your API server, which downloads the image from the source and gives it to your Unity client.

**P.S:** In addition, when you attempt to load webP images, the server performs conversion to `JPEG format`.
If you try to upload a `JPEG/PNG`, the image will be returned with the original format.

Before you can upload a picture of an item from the NFT collection, you must select an image format. 

```c#
private async void NftCollectionsClaimed(NftCollectionData nftCollections)
{
    var firstItem = collections.Items[0];

    string bestIconUrl = firstItem.GetBestResolutionPng();

    string littleIconUrl = firstItem.Get5x5ResolutionWebp();
    string smallIconUrl = firstItem.Get100x100ResolutionWebp();
    string mediumIconUrl = firstItem.Get500x500ResolutionWebp();
    string bigIconUrl = firstItem.Get1500x1500ResolutionWebp();
}
```

**P.S:** The method names reflect the size of the webp image in pixels.

### Loading TON balance

Starting with `version 0.2.8`, added possibility to get current balance of connected wallet **(only Toncoin at the moment)**.

Implementation of current balance request is shown below:

```c#
public sealed class TonBalanceLoadingExample: MonoBehaviour
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

If you need to transfer your Ton balance to Nanoton and reverse, you can use the following methods:

```c#
private void TonBalanceClaimed(decimal tonBalance)
{
    decimal nanotons = tonBalance.ToNanoton();
    decimal tons = nanotons.FromNanoton()

    Debug.Log($"Loaded TON balance: {tonBalance}");
    Debug.Log($"Loaded TON balance in nanotons: {nanotons}");

    _balanceBar.text = $"{tonBalance} TON";
}
```

# Build

In order to build a project, you must first configure several settings in the `Player Settings` window:
1. Set the custom `Uniton` build template, which is available after importing the library into the project. It can be set under `Build Settings -> Player Settings -> Resolution and Presentation -> WebGL Template`,
2. Then you need to switch the `Managed Stripping Level` property type to **Minimal**, so that the library functionality is not cut out when building the uniton and works correctly,
3. Now the final touches, under `Player Settings -> Publish Settings` you need to switch the `Enable Exception` property to any value except `Explicitly Thrown Exceptions Only`. Because if build errors occur, you can **freeze or even crash** the project.

After a successful build, **2 files** will be created in the build directory: `icon.png` and `dAppData.json`. These will contain all the information you entered earlier in the `dApp Config` window.
You can now publish your project anywhere you want. After publishing your project, go back to the `dApp Config` window and specify the correct link so that the SDK will work correctly with your project.

For test mode **I recommend** using `Github Actions` with their `Static Page` feature. A guide to deploying a WebGL build to Unity can be found online, so there's no need to describe it here.

# Backend Set Up

First, you need to download the source code of this Proxy Server, which the SDK accesses to load and convert pictures from NFT collections:

```
https://github.com/MrVeit/Veittech-UnitonConnect-ServerAPI
```

If you already have Node.js installed on your Windows computer (does anyone make Unity games on Linux? :D), you can skip this step and move on to the next one. If you still don't have it installed, you need to go to the official Node.js website and [install it yourself](https://nodejs.org/en/).

After installing and cloning the above two repositories, you can open the project in `VS Code` or `any other code editor` that **supports Node.js**.

Now, to be able to run the proxy locally and start testing, you need to create an environment variable repository. To do this, you need to create a file named `.env` in the project directory, but **without a file format**.

The following information must be filled in for the Proxy Server environment variables repository:

```config
HOST = http://localhost:
PORT = 3000
```

- Variable `HOST` is a link to your domain where the server is running, for testing purposes we will use the local address `http://localhost`,
- Variable `PORT` is a free port, which will listen to the proxy server to receive and send requests to the unity game client.

You can now run a proxy server to download pictures from nft collections. 
Open a terminal in the code editor in which you opened the project and type the following command:

```
npm start
```

**IMPORTANT:** In case you encounter a startup problem at this stage, it means you don't have Node.js installed or it was installed incorrectly.
Try reinstalling or searching for a solution to your problem on the Internet.

**P.S:** Now you can go back to the Unity project, open the dApp configuration window via `Uniton Connect -> dApp Config` and fill the `API Server URL` field with `http://localhost:3000`.

# Production Backend Deploy

```
Soon
```

# Donations

Ton Wallet (TON/NOT/USDt):
```
UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6
```

Multichain Wallet (BTC/ETH/BNB/MATIC)
```
0x231803Df809C207FaA330646BB5547fD087FEcA1
```

# Support

[![Email](https://img.shields.io/badge/-gmail-090909?style=for-the-badge&logo=gmail)](https://mail.google.com/mail/?view=cm&fs=1&to=misster.veit@gmail.com)
[![Telegram](https://img.shields.io/badge/-Telegram-090909?style=for-the-badge&logo=telegram)](https://t.me/unigram_tools)
