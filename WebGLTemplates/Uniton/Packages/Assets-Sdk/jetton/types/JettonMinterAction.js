"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseJettonMinterTransaction = void 0;
var JettonMinterMessage_1 = require("./JettonMinterMessage");
var TransferAction_1 = require("../../common/types/TransferAction");
function parseJettonMinterTransaction(tx) {
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
  var inMessage = (0, JettonMinterMessage_1.loadJettonMinterMessage)(tx.inMessage.body.beginParse());
  if (inMessage.kind === 'mint') {
    return {
      kind: 'mint',
      queryId: inMessage.queryId,
      amount: inMessage.amount,
      recipient: inMessage.to,
      responseAddress: inMessage.responseAddress,
      forwardPayload: inMessage.forwardPayload,
      forwardTonAmount: inMessage.forwardTonAmount,
      value: tx.inMessage.info.value.coins,
      transaction: tx
    };
  }
  if (inMessage.kind === 'internal_transfer') {
    return {
      kind: 'burn',
      queryId: inMessage.queryId,
      amount: inMessage.amount,
      from: inMessage.from,
      value: tx.inMessage.info.value.coins,
      transaction: tx
    };
  }
  if (inMessage.kind === 'change_admin') {
    return {
      kind: 'change_admin',
      queryId: inMessage.queryId,
      newAdmin: inMessage.newAdmin,
      value: tx.inMessage.info.value.coins,
      transaction: tx
    };
  }
  if (inMessage.kind === 'change_content') {
    return {
      kind: 'change_content',
      queryId: inMessage.queryId,
      newContent: inMessage.newContent,
      value: tx.inMessage.info.value.coins,
      transaction: tx
    };
  }
  return {
    kind: 'unknown',
    transaction: tx
  };
}
exports.parseJettonMinterTransaction = parseJettonMinterTransaction;