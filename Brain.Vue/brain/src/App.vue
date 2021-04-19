<template>
  <div id="app">
    <transition  mode="out-in" enter-active-class="animate__animated animate__fadeIn" leave-active-class="animate__animated animate__fadeOut">
      <router-view style="animation-duration: 0.3s"></router-view>
    </transition>
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue';
import Component from "vue-class-component";
import Vue from "vue";

@Component({
  components: {
    Navbar
  }
})
class App extends Vue {
  
  notifications = [];
  
  mounted(){
    this.$root.$on('notification', (notification) => {
      console.log(notification);
      this.notifications.push(notification);
    })
  }
  
}
export default App;
</script>

<style>
  @import url('https://fonts.googleapis.com/css2?family=Open+Sans&display=swap');
  
  html, body {
    padding: 0;
    margin: 0;
    color: #2c3e50;
  }

  * {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
  }
  
  #app {
    font-family: 'Open Sans', sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: left;
    background: #ffffff;
    min-height: 100vh;
  }
  
</style>
