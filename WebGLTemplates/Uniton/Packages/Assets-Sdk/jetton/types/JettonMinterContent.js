"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonMinterContent = exports.storeJettonMinterContent = void 0;
function storeJettonMinterContent(src) {
  return function (builder) {
    builder.storeCoins(0);
    builder.storeAddress(src.admin);
    builder.storeRef(src.content);
    builder.storeRef(src.jettonWalletCode);
  };
}
exports.storeJettonMinterContent = storeJettonMinterContent;
function loadJettonMinterContent(slice) {
  var totalSupply = slice.loadCoins();
  var adminAddress = slice.loadAddress();
  var jettonContent = slice.loadRef();
  var jettonWalletCode = slice.loadRef();
  return {
    admin: adminAddress,
    content: jettonContent,
    jettonWalletCode: jettonWalletCode
  };
}
exports.loadJettonMinterContent = loadJettonMinterContent;