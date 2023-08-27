<template>
<div class="container-fluid">
  <section class="row" >
    <div v-for="keep in keeps" :key="keep.id" class="col-3 elevation-3 selectable" style="height: 250px; background-repeat: no-repeat; background-position: center; background-size: cover;" :style="`background-image: url(${keep.img})`">
        <KeepCardComponent :keep = "keep"/>
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
<ModalComponent id="newKeepModal">
    <template #modalBody>
      <KeepForm/>
    </template>
</ModalComponent>
<ModalComponent id="newVaultModal">
    <template #modalBody>
      <VaultForm/>
    </template>
</ModalComponent>
</template>

<script>
import { computed, onMounted, watchEffect } from "vue";
import Pop from "../utils/Pop";
import {keepsService} from "../services/KeepsService"
import { AppState } from "../AppState";
import ModalComponent from "../components/ModalComponent.vue";
import KeepForm from "../components/KeepForm.vue";
import { vaultsService } from "../services/VaultsService";
import { useRoute } from "vue-router";

export default {
    setup() {
        async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        // async function getMyVaults(){
        //   try{
              
        //       await vaultsService.getMyVaults()
        //   } catch(error) {
        //       Pop.error(error.message);
        //   }
        // }
        onMounted(() => {
            getKeeps();
        });
        watchEffect(()=>{


        })
        return {
            keeps: computed(() => AppState.keeps),
            account: computed(()=> AppState?.account)
        };
    },
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;


  .home-card {
    width: 50vw;

    .card-img {
      min-height: 200px;
      min-width: 200px;
      background-repeat: no-repeat;
    }
  }
}
</style>
