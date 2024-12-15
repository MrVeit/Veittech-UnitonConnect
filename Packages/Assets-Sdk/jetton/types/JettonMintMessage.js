"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadJettonMintMessage = exports.storeJettonMintMessage = void 0;
var core_1 = require("@ton/core");
var opcodes_1 = require("../opcodes");
var JettonInternalTransferMessage_1 = require("./JettonInternalTransferMessage");
function storeJettonMintMessage(src) {
  return function (builder) {
    builder.storeUint(opcodes_1.JETTON_MINT_OPCODE, 32);
    builder.storeUint(src.queryId, 64);
    builder.storeAddress(src.to);
    builder.storeCoins(src.walletForwardValue);
    builder.storeRef((0, core_1.beginCell)().store((0, JettonInternalTransferMessage_1.storeJettonInternalTransferMessage)(src)).endCell());
  };
}
exports.storeJettonMintMessage = storeJettonMintMessage;
function loadJettonMintMessage(slice) {
  if (slice.loadUint(32) !== opcodes_1.JETTON_MINT_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var to = slice.loadAddress();
  var walletForwardValue = slice.loadCoins();
  var internalTransfer = (0, JettonInternalTransferMessage_1.loadJettonInternalTransferMessage)(slice.loadRef().beginParse());
  return {
    queryId: queryId,
    amount: internalTransfer.amount,
    from: internalTransfer.from,
    to: to,
    responseAddress: internalTransfer.responseAddress,
    forwardTonAmount: internalTransfer.forwardTonAmount,
    forwardPayload: internalTransfer.forwardPayload,
    walletForwardValue: walletForwardValue
  };
}
exports.loadJettonMintMessage = loadJettonMintMessage;
