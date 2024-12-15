"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonTransferNotificationMessage = exports.storeJettonTransferNotificationMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonTransferNotificationMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_TRANSFER_NOTIFICATION_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeCoins(src.amount);
    builder.storeAddress(src.sender);
    builder.storeMaybeRef(src.forwardPayload);
  };
}
exports.storeJettonTransferNotificationMessage = storeJettonTransferNotificationMessage;
function loadJettonTransferNotificationMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_TRANSFER_NOTIFICATION_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var amount = slice.loadCoins();
  var sender = slice.loadAddress();
  var eitherPayload = slice.loadBoolean();
  var forwardPayload = eitherPayload ? slice.loadRef() : slice.asCell();
  return {
    queryId: queryId,
    amount: amount,
    sender: sender,
    forwardPayload: forwardPayload
  };
}
exports.loadJettonTransferNotificationMessage = loadJettonTransferNotificationMessage;
