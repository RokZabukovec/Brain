<template>
  <div id="app">
    <div aria-live="polite" aria-atomic="true" class="bg-dark position-relative bd-example-toasts" v-bind:variant="alertVariant" v-show="showAlert">
      <div class="toast-container position-absolute p-3" id="toastPlacement">
        <div class="toast">
          <div class="toast-header">
            <strong class="me-auto">{{ notification.title }}</strong>
          </div>
          <div class="toast-body">
            {{ notification.description }}
          </div>
        </div>
      </div>
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
  .notification {
    top: 5%;
    right: 5%;
    max-width: 50%;
  }
</style>
