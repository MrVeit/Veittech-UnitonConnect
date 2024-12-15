"use strict";

function ownKeys(e, r) { var t = Object.keys(e); if (Object.getOwnPropertySymbols) { var o = Object.getOwnPropertySymbols(e); r && (o = o.filter(function (r) { return Object.getOwnPropertyDescriptor(e, r).enumerable; })), t.push.apply(t, o); } return t; }
function _objectSpread(e) { for (var r = 1; r < arguments.length; r++) { var t = null != arguments[r] ? arguments[r] : {}; r % 2 ? ownKeys(Object(t), !0).forEach(function (r) { _defineProperty(e, r, t[r]); }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys(Object(t)).forEach(function (r) { Object.defineProperty(e, r, Object.getOwnPropertyDescriptor(t, r)); }); } return e; }
function _defineProperty(e, r, t) { return (r = _toPropertyKey(r)) in e ? Object.defineProperty(e, r, { value: t, enumerable: !0, configurable: !0, writable: !0 }) : e[r] = t, e; }
function _toPropertyKey(t) { var i = _toPrimitive(t, "string"); return "symbol" == _typeof(i) ? i : i + ""; }
function _toPrimitive(t, r) { if ("object" != _typeof(t) || !t) return t; var e = t[Symbol.toPrimitive]; if (void 0 !== e) { var i = e.call(t, r || "default"); if ("object" != _typeof(i)) return i; throw new TypeError("@@toPrimitive must return a primitive value."); } return ("string" === r ? String : Number)(t); }
function _regeneratorRuntime() { "use strict"; /*! regenerator-runtime -- Copyright (c) 2014-present, Facebook, Inc. -- license (MIT): https://github.com/facebook/regenerator/blob/main/LICENSE */ _regeneratorRuntime = function _regeneratorRuntime() { return e; }; var t, e = {}, r = Object.prototype, n = r.hasOwnProperty, o = Object.defineProperty || function (t, e, r) { t[e] = r.value; }, i = "function" == typeof Symbol ? Symbol : {}, a = i.iterator || "@@iterator", c = i.asyncIterator || "@@asyncIterator", u = i.toStringTag || "@@toStringTag"; function define(t, e, r) { return Object.defineProperty(t, e, { value: r, enumerable: !0, configurable: !0, writable: !0 }), t[e]; } try { define({}, ""); } catch (t) { define = function define(t, e, r) { return t[e] = r; }; } function wrap(t, e, r, n) { var i = e && e.prototype instanceof Generator ? e : Generator, a = Object.create(i.prototype), c = new Context(n || []); return o(a, "_invoke", { value: makeInvokeMethod(t, r, c) }), a; } function tryCatch(t, e, r) { try { return { type: "normal", arg: t.call(e, r) }; } catch (t) { return { type: "throw", arg: t }; } } e.wrap = wrap; var h = "suspendedStart", l = "suspendedYield", f = "executing", s = "completed", y = {}; function Generator() {} function GeneratorFunction() {} function GeneratorFunctionPrototype() {} var p = {}; define(p, a, function () { return this; }); var d = Object.getPrototypeOf, v = d && d(d(values([]))); v && v !== r && n.call(v, a) && (p = v); var g = GeneratorFunctionPrototype.prototype = Generator.prototype = Object.create(p); function defineIteratorMethods(t) { ["next", "throw", "return"].forEach(function (e) { define(t, e, function (t) { return this._invoke(e, t); }); }); } function AsyncIterator(t, e) { function invoke(r, o, i, a) { var c = tryCatch(t[r], t, o); if ("throw" !== c.type) { var u = c.arg, h = u.value; return h && "object" == _typeof(h) && n.call(h, "__await") ? e.resolve(h.__await).then(function (t) { invoke("next", t, i, a); }, function (t) { invoke("throw", t, i, a); }) : e.resolve(h).then(function (t) { u.value = t, i(u); }, function (t) { return invoke("throw", t, i, a); }); } a(c.arg); } var r; o(this, "_invoke", { value: function value(t, n) { function callInvokeWithMethodAndArg() { return new e(function (e, r) { invoke(t, n, e, r); }); } return r = r ? r.then(callInvokeWithMethodAndArg, callInvokeWithMethodAndArg) : callInvokeWithMethodAndArg(); } }); } function makeInvokeMethod(e, r, n) { var o = h; return function (i, a) { if (o === f) throw Error("Generator is already running"); if (o === s) { if ("throw" === i) throw a; return { value: t, done: !0 }; } for (n.method = i, n.arg = a;;) { var c = n.delegate; if (c) { var u = maybeInvokeDelegate(c, n); if (u) { if (u === y) continue; return u; } } if ("next" === n.method) n.sent = n._sent = n.arg;else if ("throw" === n.method) { if (o === h) throw o = s, n.arg; n.dispatchException(n.arg); } else "return" === n.method && n.abrupt("return", n.arg); o = f; var p = tryCatch(e, r, n); if ("normal" === p.type) { if (o = n.done ? s : l, p.arg === y) continue; return { value: p.arg, done: n.done }; } "throw" === p.type && (o = s, n.method = "throw", n.arg = p.arg); } }; } function maybeInvokeDelegate(e, r) { var n = r.method, o = e.iterator[n]; if (o === t) return r.delegate = null, "throw" === n && e.iterator["return"] && (r.method = "return", r.arg = t, maybeInvokeDelegate(e, r), "throw" === r.method) || "return" !== n && (r.method = "throw", r.arg = new TypeError("The iterator does not provide a '" + n + "' method")), y; var i = tryCatch(o, e.iterator, r.arg); if ("throw" === i.type) return r.method = "throw", r.arg = i.arg, r.delegate = null, y; var a = i.arg; return a ? a.done ? (r[e.resultName] = a.value, r.next = e.nextLoc, "return" !== r.method && (r.method = "next", r.arg = t), r.delegate = null, y) : a : (r.method = "throw", r.arg = new TypeError("iterator result is not an object"), r.delegate = null, y); } function pushTryEntry(t) { var e = { tryLoc: t[0] }; 1 in t && (e.catchLoc = t[1]), 2 in t && (e.finallyLoc = t[2], e.afterLoc = t[3]), this.tryEntries.push(e); } function resetTryEntry(t) { var e = t.completion || {}; e.type = "normal", delete e.arg, t.completion = e; } function Context(t) { this.tryEntries = [{ tryLoc: "root" }], t.forEach(pushTryEntry, this), this.reset(!0); } function values(e) { if (e || "" === e) { var r = e[a]; if (r) return r.call(e); if ("function" == typeof e.next) return e; if (!isNaN(e.length)) { var o = -1, i = function next() { for (; ++o < e.length;) if (n.call(e, o)) return next.value = e[o], next.done = !1, next; return next.value = t, next.done = !0, next; }; return i.next = i; } } throw new TypeError(_typeof(e) + " is not iterable"); } return GeneratorFunction.prototype = GeneratorFunctionPrototype, o(g, "constructor", { value: GeneratorFunctionPrototype, configurable: !0 }), o(GeneratorFunctionPrototype, "constructor", { value: GeneratorFunction, configurable: !0 }), GeneratorFunction.displayName = define(GeneratorFunctionPrototype, u, "GeneratorFunction"), e.isGeneratorFunction = function (t) { var e = "function" == typeof t && t.constructor; return !!e && (e === GeneratorFunction || "GeneratorFunction" === (e.displayName || e.name)); }, e.mark = function (t) { return Object.setPrototypeOf ? Object.setPrototypeOf(t, GeneratorFunctionPrototype) : (t.__proto__ = GeneratorFunctionPrototype, define(t, u, "GeneratorFunction")), t.prototype = Object.create(g), t; }, e.awrap = function (t) { return { __await: t }; }, defineIteratorMethods(AsyncIterator.prototype), define(AsyncIterator.prototype, c, function () { return this; }), e.AsyncIterator = AsyncIterator, e.async = function (t, r, n, o, i) { void 0 === i && (i = Promise); var a = new AsyncIterator(wrap(t, r, n, o), i); return e.isGeneratorFunction(r) ? a : a.next().then(function (t) { return t.done ? t.value : a.next(); }); }, defineIteratorMethods(g), define(g, u, "Generator"), define(g, a, function () { return this; }), define(g, "toString", function () { return "[object Generator]"; }), e.keys = function (t) { var e = Object(t), r = []; for (var n in e) r.push(n); return r.reverse(), function next() { for (; r.length;) { var t = r.pop(); if (t in e) return next.value = t, next.done = !1, next; } return next.done = !0, next; }; }, e.values = values, Context.prototype = { constructor: Context, reset: function reset(e) { if (this.prev = 0, this.next = 0, this.sent = this._sent = t, this.done = !1, this.delegate = null, this.method = "next", this.arg = t, this.tryEntries.forEach(resetTryEntry), !e) for (var r in this) "t" === r.charAt(0) && n.call(this, r) && !isNaN(+r.slice(1)) && (this[r] = t); }, stop: function stop() { this.done = !0; var t = this.tryEntries[0].completion; if ("throw" === t.type) throw t.arg; return this.rval; }, dispatchException: function dispatchException(e) { if (this.done) throw e; var r = this; function handle(n, o) { return a.type = "throw", a.arg = e, r.next = n, o && (r.method = "next", r.arg = t), !!o; } for (var o = this.tryEntries.length - 1; o >= 0; --o) { var i = this.tryEntries[o], a = i.completion; if ("root" === i.tryLoc) return handle("end"); if (i.tryLoc <= this.prev) { var c = n.call(i, "catchLoc"), u = n.call(i, "finallyLoc"); if (c && u) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } else if (c) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); } else { if (!u) throw Error("try statement without catch or finally"); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } } } }, abrupt: function abrupt(t, e) { for (var r = this.tryEntries.length - 1; r >= 0; --r) { var o = this.tryEntries[r]; if (o.tryLoc <= this.prev && n.call(o, "finallyLoc") && this.prev < o.finallyLoc) { var i = o; break; } } i && ("break" === t || "continue" === t) && i.tryLoc <= e && e <= i.finallyLoc && (i = null); var a = i ? i.completion : {}; return a.type = t, a.arg = e, i ? (this.method = "next", this.next = i.finallyLoc, y) : this.complete(a); }, complete: function complete(t, e) { if ("throw" === t.type) throw t.arg; return "break" === t.type || "continue" === t.type ? this.next = t.arg : "return" === t.type ? (this.rval = this.arg = t.arg, this.method = "return", this.next = "end") : "normal" === t.type && e && (this.next = e), y; }, finish: function finish(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.finallyLoc === t) return this.complete(r.completion, r.afterLoc), resetTryEntry(r), y; } }, "catch": function _catch(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.tryLoc === t) { var n = r.completion; if ("throw" === n.type) { var o = n.arg; resetTryEntry(r); } return o; } } throw Error("illegal catch attempt"); }, delegateYield: function delegateYield(e, r, n) { return this.delegate = { iterator: values(e), resultName: r, nextLoc: n }, "next" === this.method && (this.arg = t), y; } }, e; }
function _slicedToArray(r, e) { return _arrayWithHoles(r) || _iterableToArrayLimit(r, e) || _unsupportedIterableToArray(r, e) || _nonIterableRest(); }
function _nonIterableRest() { throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); }
function _unsupportedIterableToArray(r, a) { if (r) { if ("string" == typeof r) return _arrayLikeToArray(r, a); var t = {}.toString.call(r).slice(8, -1); return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray(r, a) : void 0; } }
function _arrayLikeToArray(r, a) { (null == a || a > r.length) && (a = r.length); for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e]; return n; }
function _iterableToArrayLimit(r, l) { var t = null == r ? null : "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"]; if (null != t) { var e, n, i, u, a = [], f = !0, o = !1; try { if (i = (t = t.call(r)).next, 0 === l) { if (Object(t) !== t) return; f = !1; } else for (; !(f = (e = i.call(t)).done) && (a.push(e.value), a.length !== l); f = !0); } catch (r) { o = !0, n = r; } finally { try { if (!f && null != t["return"] && (u = t["return"](), Object(u) !== u)) return; } finally { if (o) throw n; } } return a; } }
function _arrayWithHoles(r) { if (Array.isArray(r)) return r; }
function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function asyncGeneratorStep(n, t, e, r, o, a, c) { try { var i = n[a](c), u = i.value; } catch (n) { return void e(n); } i.done ? t(u) : Promise.resolve(u).then(r, o); }
function _asyncToGenerator(n) { return function () { var t = this, e = arguments; return new Promise(function (r, o) { var a = n.apply(t, e); function _next(n) { asyncGeneratorStep(a, r, o, _next, _throw, "next", n); } function _throw(n) { asyncGeneratorStep(a, r, o, _next, _throw, "throw", n); } _next(void 0); }); }; }
var __importDefault = void 0 && (void 0).__importDefault || function (mod) {
  return mod && mod.__esModule ? mod : {
    "default": mod
  };
};
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.retry = exports.formatAddressLink = exports.formatAddress = exports.printAddress = exports.printInfo = exports.createEnv = exports.createContentResolver = exports.createStorageEnv = void 0;
var core_1 = require("@ton/core");
var __1 = require("..");
var content_1 = require("../content");
var chalk_1 = __importDefault(require("chalk"));
var boxen_1 = __importDefault(require("boxen"));
function createStorageEnv() {
  if (process.env.STORAGE_TYPE === undefined) throw new Error('No STORAGE_TYPE in env!');
  if (process.env.STORAGE_TYPE === 'pinata') {
    if (process.env.PINATA_API_KEY === undefined) throw new Error('No PINATA_API_KEY in env!');
    if (process.env.PINATA_SECRET_KEY === undefined) throw new Error('No PINATA_SECRET_KEY in env!');
    return new __1.PinataStorage(process.env.PINATA_API_KEY, process.env.PINATA_SECRET_KEY);
  } else if (process.env.STORAGE_TYPE === 's3') {
    if (process.env.S3_ACCESS_KEY_ID === undefined) throw new Error('No S3_ACCESS_KEY_ID in env!');
    if (process.env.S3_SECRET_ACCESS_KEY === undefined) throw new Error('No S3_SECRET_ACCESS_KEY in env!');
    if (process.env.S3_BUCKET === undefined) throw new Error('No S3_BUCKET in env!');
    return new __1.S3Storage(process.env.S3_ACCESS_KEY_ID, process.env.S3_SECRET_ACCESS_KEY, process.env.S3_BUCKET);
  }
  throw new Error("Unknown storage type: ".concat(process.env.STORAGE_TYPE));
}
exports.createStorageEnv = createStorageEnv;
function createContentResolver() {
  if (process.env.IPFS_GATEWAY_TYPE === undefined) throw new Error('No IPFS_GATEWAY_TYPE in env!');
  if (process.env.IPFS_GATEWAY_TYPE === 'ipfs.io') {
    return new content_1.DefaultContentResolver(function (id) {
      return "https://ipfs.io/ipfs/".concat(id);
    });
  }
  if (process.env.IPFS_GATEWAY_TYPE === 'https') {
    if (process.env.IPFS_GATEWAY === undefined) throw new Error('No IPFS_GATEWAY in env!');
    var ipfsGateway = new URL(process.env.IPFS_GATEWAY);
    return new content_1.DefaultContentResolver(function (id) {
      ipfsGateway.pathname = '/ipfs/' + id;
      return ipfsGateway.toString();
    });
  }
  if (process.env.IPFS_GATEWAY_TYPE === 'pinata') {
    if (process.env.IPFS_GATEWAY === undefined) throw new Error('No IPFS_GATEWAY in env!');
    if (process.env.IPFS_GATEWAY_API_KEY === undefined) throw new Error('No IPFS_GATEWAY_API_KEY in env!');
    var _ipfsGateway = new URL(process.env.IPFS_GATEWAY);
    return new content_1.DefaultContentResolver(function (id) {
      _ipfsGateway.pathname = '/ipfs/' + id;
      _ipfsGateway.searchParams.set('pinataGatewayToken', process.env.IPFS_GATEWAY_API_KEY);
      return _ipfsGateway.toString();
    });
  }
  throw new Error("Unknown IPFS gateway type: ".concat(process.env.IPFS_GATEWAY_TYPE));
}
exports.createContentResolver = createContentResolver;
function createEnv() {
  return _createEnv.apply(this, arguments);
}
function _createEnv() {
  _createEnv = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee() {
    var contentResolver, storage, client, keyPair, publicKey, secretKey, walletContract, sender, sdk;
    return _regeneratorRuntime().wrap(function _callee$(_context) {
      while (1) switch (_context.prev = _context.next) {
        case 0:
          if (!(process.env.WALLET_TYPE === undefined)) {
            _context.next = 2;
            break;
          }
          throw new Error('No WALLET_TYPE in env!');
        case 2:
          if (!(process.env.WALLET_TYPE !== 'highload-v2')) {
            _context.next = 4;
            break;
          }
          throw new Error("Unknown wallet type: ".concat(process.env.WALLET_TYPE));
        case 4:
          if (!(process.env.MNEMONIC === undefined)) {
            _context.next = 6;
            break;
          }
          throw new Error('No MNEMONIC in env!');
        case 6:
          if (!(process.env.NETWORK === undefined)) {
            _context.next = 8;
            break;
          }
          throw new Error('No NETWORK in env!');
        case 8:
          if (!(process.env.NETWORK !== 'mainnet' && process.env.NETWORK !== 'testnet')) {
            _context.next = 10;
            break;
          }
          throw new Error("Unknown network: ".concat(process.env.NETWORK));
        case 10:
          contentResolver = createContentResolver();
          storage = createStorageEnv();
          _context.next = 14;
          return (0, __1.createApi)(process.env.NETWORK);
        case 14:
          client = _context.sent;
          _context.next = 17;
          return (0, __1.importKey)(process.env.MNEMONIC);
        case 17:
          keyPair = _context.sent;
          publicKey = keyPair.publicKey, secretKey = keyPair.secretKey;
          _context.next = 21;
          return (0, __1.createWallet)(process.env.WALLET_TYPE, publicKey);
        case 21:
          walletContract = _context.sent;
          sender = client.open(walletContract).sender(keyPair.secretKey);
          sdk = __1.AssetsSDK.create({
            storage: storage,
            api: client,
            sender: sender,
            contentResolver: contentResolver
          });
          return _context.abrupt("return", {
            sdk: sdk,
            network: process.env.NETWORK,
            storage: storage,
            sender: sender,
            client: client
          });
        case 25:
        case "end":
          return _context.stop();
      }
    }, _callee);
  }));
  return _createEnv.apply(this, arguments);
}
exports.createEnv = createEnv;
function printInfo(info, network) {
  var _rows$find;
  var keys = Object.keys(info);
  var rows = [];
  for (var _i = 0, _keys = keys; _i < _keys.length; _i++) {
    var key = _keys[_i];
    var value = info[key];
    if (typeof value === 'string') {
      value = chalk_1["default"].green(value);
    } else if (typeof value === 'bigint') {
      value = chalk_1["default"].yellow(value.toString());
    } else if (typeof value === 'number') {
      value = chalk_1["default"].cyan(value);
    } else if (typeof value === 'boolean') {
      value = chalk_1["default"].blue(value);
    } else if (value === null) {
      value = chalk_1["default"].red('null');
    } else if (value === undefined) {
      value = chalk_1["default"].red('undefined');
    } else if (core_1.Address.isAddress(value)) {
      value = "".concat(chalk_1["default"].magenta(formatAddress(value, network)), " ").concat(chalk_1["default"].blue(formatAddressLink(value, network)));
    } else if (value instanceof core_1.Cell) {
      value = chalk_1["default"].blue(value.toString('base64'));
    } else {
      throw new Error("Unknown type: ".concat(_typeof(value)));
    }
    rows.push([key, value]);
  }
  console.log((0, boxen_1["default"])(rows.filter(function (_ref) {
    var _ref2 = _slicedToArray(_ref, 1),
      key = _ref2[0];
    return key !== 'name';
  }).map(function (_ref3) {
    var _ref4 = _slicedToArray(_ref3, 2),
      key = _ref4[0],
      value = _ref4[1];
    return "".concat(chalk_1["default"].bold(key), ": ").concat(value);
  }).join('\n'), {
    padding: {
      top: 0,
      bottom: 0,
      left: 1,
      right: 1
    },
    borderStyle: 'round',
    borderColor: 'green',
    title: (_rows$find = rows.find(function (_ref5) {
      var _ref6 = _slicedToArray(_ref5, 2),
        key = _ref6[0],
        value = _ref6[1];
      return key === 'name';
    })) === null || _rows$find === void 0 ? void 0 : _rows$find[1]
  }));
}
exports.printInfo = printInfo;
function printAddress(address, network) {
  var name = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 'wallet';
  var formattedAddress = formatAddress(address, network);
  var formattedAddressLink = formatAddressLink(address, network);
  console.log("Your ".concat(name, " has the address ").concat(formattedAddress, "\nYou can view it at ").concat(formattedAddressLink));
}
exports.printAddress = printAddress;
function formatAddress(address, network) {
  if (!address) {
    return 'null';
  }
  return address.toString({
    testOnly: network === 'testnet',
    bounceable: true
  });
}
exports.formatAddress = formatAddress;
function formatAddressLink(address, network) {
  if (!address) {
    return 'null';
  }
  return "https://".concat(network === 'testnet' ? 'testnet.' : '', "tonviewer.com/").concat(formatAddress(address, network));
}
exports.formatAddressLink = formatAddressLink;
function retry(_x, _x2) {
  return _retry.apply(this, arguments);
}
function _retry() {
  _retry = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(fn, options) {
    var _retries$delay$option, retries, delay, name, _loop, _ret, i;
    return _regeneratorRuntime().wrap(function _callee2$(_context3) {
      while (1) switch (_context3.prev = _context3.next) {
        case 0:
          _retries$delay$option = _objectSpread({
            retries: 3,
            delay: 1000
          }, options), retries = _retries$delay$option.retries, delay = _retries$delay$option.delay, name = _retries$delay$option.name;
          _loop = /*#__PURE__*/_regeneratorRuntime().mark(function _loop(i) {
            return _regeneratorRuntime().wrap(function _loop$(_context2) {
              while (1) switch (_context2.prev = _context2.next) {
                case 0:
                  _context2.prev = 0;
                  _context2.next = 3;
                  return fn();
                case 3:
                  _context2.t0 = _context2.sent;
                  return _context2.abrupt("return", {
                    v: _context2.t0
                  });
                case 7:
                  _context2.prev = 7;
                  _context2.t1 = _context2["catch"](0);
                  console.log("Attempt ".concat(i + 1, " failed: ").concat(name ? name + ': ' : '').concat(_context2.t1));
                case 10:
                  _context2.next = 12;
                  return new Promise(function (resolve) {
                    return setTimeout(resolve, delay * Math.pow(2, i));
                  });
                case 12:
                case "end":
                  return _context2.stop();
              }
            }, _loop, null, [[0, 7]]);
          });
          i = 0;
        case 3:
          if (!(i < retries)) {
            _context3.next = 11;
            break;
          }
          return _context3.delegateYield(_loop(i), "t0", 5);
        case 5:
          _ret = _context3.t0;
          if (!_ret) {
            _context3.next = 8;
            break;
          }
          return _context3.abrupt("return", _ret.v);
        case 8:
          i++;
          _context3.next = 3;
          break;
        case 11:
          throw new Error('Exceeded number of retries');
        case 12:
        case "end":
          return _context3.stop();
      }
    }, _callee2);
  }));
  return _retry.apply(this, arguments);
}
exports.retry = retry;
