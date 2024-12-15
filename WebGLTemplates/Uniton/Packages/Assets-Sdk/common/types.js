"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseExcessReturnOptions = exports.parseNotifyOptions = void 0;
var core_1 = require("@ton/core");
function parseNotifyOptions(options) {
  if (options === false) {
    return null;
  }
  if (_typeof(options) === 'object') {
    var _options$amount, _options$payload;
    return {
      amount: (_options$amount = options.amount) !== null && _options$amount !== void 0 ? _options$amount : (0, core_1.toNano)('0.01'),
      payload: (_options$payload = options.payload) !== null && _options$payload !== void 0 ? _options$payload : null
    };
  }
  return {
    amount: (0, core_1.toNano)('0.01'),
    payload: null
  };
}
exports.parseNotifyOptions = parseNotifyOptions;
function parseExcessReturnOptions(options, sender) {
  if (options === false) {
    return null;
  }
  if (_typeof(options) === 'object') {
    var _options$address;
    return {
      address: (_options$address = options.address) !== null && _options$address !== void 0 ? _options$address : sender.address
    };
  }
  return {
    address: sender.address
  };
}
exports.parseExcessReturnOptions = parseExcessReturnOptions;