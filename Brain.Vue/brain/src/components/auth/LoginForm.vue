<template>
  <form>
    <div class="form-group">
      <label for="email">Email</label>
      <input type="email" class="form-control" id="email" aria-describedby="emailHelp" placeholder="Email" name="email">
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="password" class="form-control" id="password" placeholder="Password" name="password">
    </div>
    <button type="submit" class="btn btn-block btn-primary mt-5">Login</button>
  </form>
</template>

<script>
export default {
  name: "LoginForm"
}
</script>

<style scoped>

</style>