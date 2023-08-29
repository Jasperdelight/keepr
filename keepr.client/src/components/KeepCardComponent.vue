<template>
  <figcaption class="justify-content-around">
<div class="">
  <button style="" class="btn btn-outline glassbox" data-bs-toggle="modal" data-bs-target="#keepModal" @click="setActiveKeep(keep.id)">
    {{ keep.name }}
  </button>
</div>
<div class=" text-end">
  
</div>
<!-- </section> -->
<!-- <section class="row"> -->
  <div class=" text-end">
    <router-link :to= "{name: 'Profile', params:{profileId: keep.creator.id}}"  >
      <img :src="keep.creator.picture" :alt="keep.creator.name" :title="`Keep Creator ${keep.creator.name}`" class="img-fluid avatar">
    </router-link>
  </div>
  <button v-if="keep.creatorId == account.id" @click="removeKeep(keep.id)" class="btn btn-outline-danger">x</button>
  </figcaption>
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
          const wantsToRemove = await Pop.confirm('Are you sure you want to remove this Vault?')
          if (!wantsToRemove) { return }
          await keepsService.removeKeep(keepId)
        } catch(error) {
            Pop.error(error.message);
        }
      }
      
    }
  }
}
</script>


<style lang="scss" scoped>

body {
  background-color: #000;
  font: 1.1em Arial, Helvetica, sans-serif;
}

img {
  max-width: 100%;
  display: block;
}

figure {
  margin: 0;
  display: grid;
  // grid-template-rows: 1fr auto;
  background-color: white;
}

figure > img {
  grid-row: 1 / -1;
  grid-column: 1;
}

figure a {
  color: black;
  text-decoration: none;
}

figcaption {
  grid-row: 2;
  grid-column: 1;
  background-color: rgba(255, 255, 255, 0);
  padding: .2em .5em;
  justify-self: start;
  position: relative;
  display: flex;
  bottom: 60px;
}

.container {
  display: grid;
  gap: 10px;
  grid-template-columns: 1fr 2fr;
  // height: 200vh;
  align-tracks: inherit ;
  align-content: start;
}
figcaption .avatar {
  display: flex;
  align-items: center;
}
.avatar{
  height: 40px;
  width: 40px;
  border-radius: 50%;
  object-fit: cover;
  position: relative;
}
.glassbox{
  background-color: rgba(255, 255, 255, 0.658);
}

</style>