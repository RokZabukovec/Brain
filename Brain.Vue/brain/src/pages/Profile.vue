<template>
  <div class="container-fluid">
    <div class="row">
      <SidebarNav />
      <div class="container mt-4">
        <div class="row header-section">
          <Header title="Commands" description="Command cheatsheet made for you, by you."/>
          <div class="col-md-6 mx-auto search">
            <Searchbar/>
          </div>
        </div>
        <div class="row">
          <div class="platforms-container col-sm-12">
            <Platforms/>
          </div>
          <div class="commands-container col-sm-12">
            <Commands/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Platforms from '@/components/profile/Platforms.vue';
import Commands from '@/components/profile/Commands.vue';
import SidebarNav from '@/components/SidebarNav';
import Header from '@/components/profile/header/Header';
import Searchbar from '@/components/profile/search/Searchbar';

export default {
  name: 'Profile',
  components: {Platforms, Commands, SidebarNav, Header, Searchbar}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .header-section {
    padding: 2em;
    border-radius: 5px;
    box-shadow: 1px 1px 15px rgba(98, 97, 97, 0.1);
  }
  
  .platforms-container, .commands-container {
    padding: 0;
  }
  
</style>
