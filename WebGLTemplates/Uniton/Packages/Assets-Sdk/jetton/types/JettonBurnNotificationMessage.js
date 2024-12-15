"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonBurnNotificationMessage = exports.storeJettonBurnNotificationMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonBurnNotificationMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_BURN_NOTIFICATION_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeCoins(src.amount);
    builder.storeAddress(src.sender);
    builder.storeAddress(src.responseDestination);
  };
}
exports.storeJettonBurnNotificationMessage = storeJettonBurnNotificationMessage;
function loadJettonBurnNotificationMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_BURN_NOTIFICATION_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var amount = slice.loadCoins();
  var sender = slice.loadAddress();
  var responseDestination = slice.loadMaybeAddress();
  return {
    queryId: queryId,
    amount: amount,
    sender: sender,
    responseDestination: responseDestination
  };
}
exports.loadJettonBurnNotificationMessage = loadJettonBurnNotificationMessage;