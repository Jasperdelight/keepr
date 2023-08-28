<template>
  <div  class="modal-header modal-lg p-0 align-items-start">
    <h2 class="p-2 m-2">Add Your Vault</h2>
    <button type="button" class="close btn btn-outline" data-bs-dismiss="modal" aria-label="Close" >
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12">

        
        <!-- Form -->
        <form action="" @submit.prevent="createVault()">
  <div class="form-group">
      <!-- <label for="name" class="">Name</label> -->
      
      <input v-model="editable.name" class="form-control " type="text" placeholder="Name.." id="vaultName" required>

  </div>
  <div class="form-group">
    <!-- <label for="img" class="">Image</label> -->
    <input v-model="editable.img" class="form-control my-2" type="url" placeholder="Image.." id="img" required>
  </div>
  <div class="form-group">
    <!-- <label for="description" class="">Description</label> -->
    <textarea v-model="editable.description" rows="3" class="form-control" type="text" placeholder="Description.." id="description" required></textarea>
  </div>
  <div class="">
  </div>
  <div class="row">
    <div class="col-md-7 col-12"></div>
    <div class="col-md-5 col-12 text-end d-flex justify-content-end">
      <section class="row">
        <div class="col-12 fs-7 text-secondary">Private Vaults can only be seen by you</div>
        <div class="col-12 d-flex justify-content-end">
          <input v-model="editable.isPrivate" class="mb-3 me-1" type="checkbox" placeholder="Description.." id="description">
          <p class="fs-2">Make Vault Private?</p>
        </div>
        <div class="col-12">
          <button class="btn btn-dark m-2">Create Vault</button>
        </div>
      </section>
    
  </div>
</div>
</form>
      </div>
    </section>
  </div>

</template>

2
<script>
import { computed, ref } from "vue";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";

export default {
  setup(){
    const editable = ref({})
    return {
      editable,
      async createVault(){
        try{
            const formData = editable.value
            await vaultsService.createVault(formData)
            editable.value = {}
            Modal.getOrCreateInstance('#newVaultModal').hide()
            Pop.toast("Vault Created")
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