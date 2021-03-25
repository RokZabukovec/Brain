<template>
    <div class="platforms mt-5">
      <div class="d-flex justify-content-start">
        <div class="new-platform">
          <b-button id="show-btn" variant="outline-primary" @click="$bvModal.show('new-platform')">New</b-button>
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
          
          <b-button v-b-modal.modal-scrollable variant="outline-warning" @click="$bvModal.show('edit-platforms')" class="ml-2">Edit</b-button>

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
        <div v-for="platform in platforms" v-bind:key="platform.id">
          <div class="command py-2 px-3 d-flex mx-3"  v-on:click.prevent="platformClick(platform.name, platform.id)">
            <p class="platform-link fs-3" :data-platform-id="platform.id">{{ platform.name }}</p>
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
          }
        })
        .catch(error => {
          console.log(error);
        });
  }
  
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
    this.$root.$emit('update-commands', {id: platformId, name: platformName});
  }

  deletePlatform(platformId){
    let url = "https://localhost:5001/api/platforms/" + platformId;
    axios.delete(url)
        .then(response => {
          console.log(response);
          console.log(this.platforms);
          this.platforms.splice(this.platforms.findIndex(platform => platform.id === platformId), 1);
          this.$bvModal.hide('edit-platforms');
        })
        .catch(error => {
          console.log(error);
        });
  }
}
export default Platforms;
</script>

<style scoped>
  #show-btn{
    background: none;
  }
  .command {
    cursor: pointer;
    transition: border-bottom 0.3s ease-in;
    box-sizing: border-box;
  }
  
  .command:hover {
    border-radius: 30px;
    background: #dedede;
    color: #1b1e21;
  }
  
  .platform-link {
    padding: 0;
    margin: 0;
  }
</style>
