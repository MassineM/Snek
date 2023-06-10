<template>
  <main>
    <notifications
      position="top right"
      classes="vue-notification"
      width="24vw"
    />
    <nav>
      <div class="center">
        <img
          :src="require(`../assets/img/Snake.png`)"
          alt="logo"
          @click="$router.push('/')"
        />
        <ul>
          <li
            class="username"
            v-if="$store.state.auth"
            @click="$router.push('/scores')"
          >
            {{ $store.state.auth.user.username }}
          </li>
          <li class="clickable" @click="$router.push('/scores')">Scores</li>
          <li class="clickable" v-if="$store.state.auth" @click="logout()">
            Se deconnecter
          </li>
          <li
            class="clickable"
            v-if="!$store.state.auth"
            @click="toggleConnectModal()"
          >
            Se connecter
          </li>
        </ul>
      </div>
    </nav>
    <div class="container center">
      <nuxt />
    </div>
    <footer>
      <ul class="center">
        <li>Snek 2021© - EILCO</li>
        <li>Unity 3D</li>
      </ul>
    </footer>
    <div v-if="showConnectModal && mode == 'connect'" class="connectModal">
      <form action="">
        <div class="flexButtons">
          <div
            class="btn"
            :class="{ actif: mode == 'connect' }"
            @click="switchMode('connect')"
          >
            Connexion
          </div>
          <div
            class="btn"
            :class="{ actif: mode == 'register' }"
            @click="switchMode('register')"
          >
            Inscription
          </div>
        </div>
        <input type="email" v-model="user.email" placeholder="Email" />
        <input
          type="password"
          v-model="user.password"
          placeholder="Mot de passe"
        />
        <a href="#" class="btn submit" @click.prevent="login">Se connecter</a>
      </form>
    </div>
    <div v-if="showConnectModal && mode == 'register'" class="connectModal">
      <form action="">
        <div class="flexButtons">
          <div
            class="btn"
            :class="{ actif: mode == 'connect' }"
            @click="switchMode('connect')"
          >
            Connexion
          </div>
          <div
            class="btn"
            :class="{ actif: mode == 'register' }"
            @click="switchMode('register')"
          >
            Inscription
          </div>
        </div>
        <input type="text" v-model="user.username" placeholder="Pseudo" />
        <input type="email" v-model="user.email" placeholder="Email" />
        <input
          type="password"
          v-model="user.password"
          placeholder="Mot de passe"
        />
        <input
          type="password"
          v-model="user.confirm_password"
          placeholder="Confirmer votre mot de passe"
        />
        <a href="#" class="btn submit" @click.prevent="register">S'inscrire</a>
      </form>
    </div>
  </main>
</template>

<script>
const Cookie = process.client ? require("js-cookie") : undefined;
export default {
  components: {},
  data: () => {
    return {
      mode: "register",
      showConnectModal: false,
      user: {
        username: "",
        email: "",
        password: "",
      },
      reg_mail:
        /^(([^<>()[\],;:\s@]+(\.[^<>()[\],;:\s@]+)*)|(.+))@(([^<>()[\],;:\s@]+\.)+[^<>()[\],;:\s@]{2,})$/i,
    };
  },
  mounted: function () {},
  methods: {
    toggleConnectModal() {
      this.showConnectModal = !this.showConnectModal;
    },
    switchMode(mode) {
      this.mode = mode;
    },
    checkInputs(register) {
      if (register) {
        if (this.user.username.trim().length == 0) {
          this.$notify({
            type: "warn",
            duration: 4000,
            title: "Champ obligatoire",
            text: "Votre pseudo n'est pas valide !",
          });
          console.log(text);
          return false;
        }
        if (this.user.password !== this.user.confirm_password) {
          this.$notify({
            type: "warn",
            duration: 4000,
            title: "Champ obligatoire",
            text: "Vos deux mot de passe ne correspondent pas !",
          });
          console.log(text);
          return false;
        }
      }
      if (
        !this.reg_mail.test(this.user.email) &&
        !this.user.email.trim().length == 0
      ) {
        this.$notify({
          duration: 4000,
          type: "warn",
          title: "Champ obligatoire",
          text: "Votre adresse mail n'est pas valide !",
        });
        console.log(text);
        return false;
      }
      if (this.user.password.trim().length < 8) {
        this.$notify({
          type: "warn",
          duration: 4000,
          title: "Champ obligatoire",
          text: "Votre mot de passe n'est pas valide !",
        });
        console.log(text);
        return false;
      }

      return true;
    },
    register() {
      if (this.checkInputs(true)) {
        this.$post("/register", this.user)
          .then((res) => {
            if (res.data.success) {
              this.user = {
                username: "",
                email: "",
                password: "",
              };
              this.switchMode("connect");
              this.$notify({
                type: "success",
                duration: 4000,
                title: res.data.title,
                text: res.data.message,
              });
            } else {
              this.$notify({
                type: "error",
                title: res.data.title,
                duration: 4000,
                text: res.data.message,
              });
            }
          })
          .catch((err) => {
            this.$notify({
              type: "error",
              title: "Oups !",
              duration: 4000,
              text: "Une erreur s'est produite lors de la création de votre compte, veuilliez rééssayer",
            });
          });
      }
    },
    login() {
      if (this.checkInputs(false)) {
        this.$post("/login", {
          email: this.user.email,
          password: this.user.password,
        })
          .then((res) => {
            if (res.data.success) {
              this.$store.commit("setAuth", res.data);
              Cookie.set("auth", res.data);
              if (process.browser)
                localStorage.setItem(
                  "last_login_address_used_successfully",
                  this.user.email.toLowerCase()
                );
              this.user = res.data.user;
              this.toggleConnectModal();
              this.$router.push("/");
              this.$notify({
                type: "success",
                duration: 4000,
                title: "Connecté !",
                text: "Bienvenue " + this.user.username + " !",
              });
            } else {
              this.$notify({
                type: "error",
                title: res.data.title,
                duration: 4000,
                text: res.data.message,
              });
            }
          })
          .catch((err) => {
            this.$notify({
              type: "error",
              title: "Oups !",
              duration: 4000,
              text: "Une erreur s'est produite lors de la tentative de connexion, veuillez réessayer",
            });
          });
      }
    },
    logout() {
      Cookie.set("auth", null);
      this.$store.state.auth = null;
      this.$router.push("/");
    },
  },
};
</script>
<style lang="css" scoped></style>
