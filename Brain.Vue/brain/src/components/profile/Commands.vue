<template>
  <div class="commands">
    <div v-for="command in commands" v-bind:key="command.id" >
      <div class="command mt-1 d-flex flex-row justify-content-between">
        <code>{{ command.name }}</code>
        <p>{{ command.description }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import Vue from 'vue'
import Component from 'vue-class-component'
import axios from 'axios'

@Component
class Commands extends Vue {

  commands = [];

  mounted(){
   axios.get("https://localhost:5001/api/commands",{
     headers: {
       Authorization: 'Bearer ' + localStorage.getItem('brain_token')
     }
   })
   .then(response => {
      this.commands = response.data;
   })
   .catch(error => {
     console.log(error);
   });
  }

}
export default Commands;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .commands {
    background: #ffffff;
    padding: 5px;
  }
</style>
