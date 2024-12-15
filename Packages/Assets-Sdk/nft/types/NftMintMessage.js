"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftMintMessage = exports.storeNftMintMessage = void 0;
var core_1 = require("@ton/core");
var opcodes_1 = require("../opcodes");
function storeNftMintMessage(src, storeParams) {
  return function (builder) {
    builder.storeUint(opcodes_1.NFT_MINT_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeUint(src.itemIndex, 64);
    builder.storeCoins(src.value);
    builder.storeRef((0, core_1.beginCell)().store(storeParams(src.itemParams)).endCell());
  };
}
exports.storeNftMintMessage = storeNftMintMessage;
function loadNftMintMessage(slice, loadParams) {
  if (slice.loadUint(32) !== opcodes_1.NFT_MINT_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var itemIndex = slice.loadUintBig(64);
  var value = slice.loadCoins();
  var itemParams = slice.loadRef();
  return {
    itemIndex: itemIndex,
    value: value,
    itemParams: loadParams(itemParams.beginParse()),
    queryId: queryId
  };
}
exports.loadNftMintMessage = loadNftMintMessage;
