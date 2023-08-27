<template>
  <router-link :to="{name: 'Vault', params:{vaultId: vault.id}}">
    <button @click="setActiveVault(vault.id)">{{ vault.name }}</button>
  </router-link>
</template>


<script>
import { Vault } from "../models/Vault";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  props: {
    vault:{type: Vault, required: true}
  },
  setup(){
    
    return {
      setActiveVault(vaultId){
        try{
            vaultsService.setActiveVault(vaultId)
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