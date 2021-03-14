<template>
  <div class="container-fluid mt-2">
    <div class="row">
      <div class="d-none d-md-block col-md-3 section">
        <Platforms />
      </div>
      <div class="col-md-6 col-sm-12 section">
        <Commands/>
        </div>
      <div class="d-none d-md-block col-md-3 section"><h1>Categories</h1></div>

    </div>
  </div>
</template>

<script>
import Platforms from '../components/profile/Platforms.vue';
import Commands from '../components/profile/Commands.vue';

export default {
  name: 'Profile',
  components: {Platforms, Commands}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.section {
  min-height: 100vh;
}
</style>
