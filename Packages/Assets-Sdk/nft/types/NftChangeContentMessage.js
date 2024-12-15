"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftChangeContentMessage = exports.storeNftChangeContentMessage = void 0;
var NftRoyaltyParams_1 = require("./NftRoyaltyParams");
var opcodes_1 = require("../opcodes");
function storeNftChangeContentMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.NFT_CHANGE_CONTENT_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeRef(src.newContent);
    builder.store((0, NftRoyaltyParams_1.storeNftRoyaltyParams)(src.newRoyaltyParams));
  };
}
exports.storeNftChangeContentMessage = storeNftChangeContentMessage;
function loadNftChangeContentMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.NFT_CHANGE_CONTENT_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var newContent = slice.loadRef();
  var newRoyaltyParams = (0, NftRoyaltyParams_1.loadNftRoyaltyParams)(slice.loadRef().beginParse());
  return {
    queryId: queryId,
    newContent: newContent,
    newRoyaltyParams: newRoyaltyParams
  };
}
exports.loadNftChangeContentMessage = loadNftChangeContentMessage;
