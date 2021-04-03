<template>
  <div class="header">
    <h1>{{ title }}</h1>
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
  .header {
    background: #eaeaea;
    box-shadow: 3px 3px 3px #ccc;
    padding: 1em 2em;
    border-radius: 5px;
  }
</style>