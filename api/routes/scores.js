const express = require("express");
const router = express.Router();
const mongoose = require("mongoose");
const Score = require("../models/scores");
const { debug } = require("../modules/dumper");

// ? GET ALL scoreS
router.get("/scores", adminOnly, async (req, res) => {
  const limit = parseInt(req.query.limit || 10);
  const offset = parseInt(req.query.offset || 0);
  let sort = {};
  let query = {};

  if (req.query.sortBy && req.query.sortDesc)
    if (req.query.sortBy == "firstname")
      sort = { firstname: req.query.sortDesc == "true" ? -1 : 1 };

  if (req.query.filterBy)
    query.$or = [{ firstname: { $regex: req.query.filterBy, $options: "i" } }];

  try {
    const scores = await Score.find(query)
      .sort(sort)
      .limit(limit)
      .skip(offset)
      .populate("user");
    const total_count = await Score.countDocuments(query);
    if (scores.length === 0) {
      debug("NO SCORE FOUND");
      return res.status(200).json({
        success: false,
        message: "NO SCORE FOUND",
      });
    }
    debug("GET scoreS SUCCESSFULLY", true);
    return res.status(200).json({
      scores: scores,
      total_count: total_count,
      success: true,
      message: "GET SCORES SUCCESSFULLY",
    });
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

// ? GET A SINGLE score BY ID
router.get("/score/:id", userOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.params.id)) {
    debug("INVALID ID", false);
    return res.status(400).json({
      success: false,
      message: "INVALID ID",
    });
  }

  try {
    const score = await Score.findById(req.params.id).populate("user");
    if (!score) {
      debug("NO SCORE FOUND");
      return res.status(204).json({
        success: false,
        message: "NO SCORE FOUND",
      });
    }
    return res.status(200).json({
      score: score,
      success: true,
      message: "GET SCORE BY ID SUCCESSFUL",
    });
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

// ? DELETE A SINGLE score
router.delete("/score/:id", adminOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.params.id)) {
    debug("INVALID ID", false);
    return res.status(400).json({
      success: false,
      message: "INVALID ID",
    });
  }

  if (req.decodedToken.score.id == req.params.id) {
    debug("ADMINS CANNOT DELETE THEMSELVES", false);
    return res
      .status(400)
      .json({ success: false, message: "ADMINS CANNOT DELETE THEMSELVES" });
  }
  try {
    const score = await Score.findById(req.params.id);
    if (!score) {
      debug("NO SCORE FOUND");
      return res.status(204).json({
        success: false,
        message: "NO SCORE FOUND",
      });
    }

    await score.deleteOne({ _id: req.params.id });
    debug("SCORE DELETED SUCCESSFULLY", true);
    return res.status(200).json("SCORE DELETED SUCCESSFULLY");
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

// ? UPDATE A SINGLE score
router.patch("/score", userOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.body._id) || !req.body._id) {
    debug("INVALID ID", false);
    return res.status(400).json({
      success: false,
      message: "INVALID ID",
    });
  }
  try {
    const score = await Score.findById(req.body._id).populate("user");
    if (!score) {
      debug("NO score FOUND");
      return res.status(400).json({
        success: false,
        message: "NO SCORE FOUND",
      });
    }
    for (let key of Object.keys(req.body)) score[key] = req.body[key];

    const updated = await score.save();

    debug("score UPDATED SUCCESSFULLY", true);
    return res.status(200).json({
      updated: updated,
      success: true,
      message: "SCORE UPDATED SUCCESSFULLY",
    });
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

module.exports = router;
