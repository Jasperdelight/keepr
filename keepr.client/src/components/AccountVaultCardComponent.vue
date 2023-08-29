<template>
  <div class="d-flex justify-content-between">

    <router-link :to="{name: 'Vault', params:{vaultId: vault.id}}">
    <button class="btn glassbox" @click="setMyActiveVault(vault.id)">{{ vault.name }}</button>
  </router-link>
  <i v-if="vault.isPrivate" title="Vault is Private" class="mdi mdi-lock text-white text-shadow rounded"></i>
  <button title="Delete Vault" @click="removeVault(vault.id)" class="btn btn-outline-danger">x</button>
</div>
</template>


<script>
import { computed } from "vue";
import { Vault } from "../models/Vault";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";

export default {
  props: {
    vault:{type: Vault, required: true}
  },
  setup(){
    
    return {
      account: computed(()=> AppState.account),
      async setMyActiveVault(vaultId){
        try{
            await vaultsService.setMyActiveVault(vaultId)
        } catch(error) {
            Pop.error(error.message);
        }
      },
      async removeVault(vaultId){
        try{
          const wantsToRemove = await Pop.confirm('Are you sure you want to remove this Vault?')
          if (!wantsToRemove) { return }
            await vaultsService.removeVault(vaultId)
        } catch(error) {
            Pop.error(error.message);
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.glassbox{
  background-color: rgba(255, 255, 255, 0.658);
}
</style>