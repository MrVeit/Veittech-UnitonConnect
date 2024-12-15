"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonBurnMessage = exports.storeJettonBurnMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonBurnMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_BURN_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeCoins(src.amount);
    builder.storeAddress(src.responseDestination);
    builder.storeMaybeRef(src.customPayload);
  };
}
exports.storeJettonBurnMessage = storeJettonBurnMessage;
function loadJettonBurnMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_BURN_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var amount = slice.loadCoins();
  var responseDestination = slice.loadMaybeAddress();
  var customPayload = slice.loadMaybeRef();
  return {
    queryId: queryId,
    amount: amount,
    responseDestination: responseDestination,
    customPayload: customPayload
  };
}
exports.loadJettonBurnMessage = loadJettonBurnMessage;
