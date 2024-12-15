"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function ownKeys(e, r) { var t = Object.keys(e); if (Object.getOwnPropertySymbols) { var o = Object.getOwnPropertySymbols(e); r && (o = o.filter(function (r) { return Object.getOwnPropertyDescriptor(e, r).enumerable; })), t.push.apply(t, o); } return t; }
function _objectSpread(e) { for (var r = 1; r < arguments.length; r++) { var t = null != arguments[r] ? arguments[r] : {}; r % 2 ? ownKeys(Object(t), !0).forEach(function (r) { _defineProperty(e, r, t[r]); }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys(Object(t)).forEach(function (r) { Object.defineProperty(e, r, Object.getOwnPropertyDescriptor(t, r)); }); } return e; }
function _defineProperty(e, r, t) { return (r = _toPropertyKey(r)) in e ? Object.defineProperty(e, r, { value: t, enumerable: !0, configurable: !0, writable: !0 }) : e[r] = t, e; }
function _toPropertyKey(t) { var i = _toPrimitive(t, "string"); return "symbol" == _typeof(i) ? i : i + ""; }
function _toPrimitive(t, r) { if ("object" != _typeof(t) || !t) return t; var e = t[Symbol.toPrimitive]; if (void 0 !== e) { var i = e.call(t, r || "default"); if ("object" != _typeof(i)) return i; throw new TypeError("@@toPrimitive must return a primitive value."); } return ("string" === r ? String : Number)(t); }
var __importDefault = void 0 && (void 0).__importDefault || function (mod) {
  return mod && mod.__esModule ? mod : {
    "default": mod
  };
};
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.parseNftContent = exports.nftContentToInternal = void 0;
var core_1 = require("@ton/core");
var zod_1 = __importDefault(require("zod"));
var content_1 = require("../content");
function nftContentToInternal(content) {
  var _content$imageData;
  return {
    uri: content.uri,
    name: content.name,
    description: content.description,
    image: content.image,
    image_data: (_content$imageData = content.imageData) === null || _content$imageData === void 0 ? void 0 : _content$imageData.toString('base64')
  };
}
exports.nftContentToInternal = nftContentToInternal;
function parseNftContent(dc) {
  var _dc$offchainFields, _dc$onchainFields;
  var decoded = (0, content_1.decodeSimpleFields)(dc, {
    name: {
      onchain: content_1.bufferToStr,
      offchain: function offchain(v) {
        return zod_1["default"].string().parse(v);
      }
    },
    description: {
      onchain: content_1.bufferToStr,
      offchain: function offchain(v) {
        return zod_1["default"].string().parse(v);
      }
    }
  });
  decoded.image = (0, content_1.decodeImage)(dc);
  var out = _objectSpread(_objectSpread({}, decoded), {}, {
    type: dc.type,
    unknownOffchainFields: (_dc$offchainFields = dc.offchainFields) !== null && _dc$offchainFields !== void 0 ? _dc$offchainFields : {},
    unknownOnchainFields: (_dc$onchainFields = dc.onchainFields) !== null && _dc$onchainFields !== void 0 ? _dc$onchainFields : core_1.Dictionary.empty(),
    offchainUrl: dc.offchainUrl
  });
  return out;
}
exports.parseNftContent = parseNftContent;
