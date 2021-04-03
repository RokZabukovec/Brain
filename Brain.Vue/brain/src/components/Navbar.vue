<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <a class="navbar-brand" href="#">BRAIN</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav ml-auto" v-if="isAuthenticated">
          <li class="nav-item">
            <router-link to="/profile" class="nav-link">Profile</router-link>
          </li>
          <li class="nav-item">
            <a href="#" @click.prevent="logout" class="nav-link">Logout</a>
          </li>
      </ul>
      <ul class="navbar-nav ml-auto" v-else>
          <li class="nav-item">
            <router-link to="/login" class="nav-link">Login</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/register" class="nav-link">Register</router-link>
          </li>
      </ul>
    </div>
  </nav>
</template>

<script>
import Vue from "vue";
import Component from "vue-class-component";
import Authentication from "@/services/authentication";

@Component
class Navbar extends Vue {
  
  isAuthenticated = Authentication.isAuthenticated();
  
  mounted() {
    this.$root.$on('login', (login) => {
      this.isAuthenticated = login;
      console.log('Logged in')
    })
    
  }

  logout() {
    localStorage.removeItem('brain_token');
    this.$router.push({name: 'Login'});
    this.isAuthenticated = false;
    let notification = {
      title: 'Logout Successful',
      error: false
    }
    this.$root.$emit('notification', notification);
  }
  
}

export default Navbar;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
          
  .navbar-toggler {
    border: none;
  }
</style>
