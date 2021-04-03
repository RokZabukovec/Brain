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
    var data = {
      "Email": this.email,
      "Password": this.password
    }

    axios({
      method: 'post',
      url: "https://localhost:5001/api/auth/login",
      data: data
    }).then(response => {
      console.log(response)
      if (response.data._success) {
        Authentication.setToken(response.data._token)
        this.$router.push({name: 'Profile'});
        let notification = {
          title: 'Login Successful',
          description: null,
          error: false
        }
        
        this.$root.$emit('notification', notification);
        this.$root.$emit('login', true);
      } else {
        console.log('Failed')
        let notification = {
          title: 'Login failed',
          description: 'Something went wrong. Try again.',
          error: false
        }
        this.$root.$emit('notification', notification);
        this.$root.$emit('login', false);
      }
    }).catch(error => {
      
      let notification = {
        title: 'Login Failed',
        error: true
      }

      this.$root.$emit('notification', notification);
      this.$root.$emit('login', false);
      console.log(error);
    });
  }
  
}


export default LoginForm;
</script>

<style scoped>

</style>