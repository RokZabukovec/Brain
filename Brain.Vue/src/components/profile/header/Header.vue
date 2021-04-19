<template>
  <div class="header mx-auto">
    <h3>{{ title }}</h3>
    <p>{{ description }}</p>
  </div>
</template>

<script>
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  props: ['title', 'description']
})
class Header extends Vue {
  
}

export default Header;
</script>

<style scoped>

  .header{
    text-align: center;
  }
  
  .header h3 {
    font-weight: bold;
  }
</style>