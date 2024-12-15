#!/usr/bin/env node
"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
require("dotenv/config");
var cancel_nft_sale_1 = require("./cancel-nft-sale");
var deploy_jetton_1 = require("./deploy-jetton");
var deploy_nft_collection_1 = require("./deploy-nft-collection");
var get_wallet_state_1 = require("./get-wallet-state");
var get_jetton_1 = require("./get-jetton");
var get_jetton_balance_1 = require("./get-jetton-balance");
var get_nft_collection_1 = require("./get-nft-collection");
var get_nft_collection_item_1 = require("./get-nft-collection-item");
var get_nft_collection_items_1 = require("./get-nft-collection-items");
var get_nft_item_1 = require("./get-nft-item");
var get_nft_sale_1 = require("./get-nft-sale");
var mint_jetton_1 = require("./mint-jetton");
var mint_nft_1 = require("./mint-nft");
var mint_sbt_1 = require("./mint-sbt");
var put_nft_for_sale_1 = require("./put-nft-for-sale");
var setup_env_1 = require("./setup-env");
var transfer_jetton_1 = require("./transfer-jetton");
var transfer_nft_1 = require("./transfer-nft");
var transfer_ton_1 = require("./transfer-ton");
var commands = {
  'setup-env': {
    main: setup_env_1.main,
    description: 'Sets up the environment for the application'
  },
  'get-wallet-state': {
    main: get_wallet_state_1.main,
    description: 'Print your wallet type and balance'
  },
  'cancel-nft-sale': {
    main: cancel_nft_sale_1.main,
    description: 'Cancels an existing NFT sale'
  },
  'deploy-jetton': {
    main: deploy_jetton_1.main,
    description: 'Create and deploy a new jetton'
  },
  'deploy-nft-collection': {
    main: deploy_nft_collection_1.main,
    description: 'Create and deploy a new NFT collection'
  },
  'get-jetton': {
    main: get_jetton_1.main,
    description: 'Print details of an existing jetton'
  },
  'get-jetton-balance': {
    main: get_jetton_balance_1.main,
    description: 'Print balance of a specific jetton wallet'
  },
  'get-nft-collection': {
    main: get_nft_collection_1.main,
    description: 'Print details of an existing NFT collection'
  },
  'get-nft-collection-item': {
    main: get_nft_collection_item_1.main,
    description: 'Print details of an item from the NFT collection'
  },
  'get-nft-collection-items': {
    main: get_nft_collection_items_1.main,
    description: 'Print details of all items from the NFT collection'
  },
  'get-nft-item': {
    main: get_nft_item_1.main,
    description: 'Print details of an NFT item'
  },
  'get-nft-sale': {
    main: get_nft_sale_1.main,
    description: 'Print details of an NFT sale'
  },
  'mint-jetton': {
    main: mint_jetton_1.main,
    description: 'Mint jettons and sends them to the wallet'
  },
  'mint-nft': {
    main: mint_nft_1.main,
    description: 'Mint NFT to the wallet'
  },
  'mint-sbt': {
    main: mint_sbt_1.main,
    description: 'Mint SBT to the wallet'
  },
  'put-nft-for-sale': {
    main: put_nft_for_sale_1.main,
    description: 'Puts an NFT item for sale'
  },
  'transfer-jetton': {
    main: transfer_jetton_1.main,
    description: 'Transfer jetton to another wallet'
  },
  'transfer-nft': {
    main: transfer_nft_1.main,
    description: 'Transfer NFT to another wallet'
  },
  'transfer-ton': {
    main: transfer_ton_1.main,
    description: 'Transfer TON to another wallet'
  }
};
function help() {
  console.log("Usage: assets-cli <command>\nCommands:");
  for (var cmd in commands) {
    var _commandInfo = commands[cmd];
    console.log("  - ".concat(cmd, " ").concat(Array(30 - cmd.length).fill(' ').join('')).concat(_commandInfo.description));
  }
}
var command = process.argv[2];
if (!command) {
  help();
  process.exit(0);
}
var commandInfo = commands[command];
if (commandInfo) {
  commandInfo.main()["catch"](function (e) {
    console.error(e);
    process.exit(1);
  });
} else {
  throw new Error("Unknown command: ".concat(command));
}
