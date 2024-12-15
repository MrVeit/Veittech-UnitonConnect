"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftExcessesMessage = exports.storeNftExcessesMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeNftExcessesMessage(message) {
  return function (builder) {
    var queryId = message.queryId;
    builder.storeUint(opcodes_1.NFT_EXCESSES_OPCODE, 32).storeUint(queryId, 64);
  };
}
exports.storeNftExcessesMessage = storeNftExcessesMessage;
function loadNftExcessesMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_EXCESSES_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  return {
    queryId: queryId
  };
}
exports.loadNftExcessesMessage = loadNftExcessesMessage;
