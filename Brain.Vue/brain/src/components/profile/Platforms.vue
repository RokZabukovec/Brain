<template>
    <div class="platforms mt-5">
      <div class="d-flex justify-content-start position-relative">
        <div class="new-platform">
          <b-modal id="new-platform" hide-footer title="Add platform">
            <div>
              <b-form @submit.prevent="onSubmit">
                <b-form-group label="Name" label-for="platform-name">
                  <b-form-input id="platform-name" v-model="form.Name" type="text" placeholder="Enter name" required></b-form-input>
                </b-form-group>

                <b-form-group label="Description" label-for="platform-description">
                  <b-form-input id="platform-description" v-model="form.Description" placeholder="Enter description"></b-form-input>
                </b-form-group>

                <b-button type="submit" variant="primary">Submit</b-button>
              </b-form>
            </div>
          </b-modal>

          <b-modal id="edit-platforms" title="Edit platforms">
            <table class="table table-striped">
              <thead>
              <tr>
                <th scope="col">#</th>
                <th scope="col">Name</th>
                <th scope="col">Edit</th>
                <th scope="col">Delete</th>
              </tr>
              </thead>
              <tbody>
              <tr  v-for="platform in platforms" v-bind:key="platform.id">
                <th scope="row">{{ platform.id }}</th>
                <td>{{ platform.name }}</td>
                <td><button class="btn btn-warning">Edit</button></td>
                <td><button class="btn btn-danger" @click="deletePlatform(platform.id)">Delete</button></td>
              </tr>
              </tbody>
            </table>
          </b-modal>
        </div>
        <div v-for="platform in platforms" v-bind:key="platform.id" v-show="platforms.length > 0">
          <div class="d-flex mr-5 platform"  v-on:click.prevent="platformClick(platform.name, platform.id)">
            <p :class="{'active': selected === platform.id}" :data-platform-id="platform.id">{{ platform.name }}</p>
          </div>
        </div>
        
        <div class="platform-actions position-absolute d-flex">
          <p class="ml-2 platform-action" @click="$bvModal.show('new-platform')" v-show="platforms.length">New</p>
          <p class="ml-2 platform-action" @click="$bvModal.show('edit-platforms')" v-show="platforms.length">Edit</p>
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
  
  selected = null;
  
  form = {
    Name: '',
    Description: ''
  }
  
  onSubmit(){
    axios.post("https://localhost:5001/api/platforms", this.form)
        .then(response => {
          if (response.status === 200) {
            this.platforms.push(response.data);
            this.$bvModal.hide('new-platform');
            this.form.Name = '';
            this.form.Description = '';
            this.$root.$emit('platform-created', response.data);
            this.selected = response.data.id;
          }
        })
        .catch(error => {
          console.log(error);
        });
  }
  
  mounted() {
    axios.get("https://localhost:5001/api/platforms")
        .then(response => {
          this.platforms = response.data;
          this.$root.$emit('profile-loaded', this.platforms[0]);
          this.selected = this.platforms[0].id;
        })
        .catch(error => {
          console.log(error);
        });
  }

  platformClick(platformName, platformId) {
    this.$root.$emit('update-commands', {id: platformId, name: platformName});
    this.selected = platformId;
  }

  deletePlatform(platformId){
    let url = "https://localhost:5001/api/platforms/" + platformId;
    axios.delete(url)
        .then(() => {
          this.platforms.splice(this.platforms.findIndex(platform => platform.id === platformId), 1);
          this.$bvModal.hide('edit-platforms');
          this.$root.$emit('platform-deleted', {deleted: platformId, first: this.platforms[0]});
        })
        .catch(error => {
          console.log(error);
        });
  }
}
export default Platforms;
</script>

<style scoped>

  .active {
    border-bottom: 2px solid #0b2e13;
  }
  
  .platform {
    cursor: pointer;
  }
  
  .platform-action {
    cursor: pointer;
    font-weight: bold;
  }

  .platform-action:hover {
    color: #dab900;
  }
  
  .platform-actions {
    right: 0;
  }
</style>
