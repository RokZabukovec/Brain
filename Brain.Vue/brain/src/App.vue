<template>
  <div id="app">
    <Navbar />
    <div class="notification" v-if="notification.title" v-on:notification = "showNotification">
      <h3>{{ notification.title }}</h3>
    </div>
    <router-view></router-view>
  </div>
</template>

<script>
import Navbar from './components/Navbar.vue';
import Component from "vue-class-component";
import Vue from "vue";

@Component({
  components: {
    Navbar
  }
})
class App extends Vue {
  notification = {
    title: null
  };

  showNotification(notification) {
    console.log('Notification', notification);
    this.notification = notification;
  }
  mounted(){
    this.$root.$on('notification', (notification) => {
      this.notification = notification;
      console.log('Notification received');
    })
  }
}
export default App;
</script>

<style>

  html, body {
    padding: 0;
    margin: 0;
    color: #2c3e50;
  }
  
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: left;
    background: #ffffff;
    min-height: 100vh;
  }
</style>
