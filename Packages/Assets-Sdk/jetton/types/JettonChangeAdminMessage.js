"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonChangeAdminMessage = exports.storeJettonChangeAdminMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeJettonChangeAdminMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_CHANGE_ADMIN_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeAddress(src.newAdmin);
  };
}
exports.storeJettonChangeAdminMessage = storeJettonChangeAdminMessage;
function loadJettonChangeAdminMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_CHANGE_ADMIN_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var newAdmin = slice.loadAddress();
  return {
    queryId: queryId,
    newAdmin: newAdmin
  };
}
exports.loadJettonChangeAdminMessage = loadJettonChangeAdminMessage;
