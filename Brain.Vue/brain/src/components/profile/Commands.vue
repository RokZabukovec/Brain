<template>
  <div class="container">
    <div class="row">
      <div class="empty col-md-12 mt-5 px-0" v-show="categories.length === 0">
        <h5 class="text-secondary font-weight-bold">Getting started</h5>
        <ol>
          <li v-show="platformId == null">Create a <span class="show-btn" @click="$bvModal.show('new-platform')">new platform.</span></li>
          <li>Add a <span class="show-btn" @click="$bvModal.show('new-category')">first category</span> for the created platform.</li>
        </ol>
      </div>
      <div class="col-md-12 px-0 py-5">
        <div v-for="category in categories" v-bind:key="category.id">
          <Category v-bind:category="category"/>
        </div>
        <button class="btn btn-block btn-primary" @click="$bvModal.show('new-category')">New category</button>
        <!-- NEW CATEGORY MODAL-->
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
    this.$root.$on('update-commands', (platform) => {
      let url = "https://localhost:5001/api/categories/platform/" + platform.id;
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
      this.platformId = platform.id;
    });

    this.$root.$on('profile-loaded', (platform) => {
      console.log(platform);
      this.platformId = platform.id;
    });
    
    if (this.platformId){
      let url = "https://localhost:5001/api/categories/platform/" + this.platformId;
      axios.get(url)
          .then(response => {
            this.categories = response.data;
          })
          .catch(error => {
            console.log(error);
          });
    }
  }

  onCategorySave(){
    let url = "https://localhost:5001/api/categories";
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

  .show-btn {
    color: #005cbf;
    cursor: pointer;
    font-weight: bold;
  }
  
  #show-btn:hover {
    text-decoration: underline;
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
