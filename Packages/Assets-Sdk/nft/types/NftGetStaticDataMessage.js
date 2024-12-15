"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftGetStaticDataMessage = exports.storeNftGetStaticDataMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeNftGetStaticDataMessage(message) {
  return function (builder) {
    var queryId = message.queryId;
    builder.storeUint(opcodes_1.NFT_GET_STATIC_DATA_OPCODE, 32).storeUint(queryId, 64);
  };
}
exports.storeNftGetStaticDataMessage = storeNftGetStaticDataMessage;
function loadNftGetStaticDataMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_GET_STATIC_DATA_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  return {
    queryId: queryId
  };
}
exports.loadNftGetStaticDataMessage = loadNftGetStaticDataMessage;
