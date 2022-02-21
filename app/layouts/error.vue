<template>
  <div class="content">
    <h1>{{ error.code }}</h1>
    <h2>{{ error.title }}</h2>
    <p>{{ error.message }}</p>
    <a href="#" class="btn submit" @click.prevent="go_back">
      Retour à l'accueil
    </a>
  </div>
</template>

<script>
export default {
  data: () => {
    return {
      error: {
        code: null,
        title: null,
      },
      errors_log: [
        {
          code: 403,
          title: "Halte ! Permission Refusée ",
          message:
            "Désolé ! Vous n'avez pas l'autorisation pour accéder à cette page",
        },
        {
          code: 500,
          title: "Oops ! Une erreur s'est produite.",
          message:
            "Une erreur s'est produite lors du chargement de la page, veuillez rééssayer !",
        },
      ],
    };
  },
  mounted: function () {
    this.catch_error();
  },
  methods: {
    catch_error() {
      if (this.$route.query.code)
        for (const error_log of this.errors_log)
          if (error_log.code == this.$route.query.code) {
            this.error = {
              code: this.$route.query.code,
              title: error_log.title,
              message: error_log.message,
            };
          }
      if (!this.error.code) {
        this.error = {
          code: 404,
          title: "Oups ! Page Introuvable",
          message:
            "La page que vous avez demandé est introuvable ou a été deplacée !",
        };
      }
    },
    go_back() {
      this.$router.push("/");
    },
  },
};
</script>

<style lang="scss" scoped></style>
