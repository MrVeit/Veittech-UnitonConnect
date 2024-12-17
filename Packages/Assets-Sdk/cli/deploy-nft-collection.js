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
var common_1 = require("./common");
var inquirer_1 = __importDefault(require("inquirer"));
var promises_1 = require("fs/promises");
var core_1 = require("@ton/core");
function promptForUserInput(_x) {
  return _promptForUserInput.apply(this, arguments);
}
function _promptForUserInput() {
  _promptForUserInput = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(params) {
    var defaultRoyaltyRecipient, _yield$inquirer_1$def, name, description, image, type, formattedImage, formattedDescription, royaltyParams, _yield$inquirer_1$def2, royalty, royaltyRecipient, royaltyDenominator, royaltyNumerator;
    return _regeneratorRuntime().wrap(function _callee2$(_context2) {
      while (1) switch (_context2.prev = _context2.next) {
        case 0:
          defaultRoyaltyRecipient = params.defaultRoyaltyRecipient;
          _context2.next = 3;
          return inquirer_1["default"].prompt([{
            name: 'type',
            message: 'Choose collection type',
            type: 'list',
            choices: ['nft', 'sbt']
          }, {
            name: 'name',
            message: 'Enter collection name'
          }, {
            name: 'description',
            message: 'Enter collection description'
          }, {
            name: 'image',
            message: 'Enter image path or link',
            validate: function validate(input) {
              return _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee() {
                var response;
                return _regeneratorRuntime().wrap(function _callee$(_context) {
                  while (1) switch (_context.prev = _context.next) {
                    case 0:
                      if (!(input.startsWith('http://') || input.startsWith('https://'))) {
                        _context.next = 7;
                        break;
                      }
                      _context.next = 3;
                      return fetch(input);
                    case 3:
                      response = _context.sent;
                      if (response.ok) {
                        _context.next = 6;
                        break;
                      }
                      return _context.abrupt("return", 'Image file not found');
                    case 6:
                      return _context.abrupt("return", true);
                    case 7:
                      _context.prev = 7;
                      _context.next = 10;
                      return (0, promises_1.readFile)(input);
                    case 10:
                      return _context.abrupt("return", true);
                    case 13:
                      _context.prev = 13;
                      _context.t0 = _context["catch"](7);
                      return _context.abrupt("return", 'Image file not found');
                    case 16:
                    case "end":
                      return _context.stop();
                  }
                }, _callee, null, [[7, 13]]);
              }))();
            }
          }]);
        case 3:
          _yield$inquirer_1$def = _context2.sent;
          name = _yield$inquirer_1$def.name;
          description = _yield$inquirer_1$def.description;
          image = _yield$inquirer_1$def.image;
          type = _yield$inquirer_1$def.type;
          if (!(image === '')) {
            _context2.next = 12;
            break;
          }
          formattedImage = {
            kind: 'none'
          };
          _context2.next = 20;
          break;
        case 12:
          if (!(image.startsWith('http://') || image.startsWith('https://'))) {
            _context2.next = 16;
            break;
          }
          formattedImage = {
            kind: 'url',
            url: image
          };
          _context2.next = 20;
          break;
        case 16:
          _context2.next = 18;
          return (0, promises_1.readFile)(image);
        case 18:
          _context2.t0 = _context2.sent;
          formattedImage = {
            kind: 'file',
            file: _context2.t0
          };
        case 20:
          if (typeof description === 'string' && description !== '') {
            formattedDescription = description;
          }
          if (!(type === 'nft')) {
            _context2.next = 30;
            break;
          }
          _context2.next = 24;
          return inquirer_1["default"].prompt([{
            name: 'royalty',
            message: 'Enter royalty percentage (eg. 5%)',
            "default": '5%',
            type: 'number',
            validate: function validate(input) {
              var royalty = parseFloat(input);
              if (isNaN(royalty) || royalty < 0 || royalty > 100) {
                return 'Royalty must be a number between 0 and 100';
              }
              return true;
            }
          }, {
            name: 'royaltyRecipient',
            message: 'Enter royalty recipient address (default: your wallet address)',
            "default": defaultRoyaltyRecipient
          }]);
        case 24:
          _yield$inquirer_1$def2 = _context2.sent;
          royalty = _yield$inquirer_1$def2.royalty;
          royaltyRecipient = _yield$inquirer_1$def2.royaltyRecipient;
          // converting royalty to fraction
          royaltyDenominator = 10000n;
          royaltyNumerator = BigInt(Math.round(parseFloat(royalty) * Number(royaltyDenominator) / 100));
          royaltyParams = {
            numerator: royaltyNumerator,
            denominator: royaltyDenominator,
            recipient: core_1.Address.parse(royaltyRecipient)
          };
        case 30:
          return _context2.abrupt("return", {
            name: name,
            description: formattedDescription,
            image: formattedImage,
            type: type,
            royaltyParams: royaltyParams
          });
        case 31:
        case "end":
          return _context2.stop();
      }
    }, _callee2);
  }));
  return _promptForUserInput.apply(this, arguments);
}
function main() {
  return _main.apply(this, arguments);
}
function _main() {
  _main = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee3() {
    var _yield, sdk, network, sender, _yield$promptForUserI, name, description, image, type, royaltyParams, uploadedImage, collectionContent, createdCollection, collectionParams, collectionInfo;
    return _regeneratorRuntime().wrap(function _callee3$(_context3) {
      while (1) switch (_context3.prev = _context3.next) {
        case 0:
          _context3.next = 2;
          return (0, common_1.createEnv)();
        case 2:
          _yield = _context3.sent;
          sdk = _yield.sdk;
          network = _yield.network;
          sender = _yield.sender;
          _context3.next = 8;
          return promptForUserInput({
            defaultRoyaltyRecipient: (0, common_1.formatAddress)(sender.address, network)
          });
        case 8:
          _yield$promptForUserI = _context3.sent;
          name = _yield$promptForUserI.name;
          description = _yield$promptForUserI.description;
          image = _yield$promptForUserI.image;
          type = _yield$promptForUserI.type;
          royaltyParams = _yield$promptForUserI.royaltyParams;
          if (!(image.kind === 'url')) {
            _context3.next = 18;
            break;
          }
          uploadedImage = image.url;
          _context3.next = 25;
          break;
        case 18:
          if (!(image.kind === 'file')) {
            _context3.next = 24;
            break;
          }
          _context3.next = 21;
          return (0, common_1.retry)(function () {
            return sdk.storage.uploadFile(image.file);
          }, {
            name: 'upload image'
          });
        case 21:
          uploadedImage = _context3.sent;
          _context3.next = 25;
          break;
        case 24:
          uploadedImage = undefined;
        case 25:
          collectionContent = {
            commonContent: '',
            collectionContent: {
              name: name,
              description: description,
              image: uploadedImage
            }
          };
          if (!(type === 'nft')) {
            _context3.next = 33;
            break;
          }
          collectionParams = {
            royaltyParams: royaltyParams
          };
          _context3.next = 30;
          return sdk.deployNftCollection(collectionContent, collectionParams);
        case 30:
          createdCollection = _context3.sent;
          _context3.next = 40;
          break;
        case 33:
          if (!(type === 'sbt')) {
            _context3.next = 39;
            break;
          }
          _context3.next = 36;
          return sdk.deploySbtCollection(collectionContent);
        case 36:
          createdCollection = _context3.sent;
          _context3.next = 40;
          break;
        case 39:
          throw new Error("Unknown collection type: ".concat(type));
        case 40:
          collectionInfo = {
            name: name,
            description: description,
            image: uploadedImage,
            'collection address': createdCollection.address
          };
          (0, common_1.printInfo)(collectionInfo, network);
        case 42:
        case "end":
          return _context3.stop();
      }
    }, _callee3);
  }));
  return _main.apply(this, arguments);
}
exports.main = main;