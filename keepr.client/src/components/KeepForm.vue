<template>
  <div  class="modal-header modal-lg p-0 align-items-start">
    <h2 class="p-2 m-2">Add Your Keep</h2>
    <button type="button" class="close btn btn-outline" data-bs-dismiss="modal" aria-label="Close" >
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12">

        
        <!-- Form -->
        <form action="" @submit.prevent="createKeep()">
  <div class="form-group">
      <!-- <label for="name" class="">Name</label> -->
      
      <input v-model="editable.name" class="form-control " type="text" placeholder="Name.." id="name" required>

  </div>
  <div class="form-group">
    <!-- <label for="img" class="">Image</label> -->
    <input v-model="editable.img" class="form-control my-2" type="url" placeholder="Image.." id="img" required>
  </div>
  <div class="form-group">
    <!-- <label for="description" class="">Description</label> -->
    <textarea v-model="editable.description" rows="3" class="form-control" type="text" placeholder="Description.." id="description" required></textarea>
  </div>
<div class="d-flex justify-content-end">
  <button class="btn btn-dark text-end m-2">Create</button>
</div>
</form>
      </div>
    </section>
  </div>

</template>


<script>
import { ref } from "vue";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";

export default {
  setup(){
    const editable = ref({})
    return {
      editable,
      async createKeep(){
        try{
            const formData = editable.value
            keepsService.createKeep(formData)
            editable.value = {}
            Pop.toast("Keep Created")
            Modal.getOrCreateInstance('#newKeepModal').hide()
        } catch(error) {
            Pop.error(error.message);
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>