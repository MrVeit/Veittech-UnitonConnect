"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseTransferTransaction = void 0;
var TransferMessage_1 = require("./TransferMessage");
function parseTransferTransaction(tx) {
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
  var body = tx.inMessage.body.beginParse();
  var inMessage = (0, TransferMessage_1.loadTransferMessage)(body);
  if (inMessage.kind === 'simple_transfer') {
    return {
      kind: 'simple_transfer',
      from: tx.inMessage.info.src,
      to: tx.inMessage.info.dest,
      amount: tx.inMessage.info.value.coins,
      transaction: tx
    };
  }
  if (inMessage.kind === 'text_message') {
    return {
      kind: 'text_message',
      from: tx.inMessage.info.src,
      to: tx.inMessage.info.dest,
      amount: tx.inMessage.info.value.coins,
      text: inMessage.text,
      transaction: tx
    };
  }
  if (inMessage.kind === 'encrypted_message') {
    return {
      kind: 'encrypted_message',
      from: tx.inMessage.info.src,
      to: tx.inMessage.info.dest,
      amount: tx.inMessage.info.value.coins,
      data: inMessage.data,
      transaction: tx
    };
  }
  return {
    kind: 'unknown',
    transaction: tx
  };
}
exports.parseTransferTransaction = parseTransferTransaction;