# UNITON CONNECT

[![License](https://img.shields.io/github/license/mrveit/unity-ton-connect?color=318CE7&style=flat-square)](LICENSE)
[![Version](https://img.shields.io/github/package-json/v/mrveit/unity-ton-connect?color=318CE7&style=flat-square)](package.json)
[![Unity](https://img.shields.io/badge/Unity-2020.1+-2296F3.svg?color=318CE7&style=flat-square)](https://unity.com/releases/editor/archive)

**UNITON CONNECT** - library for working with the Ton Connect ecosystem in Unity. Authorization through TON wallets via deeplink or QR code to your dApps, making transactions and more.

# Installation

**As source:**

1. Download the latest version of the SDK via [the .unityPackage file here](https://github.com/MrVeit/unity-ton-connect/releases)
2. Install the WebGL Threding Patcher plugin in your project, which allows you to run asynchronous shuffles in a single thread in WebGL builds. Initially they don't work due to lack of multithreading support there.

**WebGL Threding Patcher plugin**
```
https://github.com/VolodymyrBS/WebGLThreadingPatcher.git
```

# Initialization

Before you can use all the features of the SDK, you must initialize it in **one of two available ways**.

#### The first way is to activate `automatic initialization`:
The `UnitonConnectSDK` component has an option called "Initialize On Awake". When you activate it, the SDK will initialize automatically. You only need to subscribe to the necessary events and start working with it.

#### The second way is manual initialization in your own script:
Below is a test example of how it can look like.

```c#
public sealed class EntryPoint : MonoBehaviour
{
    private UnitonConnectSDK _unitonSDK;
    
    private void OnDisable()
    {
        _unitonSDK.OnInitialized -= UnitonSDKInitialized;        
    }

    private void Start()
    {
        _unitonSDK = UnitonConnectSDK.Instance;

        _unitonSDK.OnInitialized += UnitonSDKInitialized;        

        _unitonSDK.Initialize();
    }

    private void UnitonSDKInitialized()
    {
        Debug.Log("Success initialize UnitonSDK");
    }
}
```

At this point, you may encounter errors because your dApp application has not yet been configured. 

If you just want to test the SDK, activate the `Test Mode` option of the UnitonConnectSDK component. In this case, the SDK will initialize the library's test application.
In case you want your application name, its logo and a link to the project itself to be displayed when connecting to the wallets. You need to enter these data in the `dApp Config` window, which is located at the `Uniton Connect -> dApp Config` path.

# Source Code

**.NET Source Code:**
```
https://github.com/continuation-team/TonSdk.NET
```

# Support

[![Email](https://img.shields.io/badge/-gmail-090909?style=for-the-badge&logo=gmail)](https://mail.google.com/mail/?view=cm&fs=1&to=misster.veit@gmail.com)
[![Telegram](https://img.shields.io/badge/-Telegram-090909?style=for-the-badge&logo=telegram)](https://t.me/MrVeit)
