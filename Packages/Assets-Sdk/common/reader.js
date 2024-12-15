"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.reader = void 0;
var fs_1 = require("fs");
var path_1 = require("path");
/**
 * Creates a reader that reads files from the given base path.
 * @param basePath The base path to read files from.
 * @returns A reader that reads files from the given base path.
 */
function reader(basePath) {
  return function (path) {
    return (0, fs_1.readFileSync)((0, path_1.resolve)(basePath, path)).toString();
  };
}
exports.reader = reader;
