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
exports.bufferToStr = exports.decodeImage = exports.decodeSimpleFields = exports.loadFullContent = exports.DefaultContentResolver = void 0;
var core_1 = require("@ton/core");
var crypto_1 = require("@ton/crypto");
var DefaultContentResolver = /*#__PURE__*/function () {
  function DefaultContentResolver(ipfsGateway) {
    _classCallCheck(this, DefaultContentResolver);
    this.ipfsGateway = ipfsGateway !== null && ipfsGateway !== void 0 ? ipfsGateway : function (id) {
      return "https://ipfs.io/ipfs/".concat(id);
    };
  }
  return _createClass(DefaultContentResolver, [{
    key: "resolve",
    value: function () {
      var _resolve = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee(url) {
        return _regeneratorRuntime().wrap(function _callee$(_context) {
          while (1) switch (_context.prev = _context.next) {
            case 0:
              if (url.startsWith('ipfs://')) {
                url = this.ipfsGateway(url.slice(7));
              }
              if (url.startsWith('https://') || url.startsWith('http://')) {
                _context.next = 3;
                break;
              }
              throw new Error('Unknown URL: ' + url);
            case 3:
              _context.t0 = Buffer;
              _context.next = 6;
              return fetch(url);
            case 6:
              _context.next = 8;
              return _context.sent.arrayBuffer();
            case 8:
              _context.t1 = _context.sent;
              return _context.abrupt("return", _context.t0.from.call(_context.t0, _context.t1));
            case 10:
            case "end":
              return _context.stop();
          }
        }, _callee, this);
      }));
      function resolve(_x) {
        return _resolve.apply(this, arguments);
      }
      return resolve;
    }()
  }]);
}();
exports.DefaultContentResolver = DefaultContentResolver;
function loadSnake(s) {
  var b = [];
  while (s.remainingBits > 0 || s.remainingRefs > 0) {
    if (s.remainingBits % 8 !== 0) {
      throw new Error('Slice must contain an integer number of bytes');
    }
    b.push(s.loadBuffer(s.remainingBits / 8));
    if (s.remainingRefs === 1) {
      s = s.loadRef().beginParse();
    } else if (s.remainingRefs > 1) {
      throw new Error('Slice must contain at most 1 ref');
    }
  }
  return Buffer.concat(b);
}
var BufferValue = {
  serialize: function serialize() {
    throw new Error('Buffer serialization is not supported');
  },
  parse: function parse(src) {
    var r = src.loadRef().beginParse();
    if (r.remainingBits % 8 !== 0) {
      throw new Error('Slice must contain an integer number of bytes');
    }
    if (r.remainingRefs !== 0) {
      throw new Error('Slice must not contain refs');
    }
    return r.loadBuffer(r.remainingBits / 8);
  }
};
function loadChunked(s) {
  var d = s.loadDict(core_1.Dictionary.Keys.Uint(32), BufferValue);
  var b = [];
  for (var i = 0; i < d.size; i++) {
    var cb = d.get(i);
    if (cb === undefined) {
      throw new Error('Dict must contain sequential keys');
    }
    b.push(cb);
  }
  return Buffer.concat(b);
}
var ContentDataValue = {
  serialize: function serialize() {
    throw new Error('ContentData serialization is not supported');
  },
  parse: function parse(src) {
    var r = src.loadRef().beginParse();
    var type = r.loadUint(8);
    if (type === 0x00) {
      return loadSnake(r);
    } else if (type === 0x01) {
      return loadChunked(r);
    } else {
      throw new Error('Unknown ContentData type: ' + type);
    }
  }
};
function bufferToObj(b) {
  var parsed = JSON.parse(b.toString('utf-8'));
  if (_typeof(parsed) !== 'object') {
    throw new Error('Data must be an object');
  }
  return parsed;
}
function hashKey(key) {
  return BigInt('0x' + (0, crypto_1.sha256_sync)(key).toString('hex'));
}
function loadFullContent(_x2, _x3) {
  return _loadFullContent.apply(this, arguments);
}
function _loadFullContent() {
  _loadFullContent = _asyncToGenerator(/*#__PURE__*/_regeneratorRuntime().mark(function _callee2(data, contentResolver) {
    var ds, type, _data, uri, uriStr, offchain, _uri, _data2;
    return _regeneratorRuntime().wrap(function _callee2$(_context2) {
      while (1) switch (_context2.prev = _context2.next) {
        case 0:
          ds = data.beginParse();
          type = ds.loadUint(8);
          if (!(type === 0x00)) {
            _context2.next = 14;
            break;
          }
          _data = ds.loadDict(core_1.Dictionary.Keys.BigUint(256), ContentDataValue);
          uri = _data.get(hashKey('uri'));
          if (!(uri !== undefined)) {
            _context2.next = 11;
            break;
          }
          uriStr = uri.toString('utf-8');
          _context2.next = 9;
          return contentResolver.resolve(uriStr);
        case 9:
          offchain = _context2.sent;
          return _context2.abrupt("return", {
            type: 'semichain',
            offchainFields: bufferToObj(offchain),
            onchainFields: _data,
            offchainUrl: uriStr
          });
        case 11:
          return _context2.abrupt("return", {
            type: 'onchain',
            onchainFields: _data
          });
        case 14:
          if (!(type === 0x01)) {
            _context2.next = 22;
            break;
          }
          _uri = ds.loadStringTail();
          _context2.next = 18;
          return contentResolver.resolve(_uri);
        case 18:
          _data2 = _context2.sent;
          return _context2.abrupt("return", {
            type: 'offchain',
            offchainFields: bufferToObj(_data2),
            offchainUrl: _uri
          });
        case 22:
          throw new Error('Unknown FullContent type: ' + type);
        case 23:
        case "end":
          return _context2.stop();
      }
    }, _callee2);
  }));
  return _loadFullContent.apply(this, arguments);
}
exports.loadFullContent = loadFullContent;
function decodeSimpleFields(dc, parsers) {
  var out = {};
  for (var k in parsers) {
    if (dc.onchainFields !== undefined) {
      var h = hashKey(k);
      var v = dc.onchainFields.get(h);
      if (v !== undefined) {
        out[k] = parsers[k].onchain(v);
        dc.onchainFields["delete"](h);
        continue;
      }
    }
    if (dc.offchainFields !== undefined) {
      if (k in dc.offchainFields) {
        out[k] = parsers[k].offchain(dc.offchainFields[k]);
        delete dc.offchainFields[k];
      }
    }
  }
  return out;
}
exports.decodeSimpleFields = decodeSimpleFields;
function decodeImage(dc) {
  if (dc.onchainFields !== undefined && dc.onchainFields.has(hashKey('image')) && dc.onchainFields.has(hashKey('image_data'))) {
    throw new Error('Onchain fields contain both image and image_data');
  }
  if (dc.offchainFields !== undefined && 'image' in dc.offchainFields && 'image_data' in dc.offchainFields) {
    throw new Error('Offchain fields contain both image and image_data');
  }
  if (dc.onchainFields !== undefined) {
    var image = dc.onchainFields.get(hashKey('image'));
    if (image !== undefined) {
      dc.onchainFields["delete"](hashKey('image'));
      return image.toString('utf-8');
    }
    var imageData = dc.onchainFields.get(hashKey('image_data'));
    if (imageData !== undefined) {
      dc.onchainFields["delete"](hashKey('image_data'));
      return imageData;
    }
  }
  if (dc.offchainFields !== undefined) {
    if ('image' in dc.offchainFields) {
      var _image = dc.offchainFields.image;
      if (typeof _image !== 'string') {
        throw new Error('Image URI must be a string');
      }
      delete dc.offchainFields.image;
      return _image;
    }
    if ('image_data' in dc.offchainFields) {
      var _imageData = dc.offchainFields.image_data;
      if (typeof _imageData !== 'string') {
        throw new Error('Offchain image data must be a string');
      }
      delete dc.offchainFields.image_data;
      return Buffer.from(_imageData, 'base64');
    }
  }
  return undefined;
}
exports.decodeImage = decodeImage;
var bufferToStr = function bufferToStr(b) {
  return b.toString('utf-8');
};
exports.bufferToStr = bufferToStr;