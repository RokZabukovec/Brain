<template>
  <div class="sidebar-nav d-flex flex-column">
    <ul>
      <li><router-link to="/"><b-icon icon="house" aria-hidden="true"></b-icon></router-link></li>
      <li><router-link to="/profile"><b-icon icon="terminal" aria-hidden="true"></b-icon></router-link></li>
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
    height: calc(100% - 2em);
    margin: 1em;
    border-radius: 5px;
    background: rgb(0, 171, 181);
    color: white;
    box-shadow: 1px 1px 15px rgba(0, 171, 181, 0.5);
  }
  
  .sidebar-nav ul {
    padding: 0;
    list-style: none;
  }
  
  .sidebar-nav ul > li {
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    box-sizing: border-box;
    padding: 1em 1em;
    font-size: 1em;
    transition: 0.3s ease;
  }

  .sidebar-nav ul > li:hover {
    background: #027ab1;
  }
  
  .sidebar-nav ul > li > a {
    color: white;
    font-weight: bold;
    font-size: 1.2em;
  }
  
  .sidebar-nav ul > li > a:hover {
    text-decoration: none;
  }
</style>