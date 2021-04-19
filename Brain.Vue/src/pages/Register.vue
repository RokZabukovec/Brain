<template>
  <div class="container-fluid">
    <div class="row min-vh-100">
      <div class="col-md-12 d-flex justify-content-center align-items-center">
        <div class="login-form my-auto mx-auto w-50">
          <h1>Welcome to Brain.</h1>
          <RegisterForm />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import RegisterForm from "@/components/auth/RegisterForm";

export default {
  name: 'Register',
  components: {
    RegisterForm
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
