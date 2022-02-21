let express = require("express");
let router = express.Router();
let jwt = require("jsonwebtoken");
let bcryptjs = require("bcryptjs");

let User = require("../models/users");

router.post("/register", async (req, res) => {
  if (await User.findOne({ email: req.body.email })) {
    return res.status(200).json({
      title: "Connexion échouée !",
      success: false,
      message: "Il existe déjà un utilisateur avec cette adresse mail",
    });
  }

  const hashedPassword = await bcryptjs.hash(
    req.body.password,
    await bcryptjs.genSalt(10)
  );

  try {
    const user = new User({
      username: req.body.username,
      email: req.body.email,
      password: hashedPassword,
    });
    try {
      await user.save();
      return res.status(201).json({
        success: true,
        title: "Bravo !",
        message: "Votre compte a été créé!",
      });
    } catch (error) {
      debug(error, false);
      return res.status(400).json({
        success: false,
        message: error,
      });
    }
  } catch (error) {
    debug(error, false);
    return res.status(400).json({
      success: false,
      message: error,
    });
  }
});

router.post("/login", async function (req, res) {
  let user = await User.findOne({ email: req.body.email });

  if (!user)
    return res.status(200).json({
      success: false,
      title: "Connexion échouée !",
      message: "Aucun utilisateur n'a été trouvé pour " + req.body.email + ".",
    });

  bcryptjs.compare(req.body.password, user.password, async function (
    err,
    isSame
  ) {
    if (err) return res.status(500).json({ error: err });

    if (isSame) {
      let options = {};
      let token = jwt.sign(
        {
          user: {
            email: user.email,
            username: user.username,
            role: user.role,
            id: user.id,
          },
        },
        process.env.TOKEN_SECRET,
        options
      );

      return res.status(200).json({
        success: true,
        token: token,
        user: {
          email: user.email,
          username: user.username,
          role: user.role,
          id: user.id,
        },
      });
    } else {
      return res.status(200).json({
        success: false,
        message: "Mot de passe incorrect !",
      });
    }
  });
});

router.post("/checkRoute", function (req, res) {
  if (req.headers.authorization) {
    jwt.verify(
      req.headers.authorization,
      process.env.TOKEN_SECRET,
      async (err, decoded) => {
        if (err) {
          // Error when deconding the token
          //
          return res.status(500).json({
            success: false,
            message: "Erreur lors de la vérification du jeton.",
          });
        } else {
          // Token is valid and decoded
          // console.log('Token OK')
          // console.log(decoded.user)
          let user = await User.findById(
            decoded.user.id,
            "email username role id"
          );
          if (!user || user.archived)
            return res.status(403).json({
              success: false,
              message: "L'utilisateur n'existe plus !",
            });
          user.last_login = new Date();
          await user.save();

          return res.status(200).json({
            token: req.headers.authorization,
            user,
          });
        }
      }
    );
  } else {
    return res
      .status(403)
      .json({ success: false, msg: "Aucun Jeton n'a été trouvé" });
  }
});

global.userOnly = function (req, res, next) {
  if (req.headers.authorization) {
    jwt.verify(req.headers.authorization, process.env.TOKEN_SECRET, function (
      Err,
      decoded
    ) {
      if (Err) {
        //
        return res.status(403).send("Jeton invalid");
      } else {
        req.decodedToken = decoded;
        next();
      }
    });
  } else {
    return res.status(403).send("Jeton invalid");
  }
};

global.adminOnly = function (req, res, next) {
  if (req.headers.authorization) {
    jwt.verify(req.headers.authorization, process.env.TOKEN_SECRET, function (
      err,
      decoded
    ) {
      if (err) {
        return res.status(403).send("Invalid token");
      } else {
        if (decoded.user.role == "admin") {
          req.decodedToken = decoded;
          next();
        } else {
          return res.status(403).send("Not authorized");
        }
      }
    });
  } else {
    return res.status(403).send("Invalid token");
  }
};

module.exports = router;
