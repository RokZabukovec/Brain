<template>
  <div class="hello">
    <Navbar />
    <Hero/>
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue';
import Hero from '@/components/homepage/Hero.vue';

export default {
  name: 'Home',
  components: {
    Navbar,
    Hero
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
