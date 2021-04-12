<template>
  <div id="app">
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
  
  showAlert = false;
  
  alertVariant = 'success';
  
  notification = {
    title: null,
    description: null,
    error: false
  };
  
  mounted(){
    this.$root.$on('notification', (notification) => {
      console.log('Notified')
      this.notification = notification;
      this.showAlert = true;
      
      if (notification.error) {
        this.alertVariant = 'danger';
      } else {
        this.alertVariant = 'success';
      }
    })
  }

  dismissAlert(){
    this.showAlert = false;
    this.notification = {
      title: null,
      description: null,
      error: false
    };
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
