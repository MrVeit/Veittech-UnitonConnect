"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseJettonWalletTransaction = void 0;
var JettonWalletMessage_1 = require("./JettonWalletMessage");
var TransferAction_1 = require("../../common/types/TransferAction");
function parseJettonWalletTransaction(tx) {
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
  if (!tx.inMessage.body) {
    return {
      kind: 'unknown',
      transaction: tx
    };
  }
  var isBounced = tx.inMessage.info.bounced;
  var inMessage = (0, JettonWalletMessage_1.loadJettonWalletMessage)(tx.inMessage.body.beginParse());
  if (inMessage.kind === 'jetton_transfer') {
    return {
      kind: 'jetton_transfer',
      queryId: inMessage.queryId,
      from: tx.inMessage.info.src,
      to: inMessage.destination,
      amount: inMessage.amount,
      responseAddress: inMessage.responseDestination,
      forwardTonAmount: inMessage.forwardAmount,
      forwardPayload: inMessage.forwardPayload,
      transaction: tx
    };
  }
  if (isBounced && inMessage.kind === 'jetton_internal_transfer') {
    return {
      kind: 'jetton_transfer_failed',
      queryId: inMessage.queryId,
      amount: inMessage.amount,
      transaction: tx
    };
  }
  if (inMessage.kind === 'jetton_internal_transfer') {
    return {
      kind: 'jetton_transfer_received',
      queryId: inMessage.queryId,
      amount: inMessage.amount,
      from: tx.inMessage.info.src,
      transaction: tx
    };
  }
  if (inMessage.kind === 'jetton_burn') {
    return {
      kind: 'jetton_burn',
      queryId: inMessage.queryId,
      amount: inMessage.amount,
      transaction: tx
    };
  }
  if (isBounced && inMessage.kind === 'jetton_burn_notification') {
    return {
      kind: 'jetton_burn_failed',
      queryId: inMessage.queryId,
      amount: inMessage.amount,
      transaction: tx
    };
  }
  return {
    kind: 'unknown',
    transaction: tx
  };
}
exports.parseJettonWalletTransaction = parseJettonWalletTransaction;