<template>
  <section class="row d-flex">
<div class="col-6">
  <button style="height: 4rem;" class="btn btn-outline bg-grey " data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)">
    {{ keep.name }}
  </button>
</div>
<div class="col-6 text-end pe-0">
  
  <button v-if="keep.creatorId == account.id" @click="removeKeep(keep.id)" class="btn btn-outline-danger">x</button>
</div>
  <!-- </section> -->
  <!-- <section class="row"> -->
    <div class="col-12 text-end">
      <router-link :to= "{name: 'Profile', params:{profileId: keep.creator.id}}"  >
      <img :src="keep.creator.picture" :alt="keep.creator.name" :title="`Keep Creator ${keep.creator.name}`" class="img-fluid avatar">
    </router-link>
    </div>
  </section>
</template>


<script>
import { computed } from "vue";
import { Keep } from "../models/Keep";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
export default {
  props: {
    keep: {type: Object, required: true}
  },
  setup(){
    return {
      account: computed(()=> AppState.account),
      async setActiveKeep(keepId){
        try{
            await keepsService.setActiveKeep(keepId)
        } catch(error) {
            Pop.error(error.message);
        }
      },
      async removeKeep(keepId){
        try{
            logger.log('keepId', keepId)
        } catch(error) {
            Pop.error(error.message);
        }
      }
      
    }
  }
}
</script>


<style lang="scss" scoped>
.avatar{
  height: 40px;
  width: 40px;
  border-radius: 50%;
  object-fit: cover;
  position: relative;
  top: 350%;
}

</style>