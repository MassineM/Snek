const express = require("express");
const router = express.Router();
const mongoose = require("mongoose");
const User = require("../models/users");
const { debug } = require("../modules/dumper");

// ? GET ALL USERS
router.get("/users", adminOnly, async (req, res) => {
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
    const users = await User.find(query)
      .sort(sort)
      .limit(limit)
      .skip(offset)
      .populate("score");
    const total_count = await User.countDocuments(query);
    if (users.length === 0) {
      debug("NO USER FOUND");
      return res.status(200).json({
        success: false,
        message: "NO USER FOUND",
      });
    }
    debug("GET USERS SUCCESSFULLY", true);
    return res.status(200).json({
      users: users,
      total_count: total_count,
      success: true,
      message: "GET USERS SUCCESSFULLY",
    });
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

// ? GET A SINGLE USER BY ID
router.get("/user/:id", userOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.params.id)) {
    debug("INVALID ID", false);
    return res.status(400).json({
      success: false,
      message: "INVALID ID",
    });
  }

  try {
    const user = await User.findById(req.params.id).populate("score");
    if (!user) {
      debug("NO USER FOUND");
      return res.status(204).json({
        success: false,
        message: "NO USER FOUND",
      });
    }
    return res.status(200).json({
      user: user,
      success: true,
      message: "GET USER BY ID SUCCESSFUL",
    });
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

// ? DELETE A SINGLE USER
router.delete("/user/:id", adminOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.params.id)) {
    debug("INVALID ID", false);
    return res.status(400).json({
      success: false,
      message: "INVALID ID",
    });
  }

  if (req.decodedToken.user.id == req.params.id) {
    debug("ADMINS CANNOT DELETE THEMSELVES", false);
    return res
      .status(400)
      .json({ success: false, message: "ADMINS CANNOT DELETE THEMSELVES" });
  }
  try {
    const user = await User.findById(req.params.id);
    if (!user) {
      debug("NO USER FOUND");
      return res.status(204).json({
        success: false,
        message: "NO USER FOUND",
      });
    }

    await User.deleteOne({ _id: req.params.id });
    debug("USER DELETED SUCCESSFULLY", true);
    return res.status(204).json("toto");
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

// ? UPDATE A SINGLE USER
router.patch("/user", userOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.body._id) || !req.body._id) {
    debug("INVALID ID", false);
    return res.status(400).json({
      success: false,
      message: "INVALID ID",
    });
  }
  try {
    const user = await User.findById(req.body._id).populate("score");
    if (!user) {
      debug("NO USER FOUND");
      return res.status(400).json({
        success: false,
        message: "NO USER FOUND",
      });
    }
    for (let key of Object.keys(req.body)) user[key] = req.body[key];

    const updated = await user.save();

    debug("USER UPDATED SUCCESSFULLY", true);
    return res.status(200).json({
      updated: updated,
      success: true,
      message: "USER UPDATED SUCCESSFULLY",
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
