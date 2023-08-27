<template>
  Vault Page
</template>


<script>
import { onMounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger";
import { keepsService } from "../services/KeepsService";

export default {
  setup(){
    const route = useRoute({})
    async function getVaultKeepsByVaultId(){
      const vaultId = route.params.vaultId
      keepsService.getVaultKeepsByVaultId(vaultId)
    }
    onMounted(()=> {
      getVaultKeepsByVaultId()
    })
    return {

    }
  }
}
</script>


<style lang="scss" scoped>

</style>