"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.createSbtItemParamsValue = exports.loadSbtItemParams = exports.storeSbtItemParams = void 0;
var core_1 = require("@ton/core");
function storeSbtItemParams(src) {
  return function (builder) {
    builder.storeAddress(src.owner);
    if (typeof src.individualContent === 'string') {
      builder.storeRef((0, core_1.beginCell)().storeStringTail(src.individualContent).endCell());
    } else {
      builder.storeRef(src.individualContent);
    }
    builder.storeAddress(src.authority);
  };
}
exports.storeSbtItemParams = storeSbtItemParams;
function loadSbtItemParams(slice) {
  var owner = slice.loadAddress();
  var content = slice.loadRef();
  var authority = slice.loadMaybeAddress();
  return {
    owner: owner,
    individualContent: content,
    authority: authority
  };
}
exports.loadSbtItemParams = loadSbtItemParams;
function createSbtItemParamsValue() {
  return {
    store: storeSbtItemParams,
    load: loadSbtItemParams
  };
}
exports.createSbtItemParamsValue = createSbtItemParamsValue;
