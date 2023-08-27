<template>
  <router-link :to="{name: 'Vault', params:{vaultId: vault.id}}">
    <button class="" @click="setActiveVault(vault.id)">{{ vault.name }}</button>
  </router-link>
  <button v-if="vault.creatorId == account.id" @click="removeVault(vault.id)" class="btn btn-danger">x</button>
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
      removeVault(vaultId){
        try{
            logger.log(vaultId)
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