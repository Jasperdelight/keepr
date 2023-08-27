<template>
  <div  class="modal-header modal-lg p-0 align-items-start">
    <h2 class="p-2 m-2">Edit Account</h2>
    <button type="button" class="close btn btn-outline" data-bs-dismiss="modal" aria-label="Close" >
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12">

        
        <!-- Form -->
        <form action="" @submit.prevent="editAccount()">
          <div class="mb-2">
            <label for="name">Name</label>
            <input class="form-control" type="text" id="name" minlength="3" maxlength="75" v-model="editable.name">
          </div>
          <div class="mb-2">
            <label for="picture">Picture</label>
            <input class="form-control" type="url" id="picture" minlength="3" maxlength="200" v-model="editable.picture">
          </div>
          <div class="mb-2">
            <label for="coverImg">Cover Image</label>
            <input class="form-control" type="url" id="coverImg" minlength="3" maxlength="200" v-model="editable.coverImg">
          </div>
<div class="d-flex justify-content-end">
  <button class="btn btn-dark text-end m-2">Save</button>
</div>
</form>
      </div>
    </section>
  </div>

</template>


<script>
import { computed, ref, watchEffect } from "vue";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { accountService } from "../services/AccountService";

export default {
  setup(){
    const editable = ref({})
    watchEffect(()=> {
      editable.value = {...AppState.account}
    })
    return {
      editable,
      account: computed(()=> AppState.account),
      async editAccount(){
        try{
            const formData = editable.value
            await accountService.editAccount(formData)
            // editable.value = {}
            Pop.toast("Account info changed")
            Modal.getOrCreateInstance('#accountModal').hide()
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