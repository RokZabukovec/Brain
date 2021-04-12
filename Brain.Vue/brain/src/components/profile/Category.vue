<template>
  <div class="category">
    <div class="d-flex justify-content-between">
      <h3><b>{{ category.name }}</b></h3>
      <h5 class="new-command-btn" @click="$bvModal.show(category.name + '-' + category.id.toString())">&plus;</h5>
    </div>
    <div class="container">
      <div class="row">
        <div v-if="category.commands" class="col-sm-12 p-0 command-container" >
          <div @click="openModal(command)" v-for="command in category.commands" v-bind:key="command.id">
            <Command :command="command"></Command>
          </div>
        </div>
        <div v-if="category.commands == null || category.commands.length === 0 " class="d-flex justify-content-center w-100 py-2">
          <p class="text-muted">No commands created</p>
        </div>
      </div>
    </div>
    
    <b-modal v-bind:id="category.name + '-' + category.id.toString()" :title="'New command for ' + category.name" hide-footer>
      <b-form @submit.prevent="onCommandSave(category.id)">
        <b-form-group label="Command" label-for="command">
          <b-form-input name="command" v-model="commandForm.Name" type="text" placeholder="Enter command" required></b-form-input>
        </b-form-group>
        <b-form-group label="Description" label-for="description">
          <b-form-input name="description" v-model="commandForm.Description" type="text" placeholder="Enter description"></b-form-input>
        </b-form-group>
        <div class="actions d-flex">
          <b-button type="submit" variant="primary">Save</b-button>
          <b-button variant="danger" @click="deleteCategory(category.id)">Delete category</b-button>
        </div>
      </b-form>
    </b-modal>
  </div>
</template>

<script>

import Vue from "vue";
import Component from "vue-class-component";
import Command from "@/components/profile/Command";
import axios from "axios";

@Component({
  components: {Command},
  props: ['category']
})
class Category extends Vue {

  commandForm = {
    Name: '',
    Description: '',
    CategoryId: null
  }
  
  onCommandSave(categoryId) {
    this.commandForm.CategoryId = categoryId;
    let url = "api/commands";
    axios.post(url, this.commandForm)
        .then(response => {
          if (this.$props.category.commands == null) {
            this.$props.category.commands = [];
          }
          this.$props.category.commands.push(response.data);
          this.$bvModal.hide(this.$props.category.name + '-' + this.$props.category.id.toString());
          this.commandForm.Name = '';
          this.commandForm.Description = '';
          this.commandForm.CategoryId = null;
        })
        .catch(error => {
          console.log(error);
        });
  }

  openModal(command){
    this.$bvModal.show(command.name + '-' + command.id.toString())
  }

  deleteCategory(categoryId){
    let url = "api/categories/" + categoryId;
    axios.delete(url)
        .then(() => {
          this.$emit('category-delete', categoryId);
          this.$bvModal.hide(this.$props.category.name + '-' + this.$props.category.id.toString());
          this.commandForm.Name = '';
          this.commandForm.Description = '';
          this.commandForm.CategoryId = null;
        })
        .catch(error => {
          console.log(error);
        });
  }
}

export default Category;
</script>

<style scoped>
  
  .category {
    margin: 1em 0;
  }
  
  .new-command-btn {
    text-align: center;
  }
  
  .new-command-btn:hover{
    cursor: pointer;
    color: #0056b3;
  }

</style>