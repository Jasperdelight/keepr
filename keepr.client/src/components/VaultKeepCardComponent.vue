<template>
  <section class="row d-flex">
<div class="col-6">
  <button title="View Keep Details" style="height: 4rem;" class="btn btn-outline bg-grey " data-bs-toggle="modal" data-bs-target="#vaultKeepModal" @click="setActiveKeep(keep.id)">
    {{ keep.name }}
  </button>
</div>
<div class="col-6 text-end pe-0">
  
  <button title="Delete Keep from Vault" v-if="keep.creator.id == account.id" @click="removeVaultKeep(keep.vaultKeepId)" class="btn btn-outline-danger">x</button>
</div>
  <!-- </section> -->
  <!-- <section class="row"> -->
    <div class="col-12 text-end">
      <!-- <router-link :to= "{name: 'Profile', params:{profileId: keep.creator.id}}"  >
      <img :src="keep.creator.picture" :alt="keep.creator.name" :title="`Keep Creator ${keep.creator.name}`" class="img-fluid avatar">
    </router-link> -->
    </div>
  </section>
</template>


<script>
import { computed } from "vue";
import { Keep } from "../models/Keep";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import { vaultKeepsService } from "../services/VaultKeepsService";
export default {
  props: {
    keep: {type: Object, required: true}
  },
  setup(){
    return {
      account: computed(()=> AppState.account),
      async setActiveKeep(keepId){
        try{
          
            await keepsService.setActiveKeep(keepId)
        } catch(error) {
            Pop.error(error.message);
        }
      },
      async removeVaultKeep(keepId){
        try{
          const wantsToRemove = await Pop.confirm('Are you sure you want to remove this Vault?')
          if (!wantsToRemove) { return }
            await vaultKeepsService.removeVaultKeep(keepId)
        } catch(error) {
            Pop.error(error.message);
        }
      }
      
    }
  }
}
</script>


<style lang="scss" scoped>
.avatar{
  height: 40px;
  width: 40px;
  border-radius: 50%;
  object-fit: cover;
  position: relative;
  top: 350%;
}

</style>