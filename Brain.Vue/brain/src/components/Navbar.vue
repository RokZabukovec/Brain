<template>
<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="#">BRAIN</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarNav">
    <ul class="navbar-nav ml-auto">
      <li class="nav-item">
         <router-link to="/profile" class="nav-link">Profile</router-link>
      </li>
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
export default {
  name: 'Navbar',
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
          
  .navbar-toggler {
    border: none;
  }
</style>
