"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonInternalTransferMessage = exports.storeJettonInternalTransferMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonInternalTransferMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_INTERNAL_TRANSFER_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeCoins(src.amount);
    builder.storeAddress(src.from);
    builder.storeAddress(src.responseAddress);
    builder.storeCoins(src.forwardTonAmount);
    builder.storeMaybeRef(src.forwardPayload);
  };
}
exports.storeJettonInternalTransferMessage = storeJettonInternalTransferMessage;
function loadJettonInternalTransferMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_INTERNAL_TRANSFER_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var amount = slice.loadCoins();
  var from = slice.loadAddress();
  var responseAddress = slice.loadAddress();
  var forwardTonAmount = slice.loadCoins();
  var eitherPayload = slice.loadBoolean();
  var forwardPayload = eitherPayload ? slice.loadRef() : slice.asCell();
  return {
    queryId: queryId,
    amount: amount,
    from: from,
    responseAddress: responseAddress,
    forwardTonAmount: forwardTonAmount,
    forwardPayload: forwardPayload
  };
}
exports.loadJettonInternalTransferMessage = loadJettonInternalTransferMessage;
