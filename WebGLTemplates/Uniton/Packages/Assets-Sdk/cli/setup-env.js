"use strict";

function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function _regeneratorRuntime() { "use strict"; /*! regenerator-runtime -- Copyright (c) 2014-present, Facebook, Inc. -- license (MIT): https://github.com/facebook/regenerator/blob/main/LICENSE */ _regeneratorRuntime = function _regeneratorRuntime() { return e; }; var t, e = {}, r = Object.prototype, n = r.hasOwnProperty, o = Object.defineProperty || function (t, e, r) { t[e] = r.value; }, i = "function" == typeof Symbol ? Symbol : {}, a = i.iterator || "@@iterator", c = i.asyncIterator || "@@asyncIterator", u = i.toStringTag || "@@toStringTag"; function define(t, e, r) { return Object.defineProperty(t, e, { value: r, enumerable: !0, configurable: !0, writable: !0 }), t[e]; } try { define({}, ""); } catch (t) { define = function define(t, e, r) { return t[e] = r; }; } function wrap(t, e, r, n) { var i = e && e.prototype instanceof Generator ? e : Generator, a = Object.create(i.prototype), c = new Context(n || []); return o(a, "_invoke", { value: makeInvokeMethod(t, r, c) }), a; } function tryCatch(t, e, r) { try { return { type: "normal", arg: t.call(e, r) }; } catch (t) { return { type: "throw", arg: t }; } } e.wrap = wrap; var h = "suspendedStart", l = "suspendedYield", f = "executing", s = "completed", y = {}; function Generator() {} function GeneratorFunction() {} function GeneratorFunctionPrototype() {} var p = {}; define(p, a, function () { return this; }); var d = Object.getPrototypeOf, v = d && d(d(values([]))); v && v !== r && n.call(v, a) && (p = v); var g = GeneratorFunctionPrototype.prototype = Generator.prototype = Object.create(p); function defineIteratorMethods(t) { ["next", "throw", "return"].forEach(function (e) { define(t, e, function (t) { return this._invoke(e, t); }); }); } function AsyncIterator(t, e) { function invoke(r, o, i, a) { var c = tryCatch(t[r], t, o); if ("throw" !== c.type) { var u = c.arg, h = u.value; return h && "object" == _typeof(h) && n.call(h, "__await") ? e.resolve(h.__await).then(function (t) { invoke("next", t, i, a); }, function (t) { invoke("throw", t, i, a); }) : e.resolve(h).then(function (t) { u.value = t, i(u); }, function (t) { return invoke("throw", t, i, a); }); } a(c.arg); } var r; o(this, "_invoke", { value: function value(t, n) { function callInvokeWithMethodAndArg() { return new e(function (e, r) { invoke(t, n, e, r); }); } return r = r ? r.then(callInvokeWithMethodAndArg, callInvokeWithMethodAndArg) : callInvokeWithMethodAndArg(); } }); } function makeInvokeMethod(e, r, n) { var o = h; return function (i, a) { if (o === f) throw Error("Generator is already running"); if (o === s) { if ("throw" === i) throw a; return { value: t, done: !0 }; } for (n.method = i, n.arg = a;;) { var c = n.delegate; if (c) { var u = maybeInvokeDelegate(c, n); if (u) { if (u === y) continue; return u; } } if ("next" === n.method) n.sent = n._sent = n.arg;else if ("throw" === n.method) { if (o === h) throw o = s, n.arg; n.dispatchException(n.arg); } else "return" === n.method && n.abrupt("return", n.arg); o = f; var p = tryCatch(e, r, n); if ("normal" === p.type) { if (o = n.done ? s : l, p.arg === y) continue; return { value: p.arg, done: n.done }; } "throw" === p.type && (o = s, n.method = "throw", n.arg = p.arg); } }; } function maybeInvokeDelegate(e, r) { var n = r.method, o = e.iterator[n]; if (o === t) return r.delegate = null, "throw" === n && e.iterator["return"] && (r.method = "return", r.arg = t, maybeInvokeDelegate(e, r), "throw" === r.method) || "return" !== n && (r.method = "throw", r.arg = new TypeError("The iterator does not provide a '" + n + "' method")), y; var i = tryCatch(o, e.iterator, r.arg); if ("throw" === i.type) return r.method = "throw", r.arg = i.arg, r.delegate = null, y; var a = i.arg; return a ? a.done ? (r[e.resultName] = a.value, r.next = e.nextLoc, "return" !== r.method && (r.method = "next", r.arg = t), r.delegate = null, y) : a : (r.method = "throw", r.arg = new TypeError("iterator result is not an object"), r.delegate = null, y); } function pushTryEntry(t) { var e = { tryLoc: t[0] }; 1 in t && (e.catchLoc = t[1]), 2 in t && (e.finallyLoc = t[2], e.afterLoc = t[3]), this.tryEntries.push(e); } function resetTryEntry(t) { var e = t.completion || {}; e.type = "normal", delete e.arg, t.completion = e; } function Context(t) { this.tryEntries = [{ tryLoc: "root" }], t.forEach(pushTryEntry, this), this.reset(!0); } function values(e) { if (e || "" === e) { var r = e[a]; if (r) return r.call(e); if ("function" == typeof e.next) return e; if (!isNaN(e.length)) { var o = -1, i = function next() { for (; ++o < e.length;) if (n.call(e, o)) return next.value = e[o], next.done = !1, next; return next.value = t, next.done = !0, next; }; return i.next = i; } } throw new TypeError(_typeof(e) + " is not iterable"); } return GeneratorFunction.prototype = GeneratorFunctionPrototype, o(g, "constructor", { value: GeneratorFunctionPrototype, configurable: !0 }), o(GeneratorFunctionPrototype, "constructor", { value: GeneratorFunction, configurable: !0 }), GeneratorFunction.displayName = define(GeneratorFunctionPrototype, u, "GeneratorFunction"), e.isGeneratorFunction = function (t) { var e = "function" == typeof t && t.constructor; return !!e && (e === GeneratorFunction || "GeneratorFunction" === (e.displayName || e.name)); }, e.mark = function (t) { return Object.setPrototypeOf ? Object.setPrototypeOf(t, GeneratorFunctionPrototype) : (t.__proto__ = GeneratorFunctionPrototype, define(t, u, "GeneratorFunction")), t.prototype = Object.create(g), t; }, e.awrap = function (t) { return { __await: t }; }, defineIteratorMethods(AsyncIterator.prototype), define(AsyncIterator.prototype, c, function () { return this; }), e.AsyncIterator = AsyncIterator, e.async = function (t, r, n, o, i) { void 0 === i && (i = Promise); var a = new AsyncIterator(wrap(t, r, n, o), i); return e.isGeneratorFunction(r) ? a : a.next().then(function (t) { return t.done ? t.value : a.next(); }); }, defineIteratorMethods(g), define(g, u, "Generator"), define(g, a, function () { return this; }), define(g, "toString", function () { return "[object Generator]"; }), e.keys = function (t) { var e = Object(t), r = []; for (var n in e) r.push(n); return r.reverse(), function next() { for (; r.length;) { var t = r.pop(); if (t in e) return next.value = t, next.done = !1, next; } return next.done = !0, next; }; }, e.values = values, Context.prototype = { constructor: Context, reset: function reset(e) { if (this.prev = 0, this.next = 0, this.sent = this._sent = t, this.done = !1, this.delegate = null, this.method = "next", this.arg = t, this.tryEntries.forEach(resetTryEntry), !e) for (var r in this) "t" === r.charAt(0) && n.call(this, r) && !isNaN(+r.slice(1)) && (this[r] = t); }, stop: function stop() { this.done = !0; var t = this.tryEntries[0].completion; if ("throw" === t.type) throw t.arg; return this.rval; }, dispatchException: function dispatchException(e) { if (this.done) throw e; var r = this; function handle(n, o) { return a.type = "throw", a.arg = e, r.next = n, o && (r.method = "next", r.arg = t), !!o; } for (var o = this.tryEntries.length - 1; o >= 0; --o) { var i = this.tryEntries[o], a = i.completion; if ("root" === i.tryLoc) return handle("end"); if (i.tryLoc <= this.prev) { var c = n.call(i, "catchLoc"), u = n.call(i, "finallyLoc"); if (c && u) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } else if (c) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); } else { if (!u) throw Error("try statement without catch or finally"); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } } } }, abrupt: function abrupt(t, e) { for (var r = this.tryEntries.length - 1; r >= 0; --r) { var o = this.tryEntries[r]; if (o.tryLoc <= this.prev && n.call(o, "finallyLoc") && this.prev < o.finallyLoc) { var i = o; break; } } i && ("break" === t || "continue" === t) && i.tryLoc <= e && e <= i.finallyLoc && (i = null); var a = i ? i.completion : {}; return a.type = t, a.arg = e, i ? (this.method = "next", this.next = i.finallyLoc, y) : this.complete(a); }, complete: function complete(t, e) { if ("throw" === t.type) throw t.arg; return "break" === t.type || "continue" === t.type ? this.next = t.arg : "return" === t.type ? (this.rval = this.arg = t.arg, this.method = "return", this.next = "end") : "normal" === t.type && e && (this.next = e), y; }, finish: function finish(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.finallyLoc === t) return this.complete(r.completion, r.afterLoc), resetTryEntry(r), y; } }, "catch": function _catch(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.tryLoc === t) { var n = r.completion; if ("throw" === n.type) { var o = n.arg; resetTryEntry(r); } return o; } } throw Error("illegal catch attempt"); }, delegateYield: function delegateYield(e, r, n) { return this.delegate = { iterator: values(e), resultName: r, nextLoc: n }, "next" === this.method && (this.arg = t), y; } }, e; }
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
exports.main = void 0;
var inquirer_1 = __importDefault(require("inquirer"));
var crypto_1 = require("@ton/crypto");
var promises_1 = require("fs/promises");
var __1 = require("../");
var common_1 = require("./common");
var key_1 = require("../key");
function main() {
  return _main.apply(this, arguments);
}
function _main() {
  _main = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee() {
    var _yield$inquirer_1$def, network, wallet, mnemonic, pairs, keyPair, walletContract, address, _yield$inquirer_1$def2, storage, q, _q, _yield$inquirer_1$def3, ipfsGateway, _yield$inquirer_1$def4, gateway, _yield$inquirer_1$def5, _gateway, apikey;
    return _regeneratorRuntime().wrap(function _callee$(_context) {
      while (1) switch (_context.prev = _context.next) {
        case 0:
          _context.next = 2;
          return inquirer_1["default"].prompt([{
            name: 'network',
            message: 'Which network to use?',
            choices: ['mainnet', 'testnet'],
            type: 'list'
          }, {
            name: 'wallet',
            message: 'Which wallet type to use?',
            choices: ['highload-v2'],
            type: 'list'
          }]);
        case 2:
          _yield$inquirer_1$def = _context.sent;
          network = _yield$inquirer_1$def.network;
          wallet = _yield$inquirer_1$def.wallet;
          _context.next = 7;
          return (0, crypto_1.mnemonicNew)();
        case 7:
          mnemonic = _context.sent;
          pairs = [['NETWORK', network], ['MNEMONIC', mnemonic.join(' ')], ['WALLET_TYPE', wallet]];
          _context.next = 11;
          return (0, key_1.importKey)(mnemonic);
        case 11:
          keyPair = _context.sent;
          _context.next = 14;
          return (0, __1.createWallet)(wallet, keyPair.publicKey);
        case 14:
          walletContract = _context.sent;
          address = walletContract.address;
          _context.next = 18;
          return inquirer_1["default"].prompt([{
            name: 'storage',
            message: 'Which storage to use?',
            choices: ['pinata', 's3'],
            type: 'list'
          }]);
        case 18:
          _yield$inquirer_1$def2 = _context.sent;
          storage = _yield$inquirer_1$def2.storage;
          pairs.push(['STORAGE_TYPE', storage]);
          if (!(storage === 'pinata')) {
            _context.next = 28;
            break;
          }
          _context.next = 24;
          return inquirer_1["default"].prompt([{
            name: 'apikey',
            message: 'Please enter your Pinata API key'
          }, {
            name: 'secretkey',
            message: 'Please enter your Pinata secret key'
          }]);
        case 24:
          q = _context.sent;
          pairs.push(['PINATA_API_KEY', q.apikey], ['PINATA_SECRET_KEY', q.secretkey]);
          _context.next = 36;
          break;
        case 28:
          if (!(storage === 's3')) {
            _context.next = 35;
            break;
          }
          _context.next = 31;
          return inquirer_1["default"].prompt([{
            name: 'accesskeyid',
            message: 'Please enter your S3 access key ID'
          }, {
            name: 'secretaccesskey',
            message: 'Please enter your S3 secret access key'
          }, {
            name: 'bucket',
            message: 'Please enter the S3 bucket name to use'
          }]);
        case 31:
          _q = _context.sent;
          pairs.push(['S3_ACCESS_KEY_ID', _q.accesskeyid], ['S3_SECRET_ACCESS_KEY', _q.secretaccesskey], ['S3_BUCKET', _q.bucket]);
          _context.next = 36;
          break;
        case 35:
          throw new Error("Unknown storage type: ".concat(storage));
        case 36:
          _context.next = 38;
          return inquirer_1["default"].prompt([{
            name: 'ipfsGateway',
            message: 'Which IPFS gateway to use?',
            choices: ['pinata', 'ipfs.io', 'https'],
            type: 'list'
          }]);
        case 38:
          _yield$inquirer_1$def3 = _context.sent;
          ipfsGateway = _yield$inquirer_1$def3.ipfsGateway;
          pairs.push(['IPFS_GATEWAY_TYPE', ipfsGateway]);
          if (!(ipfsGateway === 'ipfs.io')) {
            _context.next = 45;
            break;
          }
          pairs.push(['IPFS_GATEWAY', 'https://ipfs.io/']);
          _context.next = 63;
          break;
        case 45:
          if (!(ipfsGateway === 'https')) {
            _context.next = 53;
            break;
          }
          _context.next = 48;
          return inquirer_1["default"].prompt([{
            name: 'gateway',
            message: 'Please enter the IPFS gateway to use (e.g. https://ipfs.io/)'
          }]);
        case 48:
          _yield$inquirer_1$def4 = _context.sent;
          gateway = _yield$inquirer_1$def4.gateway;
          pairs.push(['IPFS_GATEWAY', gateway]);
          _context.next = 63;
          break;
        case 53:
          if (!(ipfsGateway === 'pinata')) {
            _context.next = 62;
            break;
          }
          _context.next = 56;
          return inquirer_1["default"].prompt([{
            name: 'gateway',
            message: 'Please enter the IPFS gateway to use (e.g. https://gateway.pinata.cloud/)'
          }, {
            name: 'apikey',
            message: 'Please enter your Pinata Gateway API key'
          }]);
        case 56:
          _yield$inquirer_1$def5 = _context.sent;
          _gateway = _yield$inquirer_1$def5.gateway;
          apikey = _yield$inquirer_1$def5.apikey;
          pairs.push(['IPFS_GATEWAY', _gateway], ['IPFS_GATEWAY_API_KEY', apikey]);
          _context.next = 63;
          break;
        case 62:
          throw new Error("Unknown IPFS gateway type: ".concat(ipfsGateway));
        case 63:
          _context.prev = 63;
          _context.next = 66;
          return (0, promises_1.writeFile)('.env', pairs.map(function (p) {
            return "".concat(p[0], "=\"").concat(p[1], "\"");
          }).join('\n'), {
            flag: 'wx'
          });
        case 66:
          _context.next = 73;
          break;
        case 68:
          _context.prev = 68;
          _context.t0 = _context["catch"](63);
          console.error(_context.t0);
          console.log('Could not write the .env file. Does it already exist?');
          return _context.abrupt("return");
        case 73:
          (0, common_1.printAddress)(address, network);
          if (network === 'testnet') {
            console.log('Please use https://t.me/testgiver_ton_bot to get some test TON');
          } else {
            console.log('Please use top up your wallet with some TON');
          }
        case 75:
        case "end":
          return _context.stop();
      }
    }, _callee, null, [[63, 68]]);
  }));
  return _main.apply(this, arguments);
}
exports.main = main;