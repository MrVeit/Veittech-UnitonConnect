"use strict";

function _createForOfIteratorHelper(r, e) { var t = "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"]; if (!t) { if (Array.isArray(r) || (t = _unsupportedIterableToArray(r)) || e && r && "number" == typeof r.length) { t && (r = t); var _n = 0, F = function F() {}; return { s: F, n: function n() { return _n >= r.length ? { done: !0 } : { done: !1, value: r[_n++] }; }, e: function e(r) { throw r; }, f: F }; } throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); } var o, a = !0, u = !1; return { s: function s() { t = t.call(r); }, n: function n() { var r = t.next(); return a = r.done, r; }, e: function e(r) { u = !0, o = r; }, f: function f() { try { a || null == t["return"] || t["return"](); } finally { if (u) throw o; } } }; }
function _unsupportedIterableToArray(r, a) { if (r) { if ("string" == typeof r) return _arrayLikeToArray(r, a); var t = {}.toString.call(r).slice(8, -1); return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray(r, a) : void 0; } }
function _arrayLikeToArray(r, a) { (null == a || a > r.length) && (a = r.length); for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e]; return n; }
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.createNftMintItemValue = exports.loadNftBatchMintItem = exports.storeNftBatchMintItem = exports.loadNftBatchMintMessage = exports.storeNftBatchMintMessage = void 0;
var core_1 = require("@ton/core");
var opcodes_1 = require("../opcodes");
function storeNftBatchMintMessage(src, storeParams) {
  return function (builder) {
    var _src$queryId;
    var dict = core_1.Dictionary.empty(core_1.Dictionary.Keys.BigUint(64), createNftMintItemValue(storeParams));
    var _iterator = _createForOfIteratorHelper(src.requests),
      _step;
    try {
      for (_iterator.s(); !(_step = _iterator.n()).done;) {
        var r = _step.value;
        if (dict.has(r.index)) {
          throw new Error('Duplicate items');
        }
        dict.set(r.index, r);
      }
    } catch (err) {
      _iterator.e(err);
    } finally {
      _iterator.f();
    }
    builder.storeUint(opcodes_1.NFT_BATCH_MINT_OPCODE, 32);
    builder.storeUint((_src$queryId = src.queryId) !== null && _src$queryId !== void 0 ? _src$queryId : 0, 64);
    builder.storeRef((0, core_1.beginCell)().storeDictDirect(dict));
  };
}
exports.storeNftBatchMintMessage = storeNftBatchMintMessage;
function loadNftBatchMintMessage(slice, loadParams) {
  if (slice.loadUint(32) !== opcodes_1.NFT_BATCH_MINT_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var queryId = slice.loadUintBig(64);
  var requests = slice.loadDictDirect(core_1.Dictionary.Keys.BigUint(64), createNftMintItemValue(undefined, loadParams));
  return {
    queryId: queryId,
    requests: requests.values()
  };
}
exports.loadNftBatchMintMessage = loadNftBatchMintMessage;
function storeNftBatchMintItem(request, storeParams) {
  return function (builder) {
    var _request$value;
    builder.storeCoins((_request$value = request.value) !== null && _request$value !== void 0 ? _request$value : (0, core_1.toNano)('0.03'));
    builder.storeRef((0, core_1.beginCell)().store(storeParams(request.params)).endCell());
  };
}
exports.storeNftBatchMintItem = storeNftBatchMintItem;
function loadNftBatchMintItem(slice, loadParams) {
  var itemIndex = slice.loadUintBig(64);
  var value = slice.loadCoins();
  var params = slice.loadRef();
  return {
    index: itemIndex,
    value: value,
    params: loadParams(params.beginParse())
  };
}
exports.loadNftBatchMintItem = loadNftBatchMintItem;
function createNftMintItemValue(storeParams, loadParams) {
  return {
    serialize: function serialize(src, builder) {
      if (!storeParams) {
        throw new Error('storeParams is not defined');
      }
      builder.store(storeNftBatchMintItem(src, storeParams));
    },
    parse: function parse(src) {
      if (!loadParams) {
        throw new Error('loadParams is not defined');
      }
      return loadNftBatchMintItem(src, loadParams);
    }
  };
}
exports.createNftMintItemValue = createNftMintItemValue;
