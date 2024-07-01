# UNITON CONNECT

[![License](https://img.shields.io/github/license/mrveit/unity-ton-connect?color=318CE7&style=flat-square)](LICENSE)
[![Version](https://img.shields.io/github/package-json/v/mrveit/unity-ton-connect?color=318CE7&style=flat-square)](package.json)
[![Unity](https://img.shields.io/badge/Unity-2020.1+-2296F3.svg?color=318CE7&style=flat-square)](https://unity.com/releases/editor/archive)

**UNITON CONNECT** - library for working with the Ton Connect ecosystem in Unity. Authorization through TON wallets via deeplink or QR code to your dApps, making transactions and more.

# Installation

- Download the latest version of the SDK via [the .unityPackage file here](https://github.com/MrVeit/unity-ton-connect/releases),
- Install the [WebGL Threding Patcher plugin](https://github.com/VolodymyrBS/WebGLThreadingPatcher) in your project, which allows you to run asynchronous shuffles in a single thread in WebGL builds. Initially they don't work due to lack of multithreading support there.

**Link to install the plugin:**
```
https://github.com/VolodymyrBS/WebGLThreadingPatcher.git
```

# Initialization

Before you can use all the features of the SDK, you must initialize it in **one of two available ways**.

#### The first way is to activate `automatic initialization`:
The `UnitonConnectSDK` component has an option called `Initialize On Awake`. When you activate it, the SDK will initialize automatically. You only need to subscribe to the necessary events and start working with it.

<p align="center">
 <img width="500px" src="Assets/sdkOverview.png" alt="qr"/>
</p>

#### The second way is `manual initialization` in your own script:
Below is a test example of how it can look like.

```c#
public sealed class EntryPoint : MonoBehaviour
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

If you just want to test the SDK, activate the `Test Mode` option of the `UnitonConnectSDK` component. In this case, the SDK will initialize the library's test application.
In case you want your application name, its logo and a link to the project itself to be displayed when connecting to the wallets. You need to enter these data in the `dApp Config` window, which is located at the `Uniton Connect -> dApp Config` path.

<p align="center">
 <img width="500px" src="Assets/dAppConfigWindow.png" alt="qr"/>
</p>

Here you need to select your game/application logo, enter a title and provide a link to the site where it will be published.

The next step is to build the project in its current form. Before that you need to configure several parameters in the `Player Settings` window.
1. Install the custom `TC_Minimal` build template, which is available after importing the library into the project. It can be installed in `Build Settings -> Player Settings -> Resolution and Presentation -> WebGL Template`,
2. Then you should turn off the `Strip Engine Code` option, which is located in `Player Settings -> Othet Settings` and switch the `Managed Stripping Level` property type to **Minimal**, so that the library functionality is not cut out during unit build and it works correctly,
3. Now the final touches, in the `Player Settings -> Publish Settings` section you need to switch the `Enable Exception` property to any value except `Explicitly Thrown Exceptions Only`. Since in case of any errors in the build, you may **freeze or even crash** the project.

After successful build, **2 files will be created** in the build directory: `icon.png` and `dAppData.json`. They will contain all the information you entered earlier in the `dApp Config` window.
Now you can publish your project anywhere you want. After publishing your project, go back to the `dApp Config` window and specify the correct link for the SDK to work with your project correctly.

For test mode, **I recommend** using `Github Actions` with their `Static Page` feature. The tutorial for deploying a WebGL build to Unity can be found on the Internet, as there is no need to describe it here.
Now you can work with the SDK without any problems if you followed the second method. Anyway, this will be an additional guide when your project is ready to be published.

# Source Code

**.NET Source Code:**
```
https://github.com/continuation-team/TonSdk.NET
```

# Support

[![Email](https://img.shields.io/badge/-gmail-090909?style=for-the-badge&logo=gmail)](https://mail.google.com/mail/?view=cm&fs=1&to=misster.veit@gmail.com)
[![Telegram](https://img.shields.io/badge/-Telegram-090909?style=for-the-badge&logo=telegram)](https://t.me/MrVeit)
