<template>
    <div class="container">
      <div class="row min-vh-100">
        <div class="col-sm-12 d-flex justify-content-center align-items-center">
          <div class="login-form my-auto mx-auto w-50">
            <h3 class="font-weight-bold mb-5">Welcome back.</h3>
            <LoginForm />
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import LoginForm from "@/components/auth/LoginForm";

export default {
  name: 'Login',
  components: {
    LoginForm
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
