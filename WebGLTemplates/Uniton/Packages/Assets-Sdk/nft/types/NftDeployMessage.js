"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.loadNftDeployMessage = exports.storeNftDeployMessage = void 0;
function storeNftDeployMessage(message) {
  return function (builder) {
    var owner = message.owner,
      content = message.content;
    builder.storeAddress(owner).storeRef(content);
  };
}
exports.storeNftDeployMessage = storeNftDeployMessage;
function loadNftDeployMessage(slice) {
  var owner = slice.loadAddress();
  var content = slice.loadRef();
  return {
    owner: owner,
    content: content
  };
}
exports.loadNftDeployMessage = loadNftDeployMessage;