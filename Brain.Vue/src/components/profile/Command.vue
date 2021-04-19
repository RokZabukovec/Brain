<template>
  <div class="command">
    <h5 class="command-description">{{ command.description }}</h5>
    <code class="command-text d-flex justify-content-between align-items-center">{{ command.name }} <span @click="onCommandCopy(command.name)" class="command-copy btn">Copy</span></code>
    <b-modal v-bind:id="command.name + '-' + command.id.toString()" hide-footer>
      <b-form @submit.prevent="onCommandUpdate">
        <b-form-group label="Command" label-for="command">
          <b-form-input name="command" type="text" v-bind:value="command.name" v-model="command.name" placeholder="Enter command" required></b-form-input>
        </b-form-group>
        <b-form-group label="Description" label-for="description">
          <b-form-input name="description" type="text" v-bind:value="command.description" v-model="command.description" placeholder="Enter description"></b-form-input>
        </b-form-group>
        <div class="actions d-flex">
          <b-button type="submit" variant="primary">Update</b-button>
          <b-button type="delete" variant="danger" class="ml-2" @click="onDeleteCommand(command)">Delete</b-button>
        </div>
      </b-form>
    </b-modal>
  </div>
</template>

<script>
import Vue from "vue";
import Component from "vue-class-component";
import axios from "axios";

@Component({
  props: ['command']
})
class Command extends Vue {
  
  onCommandUpdate() {
    let url = '/api/commands';
    axios.patch(url, this.command)
        .then(() => {
          this.$bvModal.hide(this.command.name + '-' + this.command.id.toString());
        })
        .catch(error => {
          console.log(error);
        });
  }
  
  onDeleteCommand(command){
    let url = '/api/commands/' + command.id;
    axios.delete(url)
        .then(response => {
          if (response.status === 204) {
            this.$bvModal.hide(command.name + '-' + command.id.toString());
            this.$el.parentElement.remove()
          }
        })
        .catch(error => {
          console.log(error);
        });
  }

  onCommandCopy(command_name){
    navigator.clipboard.writeText(command_name).then(function() {
    }, function(err) {
      console.error('Async: Could not copy text: ', err);
    });
  }
}

export default Command;
</script>

<style scoped>
  
  .command {
    transition: 0.1s ease;
    cursor: pointer;
    border-radius: 5px;
    padding: 1em 0;
  }
  
  code.command-text {
    padding: 1em;
    margin: 0.5em 0;
    min-width: 100%;
    background: #fafafa;
    border-radius: 5px;
    display: inline-block;
    z-index: 1;
  }

  .command-copy {
    background: #dddddd;;
    color: #4e555b;
    opacity: 0;
    transition: opacity 0.3s ease;
    z-index: 2;
  }

  .command-copy:hover {
    background: #b5b4b4;
    color: #303639;
    opacity: 0;
    transition: all 0.3s ease;
    z-index: 2;
  }
  
  .command-text:hover > .command-copy{
    opacity: 1;
  }
  
  .command-description {
    font-weight: bold;
  }
  
</style>