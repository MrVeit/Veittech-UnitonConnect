"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonTransferMessage = exports.storeJettonTransferMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonTransferMessage(src) {
  return function (builder) {
    var _src$forwardAmount;
    builder.storeUint(opcodes_1.JETTON_TRANSFER_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeCoins(src.amount);
    builder.storeAddress(src.destination);
    builder.storeAddress(src.responseDestination);
    builder.storeMaybeRef(src.customPayload);
    builder.storeCoins((_src$forwardAmount = src.forwardAmount) !== null && _src$forwardAmount !== void 0 ? _src$forwardAmount : 0);
    builder.storeMaybeRef(src.forwardPayload);
  };
}
exports.storeJettonTransferMessage = storeJettonTransferMessage;
function loadJettonTransferMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_TRANSFER_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var amount = slice.loadCoins();
  var to = slice.loadAddress();
  var responseDestination = slice.loadMaybeAddress();
  var customPayload = slice.loadMaybeRef();
  var forwardAmount = slice.loadCoins();
  var eitherPayload = slice.loadBoolean();
  var forwardPayload = eitherPayload ? slice.loadRef() : slice.asCell();
  return {
    queryId: queryId,
    amount: amount,
    destination: to,
    responseDestination: responseDestination,
    customPayload: customPayload,
    forwardAmount: forwardAmount,
    forwardPayload: forwardPayload
  };
}
exports.loadJettonTransferMessage = loadJettonTransferMessage;