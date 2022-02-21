let express = require("express");
const mongoose = require("mongoose");
const fs = require("fs");

// const tutos = require("../models/tutos");
require("../routes/security");
let router = express.Router();
const User = require("../models/users");
const File = require("../models/files");

/* Modules */
const upload = require("../modules/uploader");
const fileProcess = require("../modules/fileProcessing");

const typeOf = (mimeType) => {
  if (mimeType.includes("image")) {
    return "image";
  } else if (mimeType.includes("audio")) {
    return "audio";
  } else {
    return "other";
  }
};

/* SECTION delete a file */
router.delete("/file/:file_id", userOnly, async (req, res) => {
  if (!mongoose.Types.ObjectId.isValid(req.params.file_id)) {
    return res.status(400).json("Invalid file ID");
  }
  let file_to_delete = await File.findById(req.params.file_id);
  if (!file_to_delete) return res.status(404).json("file not found");
  await file_to_delete.delete();
  return res.status(204).json("tuto has been removed");
});

/*SECTION get a file*/
router.get("/my-files", userOnly, async function (req, res) {
  let limit = parseInt(req.query.limit || 0);
  let offset = parseInt(req.query.offset || 0);
  let query = { uploaded_by: req.decodedToken.user.id };
  let sort = {};

  let total_count = await File.countDocuments(query);
  let files = await File.find(query).sort(sort).limit(limit).skip(offset);
  return res.status(200).json({
    files: files,
    total_count: total_count,
  });
});

//SECTION Récupération d'un file par id
router.get("/file/:file_id", async function (req, res) {
  if (!mongoose.Types.ObjectId.isValid(req.params.file_id))
    return res.status(400).json("Invalid file ID");
  let file = await File.findById(req.params.file_id);
  if (!file) return res.status(404).json("file not found");
  return res.status(200).json(file);
});

/* SECTION Upload a file */
router.post("/upload", [userOnly, upload.any()], async (req, res) => {
  let user = req.decodedToken
    ? await User.findById(req.decodedToken.user.id)
    : {};

  // console.log("Received : ", req.uploadedFiles);
  let response = [];
  // console.log('Processing each file ...')
  for (let file of req.uploadedFiles) {
    // console.log("FILE IS ", file);
    let result; // To know if fileProcess was ok & store the result of process
    /* ANCHOR Handle processing depending on file type */
    let type = typeOf(file.type);
    if (type == "image") {
      // console.log(`File ${file.newName} is an image, processing it ...`)
      await fileProcess
        .resizeImage(file.newName)
        .then((filename) => {
          // console.log('Result of processing is ', filename)
          result = filename;
        })
        .catch((err) => {
          console.log("ERROR ON FILE UPLOAD : ", err);
        });
    } else if (type == "audio") {
      await fileProcess
        .compressAudio(file.newName)
        .then(() => {
          result = file.newName;
        })
        .catch(() => {});
    } else {
      fs.renameSync(
        `${__dirname}/../uploads/tmp/${file.newName}`,
        `${__dirname}/../uploads/${file.newName}`
      );
      result = file.newName;
    }
    /* ANCHOR Handle file Process result */
    if (!!result) {
      // Create the file
      let created = await File.create({
        original: file.originalname,
        uploaded_by: user._id,
        filename: result,
        size: fs.statSync(`${__dirname}/../uploads/${file.newName}`).size,
        type,
      });
      // Push the response
      response.push({
        success: true,
        filename: created.filename,
        original: file.originalname,
        id: created._id.toString(),
        type: created.type,
      });
    } else {
      // Else, just push response with success false
      response.push({ success: false, original: file.originalname });
    }
  }
  // console.log('Final response is : ', response);
  if (response.some((f) => f.success)) {
    return res.status(200).json(response);
  } else {
    return res
      .status(500)
      .json("None of the file has been uploaded successfully");
  }
});

module.exports = router;
