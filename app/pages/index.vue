<template>
  <div class="content">
    <h1>SNEK</h1>
    <p>
      Bienvenue sur Snek, incontéstablement le jeu le plus fun du monde,
      qu'attendez-vous? Inscrivez-vous et rejoignez notre communauté dès
      maintenant, lancez un partie et montrez-nous qui est le boss!
    </p>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {};
  },
  mounted: function () {
    if (this.$store.state.auth) {
      this.$router.push("/snek");
    }
  },
  methods: {},
};
</script>

<style lang="css" scoped></style>
