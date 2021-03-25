<script>

import Component from "vue-class-component";
import Vue from "vue";

@Component
class Logout extends Vue {
  mounted() {
    localStorage.removeItem('brain_token');
    this.$router.push({name: 'Login'});
  }
}

export default Logout;
</script>
