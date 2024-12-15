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
exports.loadTransferMessage = exports.loadEncryptedMessage = exports.storeEncryptedMessage = exports.loadTextMessage = exports.storeTextMessage = exports.loadSimpleTransferMessage = exports.storeSimpleTransferMessage = exports.ENCRYPTED_MESSAGE_OPCODE = exports.TEXT_OPCODE = void 0;
exports.TEXT_OPCODE = 0x00000000;
exports.ENCRYPTED_MESSAGE_OPCODE = 0x2167da4b;
function storeSimpleTransferMessage(value) {
  return function (builder) {};
}
exports.storeSimpleTransferMessage = storeSimpleTransferMessage;
function loadSimpleTransferMessage(slice) {
  return {};
}
exports.loadSimpleTransferMessage = loadSimpleTransferMessage;
function storeTextMessage(value) {
  return function (builder) {
    builder.storeUint(0, 32);
    builder.storeStringTail(value.text);
  };
}
exports.storeTextMessage = storeTextMessage;
function loadTextMessage(slice) {
  if (slice.loadUint(32) !== 0) {
    throw new Error('Wrong opcode');
  }
  return {
    text: slice.loadStringTail()
  };
}
exports.loadTextMessage = loadTextMessage;
function storeEncryptedMessage(value) {
  return function (builder) {
    builder.storeUint(exports.ENCRYPTED_MESSAGE_OPCODE, 32);
    builder.storeStringTail(value.data.toString('utf-8'));
  };
}
exports.storeEncryptedMessage = storeEncryptedMessage;
function loadEncryptedMessage(slice) {
  if (slice.loadUint(32) !== exports.ENCRYPTED_MESSAGE_OPCODE) {
    throw new Error('Wrong opcode');
  }
  var data = slice.loadStringTail();
  return {
    data: Buffer.from(data, 'utf-8')
  };
}
exports.loadEncryptedMessage = loadEncryptedMessage;
function loadTransferMessage(slice) {
  if (slice.remainingBits === 0) {
    return {
      kind: 'simple_transfer'
    };
  }
  try {
    var opcode = slice.preloadUint(32);
    switch (opcode) {
      case exports.TEXT_OPCODE:
        return _objectSpread({
          kind: 'text_message'
        }, loadTextMessage(slice));
      case exports.ENCRYPTED_MESSAGE_OPCODE:
        return _objectSpread({
          kind: 'encrypted_message'
        }, loadEncryptedMessage(slice));
    }
  } catch (e) {}
  return {
    kind: 'unknown'
  };
}
exports.loadTransferMessage = loadTransferMessage;