<template>
<div class="gallery-container bg-white">
  <!-- <section class="row"> -->
    <figure v-for="keep in keeps" :key="keep.id" class="" >
      <img :src="keep.img" alt="" class="img-fluid">
        <KeepCardComponent :keep = "keep"/>
    </figure>
  <!-- </section> -->
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

        });
        watchEffect(()=>{})
        return {
            keeps: computed(() => AppState.keeps),
            account: computed(()=> AppState?.account),
            
            
             
        };
    },
}
</script>

<style scoped lang="scss">
.container {
   max-width: 1224px;
   margin: 0 auto;
}

img {
   width: 500px;
   object-fit: contain;
   border-radius: 15px;
}

.gallery-container {
  @media only screen 
and (max-width : 390px){
  column-count: 2;
}
// @media only screen 
// and (min-width : 1224px){
// }
column-count: 4;
 column-gap: 20px 20px;
 width: 100%;
}
.gallery-container2 {
 column-count: 2;
 column-gap: 20px 20px;
 width: 1200px;
}

figure {
 margin: 0;
 display: inline-block;
 margin-bottom: 0px;
 width: 100%;
}
</style>
