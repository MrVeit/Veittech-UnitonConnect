"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftRoyaltyParams = exports.storeNftRoyaltyParams = void 0;
function storeNftRoyaltyParams(src) {
  return function (builder) {
    builder.storeUint(src.numerator, 16);
    builder.storeUint(src.denominator, 16);
    builder.storeAddress(src.recipient);
  };
}
exports.storeNftRoyaltyParams = storeNftRoyaltyParams;
function loadNftRoyaltyParams(slice) {
  return {
    numerator: slice.loadUintBig(16),
    denominator: slice.loadUintBig(16),
    recipient: slice.loadAddress()
  };
}
exports.loadNftRoyaltyParams = loadNftRoyaltyParams;