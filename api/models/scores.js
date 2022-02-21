const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const ScoreSchema = mongoose.Schema({
  user: {
    type: Schema.Types.ObjectId,
    ref: "User",
  },
  score: {
    type: Number,
    default: 0,
  },
});

module.exports = mongoose.model("Scores", ScoreSchema);
