import axios from "axios";
const Cookie = require("js-cookie");

export default function ({ store, redirect }) {
  // If the user is not authenticated
  // console.log(store.state.auth)
  if (!store.state.auth) {
    return redirect("/error?code=403");
  } else {
    return new Promise((resolve) => {
      axios
        .post(
          process.env.api_uri + "/checkRoute",
          {},
          {
            headers: { Authorization: store.state.auth.token },
          }
        )
        .then(function (response) {
          store.commit("setAuth", response.data);
          if (store.state.auth.user.role == "admin") {
            resolve();
          } else {
            resolve(redirect("/error?code=403"));
          }
        })
        .catch(() => {
          Cookie.remove("auth"); // saving token in cookie for server rendering
          store.commit("setAuth", null);
          resolve(redirect("/connexion?expired=true"));
        });
    });
  }
}
