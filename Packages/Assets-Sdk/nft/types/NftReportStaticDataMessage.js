"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftReportStaticDataMessage = exports.storeNftReportStaticDataMessage = void 0;
var opcodes_1 = require("../opcodes");
function storeNftReportStaticDataMessage(message) {
  return function (builder) {
    var queryId = message.queryId,
      index = message.index,
      collection = message.collection;
    builder.storeUint(opcodes_1.NFT_REPORT_STATIC_DATA_OPCODE, 32).storeUint(queryId, 64).storeUint(index, 256).storeAddress(collection);
  };
}
exports.storeNftReportStaticDataMessage = storeNftReportStaticDataMessage;
function loadNftReportStaticDataMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_REPORT_STATIC_DATA_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var index = slice.loadUintBig(256);
  var collection = slice.loadAddress();
  return {
    queryId: queryId,
    index: index,
    collection: collection
  };
}
exports.loadNftReportStaticDataMessage = loadNftReportStaticDataMessage;
