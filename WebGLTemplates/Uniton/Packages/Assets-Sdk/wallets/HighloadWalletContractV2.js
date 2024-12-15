"use strict";

function _slicedToArray(r, e) { return _arrayWithHoles(r) || _iterableToArrayLimit(r, e) || _unsupportedIterableToArray(r, e) || _nonIterableRest(); }
function _nonIterableRest() { throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); }
function _iterableToArrayLimit(r, l) { var t = null == r ? null : "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"]; if (null != t) { var e, n, i, u, a = [], f = !0, o = !1; try { if (i = (t = t.call(r)).next, 0 === l) { if (Object(t) !== t) return; f = !1; } else for (; !(f = (e = i.call(t)).done) && (a.push(e.value), a.length !== l); f = !0); } catch (r) { o = !0, n = r; } finally { try { if (!f && null != t["return"] && (u = t["return"](), Object(u) !== u)) return; } finally { if (o) throw n; } } return a; } }
function _arrayWithHoles(r) { if (Array.isArray(r)) return r; }
function _createForOfIteratorHelper(r, e) { var t = "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"]; if (!t) { if (Array.isArray(r) || (t = _unsupportedIterableToArray(r)) || e && r && "number" == typeof r.length) { t && (r = t); var _n = 0, F = function F() {}; return { s: F, n: function n() { return _n >= r.length ? { done: !0 } : { done: !1, value: r[_n++] }; }, e: function e(r) { throw r; }, f: F }; } throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); } var o, a = !0, u = !1; return { s: function s() { t = t.call(r); }, n: function n() { var r = t.next(); return a = r.done, r; }, e: function e(r) { u = !0, o = r; }, f: function f() { try { a || null == t["return"] || t["return"](); } finally { if (u) throw o; } } }; }
function _unsupportedIterableToArray(r, a) { if (r) { if ("string" == typeof r) return _arrayLikeToArray(r, a); var t = {}.toString.call(r).slice(8, -1); return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray(r, a) : void 0; } }
function _arrayLikeToArray(r, a) { (null == a || a > r.length) && (a = r.length); for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e]; return n; }
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
exports.HighloadWalletContractV2 = void 0;
var core_1 = require("@ton/core");
var crypto_1 = require("@ton/crypto");
var utils_1 = require("../utils");
var HighloadWalletContractV2 = /*#__PURE__*/function () {
  function HighloadWalletContractV2(workchain, publicKey, walletId) {
    _classCallCheck(this, HighloadWalletContractV2);
    this.workchain = workchain;
    this.publicKey = publicKey;
    if (walletId !== null && walletId !== undefined) {
      this.walletId = walletId;
    } else {
      this.walletId = 698983191 + workchain;
    }
    // Reference: https://github.com/ton-blockchain/ton/blob/master/crypto/smartcont/highload-wallet-v2-code.fc
    var code = core_1.Cell.fromBase64('te6cckEBCQEA5QABFP8A9KQT9LzyyAsBAgEgAgMCAUgEBQHq8oMI1xgg0x/TP/gjqh9TILnyY+1E0NMf0z/T//QE0VNggED0Dm+hMfJgUXO68qIH+QFUEIf5EPKjAvQE0fgAf44WIYAQ9HhvpSCYAtMH1DAB+wCRMuIBs+ZbgyWhyEA0gED0Q4rmMQHIyx8Tyz/L//QAye1UCAAE0DACASAGBwAXvZznaiaGmvmOuF/8AEG+X5dqJoaY+Y6Z/p/5j6AmipEEAgegc30JjJLb/JXdHxQANCCAQPSWb6VsEiCUMFMDud4gkzM2AZJsIeKzn55UWg==');
    var data = (0, core_1.beginCell)().storeUint(this.walletId, 32).storeUint(0, 64).storeBuffer(this.publicKey, 32).storeDict(null).endCell();
    this.init = {
      code: code,
      data: data
    };
    this.address = (0, core_1.contractAddress)(this.workchain, this.init);
  }
  return _createClass(HighloadWalletContractV2, [{
    key: "getBalance",
    value: (
    /**
     * Get wallet balance.
     */
    function () {
      var _getBalance = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee(provider) {
        var state;
        return _regeneratorRuntime().wrap(function _callee$(_context) {
          while (1) switch (_context.prev = _context.next) {
            case 0:
              _context.next = 2;
              return provider.getState();
            case 2:
              state = _context.sent;
              return _context.abrupt("return", state.balance);
            case 4:
            case "end":
              return _context.stop();
          }
        }, _callee);
      }));
      function getBalance(_x) {
        return _getBalance.apply(this, arguments);
      }
      return getBalance;
    }()
    /**
     * Send signed message.
     */
    )
  }, {
    key: "send",
    value: (function () {
      var _send = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(provider, message) {
        return _regeneratorRuntime().wrap(function _callee2$(_context2) {
          while (1) switch (_context2.prev = _context2.next) {
            case 0:
              _context2.next = 2;
              return provider.external(message);
            case 2:
            case "end":
              return _context2.stop();
          }
        }, _callee2);
      }));
      function send(_x2, _x3) {
        return _send.apply(this, arguments);
      }
      return send;
    }()
    /**
     * Sign and send message.
     */
    )
  }, {
    key: "sendTransfer",
    value: (function () {
      var _sendTransfer = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee3(provider, args) {
        var message;
        return _regeneratorRuntime().wrap(function _callee3$(_context3) {
          while (1) switch (_context3.prev = _context3.next) {
            case 0:
              message = this.createTransfer(args);
              _context3.next = 3;
              return this.send(provider, message);
            case 3:
            case "end":
              return _context3.stop();
          }
        }, _callee3, this);
      }));
      function sendTransfer(_x4, _x5) {
        return _sendTransfer.apply(this, arguments);
      }
      return sendTransfer;
    }()
    /**
     * Create signed message.
     */
    )
  }, {
    key: "createTransfer",
    value: function createTransfer(args) {
      var seqno = Math.floor(Math.random() * (1 << 32));
      if (args.seqno !== null && args.seqno !== undefined) {
        seqno = args.seqno;
      }
      var timeout = 5 * 60; // 15 minutes
      if (args.timeout !== null && args.timeout !== undefined && args.timeout < timeout) {
        timeout = args.timeout;
      }
      var sendMode = core_1.SendMode.PAY_GAS_SEPARATELY | core_1.SendMode.IGNORE_ERRORS;
      if (args.sendMode !== null && args.sendMode !== undefined) {
        sendMode = args.sendMode;
      }
      var now = Date.now();
      if (args.now !== null && args.now !== undefined) {
        now = args.now;
      }
      return (0, core_1.beginCell)().store(storeSignedTransferHighloadWalletV2({
        secretKey: args.secretKey,
        messages: args.messages,
        seqno: seqno,
        sendMode: sendMode,
        timeout: timeout,
        walletId: this.walletId,
        now: now
      })).endCell();
    }
    /**
     * Load signed message.
     */
  }, {
    key: "loadTransfer",
    value: function loadTransfer(src) {
      return loadSignedTransferHighloadWalletV2(src);
    }
    /**
     * Send signed message and wait for processing.
     */
  }, {
    key: "sendTransferAndWait",
    value: (function () {
      var _sendTransferAndWait = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee4(provider, args) {
        var sleepInterval,
          transfer,
          _this$loadTransfer,
          queryId,
          state,
          status,
          _args4 = arguments;
        return _regeneratorRuntime().wrap(function _callee4$(_context4) {
          while (1) switch (_context4.prev = _context4.next) {
            case 0:
              sleepInterval = _args4.length > 2 && _args4[2] !== undefined ? _args4[2] : 3000;
              transfer = this.createTransfer(args);
              _this$loadTransfer = this.loadTransfer(transfer.beginParse()), queryId = _this$loadTransfer.queryId;
            case 3:
              if (!true) {
                _context4.next = 29;
                break;
              }
              _context4.prev = 4;
              _context4.next = 7;
              return provider.external(transfer);
            case 7:
              _context4.next = 11;
              break;
            case 9:
              _context4.prev = 9;
              _context4.t0 = _context4["catch"](4);
            case 11:
              _context4.next = 13;
              return (0, utils_1.sleep)(sleepInterval);
            case 13:
              _context4.next = 15;
              return provider.getState();
            case 15:
              state = _context4.sent;
              if (!(state.state.type === 'uninit')) {
                _context4.next = 18;
                break;
              }
              return _context4.abrupt("continue", 3);
            case 18:
              _context4.next = 20;
              return this.getProcessedStatus(provider, queryId);
            case 20:
              status = _context4.sent;
              if (!(status === 'processed')) {
                _context4.next = 25;
                break;
              }
              return _context4.abrupt("return");
            case 25:
              if (!(status === 'forgotten')) {
                _context4.next = 27;
                break;
              }
              throw new Error('The transfer was forgotten');
            case 27:
              _context4.next = 3;
              break;
            case 29:
            case "end":
              return _context4.stop();
          }
        }, _callee4, this, [[4, 9]]);
      }));
      function sendTransferAndWait(_x6, _x7) {
        return _sendTransferAndWait.apply(this, arguments);
      }
      return sendTransferAndWait;
    }()
    /**
     * Get processed status of message.
     */
    )
  }, {
    key: "getProcessedStatus",
    value: (function () {
      var _getProcessedStatus = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee5(provider, queryId) {
        var _yield$provider$get, stack, processedStatus;
        return _regeneratorRuntime().wrap(function _callee5$(_context5) {
          while (1) switch (_context5.prev = _context5.next) {
            case 0:
              _context5.next = 2;
              return provider.get('processed?', [{
                type: 'int',
                value: queryId
              }]);
            case 2:
              _yield$provider$get = _context5.sent;
              stack = _yield$provider$get.stack;
              processedStatus = stack.readBigNumber();
              _context5.t0 = processedStatus;
              _context5.next = _context5.t0 === -1n ? 8 : _context5.t0 === 0n ? 9 : _context5.t0 === 1n ? 10 : 11;
              break;
            case 8:
              return _context5.abrupt("return", 'processed');
            case 9:
              return _context5.abrupt("return", 'unprocessed');
            case 10:
              return _context5.abrupt("return", 'forgotten');
            case 11:
              throw new Error('Unknown processed status ' + processedStatus);
            case 12:
            case "end":
              return _context5.stop();
          }
        }, _callee5);
      }));
      function getProcessedStatus(_x8, _x9) {
        return _getProcessedStatus.apply(this, arguments);
      }
      return getProcessedStatus;
    }()
    /**
     * Create sender.
     */
    )
  }, {
    key: "sender",
    value: function sender(provider, secretKey) {
      var _this = this;
      return {
        send: function () {
          var _send2 = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee6(args) {
            return _regeneratorRuntime().wrap(function _callee6$(_context6) {
              while (1) switch (_context6.prev = _context6.next) {
                case 0:
                  _context6.next = 2;
                  return _this.sendTransferAndWait(provider, {
                    secretKey: secretKey,
                    sendMode: args.sendMode,
                    messages: [(0, core_1.internal)({
                      to: args.to,
                      value: args.value,
                      bounce: args.bounce,
                      init: args.init,
                      body: args.body
                    })]
                  });
                case 2:
                case "end":
                  return _context6.stop();
              }
            }, _callee6);
          }));
          function send(_x10) {
            return _send2.apply(this, arguments);
          }
          return send;
        }(),
        address: this.address
      };
    }
  }], [{
    key: "create",
    value: function create(args) {
      return new HighloadWalletContractV2(args.workchain, args.publicKey, args.walletId);
    }
  }]);
}();
exports.HighloadWalletContractV2 = HighloadWalletContractV2;
function createMessageRelaxedValue() {
  return {
    serialize: function serialize(args, builder) {
      var sendMode = args.sendMode,
        message = args.message;
      var messageRelaxed = (0, core_1.beginCell)().storeWritable((0, core_1.storeMessageRelaxed)(message));
      builder.storeUint(sendMode, 8);
      builder.storeRef(messageRelaxed);
    },
    parse: function parse(src) {
      var sendMode = src.loadUint(8);
      var message = (0, core_1.loadMessageRelaxed)(src.loadRef().beginParse());
      return {
        sendMode: sendMode,
        message: message
      };
    }
  };
}
function getQueryId(now, timeout, seqno) {
  var validUntil = Math.floor(now / 1000) + timeout;
  return (BigInt(validUntil) << 32n) + BigInt(seqno);
}
function storeSignedTransferHighloadWalletV2(args) {
  return function (builder) {
    var secretKey = args.secretKey,
      messages = args.messages,
      seqno = args.seqno,
      sendMode = args.sendMode,
      now = args.now,
      timeout = args.timeout,
      walletId = args.walletId;
    var queryId = getQueryId(now, timeout, seqno);
    var dict = core_1.Dictionary.empty(core_1.Dictionary.Keys.Int(16), createMessageRelaxedValue());
    var _iterator = _createForOfIteratorHelper(messages.entries()),
      _step;
    try {
      for (_iterator.s(); !(_step = _iterator.n()).done;) {
        var _step$value = _slicedToArray(_step.value, 2),
          i = _step$value[0],
          message = _step$value[1];
        dict.set(i, {
          sendMode: sendMode,
          message: message
        });
      }
    } catch (err) {
      _iterator.e(err);
    } finally {
      _iterator.f();
    }
    var signedMessage = (0, core_1.beginCell)().storeUint(walletId, 32).storeUint(queryId, 64).storeDict(dict).endCell();
    var hash = signedMessage.hash();
    var signature = (0, crypto_1.sign)(hash, secretKey);
    builder.storeBuffer(signature);
    builder.storeSlice(signedMessage.beginParse());
  };
}
function loadSignedTransferHighloadWalletV2(src) {
  var signature = src.loadBuffer(64);
  var walletId = src.loadUint(32);
  var queryId = src.loadUintBig(64);
  var dict = src.loadDict(core_1.Dictionary.Keys.Int(16), createMessageRelaxedValue());
  var messages = dict.values();
  return {
    signature: signature,
    walletId: walletId,
    queryId: queryId,
    messages: messages
  };
}