<template>
  <div class="d-flex justify-content-end">
    <div class="platforms w-50">
      <h3>Platforms</h3>
      <div class="mt-5">
        <div v-for="platform in platforms" v-bind:key="platform.id">
          <div class="command mt-4 d-flex flex-row justify-content-between">
            <a href="#" class="mb-2 fs-3" :data-platform-id="platform.id">{{ platform.name }}</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Vue from 'vue'
import Component from 'vue-class-component'
import axios from "axios";

@Component
class Platforms extends Vue {
  platforms = [];
  
  mounted(){
    axios.get("https://localhost:5001/api/platforms")
        .then(response => {
          console.log(response);
          this.platforms = response.data;
        })
        .catch(error => {
          console.log(error);
        });
  }

}
export default Platforms;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
