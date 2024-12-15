"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function _regeneratorRuntime() { "use strict"; /*! regenerator-runtime -- Copyright (c) 2014-present, Facebook, Inc. -- license (MIT): https://github.com/facebook/regenerator/blob/main/LICENSE */ _regeneratorRuntime = function _regeneratorRuntime() { return e; }; var t, e = {}, r = Object.prototype, n = r.hasOwnProperty, o = Object.defineProperty || function (t, e, r) { t[e] = r.value; }, i = "function" == typeof Symbol ? Symbol : {}, a = i.iterator || "@@iterator", c = i.asyncIterator || "@@asyncIterator", u = i.toStringTag || "@@toStringTag"; function define(t, e, r) { return Object.defineProperty(t, e, { value: r, enumerable: !0, configurable: !0, writable: !0 }), t[e]; } try { define({}, ""); } catch (t) { define = function define(t, e, r) { return t[e] = r; }; } function wrap(t, e, r, n) { var i = e && e.prototype instanceof Generator ? e : Generator, a = Object.create(i.prototype), c = new Context(n || []); return o(a, "_invoke", { value: makeInvokeMethod(t, r, c) }), a; } function tryCatch(t, e, r) { try { return { type: "normal", arg: t.call(e, r) }; } catch (t) { return { type: "throw", arg: t }; } } e.wrap = wrap; var h = "suspendedStart", l = "suspendedYield", f = "executing", s = "completed", y = {}; function Generator() {} function GeneratorFunction() {} function GeneratorFunctionPrototype() {} var p = {}; define(p, a, function () { return this; }); var d = Object.getPrototypeOf, v = d && d(d(values([]))); v && v !== r && n.call(v, a) && (p = v); var g = GeneratorFunctionPrototype.prototype = Generator.prototype = Object.create(p); function defineIteratorMethods(t) { ["next", "throw", "return"].forEach(function (e) { define(t, e, function (t) { return this._invoke(e, t); }); }); } function AsyncIterator(t, e) { function invoke(r, o, i, a) { var c = tryCatch(t[r], t, o); if ("throw" !== c.type) { var u = c.arg, h = u.value; return h && "object" == _typeof(h) && n.call(h, "__await") ? e.resolve(h.__await).then(function (t) { invoke("next", t, i, a); }, function (t) { invoke("throw", t, i, a); }) : e.resolve(h).then(function (t) { u.value = t, i(u); }, function (t) { return invoke("throw", t, i, a); }); } a(c.arg); } var r; o(this, "_invoke", { value: function value(t, n) { function callInvokeWithMethodAndArg() { return new e(function (e, r) { invoke(t, n, e, r); }); } return r = r ? r.then(callInvokeWithMethodAndArg, callInvokeWithMethodAndArg) : callInvokeWithMethodAndArg(); } }); } function makeInvokeMethod(e, r, n) { var o = h; return function (i, a) { if (o === f) throw Error("Generator is already running"); if (o === s) { if ("throw" === i) throw a; return { value: t, done: !0 }; } for (n.method = i, n.arg = a;;) { var c = n.delegate; if (c) { var u = maybeInvokeDelegate(c, n); if (u) { if (u === y) continue; return u; } } if ("next" === n.method) n.sent = n._sent = n.arg;else if ("throw" === n.method) { if (o === h) throw o = s, n.arg; n.dispatchException(n.arg); } else "return" === n.method && n.abrupt("return", n.arg); o = f; var p = tryCatch(e, r, n); if ("normal" === p.type) { if (o = n.done ? s : l, p.arg === y) continue; return { value: p.arg, done: n.done }; } "throw" === p.type && (o = s, n.method = "throw", n.arg = p.arg); } }; } function maybeInvokeDelegate(e, r) { var n = r.method, o = e.iterator[n]; if (o === t) return r.delegate = null, "throw" === n && e.iterator["return"] && (r.method = "return", r.arg = t, maybeInvokeDelegate(e, r), "throw" === r.method) || "return" !== n && (r.method = "throw", r.arg = new TypeError("The iterator does not provide a '" + n + "' method")), y; var i = tryCatch(o, e.iterator, r.arg); if ("throw" === i.type) return r.method = "throw", r.arg = i.arg, r.delegate = null, y; var a = i.arg; return a ? a.done ? (r[e.resultName] = a.value, r.next = e.nextLoc, "return" !== r.method && (r.method = "next", r.arg = t), r.delegate = null, y) : a : (r.method = "throw", r.arg = new TypeError("iterator result is not an object"), r.delegate = null, y); } function pushTryEntry(t) { var e = { tryLoc: t[0] }; 1 in t && (e.catchLoc = t[1]), 2 in t && (e.finallyLoc = t[2], e.afterLoc = t[3]), this.tryEntries.push(e); } function resetTryEntry(t) { var e = t.completion || {}; e.type = "normal", delete e.arg, t.completion = e; } function Context(t) { this.tryEntries = [{ tryLoc: "root" }], t.forEach(pushTryEntry, this), this.reset(!0); } function values(e) { if (e || "" === e) { var r = e[a]; if (r) return r.call(e); if ("function" == typeof e.next) return e; if (!isNaN(e.length)) { var o = -1, i = function next() { for (; ++o < e.length;) if (n.call(e, o)) return next.value = e[o], next.done = !1, next; return next.value = t, next.done = !0, next; }; return i.next = i; } } throw new TypeError(_typeof(e) + " is not iterable"); } return GeneratorFunction.prototype = GeneratorFunctionPrototype, o(g, "constructor", { value: GeneratorFunctionPrototype, configurable: !0 }), o(GeneratorFunctionPrototype, "constructor", { value: GeneratorFunction, configurable: !0 }), GeneratorFunction.displayName = define(GeneratorFunctionPrototype, u, "GeneratorFunction"), e.isGeneratorFunction = function (t) { var e = "function" == typeof t && t.constructor; return !!e && (e === GeneratorFunction || "GeneratorFunction" === (e.displayName || e.name)); }, e.mark = function (t) { return Object.setPrototypeOf ? Object.setPrototypeOf(t, GeneratorFunctionPrototype) : (t.__proto__ = GeneratorFunctionPrototype, define(t, u, "GeneratorFunction")), t.prototype = Object.create(g), t; }, e.awrap = function (t) { return { __await: t }; }, defineIteratorMethods(AsyncIterator.prototype), define(AsyncIterator.prototype, c, function () { return this; }), e.AsyncIterator = AsyncIterator, e.async = function (t, r, n, o, i) { void 0 === i && (i = Promise); var a = new AsyncIterator(wrap(t, r, n, o), i); return e.isGeneratorFunction(r) ? a : a.next().then(function (t) { return t.done ? t.value : a.next(); }); }, defineIteratorMethods(g), define(g, u, "Generator"), define(g, a, function () { return this; }), define(g, "toString", function () { return "[object Generator]"; }), e.keys = function (t) { var e = Object(t), r = []; for (var n in e) r.push(n); return r.reverse(), function next() { for (; r.length;) { var t = r.pop(); if (t in e) return next.value = t, next.done = !1, next; } return next.done = !0, next; }; }, e.values = values, Context.prototype = { constructor: Context, reset: function reset(e) { if (this.prev = 0, this.next = 0, this.sent = this._sent = t, this.done = !1, this.delegate = null, this.method = "next", this.arg = t, this.tryEntries.forEach(resetTryEntry), !e) for (var r in this) "t" === r.charAt(0) && n.call(this, r) && !isNaN(+r.slice(1)) && (this[r] = t); }, stop: function stop() { this.done = !0; var t = this.tryEntries[0].completion; if ("throw" === t.type) throw t.arg; return this.rval; }, dispatchException: function dispatchException(e) { if (this.done) throw e; var r = this; function handle(n, o) { return a.type = "throw", a.arg = e, r.next = n, o && (r.method = "next", r.arg = t), !!o; } for (var o = this.tryEntries.length - 1; o >= 0; --o) { var i = this.tryEntries[o], a = i.completion; if ("root" === i.tryLoc) return handle("end"); if (i.tryLoc <= this.prev) { var c = n.call(i, "catchLoc"), u = n.call(i, "finallyLoc"); if (c && u) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } else if (c) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); } else { if (!u) throw Error("try statement without catch or finally"); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } } } }, abrupt: function abrupt(t, e) { for (var r = this.tryEntries.length - 1; r >= 0; --r) { var o = this.tryEntries[r]; if (o.tryLoc <= this.prev && n.call(o, "finallyLoc") && this.prev < o.finallyLoc) { var i = o; break; } } i && ("break" === t || "continue" === t) && i.tryLoc <= e && e <= i.finallyLoc && (i = null); var a = i ? i.completion : {}; return a.type = t, a.arg = e, i ? (this.method = "next", this.next = i.finallyLoc, y) : this.complete(a); }, complete: function complete(t, e) { if ("throw" === t.type) throw t.arg; return "break" === t.type || "continue" === t.type ? this.next = t.arg : "return" === t.type ? (this.rval = this.arg = t.arg, this.method = "return", this.next = "end") : "normal" === t.type && e && (this.next = e), y; }, finish: function finish(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.finallyLoc === t) return this.complete(r.completion, r.afterLoc), resetTryEntry(r), y; } }, "catch": function _catch(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.tryLoc === t) { var n = r.completion; if ("throw" === n.type) { var o = n.arg; resetTryEntry(r); } return o; } } throw Error("illegal catch attempt"); }, delegateYield: function delegateYield(e, r, n) { return this.delegate = { iterator: values(e), resultName: r, nextLoc: n }, "next" === this.method && (this.arg = t), y; } }, e; }
function ownKeys(e, r) { var t = Object.keys(e); if (Object.getOwnPropertySymbols) { var o = Object.getOwnPropertySymbols(e); r && (o = o.filter(function (r) { return Object.getOwnPropertyDescriptor(e, r).enumerable; })), t.push.apply(t, o); } return t; }
function _objectSpread(e) { for (var r = 1; r < arguments.length; r++) { var t = null != arguments[r] ? arguments[r] : {}; r % 2 ? ownKeys(Object(t), !0).forEach(function (r) { _defineProperty(e, r, t[r]); }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys(Object(t)).forEach(function (r) { Object.defineProperty(e, r, Object.getOwnPropertyDescriptor(t, r)); }); } return e; }
function _defineProperty(e, r, t) { return (r = _toPropertyKey(r)) in e ? Object.defineProperty(e, r, { value: t, enumerable: !0, configurable: !0, writable: !0 }) : e[r] = t, e; }
function asyncGeneratorStep(n, t, e, r, o, a, c) { try { var i = n[a](c), u = i.value; } catch (n) { return void e(n); } i.done ? t(u) : Promise.resolve(u).then(r, o); }
function _asyncToGenerator(n) { return function () { var t = this, e = arguments; return new Promise(function (r, o) { var a = n.apply(t, e); function _next(n) { asyncGeneratorStep(a, r, o, _next, _throw, "next", n); } function _throw(n) { asyncGeneratorStep(a, r, o, _next, _throw, "throw", n); } _next(void 0); }); }; }
function _classCallCheck(a, n) { if (!(a instanceof n)) throw new TypeError("Cannot call a class as a function"); }
function _defineProperties(e, r) { for (var t = 0; t < r.length; t++) { var o = r[t]; o.enumerable = o.enumerable || !1, o.configurable = !0, "value" in o && (o.writable = !0), Object.defineProperty(e, _toPropertyKey(o.key), o); } }
function _createClass(e, r, t) { return r && _defineProperties(e.prototype, r), t && _defineProperties(e, t), Object.defineProperty(e, "prototype", { writable: !1 }), e; }
function _toPropertyKey(t) { var i = _toPrimitive(t, "string"); return "symbol" == _typeof(i) ? i : i + ""; }
function _toPrimitive(t, r) { if ("object" != _typeof(t) || !t) return t; var e = t[Symbol.toPrimitive]; if (void 0 !== e) { var i = e.call(t, r || "default"); if ("object" != _typeof(i)) return i; throw new TypeError("@@toPrimitive must return a primitive value."); } return ("string" === r ? String : Number)(t); }
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.AssetsSDK = void 0;
var core_1 = require("@ton/core");
var pinata_1 = require("./storage/pinata");
var s3_1 = require("./storage/s3");
var content_1 = require("./jetton/content");
var content_2 = require("./nft/content");
var utils_1 = require("./utils");
var JettonWallet_1 = require("./jetton/JettonWallet");
var JettonMinter_1 = require("./jetton/JettonMinter");
var NftCollection_1 = require("./nft/NftCollection");
var NftItem_1 = require("./nft/NftItem");
var SbtCollection_1 = require("./nft/SbtCollection");
var content_3 = require("./content");
var NftSale_1 = require("./nft/NftSale");
var noop_1 = require("./storage/noop");
var common_1 = require("./cli/common");
var WORKCHAIN = 0;
var AssetsSDK = /*#__PURE__*/function () {
  function AssetsSDK(storage, api, sender, contentResolver) {
    _classCallCheck(this, AssetsSDK);
    this.storage = storage;
    this.api = api;
    this.sender = sender;
    this.contentResolver = contentResolver;
  }
  return _createClass(AssetsSDK, [{
    key: "deployJetton",
    value: function () {
      var _deployJetton = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee(content, options) {
        var _options$adminAddress, _this$sender, _options$onchainConte;
        var adminAddress, jettonMinterContract, jetton, premintAmount;
        return _regeneratorRuntime().wrap(function _callee$(_context) {
          while (1) switch (_context.prev = _context.next) {
            case 0:
              if (this.sender) {
                _context.next = 2;
                break;
              }
              throw new Error('Sender must be defined');
            case 2:
              adminAddress = (_options$adminAddress = options === null || options === void 0 ? void 0 : options.adminAddress) !== null && _options$adminAddress !== void 0 ? _options$adminAddress : (_this$sender = this.sender) === null || _this$sender === void 0 ? void 0 : _this$sender.address;
              if (!(adminAddress === undefined)) {
                _context.next = 5;
                break;
              }
              throw new Error('Admin address must be defined in options or be available in Sender');
            case 5:
              _context.t0 = JettonMinter_1.JettonMinter;
              _context.t1 = adminAddress;
              _context.next = 9;
              return this.contentToCell((0, content_1.jettonContentToInternal)(content), (_options$onchainConte = options === null || options === void 0 ? void 0 : options.onchainContent) !== null && _options$onchainConte !== void 0 ? _options$onchainConte : false);
            case 9:
              _context.t2 = _context.sent;
              _context.t3 = {
                admin: _context.t1,
                content: _context.t2
              };
              _context.t4 = JettonMinter_1.JettonMinter.code;
              _context.t5 = WORKCHAIN;
              _context.t6 = this.contentResolver;
              jettonMinterContract = _context.t0.createFromConfig.call(_context.t0, _context.t3, _context.t4, _context.t5, _context.t6);
              jetton = this.api.open(jettonMinterContract);
              premintAmount = options === null || options === void 0 ? void 0 : options.premintAmount;
              if (!(typeof premintAmount === 'bigint' && premintAmount > 0n)) {
                _context.next = 22;
                break;
              }
              _context.next = 20;
              return jetton.sendMint(this.sender, adminAddress, premintAmount, _objectSpread(_objectSpread({}, options === null || options === void 0 ? void 0 : options.premintOptions), {}, {
                value: options === null || options === void 0 ? void 0 : options.value,
                queryId: options === null || options === void 0 ? void 0 : options.queryId
              }));
            case 20:
              _context.next = 24;
              break;
            case 22:
              _context.next = 24;
              return jetton.sendDeploy(this.sender, options === null || options === void 0 ? void 0 : options.value);
            case 24:
              return _context.abrupt("return", jetton);
            case 25:
            case "end":
              return _context.stop();
          }
        }, _callee, this);
      }));
      function deployJetton(_x, _x2) {
        return _deployJetton.apply(this, arguments);
      }
      return deployJetton;
    }()
  }, {
    key: "openJetton",
    value: function openJetton(address) {
      return this.api.open(JettonMinter_1.JettonMinter.createFromAddress(address, this.contentResolver));
    }
  }, {
    key: "deployNftCollection",
    value: function () {
      var _deployNftCollection = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(content, options) {
        var _options$adminAddress2, _this$sender2, _options$onchainConte2, _options$premintItems;
        var adminAddress, collection;
        return _regeneratorRuntime().wrap(function _callee2$(_context2) {
          while (1) switch (_context2.prev = _context2.next) {
            case 0:
              if (this.sender) {
                _context2.next = 2;
                break;
              }
              throw new Error('Sender must be defined');
            case 2:
              adminAddress = (_options$adminAddress2 = options === null || options === void 0 ? void 0 : options.adminAddress) !== null && _options$adminAddress2 !== void 0 ? _options$adminAddress2 : (_this$sender2 = this.sender) === null || _this$sender2 === void 0 ? void 0 : _this$sender2.address;
              if (!(adminAddress === undefined)) {
                _context2.next = 5;
                break;
              }
              throw new Error('Admin address must be defined in options or be available in Sender');
            case 5:
              _context2.t0 = this.api;
              _context2.t1 = NftCollection_1.NftCollection;
              _context2.t2 = adminAddress;
              _context2.t3 = (0, core_1.beginCell)();
              _context2.next = 11;
              return this.contentToCell((0, content_2.nftContentToInternal)(content.collectionContent), (_options$onchainConte2 = options === null || options === void 0 ? void 0 : options.onchainContent) !== null && _options$onchainConte2 !== void 0 ? _options$onchainConte2 : false);
            case 11:
              _context2.t4 = _context2.sent;
              _context2.t5 = _context2.t3.storeRef.call(_context2.t3, _context2.t4).storeRef((0, core_1.beginCell)().storeStringTail(content.commonContent)).endCell();
              _context2.t6 = options === null || options === void 0 ? void 0 : options.royaltyParams;
              _context2.t7 = {
                admin: _context2.t2,
                content: _context2.t5,
                royalty: _context2.t6
              };
              _context2.t8 = NftCollection_1.NftCollection.code;
              _context2.t9 = WORKCHAIN;
              _context2.t10 = this.contentResolver;
              _context2.t11 = _context2.t1.createFromConfig.call(_context2.t1, _context2.t7, _context2.t8, _context2.t9, _context2.t10);
              collection = _context2.t0.open.call(_context2.t0, _context2.t11);
              if (!(typeof (options === null || options === void 0 || (_options$premintItems = options.premintItems) === null || _options$premintItems === void 0 ? void 0 : _options$premintItems.length) === 'number' && (options === null || options === void 0 ? void 0 : options.premintItems.length) > 0)) {
                _context2.next = 25;
                break;
              }
              _context2.next = 23;
              return collection.sendBatchMint(this.sender, options === null || options === void 0 ? void 0 : options.premintItems, {
                value: options === null || options === void 0 ? void 0 : options.value,
                queryId: options === null || options === void 0 ? void 0 : options.queryId
              });
            case 23:
              _context2.next = 27;
              break;
            case 25:
              _context2.next = 27;
              return collection.sendDeploy(this.sender, options === null || options === void 0 ? void 0 : options.value);
            case 27:
              return _context2.abrupt("return", collection);
            case 28:
            case "end":
              return _context2.stop();
          }
        }, _callee2, this);
      }));
      function deployNftCollection(_x3, _x4) {
        return _deployNftCollection.apply(this, arguments);
      }
      return deployNftCollection;
    }()
  }, {
    key: "openNftCollection",
    value: function openNftCollection(address) {
      return this.api.open(NftCollection_1.NftCollection.createFromAddress(address, this.contentResolver));
    }
  }, {
    key: "deploySbtCollection",
    value: function () {
      var _deploySbtCollection = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee3(content, options) {
        var _options$adminAddress3, _this$sender3, _content$onchainConte, _options$premintItems2;
        var adminAddress, collection;
        return _regeneratorRuntime().wrap(function _callee3$(_context3) {
          while (1) switch (_context3.prev = _context3.next) {
            case 0:
              if (this.sender) {
                _context3.next = 2;
                break;
              }
              throw new Error('Sender must be defined');
            case 2:
              adminAddress = (_options$adminAddress3 = options === null || options === void 0 ? void 0 : options.adminAddress) !== null && _options$adminAddress3 !== void 0 ? _options$adminAddress3 : (_this$sender3 = this.sender) === null || _this$sender3 === void 0 ? void 0 : _this$sender3.address;
              if (!(adminAddress === undefined)) {
                _context3.next = 5;
                break;
              }
              throw new Error('Admin address must be defined in options or be available in Sender');
            case 5:
              _context3.t0 = this.api;
              _context3.t1 = SbtCollection_1.SbtCollection;
              _context3.t2 = adminAddress;
              _context3.t3 = (0, core_1.beginCell)();
              _context3.next = 11;
              return this.contentToCell((0, content_2.nftContentToInternal)(content.collectionContent), (_content$onchainConte = content === null || content === void 0 ? void 0 : content.onchainContent) !== null && _content$onchainConte !== void 0 ? _content$onchainConte : false);
            case 11:
              _context3.t4 = _context3.sent;
              _context3.t5 = _context3.t3.storeRef.call(_context3.t3, _context3.t4).storeRef((0, core_1.beginCell)().storeStringTail(content.commonContent)).endCell();
              _context3.t6 = {
                admin: _context3.t2,
                content: _context3.t5
              };
              _context3.t7 = SbtCollection_1.SbtCollection.code;
              _context3.t8 = WORKCHAIN;
              _context3.t9 = this.contentResolver;
              _context3.t10 = _context3.t1.createFromConfig.call(_context3.t1, _context3.t6, _context3.t7, _context3.t8, _context3.t9);
              collection = _context3.t0.open.call(_context3.t0, _context3.t10);
              if (!(typeof (options === null || options === void 0 || (_options$premintItems2 = options.premintItems) === null || _options$premintItems2 === void 0 ? void 0 : _options$premintItems2.length) === 'number' && (options === null || options === void 0 ? void 0 : options.premintItems.length) > 0)) {
                _context3.next = 24;
                break;
              }
              _context3.next = 22;
              return collection.sendBatchMint(this.sender, options === null || options === void 0 ? void 0 : options.premintItems, {
                value: options === null || options === void 0 ? void 0 : options.value,
                queryId: options === null || options === void 0 ? void 0 : options.queryId
              });
            case 22:
              _context3.next = 26;
              break;
            case 24:
              _context3.next = 26;
              return collection.sendDeploy(this.sender, options === null || options === void 0 ? void 0 : options.value);
            case 26:
              return _context3.abrupt("return", collection);
            case 27:
            case "end":
              return _context3.stop();
          }
        }, _callee3, this);
      }));
      function deploySbtCollection(_x5, _x6) {
        return _deploySbtCollection.apply(this, arguments);
      }
      return deploySbtCollection;
    }()
  }, {
    key: "openSbtCollection",
    value: function openSbtCollection(address) {
      return this.api.open(SbtCollection_1.SbtCollection.createFromAddress(address, this.contentResolver));
    }
  }, {
    key: "openJettonWallet",
    value: function openJettonWallet(address) {
      return this.api.open(new JettonWallet_1.JettonWallet(address));
    }
  }, {
    key: "openNftItem",
    value: function openNftItem(address) {
      return this.api.open(new NftItem_1.NftItem(address, undefined, this.contentResolver));
    }
  }, {
    key: "deployNftSale",
    value: function () {
      var _deployNftSale = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee4(params) {
        var _params$marketplace, _this$sender4, _params$createdAt, _params$marketplace2, _params$marketplaceFe, _params$marketplaceFe2, _params$royaltyTo, _params$royalty, _params$canDeployByEx;
        var marketplaceAddress, sale;
        return _regeneratorRuntime().wrap(function _callee4$(_context4) {
          while (1) switch (_context4.prev = _context4.next) {
            case 0:
              if (this.sender) {
                _context4.next = 2;
                break;
              }
              throw new Error('Sender must be defined');
            case 2:
              marketplaceAddress = (_params$marketplace = params.marketplace) !== null && _params$marketplace !== void 0 ? _params$marketplace : (_this$sender4 = this.sender) === null || _this$sender4 === void 0 ? void 0 : _this$sender4.address;
              if (!(marketplaceAddress === undefined)) {
                _context4.next = 5;
                break;
              }
              throw new Error('Marketplace address must be defined in options or be available in Sender');
            case 5:
              sale = this.api.open(NftSale_1.NftSale.createFromConfig({
                createdAt: (_params$createdAt = params.createdAt) !== null && _params$createdAt !== void 0 ? _params$createdAt : Math.floor(Date.now() / 1000),
                marketplace: (_params$marketplace2 = params.marketplace) !== null && _params$marketplace2 !== void 0 ? _params$marketplace2 : null,
                nft: params.nft,
                fullPrice: params.fullPrice,
                marketplaceFeeTo: (_params$marketplaceFe = params.marketplaceFeeTo) !== null && _params$marketplaceFe !== void 0 ? _params$marketplaceFe : null,
                marketplaceFee: (_params$marketplaceFe2 = params.marketplaceFee) !== null && _params$marketplaceFe2 !== void 0 ? _params$marketplaceFe2 : 0n,
                royaltyTo: (_params$royaltyTo = params.royaltyTo) !== null && _params$royaltyTo !== void 0 ? _params$royaltyTo : null,
                royalty: (_params$royalty = params.royalty) !== null && _params$royalty !== void 0 ? _params$royalty : 0n,
                canDeployByExternal: (_params$canDeployByEx = params.canDeployByExternal) !== null && _params$canDeployByEx !== void 0 ? _params$canDeployByEx : true
              }));
              _context4.next = 8;
              return sale.sendTopup(this.sender, {
                value: params.value,
                queryId: params.queryId
              });
            case 8:
              return _context4.abrupt("return", sale);
            case 9:
            case "end":
              return _context4.stop();
          }
        }, _callee4, this);
      }));
      function deployNftSale(_x7) {
        return _deployNftSale.apply(this, arguments);
      }
      return deployNftSale;
    }()
  }, {
    key: "openNftSale",
    value: function openNftSale(address) {
      return this.api.open(NftSale_1.NftSale.createFromAddress(address));
    }
  }, {
    key: "internalOffchainContentToCell",
    value: function () {
      var _internalOffchainContentToCell = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee5(internal) {
        var _this = this;
        var contents, contentUrl;
        return _regeneratorRuntime().wrap(function _callee5$(_context5) {
          while (1) switch (_context5.prev = _context5.next) {
            case 0:
              contents = Buffer.from(JSON.stringify(internal), 'utf-8');
              _context5.next = 3;
              return (0, common_1.retry)(function () {
                return _this.storage.uploadFile(contents);
              }, {
                name: 'upload content'
              });
            case 3:
              contentUrl = _context5.sent;
              return _context5.abrupt("return", (0, core_1.beginCell)().storeUint(0x01, 8).storeStringTail(contentUrl).endCell());
            case 5:
            case "end":
              return _context5.stop();
          }
        }, _callee5);
      }));
      function internalOffchainContentToCell(_x8) {
        return _internalOffchainContentToCell.apply(this, arguments);
      }
      return internalOffchainContentToCell;
    }()
  }, {
    key: "contentToCell",
    value: function () {
      var _contentToCell = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee6(internal, onchain) {
        return _regeneratorRuntime().wrap(function _callee6$(_context6) {
          while (1) switch (_context6.prev = _context6.next) {
            case 0:
              if (!onchain) {
                _context6.next = 4;
                break;
              }
              _context6.t0 = (0, utils_1.internalOnchainContentToCell)(internal);
              _context6.next = 7;
              break;
            case 4:
              _context6.next = 6;
              return this.internalOffchainContentToCell(internal);
            case 6:
              _context6.t0 = _context6.sent;
            case 7:
              return _context6.abrupt("return", _context6.t0);
            case 8:
            case "end":
              return _context6.stop();
          }
        }, _callee6, this);
      }));
      function contentToCell(_x9, _x10) {
        return _contentToCell.apply(this, arguments);
      }
      return contentToCell;
    }()
  }], [{
    key: "create",
    value: function create(params) {
      var _contentResolver;
      var api = params.api,
        storage = params.storage,
        sender = params.sender,
        contentResolver = params.contentResolver;
      if (!storage) {
        storage = new noop_1.NoopStorage();
      } else if ('pinataApiKey' in storage) {
        storage = pinata_1.PinataStorage.create(storage);
      } else if ('s3AccessKeyId' in storage) {
        storage = s3_1.S3Storage.create(storage);
      }
      (_contentResolver = contentResolver) !== null && _contentResolver !== void 0 ? _contentResolver : contentResolver = new content_3.DefaultContentResolver();
      return new AssetsSDK(storage, api, sender, contentResolver);
    }
  }]);
}();
exports.AssetsSDK = AssetsSDK;