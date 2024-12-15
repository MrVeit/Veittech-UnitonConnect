"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftChangeAdminMessage = exports.storeNftChangeAdminMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeNftChangeAdminMessage(src) {
  return function (builder) {
    var _src$queryId;
    builder.storeUint(opcodes_1.NFT_CHANGE_ADMIN_OPCODE, 32);
    builder.storeUint((_src$queryId = src.queryId) !== null && _src$queryId !== void 0 ? _src$queryId : 0, 64);
    builder.storeAddress(src.newAdmin);
  };
}
exports.storeNftChangeAdminMessage = storeNftChangeAdminMessage;
function loadNftChangeAdminMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_CHANGE_ADMIN_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var newAdmin = slice.loadAddress();
  return {
    queryId: queryId,
    newAdmin: newAdmin
  };
}
exports.loadNftChangeAdminMessage = loadNftChangeAdminMessage;
