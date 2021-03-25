<template>
  <form @submit="registerUser" method="get">
    <div class="form-group">
      <label for="first-name">First name</label>
      <input type="text" class="form-control" id="first-name" placeholder="First name" name="first-name" v-model="firstName">
    </div>
    <div class="form-group">
      <label for="last-name">Last name</label>
      <input type="text" class="form-control" id="last-name" placeholder="Last name" name="last-name" v-model="lasttName">
    </div>
    <div class="form-group">
      <label for="email">Email</label>
      <input type="email" class="form-control" id="email" placeholder="Email" name="email" v-model="email">
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="password" class="form-control" id="password" placeholder="Password" name="password" v-model="password">
    </div>
    <div class="form-group">
      <label for="repeat-password">Repeat password</label>
      <input type="password" class="form-control" id="repeat-password" placeholder="Repeat password" name="repeat-password" v-model="repeatPassword">
    </div>
    <button type="submit" class="btn btn-block btn-primary mt-5">Register</button>
  </form>
</template>

<script>
import Vue from "vue";
import Component from "vue-class-component";
import axios from "axios";

@Component
class RegisterForm extends Vue {
  
  firstName = '';
  
  lasttName = '';

  email = '';

  password = '';

  repeatPassword = '';
  
  registerUser(e){
    e.preventDefault();
    console.log("registerUser");
    if (this.email === '') {
      console.log('Email is missing');
      return false;
    }

    if (this.password === '' || this.repeatPassword === '' || this.password !== this.repeatPassword) {
      console.log('Password is missing');
      return false;
    }

    var data = {
      Email: this.email,
      Password: this.password
    };

    axios({
      method: 'post',
      url: "https://localhost:5001/api/auth/registration",
      headers: {},
      data: data
    }).then(response => {
      if (response.data._success) {
        localStorage.setItem('brain_token', response.data._token);
        this.$router.push({name: 'Login'});
      }
    }).catch(error => {
      console.log(error);
    });
    
  }
}

export default RegisterForm;
</script>

<style scoped>

</style>