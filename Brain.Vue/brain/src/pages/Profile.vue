<template>
  <div class="container">
    <div class="row">
      <div class="platforms-container col-sm-12">
        <Platforms/>
      </div>
      <div class="commands-container col-sm-12">
        <Commands/>
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
</style>
