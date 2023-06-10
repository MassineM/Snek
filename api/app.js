const express = require("express");
const app = express();
const mongoose = require("mongoose");
const bodyParser = require("body-parser");
const cors = require("cors");
const dumper = require("./modules/dumper");
const WebSocket = require("ws");
const wss = new WebSocket.Server({ port: 8000 }, () => {
  console.log("\n");
  dumper.debug_message("Web Socket ", "Started");
});
let connectDB = async () => {
  var options = {
    useNewUrlParser: true,
    useUnifiedTopology: true,
  };
  await mongoose
    .connect(
      `mongodb+srv://gfox841:ebccd84159@cluster0.d6nmlhd.mongodb.net/?retryWrites=true&w=majority`,
      options
    )
    .then(() => {
      dumper.debug_message("Database", "Connected");
      dumper.debug_message("Listening", "27017");
    })
    .catch((error) => {
      dumper.debug_message("Database", "Failed to connect");
      dumper.debug(error, false);
    });
};

wss.on("listening", () => {
  dumper.debug_message("Listening", "8000");
});

wss.on("connection", (ws) => {
  ws.send(`Hello `);

  ws.on("message", (data) => {
    data = JSON.parse(data);
    console.log(data);
    ws.send(JSON.stringify(data));
  });
});

connectDB();

app.listen(3000, () => {
  setTimeout(() => {
    dumper.debug_message("Node Server", "Running...");
    dumper.debug_message("Listening", "3000");
    dumper.debug_message("Nuxt Server", "Running...");
    dumper.debug_message("Listening", "8080");
  }, 500);
  // wsServer.init(server);
});

app.get("/", function (req, res) {
  return res.status(200).json({
    success: true,
    message: "ROUTE OK",
  });
});

app.use(express.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(cors());

app.use("/", require("./routes/auth"));
app.use("/", require("./routes/users"));
app.use("/", require("./routes/scores"));
