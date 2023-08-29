<template>
  <div class="container-fluid" v-if="activeKeep">
    <section class="row">
      <!-- Image half Modal LEFT -->
      <div class="col-md-6 col-12 p-0">
        <img :src="activeKeep.img" alt="" class="img-fluid" style="height: 100%;">
      </div>
      <!-- Information half RIGHT -->
      <div class="col-md-6 col-12 d-grid align-items-center mt-3">
        <!-- Header (views/kept) -->
        <section class="row">
          <div class="col-12">
            <p class="text-center"><i class="mdi mdi-eye"></i>{{ activeKeep.views }}
              <span class="ps-2">
                <i class="mdi mdi-lock"></i>{{ activeKeep.kept }}
              </span>
            </p>
          </div>
        </section>
        <!-- Body (title/description) -->
        <section class="row">
          <div class="col-12">
            <p class="fs-4 text-center">{{ activeKeep.name }}</p>
            <p class="px-2">{{ activeKeep.description }}</p>
          </div>
        </section>
        <!-- Footer (saveForm, creator info) -->
        <section class="row">
          <div class="col-10 d-flex mb-3">
      <select v-if="account?.id" v-model="editable.vaultId" class="form-select" placeholder="Vault.." id="Vault" required>
        <option v-for="vault in myVaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
      </select>
      <button v-if="account?.id" class="btn btn-secondary" @click="createVaultKeep(activeKeep.id)">Save</button>
          </div>

          <div class="col-2">
            <router-link :to= "{name: 'Profile', params:{profileId: activeKeep.creator.id}}"  >
              <img data-bs-dismiss="modal" class="profile-img" :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" :title="activeKeep.creator.name">
              <!-- <p data-bs-dismiss="modal">{{ activeKeep.creator.name }}</p> -->
            </router-link>
          </div>
        </section>
      </div>
    </section>
  </div>
</template>


<script>
import { computed, ref, watchEffect } from "vue";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import {vaultKeepsService} from "../services/VaultKeepsService"
import Pop from "../utils/Pop";
import { useRoute } from "vue-router";
import { Modal } from "bootstrap";
export default {
  setup(){
    const route = useRoute({})
    const editable = ref({})
    watchEffect(()=>{
      route.params
    })
    return {
      editable,
      activeKeep: computed(()=> AppState.activeKeep),
      myVaults: computed(()=> AppState.myVaults),
      account: computed(()=> AppState.account),
      async createVaultKeep(keepId){
        try{
          const vkData = editable.value
        vkData.keepId = keepId
        // logger.log(keepId)
        vaultKeepsService.createVaultKeep(vkData)
        Modal.getOrCreateInstance('#keepModal').hide()
        } catch(error) {
            Pop.error(error.message);
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img{
  height: 30px;
  width: 30px;
  border-radius: 50%;
}

</style>