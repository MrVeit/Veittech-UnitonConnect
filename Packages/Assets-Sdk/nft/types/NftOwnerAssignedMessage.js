"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftOwnerAssignedMessage = exports.storeNftOwnerAssignedMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeNftOwnerAssignedMessage(message) {
  return function (builder) {
    var queryId = message.queryId,
      previousOwner = message.previousOwner,
      payload = message.payload;
    builder.storeUint(opcodes_1.NFT_OWNER_ASSIGNED_OPCODE, 32).storeUint(queryId, 64).storeAddress(previousOwner).storeMaybeRef(payload);
  };
}
exports.storeNftOwnerAssignedMessage = storeNftOwnerAssignedMessage;
function loadNftOwnerAssignedMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_OWNER_ASSIGNED_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var previousOwner = slice.loadAddress();
  var eitherPayload = slice.loadBoolean();
  var payload = eitherPayload ? slice.loadRef() : slice.asCell();
  return {
    queryId: queryId,
    previousOwner: previousOwner,
    payload: payload
  };
}
exports.loadNftOwnerAssignedMessage = loadNftOwnerAssignedMessage;
