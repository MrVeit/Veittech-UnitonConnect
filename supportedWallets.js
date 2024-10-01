[
  {
    "app_name": "telegram-wallet",
    "name": "Wallet",
    "image": "https://wallet.tg/images/logo-288.png",
    "about_url": "https://wallet.tg/",
    "universal_url": "https://t.me/wallet?attach=wallet&mode=compact",
    "bridge": [
      {
        "type": "sse",
        "url": "https://bridge.ton.space/bridge"
      }
    ],
    "platforms": ["ios", "android", "macos", "windows", "linux"]
  },
  {
    "app_name": "tonkeeper",
    "name": "Tonkeeper",
    "image": "https://tonkeeper.com/assets/tonconnect-icon.png",
    "tondns": "tonkeeper.ton",
    "about_url": "https://tonkeeper.com",
    "universal_url": "https://app.tonkeeper.com/ton-connect",
    "deepLink": "tonkeeper-tc://",
    "bridge": [
      {
        "type": "sse",
        "url": "https://bridge.tonapi.io/bridge"
      },
      {
        "type": "js",
        "key": "tonkeeper"
      }
    ],
    "platforms": ["ios", "android", "chrome", "firefox", "macos"]
  },
  {
    "app_name": "mytonwallet",
    "name": "MyTonWallet",
    "image": "https://static.mytonwallet.io/icon-256.png",
    "about_url": "https://mytonwallet.io",
    "universal_url": "https://connect.mytonwallet.org",
    "bridge": [
      {
        "type": "js",
        "key": "mytonwallet"
      },
      {
        "type": "sse",
        "url": "https://tonconnectbridge.mytonwallet.org/bridge"
      }
    ],
    "platforms": [
      "chrome",
      "windows",
      "macos",
      "linux",
      "ios",
      "android",
      "firefox"
    ]
  },
  {
    "app_name": "openmask",
    "name": "OpenMask",
    "image": "https://raw.githubusercontent.com/OpenProduct/openmask-extension/main/public/openmask-logo-288.png",
    "about_url": "https://www.openmask.app/",
    "bridge": [
      {
        "type": "js",
        "key": "openmask"
      }
    ],
    "platforms": ["chrome"]
  },
  {
    "app_name": "tonhub",
    "name": "Tonhub",
    "image": "https://tonhub.com/tonconnect_logo.png",
    "about_url": "https://tonhub.com",
    "universal_url": "https://tonhub.com/ton-connect",
    "bridge": [
      {
        "type": "js",
        "key": "tonhub"
      },
      {
        "type": "sse",
        "url": "https://connect.tonhubapi.com/tonconnect"
      }
    ],
    "platforms": ["ios", "android"]
  },
  {
    "app_name": "dewallet",
    "name": "DeWallet",
    "image": "https://raw.githubusercontent.com/delab-team/manifests-images/main/WalletAvatar.png",
    "about_url": "https://delabwallet.com",
    "universal_url": "https://t.me/dewallet?attach=wallet",
    "bridge": [
      {
        "type": "sse",
        "url": "https://bridge.dewallet.pro/bridge"
      }
    ],
    "platforms": ["ios", "android", "macos", "windows", "linux"]
  },
  {
    "app_name": "xtonwallet",
    "name": "XTONWallet",
    "image": "https://xtonwallet.com/assets/img/icon-256-back.png",
    "about_url": "https://xtonwallet.com",
    "bridge": [
      {
        "type": "js",
        "key": "xtonwallet"
      }
    ],
    "platforms": ["chrome", "firefox"]
  },
  {
    "app_name": "tonwallet",
    "name": "TON Wallet",
    "image": "https://wallet.ton.org/assets/ui/qr-logo.png",
    "about_url": "https://chrome.google.com/webstore/detail/ton-wallet/nphplpgoakhhjchkkhmiggakijnkhfnd",
    "bridge": [
      {
        "type": "js",
        "key": "tonwallet"
      }
    ],
    "platforms": ["chrome"]
  },
  {
    "app_name": "bitgetTonWallet",
    "name": "Bitget Wallet",
    "image": "https://raw.githubusercontent.com/bitkeepwallet/download/main/logo/png/bitget_wallet_logo_0_gas_fee.png",
    "about_url": "https://web3.bitget.com",
    "deepLink": "bitkeep://",
    "bridge": [
      {
        "type": "js",
        "key": "bitgetTonWallet"
      },
      {
        "type": "sse",
        "url": "https://ton-connect-bridge.bgwapi.io/bridge"
      }
    ],
    "platforms": ["ios", "android", "chrome"],
    "universal_url": "https://bkcode.vip/ton-connect"
  },
  {
  "app_name": "safepalwallet",
  "name": "SafePal",
  "image": "https://s.pvcliping.com/web/public_image/SafePal_x288.png",
  "tondns":  "",
  "about_url": "https://www.safepal.com",
  "universal_url": "https://link.safepal.io/ton-connect",
  "deepLink": "safepal-tc://",
  "bridge": [
     {
        "type": "sse",
        "url": "https://ton-bridge.safepal.com/tonbridge/v1/bridge"
     },
     {
        "type": "js",
        "key": "safepalwallet"
     }
  ],
  "platforms": ["ios", "android", "chrome", "firefox"]
 },
  {
    "app_name": "okxTonWallet",
    "name": "OKX Wallet",
    "image": "https://static.okx.com/cdn/assets/imgs/247/58E63FEA47A2B7D7.png",
    "about_url": "https://www.okx.com/web3",
    "universal_url": "https://www.okx.com/download?appendQuery=true&deeplink=okx://web3/wallet/tonconnect",
    "bridge": [
      {
        "type": "js",
        "key": "okxTonWallet"
      },
      {
        "type": "sse",
        "url": "https://www.okx.com/tonbridge/discover/rpc/bridge"
      }
    ],
    "platforms": ["chrome", "safari", "firefox", "ios", "android"]
  },
  {
  "app_name": "okxTonWalletTr",
  "name": "OKX TR Wallet",
  "image": "https://static.okx.com/cdn/assets/imgs/247/587A8296F0BB640F.png",
  "about_url": "https://tr.okx.com/web3",
  "universal_url": "https://tr.okx.com/download?appendQuery=true&deeplink=okxtr://web3/wallet/tonconnect",
  "bridge": [
      {
        "type": "js",
        "key": "okxTonWallet"
      },
      {
        "type": "sse",
        "url": "https://www.okx.com/tonbridge/discover/rpc/bridge"
      }
    ],
    "platforms": [
      "chrome",
      "safari",
      "firefox",
      "ios",
      "android"
    ]
  },
  {
    "app_name": "hot",
    "name": "HOT",
    "image": "https://raw.githubusercontent.com/hot-dao/media/main/logo.png",
    "about_url": "https://hot-labs.org/",
    "universal_url": "https://t.me/herewalletbot?attach=wallet",
    "bridge": [
      {
        "type": "sse",
        "url": "https://sse-bridge.hot-labs.org"
      },
      {
        "type": "js",
        "key": "hotWallet"
      }
    ],
    "platforms": ["ios", "android", "macos", "windows", "linux"]
  },
  {
    "app_name": "bybitTonWallet",
    "name": "Bybit Wallet",
    "image": "https://s1.bycsi.com/bybit/deadpool/image-ac5bf003d25c4ae0bd21f3725694a850.png",
    "about_url": "https://www.bybit.com/web3",
    "universal_url": "https://app.bybit.com/ton-connect",
    "deepLink": "bybitapp://",
    "bridge": [
      {
        "type": "js",
        "key": "bybitTonWallet"
      },
      {
        "type": "sse",
        "url": "https://api-node.bybit.com/spot/api/web3/bridge/ton/bridge"
      }
    ],
    "platforms": ["ios", "android", "chrome"]
  },
  {
    "app_name": "GateWallet",
    "name": "GateWallet",
    "image": "https://img.gatedataimg.com/prd-ordinal-imgs/036f07bb8730716e/gateio-0925.png",
    "about_url": "https://www.gate.io/",
    "bridge": [
      {
        "type": "js",
        "key": "gatetonwallet"
      },
      {
        "type": "sse",
        "url": "https://dapp.gateio.services/tonbridge_api/bridge/v1"
      }
    ],
    "platforms": ["ios", "android"],
    "universal_url": "https://gateio.onelink.me/DmA6/web3"
  },
  {
    "app_name": "binanceWeb3TonWallet",
    "name": "Binance Web3 Wallet",
    "image": "https://public.bnbstatic.com/static/binance-w3w/ton-provider/binancew3w.png",
    "about_url": "https://www.binance.com/en/web3wallet",
    "deepLink": "bnc://app.binance.com/cedefi/ton-connect",
    "bridge": [
      {
        "type": "js",
        "key": "binancew3w"
      },
      {
        "type": "sse",
        "url": "https://wallet.binance.com/tonbridge/bridge"
      }
    ],
    "platforms":  ["ios", "android", "macos", "windows", "linux"],
    "universal_url": "https://app.binance.com/cedefi/ton-connect"
  }
]
