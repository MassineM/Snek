<template>
  <div class="content">
    <h1>Scores</h1>
    <p>
      {{ $store.state.auth ? $store.state.auth.user.id : "" }}
      <br>
      <br>
      <br>
      <br>
      <br>
      <br>
      <br>
      <br>
      
    </p>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {};
  },
  mounted: function () {},
  methods: {},
};
</script>

<style lang="scss" scoped></style>
