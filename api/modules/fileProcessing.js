const sharp = require("sharp");
const fs = require("fs");
const exec = require("child_process").exec;
const FileProcessing = {
  resizeImage: async (fileName) => {
    return new Promise((resolve, reject) => {
      sharp(`${__dirname}/../uploads/tmp/${fileName}`)
        .resize({
          width: 1200,
        })
        .toFile(`${__dirname}/../uploads/${fileName}`, function (err) {
          fs.unlinkSync(`${__dirname}/../uploads/tmp/${fileName}`);
          console.log(`Removed file tmp/${fileName}`);
          if (err) {
            return reject(err);
          } else {
            return resolve(fileName);
          }
        });
    });
  },
  compressAudio: async (fileName) => {
    // ffmpeg -i input.file -map 0:a:0 -b:a 96k output.mp3
    return new Promise((resolve, reject) => {
      let tmpPath = `${__dirname}/../uploads/tmp/${fileName}`;
      let finalPath = `${__dirname}/../uploads/${fileName}`;
      exec(
        `ffmpeg -i ${tmpPath} -map 0:a:0 -b:a 96k ${finalPath}`,
        (error, stdout, stderr) => {
          if (error) {
            console.error(`exec error: ${error}`);
            return reject(error);
          }
          // console.log(`stdout: ${stdout}`);
          // console.error(`stderr: ${stderr}`);
          if (!stderr.toLowerCase().includes("output")) return reject(stderr);
          return resolve(stderr);
        }
      );
    });
  },
};
module.exports = FileProcessing;
