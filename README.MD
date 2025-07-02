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
- [x] Reading Jettons balance,
- [x] Reading NFT collections,
- [x] Sending Toncoin between wallets,
- [x] Sending Jettons between wallets,
- [x] Sending NFT items between wallets,
- [ ] Swapping Jettons to Toncoin and reverse,
- [ ] Burning NFT items on a wallet,
- [ ] Loading last successful transactions with Toncoin,
- [x] Loading last successful transactions with Jettons,
- [ ] Loading last successful transactions with NFT Collections,
- [x] Additional utilities,
  - [x] Filter nft collections by various filters: skam, collection address and more,
  - [x] Conversion of the number of tokens to nanotons and reverse,
  - [x] Conversion of wallet addresses to different formats and visualize them,
  - [ ] Getting current price of crypto assets.

# Technical Demo

You can test the SDK without installation on a demo app [in your browser](https://mrveit.github.io/Veittech-UnitonConnect/) or directly [in the TMA (Telegram Mini App)](https://t.me/UnitonConnect_bot/dAppStart).

# Supported Wallets & Platforms

|                Wallet Provider                | WebGL Desktop | WebGL Mobile |
| --------------------------------------------- | :-----------: | :----------: |
| **Telegram Wallet**                           | ✔️           | ✔️           |
| **Ton Keeper**                                | ✔️           | ✔️           |
| **My Ton Wallet**                             | ✔️           | ✔️           |
| **Ton Hub**                                   | ❌           | ✔️           |
| **DeWallet (TMA)**                            | ✔️           | ✔️           |
| **Bitget Wallet**                             | ✔️           | ✔️           |
| **Bitget Web3 (TMA)**                         | ✔️           | ✔️           |
| **Safe Pal Wallet**                           | ⚠️           | ✔️           |
| **OKX Wallet**                                | ✔️           | ✔️           |
| **OKX Mini Wallet (TMA)**                     | ✔️           | ✔️           |
| **Hot Wallet (TMA)**                          | ✔️           | ✔️           |
| **Bybit Wallet**                              | ✔️           | ✔️           |
| **Gate Wallet**                               | ✔️           | ✔️           |
| **Binance Web3 Wallet**                       | ✔️           | ✔️           |
| **Fintopio Wallet (TMA)**                     | ✔️           | ✔️           |
| **Open Mask (Web Extension)**                 | ✔️           | ❌           |
| **XTon Wallet (Web Extension)**               | ✔️           | ❌           |
| **Ton Wallet (Web Extension)**                | ✔️           | ❌           |
| **Tobi Copilot (TMA)**                        | ⚠️           | ✔️           |
| **Tomo Wallet (TMA)**                         | ⚠️           | ✔️           |

<sub>✔️ Supported</sub> &nbsp; <sub>❌ Not Supported</sub> &nbsp; <sub>⚠️ In progress</sub>

# Supported Wallets & Connection Types

|        Wallet Provider         | QR Code  | Deeplink |
| ------------------------------ | :------: | :------: |
| **Telegram Wallet**            | 💻+📱    | 💻+📱    |
| **Ton Keeper**                 | 💻+📱    | 💻+📱    |
| **My Ton Wallet**              | 💻+📱    | 💻+📱    |
| **Ton Hub**                    | 💻+📱    | 📱       |
| **DeWallet (TMA)**             | 💻+📱    | 💻+📱   |
| **Bitget Wallet**              | 💻+📱    | 💻+📱   |
| **Bitget Web3 (TMA)**          | 💻+📱    | 💻+📱   |
| **Safe Pal Wallet**            | ❌       | 💻+📱   |
| **OKX Wallet**                 | 💻+📱    | 💻+📱   |
| **OKX Mini Wallet (TMA)**      | 💻+📱    | 💻+📱   |
| **Hot Wallet (TMA)**           | 💻+📱    | 💻+📱   |
| **Bybit Wallet**               | 💻+📱    | 💻+📱   |
| **Gate Wallet**                | 💻+📱    | 📱      |
| **Binance Web3 Wallet**        | 💻+📱    | 📱      |
| **Fintopio Wallet (TMA)**      | 💻+📱    | 💻+📱   |
| **Open Mask (Web Extension)**  | 💻       | 💻     |
| **XTon Wallet (Web Extension)**| 💻       | 💻     |
| **Ton Wallet (Web Extension)** | 💻       | 💻     |
| **Tobi Copilot (TMA)**         | 💻+📱    | 📱      |
| **Tomo Wallet (TMA)**          | ❌       | 💻+📱  |

<sub>💻+📱 All Clients</sub> <sub>💻 Desktop Client</sub> &nbsp; <sub>📱 Mobile Client</sub> &nbsp; <sub>❌ Not Supported</sub>

# Supported Tokens & Available Features

|           Token         |          Features         |
| ----------------------- | :-----------------------: |
| **TON**                 | Balance, Send             |
| **USDT**                | Balance, Send, History    |
| **GRAM**                | Balance, Send, History    |
| **NOT**                 | Balance, Send, History    |
| **DOGS**                | Balance, Send, History    |
| **Custom Jetton**       | Balance, Send, History    |
| **NFT**                 | Balance, Send, Filter     |

# SDK Interaction Types & Available Features

|                Features                  | Client-Side  | Client+Server-Side |
| ---------------------------------------- | :----------: | :----------------: |
| **Connecting TON Wallets**               | ✔️          | ✔️                 |
| **Reading Toncoin balance**              | ✔️          | ✔️                 |
| **Reading Jettons balance**              | ❌          | ✔️                 |
| **Reading NFT collections**              | ❌          | ✔️                 |
| **Sending Toncoin between wallets**      | ✔️          | ✔️                 |
| **Sending Jettons between wallets**      | ❌          | ✔️                 |
| **Sending NFTs between wallets**         | ❌          | ✔️                 |
| **Loading Jettons transactions history** | ❌          | ✔️                 |

<sub>✔️ Supported</sub> &nbsp; <sub>❌ Not Supported</sub> &nbsp; <sub>⚠️ In progress</sub>

# Dependencies

For the library to work correctly, the following dependencies **MUST BE INSTALLED** in the project before use:
- **[Newtonsoft](https://www.youtube.com/watch?v=3H6xkl_EsvQ)** - modern solution for convenient work with json files.

**P.S:** Newtonsoft 3.2+ adds 2.5 MB to the web build, but if you don't need the new features or complex types in your project, I **RECOMMEND** using the [10-year-old version](https://github.com/SaladLab/Json.Net.Unity3D) (adds only 350-500 KB, depending on the package type).

# Migration

This section is for smooth migration between sdk versions, in case of a `global API change`.

- **[From 0.2.* to 0.2.9.5](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/Docs/MigrationToNativeSDKGuide.MD#migrating-from-02-to-0295),**
- **[From 0.2.9.5 to 0.5.0](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/Docs/MigrationToNativeSDKGuide.MD#migrating-from-0295-to-050).**

# Installation

**[Download the latest version of the SDK via the .unityPackage file here](https://github.com/MrVeit/Veittech-UnitonConnect/releases)**

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

**At this point, you may encounter errors because your dApp application has not yet been configured.**

### Possible problems

If you just want to test the SDK, activate the `Test Mode` option of the `UnitonConnectSDK` component. In this case, the SDK will initialize the library's test application.
In case you want your application name, its logo and a link to the project itself to be displayed when connecting to the wallets. You need to enter these data in the `dApp Config` window, which is located at the `Uniton Connect -> dApp Config` path.

**IMPORTANT:** starting with `version 0.2.9.5` of the library, the core library has switched **to native plugins/solutions** in `JavaScript`. Therefore, the library functionality cannot work in `Editor mode`. To test the functionality of the library in your project, it is **RECOMMENDED** to make a build with a test scene from the SDK.

<p align="center">
 <img width="500px" src="Assets/dAppConfigWindow.png" alt="qr"/>
</p>

Here you need to select your game/application logo, enter a title and provide a link to the site where it will be published.

**P.S:** for correct saving of your application logo after building, please `read the compression type` of the selected SDK logo standard.

**IMPORTANT:** Starting with `version 0.2.8`, you need to provide a reference to a custom API server that loads NFT collection images `bypassing CORS` and using the new SDK options associated with the `DeFi section` (from version 0.5.0 and above).
To bring up your own API server via the `library backend template` on **Node.js**, go to the [Backend Set Up](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/README.MD#backend-set-up) section.

Now you need to do the first build of the project to generate the application configuration from the editor to a json file. 
Go to [Build](https://github.com/MrVeit/Veittech-UnitonConnect/blob/main/README.MD#build) to customize the build settings. 
After the first build and publishing the project on `Github Pages` or `another test server`, you can continue integrating the SDK into your project without any problems if you followed the second method.

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

        _unitonConnect.OnInitiliazed -= SdkInitialized;

        _unitonConnect.OnWalletConnected -= WalletConnectionFinished;
        _unitonConnect.OnWalletConnectFailed -= WalletConnectionFailed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _connectButton.interactable = false;
        _connectButton.onClick.AddListener(Connect);

        _unitonConnect.OnInitiliazed += SdkInitialized;

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

    private void WalletConnectionFinished(WalletConfig wallet)
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

**P.S:** after clicking the button and calling the `Connect()` method, a native window will open, which will display the wallets available for connection (depending on whether the web application is running on PC or phone, their number will be different).
Once the selected wallet is successfully connected, you can beautifully display its address. The SDK has a method `UnitonConnectSDK.Instance.Wallet.ToShort(int size)`, which shows only the first and last characters of the address.

**P.S:** Starting from `version 0.2.8`, you can change the address format of the connected TON Wallet.

Conversion to `HEX/RAW` format:
```c#
string baseAddress = "UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I";
string hexAddress = WalletConnectUtils.GetHEXAddress(baseAddress); 

// OR

string hexAddress = UnitonConnectSDK.Instance.Wallet.ToHex();

//result: 0:c1da9d221d87032b762ad647658a2b5115a38af4b2329d4824fb25cb65799cd9
```

Conversion to `Bounceable` format:
```c#
string baseAddress = "UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I";
string bounceableAddress = WalletConnectUtils.GetBounceableAddress(baseAddress); 

// OR

string bounceableAddress = UnitonConnectSDK.Instance.Wallet.ToBounceable();

//result: EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON
```

Convert to `Non Bounceable` format:
```c#
string baseAddress = "EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON";
string nonBounceableAddress = WalletConnectUtils.GetNonBounceableAddress(baseAddress); 

// OR

string nonBounceableAddress = UnitonConnectSDK.Instance.Wallet.ToNonBounceable();

//result: UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I
```

**IMPORTANT:** UnitonConnectSDK has an option that automatically restores the last saved wallet session. You can subscribe to the result of this event `UnitonConnectSDK.OnWalletConnectRestored` and handle the result via the boolean `isRestored`.

### Sending Toncoin

Below is the implementation of sending Toncoin considering the latest `version 0.5.0`:

```c#
public sealed class TonTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;

    private UnitonConnectSDK _unitonConnect;

    private string _latestTransactionHash;

    private void OnDisable()
    {
        _unitonConnect.OnTonTransactionSended -= TonTransactionSendingFinished;
        _unitonConnect.OnTonTransactionSendFailed -= TonTransactionSendFailed;

        _unitonConnect.OnTonTransactionConfirmed -= TonTransactionConfirmed;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;

        _unitonConnect.OnTonTransactionSended += TonTransactionSendingFinished;
        _unitonConnect.OnTonTransactionSendFailed += TonTransactionSendFailed;

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

    private void TonTransactionSendFailed(string errorMessage)
    {
        Debug.LogError($"Failed to send ton transaction, reason: {errorMessage}");
    }

    private void TonTransactionConfirmed(
        SuccessTransactionData transactionData)
    {
        Debug.Log($"Ton transaction {_latestTransactionHash} " +
            $"confirmed in blockchain with status: {transactionData.IsSuccess}");

        var status = transactionData.IsSuccess;
        var newBalance = transactionData.EndBalance.FromNanoton();
        var fee = transactionData.TotalFees.FromNanoton();
        var sendedAmount = transactionData.OutMessages[0].Value.FromNanoton();
        var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
        var convertedAddress = WalletConnectUtils.GetNonBounceableAddress(recipientAddress);
        var message = transactionData.OutMessages[0].DecodedBody.MessageText;

        var transactionDetails = $"Loaded TON transaction data: \n" +
            $"STATUS: {transactionData.IsSuccess},\n" +
            $"HASH: {_latestTransactionHash},\n" +
            $"NEW BALANCE: {newBalance} TON,\n" +
            $"TOTAL FEE: {fee} TON,\n" +
            $"SENDED AMOUNT: {sendedAmount} TON,\n" +
            $"RECIPIENT ADDRESS: {convertedAddress},\n" +
            $"MESSAGE: {message}";

        Debug.Log(transactionDetails);
    }
}
```

This example shows a test implementation of sending thincoins to my TON address.
In the `UnitonConnectSDK.Instance.SendTransaction()` method you need to pass the recipient address, the number of TONs in `decimal` and the transaction comment (optional, you can leave it out and send without it - SDK provides it). 
If the transaction was successful, the `OnTonTransactionSended` event will be called, where you can get its hash, which is parsed from the Boc passed to the native sdk.
In case the transaction was not sent, the `OnTonTransactionSendFailed` event will be called, where you can get the potential reason for the failed transaction.

**P.S:** Automatic processing of the transaction status on the blockchain has been added to actually confirm the sending of the toncoin to the recipient's address.
To get a detailed status with all transaction information, subscribe to the `OnTonTransactionConfirmed` event.

**IMPORTANT:** Transaction confirmation is done `real-time`, based on a snapshot of the blockchain via the `third-party solution Ton API. The processing time depends **on the speed of the TON blockchain.** You can adjust the processing time yourself (standard delay is 15 seconds).

### Sending Jettons

Starting with `version 0.5.0` access the ability to `send any jetton`, below shows the implementation of sending Notocin:

```c#
public sealed class JettonTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.Jetton _jettonWallet;

    private void OnDisable()
    {
        _jettonWallet.OnTransactionSended -= JettonTransactionSendingFinished;
        _jettonWallet.OnTransactionSendFailed -= JettonTransactionSendFailed;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;
        _jettonWallet = _unitonConnect.Assets.Jetton;

        _jettonWallet.OnTransactionSended += JettonTransactionSendingFinished;
        _jettonWallet.OnTransactionSendFailed += JettonTransactionSendFailed;

        _sendTransctionButton.onClick.AddListener(Send);
    }

    private void Send()
    {
        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet was not connected");

            return;
        }

        var notcoinMaster = "EQAvlWFDxGF2lXm67y4yzC17wYKD9A0guwPkMs1gOsM__NOT";
        var creatorAddress = "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";
        var message = "Made by Uniton Connect";

        _jettonWallet.SendTransaction(JettonTypes.NOT,
           creatorAddress, (decimal)10000, (decimal)0.1f, message);

        // OR

        _jettonWallet.SendTransaction(notcoinMaster,
           creatorAddress, (decimal)10000, (decimal)0.1f, message);
    }

    private void JettonTransactionSendingFinished(string masterAddress,
        SuccessTransactionData transactionData)
    {
        var sendedAmount transactionData.OutMessages[0].DecodedBody.SendedAmount;
        var recipient = transactionData.OutMessages[0].DecodedBody.RecipientAddress;
        var payload = transactionData.OutMessages[0].DecodedBody.ForwardPayload;

        string message = string.Empty;

        if (payload.IsRight)
        {
            message = payload.Value.Value.MessageText;
        }

        Debug.Log($"Jetton transaction successfully founded, "+
          $"query id: {_lastTransactionQuery}, message: {message}, "+
          $"sended: {sendedAmount}, jetton master: {masterAddress}, recipient: {recipient}");
    }

    private void JettonTransactionSendFailed(string errorMessage)
    {
        Debug.LogError($"Failed to send ton transaction, reason: {errorMessage}");
    }
}
```

Sending jettons is different from the toncoin of transactions. First of all, you need to `calculate the gas correctly` so that tokens are not lost. Based on my testing results, it is better to `specify 0.1 TON` as the gas so that **most tokens** are successfully sent and a smart contract **jetton is automatically **deployed** if the recipient does not have it.

While the result of a **successful send** for ton transactions can be tracked almost immediately, jettons require direct confirmation on the blockchain. Because of the nature of these jettons, a transaction can be "successful" and return its hash, but fail at one stage of interaction `with the smart contract`. Therefore, while sending such a transaction, it is better **to display the loading screen** in the game and wait for the exact result to confirm receipt!

P.S: the jetton transaction can also be sent without a message, so you don't have to specify it.

### Sending NFT items

Starting with `version 0.5.0` access the ability to `send items of NFT collections`, below shows the implementation of this feature:

```c#
public sealed class NftTransactionSendingExample: MonoBehaviour
{
    [SerializeField, Space] private Button _sendTransctionButton;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.NFT _nftStorage;

    private void OnDisable()
    {
        _nftStorage.OnTransactionSended -= NftTransactionSendingFinished;
        _nftStorage.OnTransactionSendFailed -= NftTransactionSendFailed;

        _sendTransctionButton.onClick.RemoveListener(Send);
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;
        _nftStorage = unitonConnect.Assets.NFT;

        _nftStorage.OnTransactionSended += NftTransactionSendingFinished;
        _nftStorage.OnTransactionSendFailed += NftTransactionSendFailed;

        _sendTransctionButton.onClick.AddListener(Send);
    }

    private void Send()
    {
        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet was not connected");

            return;
        }

        var nftItemAddress = "EQCYYWQNYgl6OAhNsU75sik2oC1HoKuerXN5-Pq9JLh-SDra";
        var creatorAddress = "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";

        _nftStorage.SendTransaction(nftItemAddress, creatorAddress, (decimal)0.05f);
    }

    private void NftTransactionSendingFinished(string nftItemAddress,
         SuccessTransactionData transactionData)
    {
        var queryId = transactionData.OutMessages[0].DecodedBody.QueryId;
        var totalFee = transactionData.TotalFees.FromNanoton();
        var recipient = transactionData.OutMessages[0].DecodedBody.NewOwner;

        Debug.Log($"Nft transaction successfully founded, query id: {queryId}, "+
          $"item address: {nftItemAddress}, recipient: {recipient}, total fee: {totalFee}");
    }

    private void NftTransactionSendFailed(string errorMessage)
    {
        Debug.LogError($"Failed to send ton transaction, reason: {errorMessage}");
    }
}
```

When sending a nft item to a recipient, you need to `specify the address` of the item itself (not the nft collection), since everyone `has a unique one`. After running test transactions, I found out that the gas price can range from `0.05-0.1 TON`, but for reliability it is better `to specify 0.1 TON` to successfully send the item to the recipient.

**P.S:** nft transfers like jettons are `confirmed directly` through the blockchain, so waiting for the final result of the transfer may take some time.

### Loading TON balance

Below is the implementation of `toncoin balance` request on the connected wallet:

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

        _unitonConnect.LoadBalance();
    }

    private void TonBalanceClaimed(decimal balance)
    {
        Debug.Log($"Loaded toncoin balance: {balance}");

        _balanceBar.text = $"{balance} TON";
    }
}
```

If you need to convert your ton balance to `nanoton and reverse`, you can use the following methods:

```c#
decimal currentBalance = 1.5f;

decimal nanotons = currentBalance.ToNanoton(); // 150000000
decimal tons = nanotons.FromNanoton(); // 1.5 TON
```

### Loading Jetton balance

Starting from `version 0.5.0` the possibility of `reading current jetton balance` on the connected wallet from the standard list or by specifying the master address has been added.

Implementation of balance request in both cases is shown below:

```c#
public sealed class JettonBalanceLoadingExample: MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI _balanceBar;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.Jetton _jettonWallet;

    private void OnDisable()
    {
        _jettonWallet.OnBalanceLoaded -= JettonBalanceClaimed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;
        _jettonWallet = _unitonConnect.Assets.Jetton;

        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        _jettonWallet.OnBalanceLoaded += JettonBalanceClaimed;

        // USDt master address
        _jettonWallet.GetBalance("EQCxE6mUtQJKFnGfaROTKOt1lZbDiiX1kCixRv7Nw2Id_sDs");

        // OR

        _jettonWallet.GetBalance(JettonTypes.USDT);
    }

    private void JettonBalanceClaimed(decimal balance, 
        string jettonName, string masterAddress)
    {
        Debug.Log($"Loaded balance {balance} for "+
            "jetton {jettonName} by master address {masterAddress}");

        _balanceBar.text = $"{balance} USDt";
    }
}
```

In case this `jetton is missing` on the wallet - the event will return 0 as balance.
You can read the list of `classic jettons`, which are automatically `read with master address` before sending [in this section](https://github.com/MrVeit/Veittech-UnitonConnect#supported-tokens--available-func).

### Loading NFT collections

Below shows the implementation of `downloading all` and `target NFTs` from a connected wallet:

```c#
public sealed class LoadingNftsExample : MonoBehaviour
{
    [SerializeField, Space] private Button _loadAllNftCollections;
    [SerializeField] private Button _loadTargetNftCollection;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.NFT _nftStorage => _unitonConnect.Assets.Nft;

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
        if (collections == null ||
           collections.Items.Count == 0)
        {
            Debug.LogWarning("No NFT collections was found on the wallet");

            return;
        }

        foreach (var nft in collections.Items)
        {
             var nftName = nft.Metadata.ItemName;
             var nftAddress = nft.Address;
             var iconUrl = nft.Get100x100ResolutionWebp();

             Texture2D nftIcon = await WalletVisualUtils.GetWalletIconFromServerAsync(iconUrl);

             Debug.Log($"Claimed nft {nftName} by address "+
                 $"{nftAddress} with icon by url: {iconUrl}");
        }
    }

    private void TargetNftCollectionClaimed(NftCollectionData collection)
    {
         Debug.Log($"Loaded target nft collection:{collection.Items.Collection.Name}");
    }

    private void NftCollectionsNotFounded()
    {
         Debug.LogError("Nft collections not found");
    }
}
```

**IMPORTANT:** To download images from NFT collections, you cannot directly access the storage links because each collection has its own servers configured. 
So if you unsuccessfully try to load a collection, you will get an empty image due to CORS blocking the request.
To do this, you need to call the `await WalletVisualUtils.GetWalletIconFromServerAsync(iconUrl)` method, which downloads the image from the source with a `request to the library backend` and gives it to your Unity client.

**P.S:** Also, when you try to download webP images, the server performs a conversion to `JPEG format`.
If you try to upload `JPEG/PNG`, the image will be returned with the original format.

Before you can upload an image of an item from the NFT collection, you must select an image format:

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

The method names reflect the size of the `webp` image `in pixels`, or you can choose a better quality image in `png`, but it may weigh many times more and take longer to load.

### Loading Jetton wallet

Starting with `version 0.5.0`, an option to **receive the target jetton wallet** has been added. This option is especially useful for checking if there is a `deployed wallet`, so that you don't need to create a transfer transaction to pay for in-game purchases and so on.

**P.S:** each existing jetton wallet is a separate `smart contract` that has its own address and is directly **participating in the transfer of jettons** to the recipient.
You can read more about this mechanism [in the official Ton documentation](https://docs.ton.org/develop/dapps/asset-processing/jettons).

Below is an example of requesting `address of USDt` wallet on a connected wallet:

```c#
public sealed class JettonWalletLoadingExample: MonoBehaviour
{
    [SerializeField, Space] private TextMeshProUGUI _usdtWalletAddress;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.Jetton _jettonWallet;

    private void OnDisable()
    {
        _jettonWallet.OnAddressParsed -= JettonAddressParsed;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;
        _jettonWallet = _unitonConnect.Assets.Jetton;

        if (!_unitonConnect.IsWalletConnected)
        {
            Debug.LogWarning("Wallet is not connected");

            return;
        }

        _jettonWallet.OnAddressParsed += JettonAddressParsed;

        // USDt master address
        _jettonWallet.GetAddress("EQCxE6mUtQJKFnGfaROTKOt1lZbDiiX1kCixRv7Nw2Id_sDs");

        // OR

        _jettonWallet.GetAddress(JettonTypes.USDT);
    }

    private void JettonAddressParsed(JettonWalletData walletConfig)
    {
        Debug.Log($"Loaded jetton wallet with address {walletConfig.Address},"+
          $"current balance: {walletConfig.Balance}, owner: {walletConfig.Owner} "+
          #"and master address: {walletConfig.MasterAddress}");

        _balanceBar.text = $"USDt wallet address: {walletConfig.Address}";
    }
}
```

If the request is successful, the `OnAddressParsed` event returns the `wallet jetton` config, where the current balance, the owner's address and the master address of the jetton itself are specified as additional fields.
Otherwise, if the wallet owner did not previously have this jetton on their balance, `null` will be returned.

**IMPORTANT:** all addresses are returned `in HEX/RAW format`, so they should be converted additionally if necessary.

**P.S:** If you need to check if the recipient has a wallet jetton, you can use the utility `UserAssetsUtils.GetJettonWalletByAddress(string jettonMasterAddress, string ownerAddress, Action<JettonWalletData> jettonWalletParsed)`, but in this case the `OnAddressParsed` event will not be called.

### Loading Jetton transactions history

Starting with `version 0.5.0`, a jetton **transaction history loading** has been added. This is especially useful to be 100% sure that `jettons have reached` the recipient.

Below is an example of how to use this option:

```c#
public sealed class LastSuccessfulJettonTransactionsLoader : MonoBehaviour
{
    [SerializeField, Space] private Button _loadTransactions;

    private UnitonConnectSDK _unitonConnect;
    private UserAssets.Jetton _jettonWallet;

    private void OnDestroy()
    {
        _jettonWallet.OnLastTransactionsLoaded -= LatestTransactionsLoaded;
    }

    private void Start()
    {
        _unitonConnect = UnitonConnectSDK.Instance;
        _jettonWallet = _unitonConnect.Assets.Jetton;

        _loadTransactions.onClick.AddListener(Load);

        _jettonWallet.OnLastTransactionsLoaded += LatestTransactionsLoaded;
    }

    private void Load()
    {
        _jettonWallet.GetLastTransactions(TransactionTypes.Sent, 10);
    }

    private void LatestTransactionsLoaded(TransactionTypes type, 
        List<JettonTransactionData> transactions)
    {
        Debug.Log($"Last jetton transactions loaded with amount: {transactions.Count}");

        foreach (var transaction in transactions)
        {
            Debug.Log($"Loaded {type} jetton transaction "+
               "with query id: {transaction.QueryId}");
        }
    }
}
```

In addition to **specifying the list size** for transactions, you can also specify their `received/sent` type. Thus, it is possible to `match the last transaction history` of the recipient and the sender in two queries.

To request `recipient transaction history`, there is an additional argument to this method:

```c#
_jettonWallet.GetLastTransactions(TransactionTypes.Received, 10, 
    "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_");
```

**IMPORTANT:** The `QueryId` property is unique and is generated each time a payload is created for a transaction token. Thus, in addition to additional data in the form of `transaction hash`, `attached message`, it is also possible to check the successful sending status by it too.

# Build

In order to create a project, you must first configure several settings in the `Player Settings` window:
1. Set the custom `Uniton Connect` build template, which is available after importing the library into the project. It can be set under `Build Settings -> Player Settings -> Resolution and Presentation -> WebGL Template`,
2. In `Player Settings -> Publish Settings` section, you need to switch the `Enable Exception` property to any value except `Explicitly Thrown Exceptions Only`. Because if build errors occur, you can **freeze or even crash** the project.

After a successful build, **2 files** will be created in the build directory: `icon.png` and `dAppData.json`. These will contain all the information you entered earlier in the `dApp Config` window.
You can now publish your project anywhere you want. After publishing your project, go back to the `dApp Config` window and specify the correct link so that the SDK will work correctly with your project.

For test mode **I recommend** using `Github Actions` with their `Static Page` feature. Guidelines for deploying a WebGL build to Unity can be found `in google`, so there is no need to describe them here.

**IMPORNANT:** If your web server is configured with headers like `Content Security Policy` and `Cross-Origin Embedder Policy`, you may have problems during the SDK initialization phase, as it will not be able to work properly unless it downloads the native libraries via CDN. 

**You have two solutions:**
- Load locally `TonWeb` and `TonConnectUI` via the specified links in `index.html` from the build template and **specify a direct path** to them (in case it is important for you to keep these headers on the server for any security),
- Disable or configure these headers incorrectly so that **they do not block loading** of external scripts (you can search on the Internet about this).

# Backend Set Up

First you need to download the source code for this `API server`, which the SDK accesses to convert images from NFT collections, as well as a number of other functions:

```
https://github.com/MrVeit/Veittech-UnitonConnect-ServerAPI
```

If you already have `installed Node.js` on your Windows computer, you can skip this step and move on to the next one. If it is still not installed, you need to go to the official Node.js website and [install it yourself](https://nodejs.org/en/).

After installing Node.js and cloning the backend repository. you can open the project in `VS Code` or `any other code editor` that **supports Node.js**.

Now, to be able to run the backend locally and start testing, you need to create environment variables. To do this, you need to create a file named `.env` in the project directory, but **without the file format**.

The following information must **MUST BE ADDED** for the backend to work:

```config
PORT=3000

TON_API_KEY="YOUR-TON-API-KEY"
TON_CENTER_API_KEY="YOUR-TON-CENTER-API-KEY"
```

- The `PORT` variable is a free port that the api server will listen on to receive requests and send responses to the unity client,
- The `TON_API_KEY` variable is the api key that will be used by the sdk client to send requests `to the Ton Api`. To get it, you need to log in via telegram [on their dashboard](https://tonconsole.com/) and then enter it here,
- The `TON_CENTER_API_KEY` variable is an api key, which is also used to send requests, but now `in Ton Center V3`, due to the unavailability of some features in the first provider. To get it, you need to enter [their official bot](https://t.me/toncenter) via telegram.

Now you can run the `backend locally`, for full access to `all SDK features`.
To do this, open a terminal in the code editor in which you opened the project and enter the following command:

```
npm start
```

If at this point you encounter a **startup problem**, it means that you **do not have Node.js** installed or it was installed incorrectly.
Try reinstalling it or search google for a solution to the problem.

**P.S:** Now you can go back to your Unity project, open the dApp configuration window via `Uniton Connect -> dApp Config` and fill the `API Server URL` field with the value `http://localhost:3000`.

**IMPORTANT:** This method will only work **FOR LOCAL TESTING**, so that requests will come into the backend of the cdk `on the local port`. How to do this, deploy web build locally - search on the same `google`.
In case you need to run the web build `elsewhere` (Github Pages, ItchIo and other sites) and `test inside a Telegram bot`, you need to upload the backend sdk to your rented `VPS/VDS server`.

# Production Backend Deploy

Here is a step-by-step guide to deploying the library backend on your server.

As an example, we'll use a virtual server on Ubuntu OC, rented from this hosting provider (if you also decide to rent a server from them, follow [this link and get a nice discount in the form of 15% cashback](https://aeza.net/?ref=482600), if you top up your balance in the first 24 hours).

**P.S:** Check the list of `installable modules`, if you have them `previously installed` on your server, you **can skip straight** [to the startup section.](https://github.com/MrVeit/Veittech-UnitonConnect#deploy)

## Installation of required modules

Once we have rented a virtual server and connected to it via SSH, we can now install the required modules.

### Installing Git

Allows you to clone public/private repositories to your server.

1. Update the list of packages on the server:
```
sudo apt update
```

2. Installing a module:
```
sudo apt install git -y
```

3. After the installation is complete, enter this command to verify that the installation was successful:
```
git --version
```

### Installing Docker

Runs your project as an isolated container and greatly speeds up the process of running it on the server, without having to write a bunch of commands.

1. First, you need to download the base packages for Docker:
```
sudo apt install apt-transport-https ca-certificates curl software-properties-common -y
```

2. Now we need to add the official Docker GPG key:
```
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
```

3. Now we need to add the docker repository to APT:
```
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

4. Update the list of packages on the server:
```
sudo apt update
```

5. Check that Docker will be installed from the official repository with the current version:
```
apt-cache policy docker-ce
```

6. The long-awaited installation of Docker on the server:
```
sudo apt install docker-ce -y
```

### Installing Make

Allows you to quickly deploy/stop/delete your projects on the server, without having to type a bunch of commands.

1. Update the list of packages on the server:
```
sudo apt update
```

2. Installing a package:
```
sudo apt install build-essential -y
```

3. Check that the module has been installed correctly:
```
make --version
```

### Installing Nginx

The web server, which in this example will `proxy all requests` to your API server when you send `GET` and `POST` requests to your server's IP address or domain.

1. Update the list of packages on the server:
```
sudo apt update
```

2. Installing a package:
```
sudo apt install nginx -y
```

3. Now it is necessary to check the status of Nginx operation:
```
sudo systemctl status nginx
```

4. Go to the IP address of your server in your browser:
```
http://YOUR_SERVER_IP_ADDRESS
```

After the correct installation, you need to make sure that `port 80` is open on the server.
If Nginx is successfully installed and working correctly, you will **see a welcome page.**

## Configuring components

### Configuring HTTPS connection

Telegram has very strict rules regarding requests to third-party resources from Telegram bots. Therefore, it is necessary to add the ability to connect to the API server via HTTPS 

**P.S:** In general, this is a very important point, because with HTTP connection data can be quietly listened to by third parties, which entails information leakage and danger for your users).

To proceed, you need to **register a domain** for your server so that you can install the certificate there.

You can do this [right here](https://my.aeza.net/order/domain) if you have already clicked the link and received a [nice discount at checkout](https://aeza.net/?ref=482600).

Once you have connected your server's IP address in the domain settings, you can start setting up the connection.
If you don't have a reliable SSL certificate, you can generate one with a few commands.

1. Update the list of packages on the server:
```
sudo apt update
```

2. Install the package and plugin for Nginx:
```
sudo apt install certbot python3-certbot-nginx -y
``` 

3. Obtaining SSL certificate:
```
sudo certbot --nginx -d YOUR_DOMAIN_NAME
```

In the `YOUR_DOMAIN_NAME` field, write the address of the leased domain that you should have previously purchased at this point.

Follow the on-screen instructions to complete the process, many of which you can skip by pressing the `Enter` button. 
Certbot will prompt you to choose whether to redirect `HTTP` traffic to `HTTPS`, which is recommended for security.

### Configuring API server configuration

1. Now you need to go to the nginx configuration directory and create a configuration for your API server:
```
cd /etc/nginx/sites-available/
```

2. Create a configuration with the name of your domain:
```
nano YOUR_DOMAIN_NAME
```

3. Copy and paste this configuration data, but replace `YOUR_DOMAIN_NAME` with your domain:

```nginx

server
{
    listen 80;
    server_name YOUR_DOMAIN_NAME;

    if ($host = YOUR_DOMAIN_NAME) {
        return 301 https://$host$request_uri;
    }
}

server
{
   listen 443 ssl;
   server_name YOUR_DOMAIN_NAME;

   ssl_certificate /etc/letsencrypt/live/YOUR_DOMAIN_NAME/fullchain.pem; # managed by Certbot
   ssl_certificate_key /etc/letsencrypt/live/YOUR_DOMAIN_NAME/privkey.pem; # managed by Certbot
   include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
   ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot

   location /api/uniton-connect/v1 {
     proxy_pass http://localhost:3000;
     proxy_set_header Host $host;
     proxy_set_header X-Real-IP $remote_addr;
     proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
     proxy_set_header X-Forwarded-Proto $scheme;

     add_header 'Access-Control-Allow-Origin' '*' always;
     add_header 'Access-Control-Allow-Methods' 'GET, POST, PUT, DELETE, OPTIONS' always;
     add_header 'Access-Control-Allow-Headers' 'Origin, Content-Type, Accept, Authorization' always;

     if ($request_method = OPTIONS) {
      return 204;
     }

     add_header 'X-Content-Type-Options' 'nosniff';
     add_header 'Server' 'nginx';
     add_header 'Cache-Control' 'no-store, no-cache, must-revalidate, proxy-revalidate, max-age=0';
   }
}
```

Also, in addition to setting up nginx, in this configuration we set up mandatory headers so that the connection succeeds and is not blocked.

**IMPORTANT:** Failure to do so will result in an attempt to send `any GET/POST` request from your Web App **inside Telegram being blocked.**

4. Create a symbolic link to your configuration in the sites-enabled directory:
```
sudo ln -s /etc/nginx/sites-available/YOUR_DOMAIN_NAME /etc/nginx/sites-enabled/
```

5. Check the Nginx configuration for syntax errors:
```
sudo nginx -t
```

**IMPORTANT:** In case you see `syntax is OK`, you can proceed to the next step.

6. Reboot Nginx to apply the new configuration:
```
sudo systemctl restart nginx
```

## Deploy

After installing all the necessary modules and setting up the necessary configurations, you can start to deploy the backend.

1. Go to the main directory of the server:
```
cd /root/
```

2. Clone the repository with the API server into the root directory:
```
git clone https://github.com/MrVeit/Veittech-UnitonConnect-ServerAPI.git
```

3. Get the contents of the directory:
```
ls -l
```

4. Navigate to the server API files:
```
cd Veittech-UnitonConnect-ServerAPI/src
```

5. Now we need to create a file with the storage of environment variables that are used in the project:
```
nano .env
```

**P.S:** We have already created it earlier [at this stage](https://github.com/MrVeit/Veittech-UnitonConnect#backend-set-up), so copy and paste these values.

6. Start building and creating a docker container with an API server with a single command:
```
make run
```

7. To verify that the docker container is running, enter the following command:
```
docker ps
```

**P.S:** If the container is successfully started, you will see its name in the list, its id, and the time since it was started.

8. After the deploy, check the docker container logs to make sure no errors occurred:
```
make logs
```

If the launch **was successful**, you will see only one line in the logs: `Uniton Connect Backend is listening at 3000`.
The library backend `is now running` on your server and `eady to receive request`s from the Unity client to **work with Jettons, NFT and other** elements.

# Donations

Ton Wallet (TON/NOT/USDt):
```
UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6
```

Multichain Wallet (BTC/ETH/BNB/MATIC)
```
0x231803Df809C207FaA330646BB5547fD087FEcA1
```

**P.S:** If you donate, please include **your github/telegram nickname** in the `transfer comment` so I can add you as a sponsor of my library, thanks for your support!

# Support

[![Email](https://img.shields.io/badge/-gmail-090909?style=for-the-badge&logo=gmail)](https://mail.google.com/mail/?view=cm&fs=1&to=misster.veit@gmail.com)
[![Telegram](https://img.shields.io/badge/-Telegram-090909?style=for-the-badge&logo=telegram)](https://t.me/unigram_tools)
