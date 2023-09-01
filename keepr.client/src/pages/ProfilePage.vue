<template>
<div v-if="profile" class="container-fluid">
  <section class="row">
    <div class="col-8 m-auto text-center mt-4">
      <img class="img-fluid cover-img rounded" :src="profile.coverImg" :alt="profile.name">
    </div>
    <div class="col-12 text-center">
      <img class="avatar" :src="profile.picture" :alt="profile.name">
    </div>
    <h1 class="text-center">{{ profile.name }}</h1>
    <p class="text-center">{{ profileVaults.length }} Vaults | {{ profileKeeps.length }} Keeps</p>
    <div class="col-10 m-auto">
      <h2>Vaults</h2>
      <section class="row">
        <div v-for="vault in profileVaults" :key="vault.id" class="col-md-3 col-6 elevation-3 m-1" style="height: 150px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${vault.img})`">
          <VaultCardComponent :vault = "vault"/>
        </div>
      </section>
    </div>
    <div class="col-10 m-auto">
      <h3>Keeps</h3>
      <section class="row">
        <div v-for="keep in profileKeeps" :key="keep.id" class="col-md-3 col-6 elevation-3 m-1" style="height: 250px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${keep.img})`">
          <AccountKeepCardComponent :keep = "keep"/>
          </div>
      </section>
    </div>
  </section>
</div>
<div v-else class="container-fluid">
    <div class="row">
      <div class="col-12 p-4">
        <p class="fs-1">
          Loading....
        </p>
      </div>
    </div>
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
import { Modal } from "bootstrap";
import { computed, onMounted, onUnmounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { vaultsService } from "../services/VaultsService";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import {profilesService} from "../services/ProfilesService"
export default {
  setup(){
    const route = useRoute()
    async function getProfileVaults(){
      try{
          let profileId = route.params.profileId
          await vaultsService.getProfileVaults(profileId)
      } catch(error) {
          Pop.error(error.message);
      }
}
    async function getProfileKeeps(){
      try{
          let profileId = route.params.profileId
          await keepsService.getProfileKeeps(profileId)
      } catch(error) {
          Pop.error(error.message);
      }
    }
    async function getProfile(){
      try{
          let profileId = route.params.profileId
          await profilesService.getProfile(profileId)
      } catch(error) {
          Pop.error(error.message);
      }
    }
    watchEffect(()=> {
      getProfileVaults(route.params.profileId)
      getProfileKeeps(route.params.profileId)
      getProfile(route.params.profileId)
    })
    onUnmounted(()=>{
      AppState.profile = [],
      AppState.profileKeeps = {},
      AppState.profileVaults= {}
    })
    return {
      profileKeeps: computed(()=> AppState.profileKeeps),
      profileVaults: computed(()=> AppState.profileVaults),
      profile: computed(()=> AppState.profile)
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
.avatar{
  height: 70px;
  width: 70px;
  border-radius: 50%;
  object-fit: cover;
  position: relative;
  bottom: 20px;
}
</style>