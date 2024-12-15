"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseSbtCollectionTransaction = void 0;
var SbtItemParams_1 = require("./SbtItemParams");
var NftCollectionMessage_1 = require("./NftCollectionMessage");
function parseSbtCollectionTransaction(tx) {
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
  var inMessage = (0, NftCollectionMessage_1.loadNftCollectionMessage)(tx.inMessage.body.beginParse(), (0, SbtItemParams_1.createSbtItemParamsValue)());
  if (inMessage.kind === 'mint') {
    return {
      kind: 'mint',
      index: inMessage.itemIndex,
      owner: inMessage.itemParams.owner,
      content: inMessage.itemParams.individualContent,
      authority: inMessage.itemParams.authority,
      transaction: tx
    };
  }
  if (inMessage.kind === 'mint_batch') {
    return {
      kind: 'mint_batch',
      items: inMessage.requests.map(function (item) {
        return {
          index: item.index,
          owner: item.params.owner,
          content: item.params.individualContent,
          authority: item.params.authority
        };
      }),
      transaction: tx
    };
  }
  if (inMessage.kind === 'change_admin') {
    return {
      kind: 'change_owner',
      newOwner: inMessage.newAdmin,
      transaction: tx
    };
  }
  if (inMessage.kind === 'change_content') {
    return {
      kind: 'change_content',
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
exports.parseSbtCollectionTransaction = parseSbtCollectionTransaction;
