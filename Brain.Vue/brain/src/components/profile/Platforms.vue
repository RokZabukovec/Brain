<template>
  <div class="d-flex justify-content-end">
    <div class="platforms w-50">
      <h3>Platforms</h3>
      <div class="mt-5">
        <div v-for="platform in platforms" v-bind:key="platform.id">
          <div class="command py-2 px-3 d-flex flex-row justify-content-between"  v-on:click.prevent="platformClick(platform.name, platform.id)">
            <p class="platform-link fs-3" :data-platform-id="platform.id">{{ platform.name }}</p>
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

  platformClick(platformName, platformId) {
    console.log('EMIT EVENT');
    this.$root.$emit('update-commands', {id: platformId, name: platformName});
  }
}
export default Platforms;
</script>

<style scoped>
  .command {
    cursor: pointer;
    transition: border-bottom 0.3s ease-in;
    box-sizing: border-box;
  }
  
  .command:hover {
    background: deeppink;
    border-radius: 5px;
    color: white;
  }
  
  .platform-link {
    padding: 0;
    margin: 0;
  }
</style>
