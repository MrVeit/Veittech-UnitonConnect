"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.createNftItemParamsValue = exports.loadNftItemParams = exports.storeNftItemParams = void 0;
var core_1 = require("@ton/core");
function storeNftItemParams(src) {
  return function (builder) {
    builder.storeAddress(src.owner);
    if (typeof src.individualContent === 'string') {
      builder.storeRef((0, core_1.beginCell)().storeStringTail(src.individualContent).endCell());
    } else {
      builder.storeRef(src.individualContent);
    }
  };
}
exports.storeNftItemParams = storeNftItemParams;
function loadNftItemParams(slice) {
  return {
    owner: slice.loadAddress(),
    individualContent: slice.loadRef()
  };
}
exports.loadNftItemParams = loadNftItemParams;
function createNftItemParamsValue() {
  return {
    store: storeNftItemParams,
    load: loadNftItemParams
  };
}
exports.createNftItemParamsValue = createNftItemParamsValue;