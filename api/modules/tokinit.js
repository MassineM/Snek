//TODO iughdghdifhgidfhgiufd

module.exports = (length) => {
  var randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-abcdefghijklmnopqrstuvwxyz";
  var token = "";
  for (var i = 0; i < length; i++) {
    token += randomChars.charAt(Math.floor(Math.random() * randomChars.length));
  }
  return token;
};
