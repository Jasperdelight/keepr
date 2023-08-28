<template>
<div class="container-fluid">
  <section class="grid" ref="grid">
    <div v-for="keep in keeps" :key="keep.id" class="grid-item" style=" background-repeat: no-repeat; background-position:0%; background-size: cover;" :style="`background-image: url(${keep.img})`">
      <img :src="keep.img" alt="" class="img-fluid">
        <KeepCardComponent :keep = "keep"/>
    </div>
  </section>
</div>

</template>

<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import Pop from "../utils/Pop";
import {keepsService} from "../services/KeepsService"
import { AppState } from "../AppState";
import ModalComponent from "../components/ModalComponent.vue";
import KeepForm from "../components/KeepForm.vue";
import { vaultsService } from "../services/VaultsService";
import { useRoute } from "vue-router";
import Masonry from "masonry-layout"
export default {
    setup() {
      const grid = ref(null);
      async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        onMounted(() => {
          getKeeps();
              const masonry = new Masonry(grid.value, {
                itemSelector: ".grid-item",
                gutter: 10,
              });
        });
        watchEffect(()=>{})
        return {
            keeps: computed(() => AppState.keeps),
            account: computed(()=> AppState?.account),
            grid,
            
             
        };
    },
}
</script>

<style scoped lang="scss">
.grid-item{
  width: 200px;
  img {
    width: 100%;
  }
}
</style>
