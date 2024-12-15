"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftCollectionData = exports.storeNftCollectionData = void 0;
var core_1 = require("@ton/core");
var NftRoyaltyParams_1 = require("./NftRoyaltyParams");
function storeNftCollectionData(src) {
  return function (builder) {
    builder.storeAddress(src.admin);
    builder.storeUint(0, 64);
    builder.storeRef(src.content);
    builder.storeRef(src.itemCode);
    builder.storeRef((0, core_1.beginCell)().store((0, NftRoyaltyParams_1.storeNftRoyaltyParams)(src.royalty)).endCell());
  };
}
exports.storeNftCollectionData = storeNftCollectionData;
function loadNftCollectionData(slice) {
  return {
    admin: slice.loadAddress(),
    content: slice.loadRef(),
    itemCode: slice.loadRef(),
    royalty: (0, NftRoyaltyParams_1.loadNftRoyaltyParams)(slice)
  };
}
exports.loadNftCollectionData = loadNftCollectionData;
