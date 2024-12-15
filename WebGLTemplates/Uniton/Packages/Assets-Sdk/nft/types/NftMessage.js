"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function ownKeys(e, r) { var t = Object.keys(e); if (Object.getOwnPropertySymbols) { var o = Object.getOwnPropertySymbols(e); r && (o = o.filter(function (r) { return Object.getOwnPropertyDescriptor(e, r).enumerable; })), t.push.apply(t, o); } return t; }
function _objectSpread(e) { for (var r = 1; r < arguments.length; r++) { var t = null != arguments[r] ? arguments[r] : {}; r % 2 ? ownKeys(Object(t), !0).forEach(function (r) { _defineProperty(e, r, t[r]); }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys(Object(t)).forEach(function (r) { Object.defineProperty(e, r, Object.getOwnPropertyDescriptor(t, r)); }); } return e; }
function _defineProperty(e, r, t) { return (r = _toPropertyKey(r)) in e ? Object.defineProperty(e, r, { value: t, enumerable: !0, configurable: !0, writable: !0 }) : e[r] = t, e; }
function _toPropertyKey(t) { var i = _toPrimitive(t, "string"); return "symbol" == _typeof(i) ? i : i + ""; }
function _toPrimitive(t, r) { if ("object" != _typeof(t) || !t) return t; var e = t[Symbol.toPrimitive]; if (void 0 !== e) { var i = e.call(t, r || "default"); if ("object" != _typeof(i)) return i; throw new TypeError("@@toPrimitive must return a primitive value."); } return ("string" === r ? String : Number)(t); }
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftMessage = void 0;
var NftDeployMessage_1 = require("./NftDeployMessage");
var NftTransferMessage_1 = require("./NftTransferMessage");
var NftGetStaticDataMessage_1 = require("./NftGetStaticDataMessage");
var NftReportStaticDataMessage_1 = require("./NftReportStaticDataMessage");
var NftOwnerAssignedMessage_1 = require("./NftOwnerAssignedMessage");
var opcodes_1 = require("../opcodes");
var NftExcessesMessage_1 = require("./NftExcessesMessage");
function loadNftMessage(slice) {
  try {
    var opcode = slice.preloadUint(32);
    switch (opcode) {
      case opcodes_1.NFT_TRANSFER_OPCODE:
        return _objectSpread({
          kind: 'nft_transfer'
        }, (0, NftTransferMessage_1.loadNftTransferMessage)(slice));
      case opcodes_1.NFT_OWNER_ASSIGNED_OPCODE:
        return _objectSpread({
          kind: 'owner_assigned'
        }, (0, NftOwnerAssignedMessage_1.loadNftOwnerAssignedMessage)(slice));
      case opcodes_1.NFT_REPORT_STATIC_DATA_OPCODE:
        return _objectSpread({
          kind: 'report_static_data'
        }, (0, NftReportStaticDataMessage_1.loadNftReportStaticDataMessage)(slice));
      case opcodes_1.NFT_GET_STATIC_DATA_OPCODE:
        return _objectSpread({
          kind: 'get_static_data'
        }, (0, NftGetStaticDataMessage_1.loadNftGetStaticDataMessage)(slice));
      case opcodes_1.NFT_EXCESSES_OPCODE:
        return _objectSpread({
          kind: 'excesses'
        }, (0, NftExcessesMessage_1.loadNftExcessesMessage)(slice));
    }
    if (slice.remainingBits === 256 + 11 && slice.remainingRefs === 1) {
      return _objectSpread({
        kind: 'nft_deploy'
      }, (0, NftDeployMessage_1.loadNftDeployMessage)(slice));
    }
  } catch (e) {}
  return {
    kind: 'unknown'
  };
}
exports.loadNftMessage = loadNftMessage;