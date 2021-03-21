<template v-on:update-commands="updateCommands">
  <div class="container">
    <div class="row">
      <Searchbar/>
      <CategoryModal/>
      <div class="empty col-md-12 d-flex justify-content-center mt-5" v-if="categories.length === 0">
        <p class="text-secondary">No commands found</p>
      </div>
      <div class="col-md-12 px-0">
        <div v-for="category in categories" v-bind:key="category.id" >
          <div class="category card mt-5">
            <div class="card-body">
              <h5 class="card-title">{{ category.name }}</h5>
              <p class="card-text">{{ category.description }}</p>
              <div class="d-flex justify-content-between" v-for="command in category.commands" v-bind:key="command.id" >
                <code>{{ command.name }}</code>
                <p>{{ command.description }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Vue from 'vue'
import Component from 'vue-class-component'
import axios from 'axios'
import Searchbar from "@/components/profile/search/Searchbar";

@Component ({
  components: {
    Searchbar,
  }
})

class Commands extends Vue {

  categories = [];
  
  
  mounted(){
    this.$root.$on('update-commands', (platform) =>{
      let url = "https://localhost:5001/api/categories/platform/" + platform.id;
      axios.get(url)
          .then(response => {
            this.categories = response.data;
          })
          .catch(error => {
            console.log(error);
          });
    });
    
   axios.get("https://localhost:5001/api/categories")
   .then(response => {
      console.log(response);
      this.categories = response.data;
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
  .category {
    animation: fadeIn 0.3s;
  }

  @keyframes fadeIn {
    from {
      opacity: 0;
    }
    to {
      opacity: 1;
    }
  }
</style>
