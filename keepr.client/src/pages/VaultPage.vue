<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 mb-0 pb-0">
        <p class="fs-3 text-center mb-0 pb-0">{{ activeVault.name }}</p>
      </div>
      <div class="col-10 m-auto text-center">
      <img class="img-fluid cover-img rounded" :src="activeVault.img" alt="Vault Name">
    </div>
    <div class="col-12 m-auto text-center">
      <p class="">
        {{ activeVaultKeeps.length }} Keeps
      </p>
    </div>
    <div v-for="keep in activeVaultKeeps" :key="keep.id" class="col-md-3 col-6 elevation-3" style="height: 250px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${keep.img})`">
          <VaultKeepCardComponent :keep = "keep"/>
          </div>
    </section>
  </div>
  <ModalComponent id="keepModal">
  <!-- <template #modalHeader>
      header
    </template> -->
    <template #modalBody>
      <KeepModalSlot/>
    </template>
</ModalComponent>
</template>


<script>
import { computed, onMounted, onUnmounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
export default {
  setup(){
    const route = useRoute()
    const router = useRouter()
    async function getVaultKeepsByVaultId(){
      try{
        let vaultId = route.params.vaultId
        await keepsService.getVaultKeepsByVaultId(vaultId)
      } catch(error) {
          Pop.error(error.response.data);
          // logger.log(error.response.data)
          if(error.response.data.includes('😌')){
          router.push({name: "Home"})
        }
      }
    }
    async function setActiveVault(){
        try{
            
            await vaultsService.setActiveVault(route.params.vaultId)
        } catch(error) {
            Pop.error(error.message);
        }
      }
    onMounted(()=>{
      setActiveVault()
    })
    watchEffect(()=> {
      getVaultKeepsByVaultId(route.params.vaultId)
    })
    onUnmounted(()=>{
      AppState.activeVault = {},
      AppState.activeVaultKeeps = []
    })
    return {
      activeVault: computed(()=> AppState.activeVault),
      activeVaultKeeps: computed(()=> AppState.activeVaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img{
  height: 15rem;
  width: 30rem;
  object-fit: cover;
}
</style>