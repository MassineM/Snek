const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const UsersSchema = mongoose.Schema({
  username: {
    type: String,
    required: true,
  },
  email: {
    type: String,
    required: true,
  },
  password: {
    type: String,
    required: true,
  },
  validation_token: {
    type: String,
    default: null,
  },
  role: {
    type: String,
    default: "user",
  },
  score: {
    type: Schema.Types.ObjectId,
    ref: "Score",
  },
  level: {
    type: Number,
    default: 1,
  },
});

module.exports = mongoose.model("Users", UsersSchema);
