"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftTransferMessage = exports.storeNftTransferMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeNftTransferMessage(message) {
  return function (builder) {
    var queryId = message.queryId,
      newOwner = message.newOwner,
      responseDestination = message.responseDestination,
      customPayload = message.customPayload,
      forwardAmount = message.forwardAmount,
      forwardPayload = message.forwardPayload;
    builder.storeUint(opcodes_1.NFT_TRANSFER_OPCODE, 32).storeUint(queryId, 64).storeAddress(newOwner).storeAddress(responseDestination).storeMaybeRef(customPayload).storeCoins(forwardAmount).storeMaybeRef(forwardPayload);
  };
}
exports.storeNftTransferMessage = storeNftTransferMessage;
function loadNftTransferMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_TRANSFER_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var newOwner = slice.loadAddress();
  var responseDestination = slice.loadMaybeAddress();
  var customPayload = slice.loadMaybeRef();
  var forwardAmount = slice.loadCoins();
  var eitherPayload = slice.loadBoolean();
  var forwardPayload = eitherPayload ? slice.loadRef() : slice.asCell();
  return {
    queryId: queryId,
    newOwner: newOwner,
    responseDestination: responseDestination,
    customPayload: customPayload,
    forwardAmount: forwardAmount,
    forwardPayload: forwardPayload
  };
}
exports.loadNftTransferMessage = loadNftTransferMessage;
