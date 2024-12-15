"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonExcessesMessage = exports.storeJettonExcessesMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonExcessesMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_EXCESSES_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
  };
}
exports.storeJettonExcessesMessage = storeJettonExcessesMessage;
function loadJettonExcessesMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_EXCESSES_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  return {
    queryId: queryId
  };
}
exports.loadJettonExcessesMessage = loadJettonExcessesMessage;
