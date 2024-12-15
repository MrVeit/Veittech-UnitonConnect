"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.writer = void 0;
var fs_1 = require("fs");
var path_1 = require("path");
/**
 * Creates a writer that writes files to the given base path.
 * @param basePath The base path to write files to.
 * @returns A writer that writes files to the given base path.
 */
function writer(basePath) {
  return function (path, content) {
    return (0, fs_1.writeFileSync)((0, path_1.resolve)(basePath, path), content);
  };
}
exports.writer = writer;