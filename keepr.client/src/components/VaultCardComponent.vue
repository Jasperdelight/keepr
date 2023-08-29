<template>
  <div class="d-flex justify-content-between">

    <router-link :to="{name: 'Vault', params:{vaultId: vault.id}}">
      <button title="View Vault Details" class="btn glassbox" @click="setActiveVault(vault.id)">{{ vault.name }}</button>
    </router-link>
    <i v-if="vault.isPrivate" title="Vault is Private" class="mdi mdi-lock text-white text-shadow rounded"></i>
  </div>
    <!-- <button v-if="vault.creatorId == account.id" @click="removeVault(vault.id)" class="btn btn-danger">x</button> -->
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
      setActiveVault(vaultId){
        try{
            vaultsService.setActiveVault(vaultId)
        } catch(error) {
            Pop.error(error.message);
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped>
.glassbox{
  background-color: rgba(255, 255, 255, 0.658);
}
</style>