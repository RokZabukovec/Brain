<template>
  <div class="row">
    <div class="container">
      <div class="empty d-flex flex-column align-items-center col-md-12 mt-5 px-0" v-if="categories.length === 0">
        <h5 class="text-secondary font-weight-bold">Getting started</h5>
        <div class="getting-started">
          <div class="getting-started-step d-flex flex-column" v-if="platformId == null">
            <p>Create a <span class="show-btn" @click="$bvModal.show('new-platform')">new platform.</span></p>
          </div>
          <div class="getting-started-step">
            <p>Add a <span class="show-btn" @click="$bvModal.show('new-category')">first category</span> for the created platform.</p>
          </div>
        </div>
      </div>
      <div class="col-md-10 px-0 py-5">
        <div v-for="category in categories" v-bind:key="category.id" class="background-gradient">
          <Category v-bind:category="category"/>
        </div>
        <div class="new-category-btn w-100 d-flex justify-content-end fixed-bottom p-5">
          <button v-show="platformId !== null" class="btn brain-btn-basic" @click="$bvModal.show('new-category')">New category</button>
        </div>
        <div class="new-category mt-5">
          <b-modal id="new-category" title="Add category" hide-footer>
            <div>
              <b-form @submit.prevent="onCategorySave">
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
import Command from '@/components/profile/Command';
import Category from '@/components/profile/Category';

@Component({
  components: {
    Command,
    Category
  }
})
class Commands extends Vue {

  platformId = null;
  
  form = {
    Name: '',
    Description: '',
    PlatformId: this.platformId
  }
  
  categories = [];
  
  mounted(){
    this.$root.$on('category-delete', (categoryId) => {
      this.categories.splice(this.categories.findIndex(category => category.id === categoryId), 1);
    });
    
    this.$root.$on('update-commands', (platform) => {
      let url = "/api/categories/platform/" + platform.id;
      this.platformId = platform.id;
      axios.get(url)
          .then(response => {
            this.categories = response.data;
          })
          .catch(error => {
            console.log(error);
          });
    });
    
    this.$root.$on('platform-created', (platform) => {
      if (platform) {
        let url = "/api/categories/platform/" + platform.id;
        axios.get(url)
            .then(response => {
              this.categories = response.data;
            })
            .catch(error => {
              console.log(error);
            });
      }
    });

    this.$root.$on('platform-deleted', (response) => {
      if (response.first) {
        this.platformId = response.first.id;
        let url = "/api/categories/platform/" + response.first.id;
        axios.get(url)
            .then(response => {
              this.categories = response.data;
            })
            .catch(error => {
              console.log(error);
            });
      }
    });
    
    this.$root.$on('profile-loaded', (platform) => {
      if (platform) {
        this.platformId = platform.id;
        let url = "/api/categories/platform/" + platform.id;
        axios.get(url)
            .then(response => {
              this.categories = response.data;
            })
            .catch(error => {
              console.log(error);
            });
      }
    });

  }

  onCategorySave(){
    let url = "/api/categories";
    this.form.PlatformId = this.platformId;
    axios.post(url, this.form)
        .then(response => {
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
  .getting-started-step {
    padding: 1em;
    border: 1px solid grey;
    margin: 2em 0;
    border-radius: 5px;
    text-align: center;
    color: #4e555b;
  }
  
  .brain-btn-basic {
    background: #00ABB5;
    color: white;
    box-shadow: 2px 2px 15px rgba(0, 160, 171, 0.5);
    transition: all 0.3s ease;
  }
  
  .brain-btn-basic:hover {
    background: #00a0ab;
    box-shadow: 10px 10px 25px rgba(0, 160, 171, 0.5);
  }
  
  .show-btn {
    color: #00ABB5;
    cursor: pointer;
    font-weight: bold;
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
