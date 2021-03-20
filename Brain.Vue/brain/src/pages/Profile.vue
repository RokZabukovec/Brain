<template>
  <div class="container-fluid">
    <div class="row">
      <div class="d-none d-md-block col-md-3 section p-5 m-0 platforms">
        <Platforms />
      </div>
      <div class="col-md-9 col-sm-12 section">
        <div class="mx-auto w-100 mt-5">
          <Commands/>
        </div>
      </div>
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
  .platforms {
    background: #f5f5fa;
  }
</style>
