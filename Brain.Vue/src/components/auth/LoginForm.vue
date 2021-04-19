<template>
  <form @submit.prevent="loginUser">
    <div class="form-group">
      <label for="email">Email</label>
      <input type="email" class="form-control" id="email" aria-describedby="emailHelp" placeholder="Email" name="email" v-model="email">
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="password" class="form-control" id="password" placeholder="Password" name="password" v-model="password">
    </div>
    <button type="submit" class="btn btn-block btn-primary mt-5">Login</button>
  </form>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import Authentication from "@/services/authentication";
class LoginForm extends Vue {
  
  email = '';

  password = '';
  
  
  loginUser() {
    let data = {
      "Email": this.email,
      "Password": this.password
    }

    axios({
      method: 'post',
      url: "api/auth/login",
      data: data
    }).then(response => {
      if (response.data._success) {
        Authentication.setToken(response.data._token)
        this.$router.push({name: 'Profile'});
        this.$root.$emit('login', true);
      } else {
        this.$root.$emit('login', false);
      }
    }).catch(() => {
      this.$root.$emit('login', false);
    });
  }
}


export default LoginForm;
</script>

<style scoped>

</style>