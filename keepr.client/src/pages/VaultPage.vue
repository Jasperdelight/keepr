<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 mb-0 pb-0">
        <p class="fs-3 text-center mb-0 pb-0">{{ activeVault.name }}</p>
      </div>
      <div class="col-10 m-auto text-center">
      <img class="img-fluid cover-img rounded" :src="activeVault.img" :alt="activeVault.name">
    </div>
    <div class="col-12 m-auto text-center">
      <p class="">
        {{ activeVaultKeeps.length }} Keeps
      </p>
    </div>
    <div v-for="keep in activeVaultKeeps" :key="keep.vaultKeepId" class="col-3 elevation-3 selectable" style="height: 250px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${keep.keeps.img})`">
          <KeepCardComponent :keep = "keep.keeps"/>
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
import { computed, onMounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";

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