"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function _regeneratorRuntime() { "use strict"; /*! regenerator-runtime -- Copyright (c) 2014-present, Facebook, Inc. -- license (MIT): https://github.com/facebook/regenerator/blob/main/LICENSE */ _regeneratorRuntime = function _regeneratorRuntime() { return e; }; var t, e = {}, r = Object.prototype, n = r.hasOwnProperty, o = Object.defineProperty || function (t, e, r) { t[e] = r.value; }, i = "function" == typeof Symbol ? Symbol : {}, a = i.iterator || "@@iterator", c = i.asyncIterator || "@@asyncIterator", u = i.toStringTag || "@@toStringTag"; function define(t, e, r) { return Object.defineProperty(t, e, { value: r, enumerable: !0, configurable: !0, writable: !0 }), t[e]; } try { define({}, ""); } catch (t) { define = function define(t, e, r) { return t[e] = r; }; } function wrap(t, e, r, n) { var i = e && e.prototype instanceof Generator ? e : Generator, a = Object.create(i.prototype), c = new Context(n || []); return o(a, "_invoke", { value: makeInvokeMethod(t, r, c) }), a; } function tryCatch(t, e, r) { try { return { type: "normal", arg: t.call(e, r) }; } catch (t) { return { type: "throw", arg: t }; } } e.wrap = wrap; var h = "suspendedStart", l = "suspendedYield", f = "executing", s = "completed", y = {}; function Generator() {} function GeneratorFunction() {} function GeneratorFunctionPrototype() {} var p = {}; define(p, a, function () { return this; }); var d = Object.getPrototypeOf, v = d && d(d(values([]))); v && v !== r && n.call(v, a) && (p = v); var g = GeneratorFunctionPrototype.prototype = Generator.prototype = Object.create(p); function defineIteratorMethods(t) { ["next", "throw", "return"].forEach(function (e) { define(t, e, function (t) { return this._invoke(e, t); }); }); } function AsyncIterator(t, e) { function invoke(r, o, i, a) { var c = tryCatch(t[r], t, o); if ("throw" !== c.type) { var u = c.arg, h = u.value; return h && "object" == _typeof(h) && n.call(h, "__await") ? e.resolve(h.__await).then(function (t) { invoke("next", t, i, a); }, function (t) { invoke("throw", t, i, a); }) : e.resolve(h).then(function (t) { u.value = t, i(u); }, function (t) { return invoke("throw", t, i, a); }); } a(c.arg); } var r; o(this, "_invoke", { value: function value(t, n) { function callInvokeWithMethodAndArg() { return new e(function (e, r) { invoke(t, n, e, r); }); } return r = r ? r.then(callInvokeWithMethodAndArg, callInvokeWithMethodAndArg) : callInvokeWithMethodAndArg(); } }); } function makeInvokeMethod(e, r, n) { var o = h; return function (i, a) { if (o === f) throw Error("Generator is already running"); if (o === s) { if ("throw" === i) throw a; return { value: t, done: !0 }; } for (n.method = i, n.arg = a;;) { var c = n.delegate; if (c) { var u = maybeInvokeDelegate(c, n); if (u) { if (u === y) continue; return u; } } if ("next" === n.method) n.sent = n._sent = n.arg;else if ("throw" === n.method) { if (o === h) throw o = s, n.arg; n.dispatchException(n.arg); } else "return" === n.method && n.abrupt("return", n.arg); o = f; var p = tryCatch(e, r, n); if ("normal" === p.type) { if (o = n.done ? s : l, p.arg === y) continue; return { value: p.arg, done: n.done }; } "throw" === p.type && (o = s, n.method = "throw", n.arg = p.arg); } }; } function maybeInvokeDelegate(e, r) { var n = r.method, o = e.iterator[n]; if (o === t) return r.delegate = null, "throw" === n && e.iterator["return"] && (r.method = "return", r.arg = t, maybeInvokeDelegate(e, r), "throw" === r.method) || "return" !== n && (r.method = "throw", r.arg = new TypeError("The iterator does not provide a '" + n + "' method")), y; var i = tryCatch(o, e.iterator, r.arg); if ("throw" === i.type) return r.method = "throw", r.arg = i.arg, r.delegate = null, y; var a = i.arg; return a ? a.done ? (r[e.resultName] = a.value, r.next = e.nextLoc, "return" !== r.method && (r.method = "next", r.arg = t), r.delegate = null, y) : a : (r.method = "throw", r.arg = new TypeError("iterator result is not an object"), r.delegate = null, y); } function pushTryEntry(t) { var e = { tryLoc: t[0] }; 1 in t && (e.catchLoc = t[1]), 2 in t && (e.finallyLoc = t[2], e.afterLoc = t[3]), this.tryEntries.push(e); } function resetTryEntry(t) { var e = t.completion || {}; e.type = "normal", delete e.arg, t.completion = e; } function Context(t) { this.tryEntries = [{ tryLoc: "root" }], t.forEach(pushTryEntry, this), this.reset(!0); } function values(e) { if (e || "" === e) { var r = e[a]; if (r) return r.call(e); if ("function" == typeof e.next) return e; if (!isNaN(e.length)) { var o = -1, i = function next() { for (; ++o < e.length;) if (n.call(e, o)) return next.value = e[o], next.done = !1, next; return next.value = t, next.done = !0, next; }; return i.next = i; } } throw new TypeError(_typeof(e) + " is not iterable"); } return GeneratorFunction.prototype = GeneratorFunctionPrototype, o(g, "constructor", { value: GeneratorFunctionPrototype, configurable: !0 }), o(GeneratorFunctionPrototype, "constructor", { value: GeneratorFunction, configurable: !0 }), GeneratorFunction.displayName = define(GeneratorFunctionPrototype, u, "GeneratorFunction"), e.isGeneratorFunction = function (t) { var e = "function" == typeof t && t.constructor; return !!e && (e === GeneratorFunction || "GeneratorFunction" === (e.displayName || e.name)); }, e.mark = function (t) { return Object.setPrototypeOf ? Object.setPrototypeOf(t, GeneratorFunctionPrototype) : (t.__proto__ = GeneratorFunctionPrototype, define(t, u, "GeneratorFunction")), t.prototype = Object.create(g), t; }, e.awrap = function (t) { return { __await: t }; }, defineIteratorMethods(AsyncIterator.prototype), define(AsyncIterator.prototype, c, function () { return this; }), e.AsyncIterator = AsyncIterator, e.async = function (t, r, n, o, i) { void 0 === i && (i = Promise); var a = new AsyncIterator(wrap(t, r, n, o), i); return e.isGeneratorFunction(r) ? a : a.next().then(function (t) { return t.done ? t.value : a.next(); }); }, defineIteratorMethods(g), define(g, u, "Generator"), define(g, a, function () { return this; }), define(g, "toString", function () { return "[object Generator]"; }), e.keys = function (t) { var e = Object(t), r = []; for (var n in e) r.push(n); return r.reverse(), function next() { for (; r.length;) { var t = r.pop(); if (t in e) return next.value = t, next.done = !1, next; } return next.done = !0, next; }; }, e.values = values, Context.prototype = { constructor: Context, reset: function reset(e) { if (this.prev = 0, this.next = 0, this.sent = this._sent = t, this.done = !1, this.delegate = null, this.method = "next", this.arg = t, this.tryEntries.forEach(resetTryEntry), !e) for (var r in this) "t" === r.charAt(0) && n.call(this, r) && !isNaN(+r.slice(1)) && (this[r] = t); }, stop: function stop() { this.done = !0; var t = this.tryEntries[0].completion; if ("throw" === t.type) throw t.arg; return this.rval; }, dispatchException: function dispatchException(e) { if (this.done) throw e; var r = this; function handle(n, o) { return a.type = "throw", a.arg = e, r.next = n, o && (r.method = "next", r.arg = t), !!o; } for (var o = this.tryEntries.length - 1; o >= 0; --o) { var i = this.tryEntries[o], a = i.completion; if ("root" === i.tryLoc) return handle("end"); if (i.tryLoc <= this.prev) { var c = n.call(i, "catchLoc"), u = n.call(i, "finallyLoc"); if (c && u) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } else if (c) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); } else { if (!u) throw Error("try statement without catch or finally"); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } } } }, abrupt: function abrupt(t, e) { for (var r = this.tryEntries.length - 1; r >= 0; --r) { var o = this.tryEntries[r]; if (o.tryLoc <= this.prev && n.call(o, "finallyLoc") && this.prev < o.finallyLoc) { var i = o; break; } } i && ("break" === t || "continue" === t) && i.tryLoc <= e && e <= i.finallyLoc && (i = null); var a = i ? i.completion : {}; return a.type = t, a.arg = e, i ? (this.method = "next", this.next = i.finallyLoc, y) : this.complete(a); }, complete: function complete(t, e) { if ("throw" === t.type) throw t.arg; return "break" === t.type || "continue" === t.type ? this.next = t.arg : "return" === t.type ? (this.rval = this.arg = t.arg, this.method = "return", this.next = "end") : "normal" === t.type && e && (this.next = e), y; }, finish: function finish(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.finallyLoc === t) return this.complete(r.completion, r.afterLoc), resetTryEntry(r), y; } }, "catch": function _catch(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.tryLoc === t) { var n = r.completion; if ("throw" === n.type) { var o = n.arg; resetTryEntry(r); } return o; } } throw Error("illegal catch attempt"); }, delegateYield: function delegateYield(e, r, n) { return this.delegate = { iterator: values(e), resultName: r, nextLoc: n }, "next" === this.method && (this.arg = t), y; } }, e; }
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
exports.NftCollectionBase = void 0;
var core_1 = require("@ton/core");
var content_1 = require("../content");
var content_2 = require("./content");
var nft_collection_editable_1 = require("./contracts/build/nft-collection-editable");
var NftChangeContentMessage_1 = require("./types/NftChangeContentMessage");
var NftMintMessage_1 = require("./types/NftMintMessage");
var NftBatchMintMessage_1 = require("./types/NftBatchMintMessage");
var NftChangeAdminMessage_1 = require("./types/NftChangeAdminMessage");
var NftCollectionBase = /*#__PURE__*/function () {
  function NftCollectionBase(address, init, contentResolver, nftItemParamsValue) {
    _classCallCheck(this, NftCollectionBase);
    this.address = address;
    this.init = init;
    this.contentResolver = contentResolver;
    this.itemParamsValue = nftItemParamsValue;
  }
  return _createClass(NftCollectionBase, [{
    key: "sendDeploy",
    value: function () {
      var _sendDeploy = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee(provider, sender, value) {
        return _regeneratorRuntime().wrap(function _callee$(_context) {
          while (1) switch (_context.prev = _context.next) {
            case 0:
              _context.next = 2;
              return provider.internal(sender, {
                value: value !== null && value !== void 0 ? value : (0, core_1.toNano)('0.05'),
                bounce: true
              });
            case 2:
            case "end":
              return _context.stop();
          }
        }, _callee);
      }));
      function sendDeploy(_x, _x2, _x3) {
        return _sendDeploy.apply(this, arguments);
      }
      return sendDeploy;
    }()
  }, {
    key: "sendMint",
    value: function () {
      var _sendMint = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(provider, sender, item, options) {
        var _options$value, _options$queryId, _item$value;
        return _regeneratorRuntime().wrap(function _callee2$(_context2) {
          while (1) switch (_context2.prev = _context2.next) {
            case 0:
              if (!(this.itemParamsValue === undefined)) {
                _context2.next = 2;
                break;
              }
              throw new Error('No item params value');
            case 2:
              _context2.next = 4;
              return provider.internal(sender, {
                value: (_options$value = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value !== void 0 ? _options$value : (0, core_1.toNano)('0.05'),
                bounce: true,
                sendMode: core_1.SendMode.PAY_GAS_SEPARATELY,
                body: (0, core_1.beginCell)().store((0, NftMintMessage_1.storeNftMintMessage)({
                  queryId: (_options$queryId = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId !== void 0 ? _options$queryId : 0n,
                  itemIndex: item.index,
                  itemParams: item,
                  value: (_item$value = item.value) !== null && _item$value !== void 0 ? _item$value : (0, core_1.toNano)('0.03')
                }, this.itemParamsValue.store)).endCell()
              });
            case 4:
            case "end":
              return _context2.stop();
          }
        }, _callee2, this);
      }));
      function sendMint(_x4, _x5, _x6, _x7) {
        return _sendMint.apply(this, arguments);
      }
      return sendMint;
    }()
  }, {
    key: "sendBatchMint",
    value: function () {
      var _sendBatchMint = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee3(provider, sender, items, options) {
        var _options$value2, _options$queryId2;
        return _regeneratorRuntime().wrap(function _callee3$(_context3) {
          while (1) switch (_context3.prev = _context3.next) {
            case 0:
              if (!(this.itemParamsValue === undefined)) {
                _context3.next = 2;
                break;
              }
              throw new Error('No item params value');
            case 2:
              _context3.next = 4;
              return provider.internal(sender, {
                value: (_options$value2 = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value2 !== void 0 ? _options$value2 : (0, core_1.toNano)('0.05') * BigInt(items.length),
                bounce: true,
                body: (0, core_1.beginCell)().store((0, NftBatchMintMessage_1.storeNftBatchMintMessage)({
                  queryId: (_options$queryId2 = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId2 !== void 0 ? _options$queryId2 : 0n,
                  requests: items.map(function (item) {
                    var _item$value2;
                    return {
                      index: item.index,
                      params: item,
                      value: (_item$value2 = item.value) !== null && _item$value2 !== void 0 ? _item$value2 : (0, core_1.toNano)('0.03')
                    };
                  })
                }, this.itemParamsValue.store)).endCell()
              });
            case 4:
            case "end":
              return _context3.stop();
          }
        }, _callee3, this);
      }));
      function sendBatchMint(_x8, _x9, _x10, _x11) {
        return _sendBatchMint.apply(this, arguments);
      }
      return sendBatchMint;
    }()
  }, {
    key: "sendChangeAdmin",
    value: function () {
      var _sendChangeAdmin = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee4(provider, sender, newAdmin, options) {
        var _options$value3, _options$queryId3;
        return _regeneratorRuntime().wrap(function _callee4$(_context4) {
          while (1) switch (_context4.prev = _context4.next) {
            case 0:
              _context4.next = 2;
              return provider.internal(sender, {
                value: (_options$value3 = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value3 !== void 0 ? _options$value3 : (0, core_1.toNano)('0.05'),
                bounce: true,
                body: (0, core_1.beginCell)().store((0, NftChangeAdminMessage_1.storeNftChangeAdminMessage)({
                  newAdmin: newAdmin,
                  queryId: (_options$queryId3 = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId3 !== void 0 ? _options$queryId3 : 0n
                })).endCell()
              });
            case 2:
            case "end":
              return _context4.stop();
          }
        }, _callee4);
      }));
      function sendChangeAdmin(_x12, _x13, _x14, _x15) {
        return _sendChangeAdmin.apply(this, arguments);
      }
      return sendChangeAdmin;
    }()
  }, {
    key: "sendChangeContent",
    value: function () {
      var _sendChangeContent = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee5(provider, sender, message, options) {
        var _options$value4, _options$queryId4;
        return _regeneratorRuntime().wrap(function _callee5$(_context5) {
          while (1) switch (_context5.prev = _context5.next) {
            case 0:
              _context5.next = 2;
              return provider.internal(sender, {
                value: (_options$value4 = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value4 !== void 0 ? _options$value4 : (0, core_1.toNano)('0.05'),
                bounce: true,
                body: (0, core_1.beginCell)().store((0, NftChangeContentMessage_1.storeNftChangeContentMessage)({
                  queryId: (_options$queryId4 = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId4 !== void 0 ? _options$queryId4 : 0n,
                  newContent: message.newContent,
                  newRoyaltyParams: message.newRoyaltyParams
                })).endCell()
              });
            case 2:
            case "end":
              return _context5.stop();
          }
        }, _callee5);
      }));
      function sendChangeContent(_x16, _x17, _x18, _x19) {
        return _sendChangeContent.apply(this, arguments);
      }
      return sendChangeContent;
    }()
  }, {
    key: "getItemAddress",
    value: function () {
      var _getItemAddress = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee6(provider, index) {
        var ret;
        return _regeneratorRuntime().wrap(function _callee6$(_context6) {
          while (1) switch (_context6.prev = _context6.next) {
            case 0:
              _context6.next = 2;
              return provider.get('get_nft_address_by_index', [{
                type: 'int',
                value: index
              }]);
            case 2:
              ret = _context6.sent;
              return _context6.abrupt("return", ret.stack.readAddress());
            case 4:
            case "end":
              return _context6.stop();
          }
        }, _callee6);
      }));
      function getItemAddress(_x20, _x21) {
        return _getItemAddress.apply(this, arguments);
      }
      return getItemAddress;
    }()
  }, {
    key: "getData",
    value: function () {
      var _getData = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee7(provider) {
        var ret;
        return _regeneratorRuntime().wrap(function _callee7$(_context7) {
          while (1) switch (_context7.prev = _context7.next) {
            case 0:
              _context7.next = 2;
              return provider.get('get_collection_data', []);
            case 2:
              ret = _context7.sent;
              return _context7.abrupt("return", {
                nextItemIndex: ret.stack.readBigNumber(),
                content: ret.stack.readCell(),
                owner: ret.stack.readAddressOpt()
              });
            case 4:
            case "end":
              return _context7.stop();
          }
        }, _callee7);
      }));
      function getData(_x22) {
        return _getData.apply(this, arguments);
      }
      return getData;
    }()
  }, {
    key: "getContent",
    value: function () {
      var _getContent = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee8(provider) {
        var data;
        return _regeneratorRuntime().wrap(function _callee8$(_context8) {
          while (1) switch (_context8.prev = _context8.next) {
            case 0:
              if (!(this.contentResolver === undefined)) {
                _context8.next = 2;
                break;
              }
              throw new Error('No content resolver');
            case 2:
              _context8.next = 4;
              return this.getData(provider);
            case 4:
              data = _context8.sent;
              _context8.t0 = (0, content_2.parseNftContent);
              _context8.next = 8;
              return (0, content_1.loadFullContent)(data.content, this.contentResolver);
            case 8:
              _context8.t1 = _context8.sent;
              return _context8.abrupt("return", (0, _context8.t0)(_context8.t1));
            case 10:
            case "end":
              return _context8.stop();
          }
        }, _callee8, this);
      }));
      function getContent(_x23) {
        return _getContent.apply(this, arguments);
      }
      return getContent;
    }()
  }, {
    key: "getItemContent",
    value: function () {
      var _getItemContent = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee9(provider, index, individualContent) {
        var res;
        return _regeneratorRuntime().wrap(function _callee9$(_context9) {
          while (1) switch (_context9.prev = _context9.next) {
            case 0:
              _context9.next = 2;
              return provider.get('get_nft_content', [{
                type: 'int',
                value: index
              }, {
                type: 'cell',
                cell: individualContent
              }]);
            case 2:
              res = _context9.sent;
              return _context9.abrupt("return", res.stack.readCell());
            case 4:
            case "end":
              return _context9.stop();
          }
        }, _callee9);
      }));
      function getItemContent(_x24, _x25, _x26) {
        return _getItemContent.apply(this, arguments);
      }
      return getItemContent;
    }()
  }]);
}();
exports.NftCollectionBase = NftCollectionBase;
NftCollectionBase.code = core_1.Cell.fromBase64(nft_collection_editable_1.nftCollectionEditableCode.codeBoc);