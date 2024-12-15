"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseNftCollectionTransaction = void 0;
var NftCollectionMessage_1 = require("./NftCollectionMessage");
var NftItemParams_1 = require("./NftItemParams");
var TransferAction_1 = require("../../common/types/TransferAction");
function parseNftCollectionTransaction(tx) {
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
  var inMessage = (0, NftCollectionMessage_1.loadNftCollectionMessage)(tx.inMessage.body.beginParse(), (0, NftItemParams_1.createNftItemParamsValue)());
  if (inMessage.kind === 'mint') {
    return {
      kind: 'mint',
      queryId: inMessage.queryId,
      index: inMessage.itemIndex,
      owner: inMessage.itemParams.owner,
      content: inMessage.itemParams.individualContent,
      transaction: tx
    };
  }
  if (inMessage.kind === 'mint_batch') {
    return {
      kind: 'mint_batch',
      queryId: inMessage.queryId,
      items: inMessage.requests.map(function (item) {
        return {
          index: item.index,
          owner: item.params.owner,
          content: item.params.individualContent
        };
      }),
      transaction: tx
    };
  }
  if (inMessage.kind === 'change_admin') {
    return {
      kind: 'change_owner',
      queryId: inMessage.queryId,
      newOwner: inMessage.newAdmin,
      transaction: tx
    };
  }
  if (inMessage.kind === 'change_content') {
    return {
      kind: 'change_content',
      queryId: inMessage.queryId,
      newContent: inMessage.newContent,
      newRoyalty: inMessage.newRoyaltyParams,
      transaction: tx
    };
  }
  return {
    kind: 'unknown',
    transaction: tx
  };
}
exports.parseNftCollectionTransaction = parseNftCollectionTransaction;