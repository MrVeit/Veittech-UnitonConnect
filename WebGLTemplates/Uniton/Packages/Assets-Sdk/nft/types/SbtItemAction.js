"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseSbtItemTransaction = void 0;
var NftMessage_1 = require("./NftMessage");
var TransferAction_1 = require("../../common/types/TransferAction");
function parseSbtItemTransaction(tx) {
  var mayBeTransfer = (0, TransferAction_1.parseTransferTransaction)(tx);
  if (mayBeTransfer.kind !== 'unknown') {
    return mayBeTransfer;
  }
  if (tx.description.type !== 'generic') {
    return {
      kind: 'unknown',
      transaction: tx
    };
  }
  if (!tx.inMessage) {
    return {
      kind: 'unknown',
      transaction: tx
    };
  }
  if (tx.inMessage.info.type !== 'internal') {
    return {
      kind: 'unknown',
      transaction: tx
    };
  }
  if (tx.description.computePhase.type !== 'vm') {
    return {
      kind: 'unknown',
      transaction: tx
    };
  }
  if (tx.description.computePhase.exitCode !== 0) {
    return {
      kind: 'unknown',
      transaction: tx
    };
  }
  var inMessage = (0, NftMessage_1.loadNftMessage)(tx.inMessage.body.beginParse());
  if (inMessage.kind === 'nft_deploy') {
    return {
      kind: 'sbt_deploy',
      owner: inMessage.owner,
      content: inMessage.content,
      collection: tx.inMessage.info.src,
      transaction: tx
    };
  }
  return {
    kind: 'unknown',
    transaction: tx
  };
}
exports.parseSbtItemTransaction = parseSbtItemTransaction;