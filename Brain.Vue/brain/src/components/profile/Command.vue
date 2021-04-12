<template>
  <div class="command">
    <h5 class="command-description">{{ command.description }}</h5>
    <code class="command-text">{{ command.name }}</code>

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
    let url = 'https://localhost:5001/api/commands';
    axios.patch(url, this.command)
        .then(() => {
          this.$bvModal.hide(this.command.name + '-' + this.command.id.toString());
        })
        .catch(error => {
          console.log(error);
        });
  }
  
  onDeleteCommand(command){
    let url = 'https://localhost:5001/api/commands/' + command.id;
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
    display: inline-block;
  }
  
  .command-description {
    font-weight: bold;
  }
  
</style>