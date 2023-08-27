<template>
<div class="container-fluid">
  <section class="row">
    <div class="col-8 m-auto text-center mt-4">
      <img class="img-fluid cover-img rounded" :src="account.coverImg" :alt="account.name">
    </div>
    <div class="col-12 text-center">
      <img class="avatar" :src="account.picture" :alt="account.name">
    </div>
    <h1 class="text-center">{{ account.name }} <button data-bs-toggle="modal" data-bs-target="#accountModal" class="btn btn-outline" title="Edit Account">...</button></h1>
    <p class="text-center">{{ myVaults.length }} Vaults | {{ myKeeps.length }} Keeps</p>
    <div class="col-10 m-auto">
      <h2>Vaults</h2>
      <section class="row">
        <div v-for="vault in myVaults" :key="vault.id" class="col-3 elevation-3" style="height: 150px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${vault.img})`">
          <AccountVaultCardComponent :vault = "vault"/>
        </div>
      </section>
    </div>

    <div class="col-10 m-auto">
      <h3>Keeps</h3>
      <section class="row">
        <div v-for="keep in myKeeps" :key="keep.id" class="col-3 elevation-3" style="height: 250px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${keep.img})`">
          <KeepCardComponent :keep = "keep"/>
          </div>
      </section>
    </div>

  </section>
</div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    async function getMyKeeps(){

    }
    return {
      account: computed(() => AppState.account),
      myVaults: computed(()=> AppState.myVaults),
      myKeeps: computed(()=> AppState.myKeeps)
      
    }
  }
}
</script>

<style scoped>
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
