<template>
  <div class="sidebar-nav d-flex flex-column">
    <ul>
      <li><router-link to="/"><b-icon icon="house-door" variant="light" aria-hidden="true"></b-icon></router-link></li>
      <li><router-link to="/profile"><b-icon icon="terminal-fill" variant="light" aria-hidden="true"></b-icon></router-link></li>
    </ul>
  </div>
</template>

<script>
import Vue from 'vue';
import Component from "vue-class-component";

@Component
class SidebarNav extends Vue {
  
}

export default SidebarNav;
</script>

<style scoped>
  .sidebar-nav {
    position: fixed;
    height: 100%;
    min-height: 100vh;
    padding: 2em;
    background: #01071c;
    color: white;
  }
  .sidebar-nav ul {
    list-style: none;
  }
  .sidebar-nav ul > li{
    margin: 1em 0;
    font-size: 2em;
  }
</style>