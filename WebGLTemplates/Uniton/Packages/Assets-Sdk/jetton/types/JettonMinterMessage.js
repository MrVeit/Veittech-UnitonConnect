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
exports.loadJettonMinterMessage = void 0;
var JettonMintMessage_1 = require("./JettonMintMessage");
var JettonInternalTransferMessage_1 = require("./JettonInternalTransferMessage");
var JettonChangeAdminMessage_1 = require("./JettonChangeAdminMessage");
var JettonChangeContentMessage_1 = require("./JettonChangeContentMessage");
var opcodes_1 = require("../opcodes");
function loadJettonMinterMessage(slice) {
  try {
    var opcode = slice.preloadUint(32);
    switch (opcode) {
      case opcodes_1.JETTON_MINT_OPCODE:
        return _objectSpread({
          kind: 'mint'
        }, (0, JettonMintMessage_1.loadJettonMintMessage)(slice));
      case opcodes_1.JETTON_INTERNAL_TRANSFER_OPCODE:
        return _objectSpread({
          kind: 'internal_transfer'
        }, (0, JettonInternalTransferMessage_1.loadJettonInternalTransferMessage)(slice));
      case opcodes_1.JETTON_CHANGE_ADMIN_OPCODE:
        return _objectSpread({
          kind: 'change_admin'
        }, (0, JettonChangeAdminMessage_1.loadJettonChangeAdminMessage)(slice));
      case opcodes_1.JETTON_CHANGE_CONTENT_OPCODE:
        return _objectSpread({
          kind: 'change_content'
        }, (0, JettonChangeContentMessage_1.loadJettonChangeContentMessage)(slice));
    }
  } catch (e) {}
  return {
    kind: 'unknown'
  };
}
exports.loadJettonMinterMessage = loadJettonMinterMessage;