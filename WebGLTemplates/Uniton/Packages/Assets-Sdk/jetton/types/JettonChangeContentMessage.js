"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonChangeContentMessage = exports.storeJettonChangeContentMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonChangeContentMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_CHANGE_CONTENT_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeRef(src.newContent);
  };
}
exports.storeJettonChangeContentMessage = storeJettonChangeContentMessage;
function loadJettonChangeContentMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_CHANGE_CONTENT_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var newContent = slice.loadRef();
  return {
    queryId: queryId,
    newContent: newContent
  };
}
exports.loadJettonChangeContentMessage = loadJettonChangeContentMessage;