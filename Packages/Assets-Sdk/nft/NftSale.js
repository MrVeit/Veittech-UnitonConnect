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
exports.NftSale = void 0;
var core_1 = require("@ton/core");
var NftSale = /*#__PURE__*/function () {
  function NftSale(address, init) {
    _classCallCheck(this, NftSale);
    this.address = address;
    this.init = init;
  }
  return _createClass(NftSale, [{
    key: "sendDeploy",
    value: function () {
      var _sendDeploy = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee(provider, sender, value) {
        return _regeneratorRuntime().wrap(function _callee$(_context) {
          while (1) switch (_context.prev = _context.next) {
            case 0:
              _context.next = 2;
              return provider.internal(sender, {
                value: value !== null && value !== void 0 ? value : (0, core_1.toNano)('1'),
                bounce: true,
                sendMode: core_1.SendMode.PAY_GAS_SEPARATELY
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
    key: "sendDeployExternal",
    value: function () {
      var _sendDeployExternal = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(provider) {
        return _regeneratorRuntime().wrap(function _callee2$(_context2) {
          while (1) switch (_context2.prev = _context2.next) {
            case 0:
              _context2.next = 2;
              return provider.external(new core_1.Cell());
            case 2:
            case "end":
              return _context2.stop();
          }
        }, _callee2);
      }));
      function sendDeployExternal(_x4) {
        return _sendDeployExternal.apply(this, arguments);
      }
      return sendDeployExternal;
    }()
  }, {
    key: "sendTopup",
    value: function () {
      var _sendTopup = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee3(provider, sender, options) {
        var _options$value, _options$queryId;
        return _regeneratorRuntime().wrap(function _callee3$(_context3) {
          while (1) switch (_context3.prev = _context3.next) {
            case 0:
              _context3.next = 2;
              return provider.internal(sender, {
                value: (_options$value = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value !== void 0 ? _options$value : (0, core_1.toNano)('0.05'),
                bounce: true,
                sendMode: core_1.SendMode.PAY_GAS_SEPARATELY,
                body: (0, core_1.beginCell)().storeUint(1, 32).storeUint((_options$queryId = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId !== void 0 ? _options$queryId : 0, 64).endCell()
              });
            case 2:
            case "end":
              return _context3.stop();
          }
        }, _callee3);
      }));
      function sendTopup(_x5, _x6, _x7) {
        return _sendTopup.apply(this, arguments);
      }
      return sendTopup;
    }()
  }, {
    key: "sendAdminMessage",
    value: function () {
      var _sendAdminMessage = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee4(provider, sender, params, value, queryId) {
        var builder;
        return _regeneratorRuntime().wrap(function _callee4$(_context4) {
          while (1) switch (_context4.prev = _context4.next) {
            case 0:
              builder = (0, core_1.beginCell)().storeUint(555, 32).storeUint(queryId !== null && queryId !== void 0 ? queryId : 0, 64);
              if (params.message instanceof core_1.Cell) {
                builder.storeRef(builder);
              } else {
                builder.storeRef((0, core_1.beginCell)().store((0, core_1.storeMessageRelaxed)(params.message)));
              }
              _context4.next = 4;
              return provider.internal(sender, {
                value: value !== null && value !== void 0 ? value : (0, core_1.toNano)('0.05'),
                bounce: true,
                sendMode: core_1.SendMode.PAY_GAS_SEPARATELY,
                body: builder.storeUint(params.sendMode, 8).endCell()
              });
            case 4:
            case "end":
              return _context4.stop();
          }
        }, _callee4);
      }));
      function sendAdminMessage(_x8, _x9, _x10, _x11, _x12) {
        return _sendAdminMessage.apply(this, arguments);
      }
      return sendAdminMessage;
    }()
  }, {
    key: "sendCancel",
    value: function () {
      var _sendCancel = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee5(provider, sender, options) {
        var _options$value2, _options$queryId2;
        var _yield$this$getData, isComplete;
        return _regeneratorRuntime().wrap(function _callee5$(_context5) {
          while (1) switch (_context5.prev = _context5.next) {
            case 0:
              _context5.next = 2;
              return this.getData(provider);
            case 2:
              _yield$this$getData = _context5.sent;
              isComplete = _yield$this$getData.isComplete;
              if (!isComplete) {
                _context5.next = 6;
                break;
              }
              throw new Error('Sale is complete');
            case 6:
              _context5.next = 8;
              return provider.internal(sender, {
                value: (_options$value2 = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value2 !== void 0 ? _options$value2 : (0, core_1.toNano)('1'),
                bounce: true,
                sendMode: core_1.SendMode.PAY_GAS_SEPARATELY,
                body: (0, core_1.beginCell)().storeUint(3, 32).storeUint((_options$queryId2 = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId2 !== void 0 ? _options$queryId2 : 0, 64).endCell()
              });
            case 8:
            case "end":
              return _context5.stop();
          }
        }, _callee5, this);
      }));
      function sendCancel(_x13, _x14, _x15) {
        return _sendCancel.apply(this, arguments);
      }
      return sendCancel;
    }()
  }, {
    key: "sendBuy",
    value: function () {
      var _sendBuy = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee6(provider, sender, options) {
        var _options$value3, _options$queryId3;
        var _yield$this$getData2, isComplete, nftOwner, fullPrice;
        return _regeneratorRuntime().wrap(function _callee6$(_context6) {
          while (1) switch (_context6.prev = _context6.next) {
            case 0:
              _context6.next = 2;
              return this.getData(provider);
            case 2:
              _yield$this$getData2 = _context6.sent;
              isComplete = _yield$this$getData2.isComplete;
              nftOwner = _yield$this$getData2.nftOwner;
              fullPrice = _yield$this$getData2.fullPrice;
              if (isComplete) {
                _context6.next = 8;
                break;
              }
              throw new Error('Sale is not complete');
            case 8:
              if (!(nftOwner === null)) {
                _context6.next = 10;
                break;
              }
              throw new Error('NFT owner is not set');
            case 10:
              _context6.next = 12;
              return provider.internal(sender, {
                value: (_options$value3 = options === null || options === void 0 ? void 0 : options.value) !== null && _options$value3 !== void 0 ? _options$value3 : fullPrice + (0, core_1.toNano)(1),
                bounce: true,
                sendMode: core_1.SendMode.PAY_GAS_SEPARATELY,
                body: (0, core_1.beginCell)().storeUint(2, 32).storeUint((_options$queryId3 = options === null || options === void 0 ? void 0 : options.queryId) !== null && _options$queryId3 !== void 0 ? _options$queryId3 : 0, 64).endCell()
              });
            case 12:
            case "end":
              return _context6.stop();
          }
        }, _callee6, this);
      }));
      function sendBuy(_x16, _x17, _x18) {
        return _sendBuy.apply(this, arguments);
      }
      return sendBuy;
    }()
  }, {
    key: "getData",
    value: function () {
      var _getData = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee7(provider) {
        var _yield$provider$get, stack;
        return _regeneratorRuntime().wrap(function _callee7$(_context7) {
          while (1) switch (_context7.prev = _context7.next) {
            case 0:
              _context7.next = 2;
              return provider.get('get_sale_data', []);
            case 2:
              _yield$provider$get = _context7.sent;
              stack = _yield$provider$get.stack;
              return _context7.abrupt("return", {
                type: stack.readNumber(),
                isComplete: stack.readBoolean(),
                createdAt: stack.readNumber(),
                marketplace: stack.readAddress(),
                nft: stack.readAddress(),
                nftOwner: stack.readAddressOpt(),
                fullPrice: stack.readBigNumber(),
                marketplaceFeeTo: stack.readAddress(),
                marketplaceFee: stack.readBigNumber(),
                royaltyTo: stack.readAddressOpt(),
                royalty: stack.readBigNumber()
              });
            case 5:
            case "end":
              return _context7.stop();
          }
        }, _callee7);
      }));
      function getData(_x19) {
        return _getData.apply(this, arguments);
      }
      return getData;
    }()
  }], [{
    key: "createFromConfig",
    value: function createFromConfig(params, code, workchain) {
      var data = (0, core_1.beginCell)().storeBit(false).storeUint(params.createdAt, 32).storeAddress(params.marketplace).storeAddress(params.nft).storeAddress(null).storeCoins(params.fullPrice).storeRef((0, core_1.beginCell)().storeAddress(params.marketplaceFeeTo).storeCoins(params.marketplaceFee).storeAddress(params.royaltyTo).storeCoins(params.royalty)).storeBit(params.canDeployByExternal).endCell();
      var init = {
        data: data,
        code: code !== null && code !== void 0 ? code : NftSale.code
      };
      return new NftSale((0, core_1.contractAddress)(workchain !== null && workchain !== void 0 ? workchain : 0, init), init);
    }
  }, {
    key: "createFromAddress",
    value: function createFromAddress(address) {
      return new NftSale(address);
    }
  }]);
}();
exports.NftSale = NftSale;
NftSale.code = core_1.Cell.fromBase64('te6cckECCwEAArkAART/APSkE/S88sgLAQIBIAMCAH7yMO1E0NMA0x/6QPpA+kD6ANTTADDAAY4d+ABwB8jLABbLH1AEzxZYzxYBzxYB+gLMywDJ7VTgXweCAP/+8vACAUgFBABXoDhZ2omhpgGmP/SB9IH0gfQBqaYAYGGh9IH0AfSB9ABhBCCMkrCgFYACqwECAs0IBgH3ZghA7msoAUmCgUjC+8uHCJND6QPoA+kD6ADBTkqEhoVCHoRagUpBwgBDIywVQA88WAfoCy2rJcfsAJcIAJddJwgKwjhdQRXCAEMjLBVADzxYB+gLLaslx+wAQI5I0NOJacIAQyMsFUAPPFgH6AstqyXH7AHAgghBfzD0UgcAlsjLHxPLPyPPFlADzxbKAIIJycOA+gLKAMlxgBjIywUmzxZw+gLLaszJgwb7AHFVUHAHyMsAFssfUATPFljPFgHPFgH6AszLAMntVAH30A6GmBgLjYSS+CcH0gGHaiaGmAaY/9IH0gfSB9AGppgBgYOCmE44BgAEqYhOmPhW8Q4YBKGATpn8cIxbMbC3MbK2QV44LJOZlvKAVxFWAAyS+G8BJrpOEBFcCBFd0VYACRWdjYKdxjgthOjq+G6hhoaYPqGAD9gHAU4ADAkB6PLRlLOOQjEzOTlTUscFkl8J4FFRxwXy4fSCEAUTjZEWuvLh9QP6QDBGUBA0WXAHyMsAFssfUATPFljPFgHPFgH6AszLAMntVOAwNyjAA+MCKMAAnDY3EDhHZRRDMHDwBeAIwAKYVUQQJBAj8AXgXwqED/LwCgDUODmCEDuaygAYvvLhyVNGxwVRUscFFbHy4cpwIIIQX8w9FCGAEMjLBSjPFiH6Astqyx8Vyz8nzxYnzxYUygAj+gITygDJgwb7AHFQZkUVBHAHyMsAFssfUATPFljPFgHPFgH6AszLAMntVOBqUYM=');
