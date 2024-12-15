"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function _regeneratorRuntime() { "use strict"; /*! regenerator-runtime -- Copyright (c) 2014-present, Facebook, Inc. -- license (MIT): https://github.com/facebook/regenerator/blob/main/LICENSE */ _regeneratorRuntime = function _regeneratorRuntime() { return e; }; var t, e = {}, r = Object.prototype, n = r.hasOwnProperty, o = Object.defineProperty || function (t, e, r) { t[e] = r.value; }, i = "function" == typeof Symbol ? Symbol : {}, a = i.iterator || "@@iterator", c = i.asyncIterator || "@@asyncIterator", u = i.toStringTag || "@@toStringTag"; function define(t, e, r) { return Object.defineProperty(t, e, { value: r, enumerable: !0, configurable: !0, writable: !0 }), t[e]; } try { define({}, ""); } catch (t) { define = function define(t, e, r) { return t[e] = r; }; } function wrap(t, e, r, n) { var i = e && e.prototype instanceof Generator ? e : Generator, a = Object.create(i.prototype), c = new Context(n || []); return o(a, "_invoke", { value: makeInvokeMethod(t, r, c) }), a; } function tryCatch(t, e, r) { try { return { type: "normal", arg: t.call(e, r) }; } catch (t) { return { type: "throw", arg: t }; } } e.wrap = wrap; var h = "suspendedStart", l = "suspendedYield", f = "executing", s = "completed", y = {}; function Generator() {} function GeneratorFunction() {} function GeneratorFunctionPrototype() {} var p = {}; define(p, a, function () { return this; }); var d = Object.getPrototypeOf, v = d && d(d(values([]))); v && v !== r && n.call(v, a) && (p = v); var g = GeneratorFunctionPrototype.prototype = Generator.prototype = Object.create(p); function defineIteratorMethods(t) { ["next", "throw", "return"].forEach(function (e) { define(t, e, function (t) { return this._invoke(e, t); }); }); } function AsyncIterator(t, e) { function invoke(r, o, i, a) { var c = tryCatch(t[r], t, o); if ("throw" !== c.type) { var u = c.arg, h = u.value; return h && "object" == _typeof(h) && n.call(h, "__await") ? e.resolve(h.__await).then(function (t) { invoke("next", t, i, a); }, function (t) { invoke("throw", t, i, a); }) : e.resolve(h).then(function (t) { u.value = t, i(u); }, function (t) { return invoke("throw", t, i, a); }); } a(c.arg); } var r; o(this, "_invoke", { value: function value(t, n) { function callInvokeWithMethodAndArg() { return new e(function (e, r) { invoke(t, n, e, r); }); } return r = r ? r.then(callInvokeWithMethodAndArg, callInvokeWithMethodAndArg) : callInvokeWithMethodAndArg(); } }); } function makeInvokeMethod(e, r, n) { var o = h; return function (i, a) { if (o === f) throw Error("Generator is already running"); if (o === s) { if ("throw" === i) throw a; return { value: t, done: !0 }; } for (n.method = i, n.arg = a;;) { var c = n.delegate; if (c) { var u = maybeInvokeDelegate(c, n); if (u) { if (u === y) continue; return u; } } if ("next" === n.method) n.sent = n._sent = n.arg;else if ("throw" === n.method) { if (o === h) throw o = s, n.arg; n.dispatchException(n.arg); } else "return" === n.method && n.abrupt("return", n.arg); o = f; var p = tryCatch(e, r, n); if ("normal" === p.type) { if (o = n.done ? s : l, p.arg === y) continue; return { value: p.arg, done: n.done }; } "throw" === p.type && (o = s, n.method = "throw", n.arg = p.arg); } }; } function maybeInvokeDelegate(e, r) { var n = r.method, o = e.iterator[n]; if (o === t) return r.delegate = null, "throw" === n && e.iterator["return"] && (r.method = "return", r.arg = t, maybeInvokeDelegate(e, r), "throw" === r.method) || "return" !== n && (r.method = "throw", r.arg = new TypeError("The iterator does not provide a '" + n + "' method")), y; var i = tryCatch(o, e.iterator, r.arg); if ("throw" === i.type) return r.method = "throw", r.arg = i.arg, r.delegate = null, y; var a = i.arg; return a ? a.done ? (r[e.resultName] = a.value, r.next = e.nextLoc, "return" !== r.method && (r.method = "next", r.arg = t), r.delegate = null, y) : a : (r.method = "throw", r.arg = new TypeError("iterator result is not an object"), r.delegate = null, y); } function pushTryEntry(t) { var e = { tryLoc: t[0] }; 1 in t && (e.catchLoc = t[1]), 2 in t && (e.finallyLoc = t[2], e.afterLoc = t[3]), this.tryEntries.push(e); } function resetTryEntry(t) { var e = t.completion || {}; e.type = "normal", delete e.arg, t.completion = e; } function Context(t) { this.tryEntries = [{ tryLoc: "root" }], t.forEach(pushTryEntry, this), this.reset(!0); } function values(e) { if (e || "" === e) { var r = e[a]; if (r) return r.call(e); if ("function" == typeof e.next) return e; if (!isNaN(e.length)) { var o = -1, i = function next() { for (; ++o < e.length;) if (n.call(e, o)) return next.value = e[o], next.done = !1, next; return next.value = t, next.done = !0, next; }; return i.next = i; } } throw new TypeError(_typeof(e) + " is not iterable"); } return GeneratorFunction.prototype = GeneratorFunctionPrototype, o(g, "constructor", { value: GeneratorFunctionPrototype, configurable: !0 }), o(GeneratorFunctionPrototype, "constructor", { value: GeneratorFunction, configurable: !0 }), GeneratorFunction.displayName = define(GeneratorFunctionPrototype, u, "GeneratorFunction"), e.isGeneratorFunction = function (t) { var e = "function" == typeof t && t.constructor; return !!e && (e === GeneratorFunction || "GeneratorFunction" === (e.displayName || e.name)); }, e.mark = function (t) { return Object.setPrototypeOf ? Object.setPrototypeOf(t, GeneratorFunctionPrototype) : (t.__proto__ = GeneratorFunctionPrototype, define(t, u, "GeneratorFunction")), t.prototype = Object.create(g), t; }, e.awrap = function (t) { return { __await: t }; }, defineIteratorMethods(AsyncIterator.prototype), define(AsyncIterator.prototype, c, function () { return this; }), e.AsyncIterator = AsyncIterator, e.async = function (t, r, n, o, i) { void 0 === i && (i = Promise); var a = new AsyncIterator(wrap(t, r, n, o), i); return e.isGeneratorFunction(r) ? a : a.next().then(function (t) { return t.done ? t.value : a.next(); }); }, defineIteratorMethods(g), define(g, u, "Generator"), define(g, a, function () { return this; }), define(g, "toString", function () { return "[object Generator]"; }), e.keys = function (t) { var e = Object(t), r = []; for (var n in e) r.push(n); return r.reverse(), function next() { for (; r.length;) { var t = r.pop(); if (t in e) return next.value = t, next.done = !1, next; } return next.done = !0, next; }; }, e.values = values, Context.prototype = { constructor: Context, reset: function reset(e) { if (this.prev = 0, this.next = 0, this.sent = this._sent = t, this.done = !1, this.delegate = null, this.method = "next", this.arg = t, this.tryEntries.forEach(resetTryEntry), !e) for (var r in this) "t" === r.charAt(0) && n.call(this, r) && !isNaN(+r.slice(1)) && (this[r] = t); }, stop: function stop() { this.done = !0; var t = this.tryEntries[0].completion; if ("throw" === t.type) throw t.arg; return this.rval; }, dispatchException: function dispatchException(e) { if (this.done) throw e; var r = this; function handle(n, o) { return a.type = "throw", a.arg = e, r.next = n, o && (r.method = "next", r.arg = t), !!o; } for (var o = this.tryEntries.length - 1; o >= 0; --o) { var i = this.tryEntries[o], a = i.completion; if ("root" === i.tryLoc) return handle("end"); if (i.tryLoc <= this.prev) { var c = n.call(i, "catchLoc"), u = n.call(i, "finallyLoc"); if (c && u) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } else if (c) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); } else { if (!u) throw Error("try statement without catch or finally"); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } } } }, abrupt: function abrupt(t, e) { for (var r = this.tryEntries.length - 1; r >= 0; --r) { var o = this.tryEntries[r]; if (o.tryLoc <= this.prev && n.call(o, "finallyLoc") && this.prev < o.finallyLoc) { var i = o; break; } } i && ("break" === t || "continue" === t) && i.tryLoc <= e && e <= i.finallyLoc && (i = null); var a = i ? i.completion : {}; return a.type = t, a.arg = e, i ? (this.method = "next", this.next = i.finallyLoc, y) : this.complete(a); }, complete: function complete(t, e) { if ("throw" === t.type) throw t.arg; return "break" === t.type || "continue" === t.type ? this.next = t.arg : "return" === t.type ? (this.rval = this.arg = t.arg, this.method = "return", this.next = "end") : "normal" === t.type && e && (this.next = e), y; }, finish: function finish(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.finallyLoc === t) return this.complete(r.completion, r.afterLoc), resetTryEntry(r), y; } }, "catch": function _catch(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.tryLoc === t) { var n = r.completion; if ("throw" === n.type) { var o = n.arg; resetTryEntry(r); } return o; } } throw Error("illegal catch attempt"); }, delegateYield: function delegateYield(e, r, n) { return this.delegate = { iterator: values(e), resultName: r, nextLoc: n }, "next" === this.method && (this.arg = t), y; } }, e; }
function asyncGeneratorStep(n, t, e, r, o, a, c) { try { var i = n[a](c), u = i.value; } catch (n) { return void e(n); } i.done ? t(u) : Promise.resolve(u).then(r, o); }
function _asyncToGenerator(n) { return function () { var t = this, e = arguments; return new Promise(function (r, o) { var a = n.apply(t, e); function _next(n) { asyncGeneratorStep(a, r, o, _next, _throw, "next", n); } function _throw(n) { asyncGeneratorStep(a, r, o, _next, _throw, "throw", n); } _next(void 0); }); }; }
function _classCallCheck(a, n) { if (!(a instanceof n)) throw new TypeError("Cannot call a class as a function"); }
function _defineProperties(e, r) { for (var t = 0; t < r.length; t++) { var o = r[t]; o.enumerable = o.enumerable || !1, o.configurable = !0, "value" in o && (o.writable = !0), Object.defineProperty(e, _toPropertyKey(o.key), o); } }
function _createClass(e, r, t) { return r && _defineProperties(e.prototype, r), t && _defineProperties(e, t), Object.defineProperty(e, "prototype", { writable: !1 }), e; }
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
exports.TonAPI = void 0;
var core_1 = require("@ton/core");
var zod_1 = __importDefault(require("zod"));
var axios_1 = __importDefault(require("axios"));
var Address = zod_1["default"].string().transform(function (v) {
  return core_1.Address.parseRaw(v);
});
var HexBuffer = zod_1["default"].string().transform(function (v) {
  return Buffer.from(v, 'hex');
});
var zBigint = zod_1["default"].union([zod_1["default"].number(), zod_1["default"].string()]).transform(function (v) {
  return BigInt(v);
});
var zStrnum = zod_1["default"].union([zod_1["default"].number(), zod_1["default"].string()]).transform(function (v) {
  return Number(v);
});
var ImagePreview = zod_1["default"].object({
  resolution: zod_1["default"].string(),
  url: zod_1["default"].string()
});
var AccountAddress = zod_1["default"].object({
  address: Address,
  name: zod_1["default"].optional(zod_1["default"].string()),
  is_scam: zod_1["default"]["boolean"](),
  icon: zod_1["default"].optional(zod_1["default"].string()),
  is_wallet: zod_1["default"]["boolean"]()
});
var NftCollection = zod_1["default"].object({
  address: Address,
  next_item_index: zBigint,
  owner: zod_1["default"].optional(AccountAddress),
  metadata: zod_1["default"].optional(zod_1["default"].record(zod_1["default"].any())),
  raw_collection_content: HexBuffer,
  previews: zod_1["default"].optional(zod_1["default"].array(ImagePreview)),
  approved_by: zod_1["default"].array(zod_1["default"].string())
});
var NftCollections = zod_1["default"].object({
  nft_collections: zod_1["default"].array(NftCollection)
});
var Price = zod_1["default"].object({
  value: zBigint,
  token_name: zod_1["default"].string()
});
var Sale = zod_1["default"].object({
  address: Address,
  market: AccountAddress,
  owner: zod_1["default"].optional(AccountAddress),
  price: Price
});
var NftItem = zod_1["default"].object({
  address: Address,
  index: zBigint,
  owner: zod_1["default"].optional(AccountAddress),
  collection: zod_1["default"].optional(zod_1["default"].object({
    address: Address,
    name: zod_1["default"].string(),
    description: zod_1["default"].string()
  })),
  verified: zod_1["default"]["boolean"](),
  metadata: zod_1["default"].record(zod_1["default"].any()),
  sale: zod_1["default"].optional(Sale),
  previews: zod_1["default"].optional(zod_1["default"].array(ImagePreview)),
  dns: zod_1["default"].optional(zod_1["default"].string()),
  approved_by: zod_1["default"].array(zod_1["default"].string())
});
var NftItems = zod_1["default"].object({
  nft_items: zod_1["default"].array(NftItem)
});
var JettonVerificationType = zod_1["default"].union([zod_1["default"].literal('whitelist'), zod_1["default"].literal('blacklist'), zod_1["default"].literal('none')]);
var JettonMetadata = zod_1["default"].object({
  address: Address,
  name: zod_1["default"].string(),
  symbol: zod_1["default"].string(),
  decimals: zStrnum,
  image: zod_1["default"].optional(zod_1["default"].string()),
  description: zod_1["default"].optional(zod_1["default"].string()),
  social: zod_1["default"].optional(zod_1["default"].array(zod_1["default"].string())),
  websites: zod_1["default"].optional(zod_1["default"].array(zod_1["default"].string())),
  catalogs: zod_1["default"].optional(zod_1["default"].array(zod_1["default"].string()))
});
var JettonInfo = zod_1["default"].object({
  mintable: zod_1["default"]["boolean"](),
  total_supply: zBigint,
  metadata: JettonMetadata,
  verification: JettonVerificationType,
  holders_count: zod_1["default"].number()
});
var Jettons = zod_1["default"].object({
  jettons: zod_1["default"].array(JettonInfo)
});
var JettonHolder = zod_1["default"].object({
  address: Address,
  owner: AccountAddress,
  balance: zBigint
});
var JettonHolders = zod_1["default"].object({
  addresses: zod_1["default"].array(JettonHolder)
});
var EncryptedComment = zod_1["default"].object({
  encryption_type: zod_1["default"].string(),
  cipher_text: HexBuffer
});
var Refund = zod_1["default"].object({
  type: zod_1["default"].string(),
  origin: Address
});
var NftItemTransferAction = zod_1["default"].object({
  sender: zod_1["default"].optional(AccountAddress),
  recipient: zod_1["default"].optional(AccountAddress),
  nft: Address,
  comment: zod_1["default"].optional(zod_1["default"].string()),
  encrypted_comment: zod_1["default"].optional(EncryptedComment),
  payload: zod_1["default"].optional(HexBuffer),
  refund: zod_1["default"].optional(Refund)
});
var ActionStatus = zod_1["default"].union([zod_1["default"].literal('ok'), zod_1["default"].literal('failed')]);
var ActionSpecificNftItemTransfer = zod_1["default"].object({
  type: zod_1["default"].literal('NftItemTransfer'),
  status: ActionStatus,
  NftItemTransfer: NftItemTransferAction
}).transform(function (v) {
  return _objectSpread({
    status: v.status
  }, v.NftItemTransfer);
});
var AccountEventGeneric = function AccountEventGeneric(t) {
  return zod_1["default"].object({
    event_id: zod_1["default"].string(),
    account: AccountAddress,
    timestamp: zod_1["default"].number(),
    actions: zod_1["default"].array(t),
    is_scam: zod_1["default"]["boolean"](),
    lt: zBigint,
    in_progress: zod_1["default"]["boolean"]()
  });
};
var AccountEventNftItemTransfer = AccountEventGeneric(ActionSpecificNftItemTransfer);
var AccountEventsGeneric = function AccountEventsGeneric(t) {
  return zod_1["default"].object({
    events: zod_1["default"].array(t),
    next_from: zBigint
  });
};
var AccountEventsNftItemTransfer = AccountEventsGeneric(AccountEventNftItemTransfer);
var TokenRates = zod_1["default"].object({
  prices: zod_1["default"].optional(zod_1["default"].record(zod_1["default"].number())),
  diff_24h: zod_1["default"].optional(zod_1["default"].record(zod_1["default"].string())),
  diff_7d: zod_1["default"].optional(zod_1["default"].record(zod_1["default"].string())),
  diff_30d: zod_1["default"].optional(zod_1["default"].record(zod_1["default"].string()))
});
var JettonPreview = zod_1["default"].object({
  address: Address,
  name: zod_1["default"].string(),
  symbol: zod_1["default"].string(),
  decimals: zStrnum,
  image: zod_1["default"].string(),
  verification: JettonVerificationType
});
var JettonBalance = zod_1["default"].object({
  balance: zBigint,
  price: zod_1["default"].optional(TokenRates),
  wallet_address: AccountAddress,
  jetton: JettonPreview
});
var JettonBalances = zod_1["default"].object({
  balances: zod_1["default"].array(JettonBalance)
});
var rawAddress = function rawAddress(address) {
  return typeof address === 'string' ? address : address.toRawString();
};
var TonAPI = /*#__PURE__*/function () {
  function TonAPI(params) {
    var _params$baseURL;
    _classCallCheck(this, TonAPI);
    this.instance = axios_1["default"].create({
      baseURL: (_params$baseURL = params === null || params === void 0 ? void 0 : params.baseURL) !== null && _params$baseURL !== void 0 ? _params$baseURL : 'https://tonapi.io',
      headers: (params === null || params === void 0 ? void 0 : params.token) === undefined ? {} : {
        'Authorization': 'Bearer ' + params.token
      }
    });
  }
  return _createClass(TonAPI, [{
    key: "getNftCollections",
    value: function () {
      var _getNftCollections = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee(params) {
        return _regeneratorRuntime().wrap(function _callee$(_context) {
          while (1) switch (_context.prev = _context.next) {
            case 0:
              _context.t0 = NftCollections;
              _context.next = 3;
              return this.instance.get('/v2/nfts/collections', {
                params: params
              });
            case 3:
              _context.t1 = _context.sent.data;
              return _context.abrupt("return", _context.t0.parse.call(_context.t0, _context.t1).nft_collections);
            case 5:
            case "end":
              return _context.stop();
          }
        }, _callee, this);
      }));
      function getNftCollections(_x) {
        return _getNftCollections.apply(this, arguments);
      }
      return getNftCollections;
    }()
  }, {
    key: "getNftCollection",
    value: function () {
      var _getNftCollection = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(collection) {
        return _regeneratorRuntime().wrap(function _callee2$(_context2) {
          while (1) switch (_context2.prev = _context2.next) {
            case 0:
              _context2.t0 = NftCollection;
              _context2.next = 3;
              return this.instance.get("/v2/nfts/collections/".concat(rawAddress(collection)));
            case 3:
              _context2.t1 = _context2.sent.data;
              return _context2.abrupt("return", _context2.t0.parse.call(_context2.t0, _context2.t1));
            case 5:
            case "end":
              return _context2.stop();
          }
        }, _callee2, this);
      }));
      function getNftCollection(_x2) {
        return _getNftCollection.apply(this, arguments);
      }
      return getNftCollection;
    }()
  }, {
    key: "getNftCollectionItems",
    value: function () {
      var _getNftCollectionItems = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee3(collection, params) {
        return _regeneratorRuntime().wrap(function _callee3$(_context3) {
          while (1) switch (_context3.prev = _context3.next) {
            case 0:
              _context3.t0 = NftItems;
              _context3.next = 3;
              return this.instance.get("/v2/nfts/collections/".concat(rawAddress(collection), "/items"), {
                params: params
              });
            case 3:
              _context3.t1 = _context3.sent.data;
              return _context3.abrupt("return", _context3.t0.parse.call(_context3.t0, _context3.t1).nft_items);
            case 5:
            case "end":
              return _context3.stop();
          }
        }, _callee3, this);
      }));
      function getNftCollectionItems(_x3, _x4) {
        return _getNftCollectionItems.apply(this, arguments);
      }
      return getNftCollectionItems;
    }()
  }, {
    key: "getNftItems",
    value: function () {
      var _getNftItems = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee4(items) {
        return _regeneratorRuntime().wrap(function _callee4$(_context4) {
          while (1) switch (_context4.prev = _context4.next) {
            case 0:
              _context4.t0 = NftItems;
              _context4.next = 3;
              return this.instance.post("/v2/nfts/_bulk", {
                account_ids: items.map(rawAddress)
              });
            case 3:
              _context4.t1 = _context4.sent.data;
              return _context4.abrupt("return", _context4.t0.parse.call(_context4.t0, _context4.t1).nft_items);
            case 5:
            case "end":
              return _context4.stop();
          }
        }, _callee4, this);
      }));
      function getNftItems(_x5) {
        return _getNftItems.apply(this, arguments);
      }
      return getNftItems;
    }()
  }, {
    key: "getNftItem",
    value: function () {
      var _getNftItem = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee5(item) {
        return _regeneratorRuntime().wrap(function _callee5$(_context5) {
          while (1) switch (_context5.prev = _context5.next) {
            case 0:
              _context5.t0 = NftItem;
              _context5.next = 3;
              return this.instance.get("/v2/nfts/".concat(rawAddress(item)));
            case 3:
              _context5.t1 = _context5.sent.data;
              return _context5.abrupt("return", _context5.t0.parse.call(_context5.t0, _context5.t1));
            case 5:
            case "end":
              return _context5.stop();
          }
        }, _callee5, this);
      }));
      function getNftItem(_x6) {
        return _getNftItem.apply(this, arguments);
      }
      return getNftItem;
    }()
  }, {
    key: "getJettons",
    value: function () {
      var _getJettons = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee6(params) {
        return _regeneratorRuntime().wrap(function _callee6$(_context6) {
          while (1) switch (_context6.prev = _context6.next) {
            case 0:
              _context6.t0 = Jettons;
              _context6.next = 3;
              return this.instance.get('/v2/jettons', {
                params: params
              });
            case 3:
              _context6.t1 = _context6.sent.data;
              return _context6.abrupt("return", _context6.t0.parse.call(_context6.t0, _context6.t1).jettons);
            case 5:
            case "end":
              return _context6.stop();
          }
        }, _callee6, this);
      }));
      function getJettons(_x7) {
        return _getJettons.apply(this, arguments);
      }
      return getJettons;
    }()
  }, {
    key: "getJetton",
    value: function () {
      var _getJetton = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee7(jettonMaster) {
        return _regeneratorRuntime().wrap(function _callee7$(_context7) {
          while (1) switch (_context7.prev = _context7.next) {
            case 0:
              _context7.t0 = JettonInfo;
              _context7.next = 3;
              return this.instance.get("/v2/jettons/".concat(rawAddress(jettonMaster)));
            case 3:
              _context7.t1 = _context7.sent.data;
              return _context7.abrupt("return", _context7.t0.parse.call(_context7.t0, _context7.t1));
            case 5:
            case "end":
              return _context7.stop();
          }
        }, _callee7, this);
      }));
      function getJetton(_x8) {
        return _getJetton.apply(this, arguments);
      }
      return getJetton;
    }()
  }, {
    key: "getJettonHolders",
    value: function () {
      var _getJettonHolders = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee8(jettonMaster, params) {
        return _regeneratorRuntime().wrap(function _callee8$(_context8) {
          while (1) switch (_context8.prev = _context8.next) {
            case 0:
              _context8.t0 = JettonHolders;
              _context8.next = 3;
              return this.instance.get("/v2/jettons/".concat(rawAddress(jettonMaster), "/holders"), {
                params: params
              });
            case 3:
              _context8.t1 = _context8.sent.data;
              return _context8.abrupt("return", _context8.t0.parse.call(_context8.t0, _context8.t1).addresses);
            case 5:
            case "end":
              return _context8.stop();
          }
        }, _callee8, this);
      }));
      function getJettonHolders(_x9, _x10) {
        return _getJettonHolders.apply(this, arguments);
      }
      return getJettonHolders;
    }()
  }, {
    key: "getNftItemTransferHistory",
    value: function () {
      var _getNftItemTransferHistory = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee9(item, params) {
        return _regeneratorRuntime().wrap(function _callee9$(_context9) {
          while (1) switch (_context9.prev = _context9.next) {
            case 0:
              _context9.t0 = AccountEventsNftItemTransfer;
              _context9.next = 3;
              return this.instance.get("/v2/nfts/".concat(rawAddress(item), "/history"), {
                params: _objectSpread({
                  limit: 100
                }, params)
              });
            case 3:
              _context9.t1 = _context9.sent.data;
              return _context9.abrupt("return", _context9.t0.parse.call(_context9.t0, _context9.t1));
            case 5:
            case "end":
              return _context9.stop();
          }
        }, _callee9, this);
      }));
      function getNftItemTransferHistory(_x11, _x12) {
        return _getNftItemTransferHistory.apply(this, arguments);
      }
      return getNftItemTransferHistory;
    }()
  }, {
    key: "getAccountNfts",
    value: function () {
      var _getAccountNfts = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee10(account, params) {
        return _regeneratorRuntime().wrap(function _callee10$(_context10) {
          while (1) switch (_context10.prev = _context10.next) {
            case 0:
              _context10.t0 = NftItems;
              _context10.next = 3;
              return this.instance.get("/v2/accounts/".concat(rawAddress(account), "/nfts"), {
                params: _objectSpread(_objectSpread({}, params), {}, {
                  collection: (params === null || params === void 0 ? void 0 : params.collection) === undefined ? undefined : rawAddress(params.collection)
                })
              });
            case 3:
              _context10.t1 = _context10.sent.data;
              return _context10.abrupt("return", _context10.t0.parse.call(_context10.t0, _context10.t1).nft_items);
            case 5:
            case "end":
              return _context10.stop();
          }
        }, _callee10, this);
      }));
      function getAccountNfts(_x13, _x14) {
        return _getAccountNfts.apply(this, arguments);
      }
      return getAccountNfts;
    }()
  }, {
    key: "getAccountJettons",
    value: function () {
      var _getAccountJettons = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee11(account, params) {
        return _regeneratorRuntime().wrap(function _callee11$(_context11) {
          while (1) switch (_context11.prev = _context11.next) {
            case 0:
              _context11.t0 = JettonBalances;
              _context11.next = 3;
              return this.instance.get("/v2/accounts/".concat(rawAddress(account), "/jettons"), {
                params: {
                  currencies: (params === null || params === void 0 ? void 0 : params.currencies) === undefined ? undefined : params.currencies.join(',')
                }
              });
            case 3:
              _context11.t1 = _context11.sent.data;
              return _context11.abrupt("return", _context11.t0.parse.call(_context11.t0, _context11.t1).balances);
            case 5:
            case "end":
              return _context11.stop();
          }
        }, _callee11, this);
      }));
      function getAccountJettons(_x15, _x16) {
        return _getAccountJettons.apply(this, arguments);
      }
      return getAccountJettons;
    }()
  }]);
}();
exports.TonAPI = TonAPI;
