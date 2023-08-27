<template>
  <button class="btn btn-outline bg-grey " data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.vaultKeeperId)">
    {{ keep.name }} test
  </button>
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

      async setActiveKeep(keepId){
        try{
            await keepsService.setActiveKeep(keepId)
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