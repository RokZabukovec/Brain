<template>
    <div class="container-fluid m-0 p-0">
      <div class="row min-vh-100">
        <div class="col-sm-12 col-md-6 background-gradient d-flex flex-column justify-content-center align-items-center">
          <h1 class="text-white">Login</h1>
          <p class="text-white">Welcome back to your brain.</p>
        </div>
        <div class="col-sm-12 col-md-6 d-flex justify-content-center align-items-center">
          <div class="login-form my-auto mx-auto w-50">
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
  .background-gradient {
    background-image: linear-gradient(to right, #6a11cb 0%, #2575fc 100%);
  }
</style>
