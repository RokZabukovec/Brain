<template>
  <div class="container">
    <div class="row">
      <div class="empty col-md-12 d-flex justify-content-center mt-5 px-0" v-if="categories.length === 0">
        <p class="text-secondary">No commands found</p>
      </div>
      <div class="col-md-12 px-0">
        <div v-for="category in categories" v-bind:key="category.id" >
          <div class="category card mt-5">
            <div class="card-body">
              <h5 class="card-title">{{ category.name }}</h5>
              <p class="card-text">{{ category.description }}</p>
              <div class="empty-category" v-if="category.commands === null">
                No commands found
              </div>
              <div class="d-flex justify-content-between" v-for="command in category.commands" v-bind:key="command.id" v-else>
                <code>{{ command.name }}</code>
                <p>{{ command.description }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="new-category mt-5">
          <div class="new-category mx-auto">
            <b-button v-b-modal.new-category variant="dark">Add category</b-button>
          </div>
          <b-modal id="new-category" title="BootstrapVue">
            <div>
              <b-form @submit.prevent="onSubmit">
                <b-form-group label="Name" label-for="platform-name">
                  <b-form-input id="platform-name" v-model="form.Name" type="text" placeholder="Enter name" required></b-form-input>
                </b-form-group>

                <b-form-group label="Description" label-for="platform-description">
                  <b-form-input id="platform-description" v-model="form.Description" placeholder="Enter description"></b-form-input>
                </b-form-group>

                <b-button type="submit" variant="primary">Save</b-button>
              </b-form>
            </div>
          </b-modal>
        </div>
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

  categories = [];
  
  form = {
    Name: '',
    Description: '',
    PlatformId: null
  }
  
  mounted(){
    this.$root.$on('update-commands', (platform) =>{
      let url = "https://localhost:5001/api/categories/platform/" + platform.id;
      this.form.PlatformId = platform.id;
      axios.get(url)
          .then(response => {
            this.categories = response.data;
            console.log(response);
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

  onSubmit(){
    let url = "https://localhost:5001/api/categories";
    axios.post(url, this.form)
        .then(response => {
          console.log('NEW CATEGORY',response);
          this.categories.push(response.data);
          this.$bvModal.hide('new-category');
          this.form.Name = '';
          this.form.Description = '';
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
