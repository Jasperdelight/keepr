<template>
  <div class="container-fluid" v-if="activeKeep">
    <section class="row">
      <!-- Image half Modal LEFT -->
      <div class="col-6 p-0">
        <img :src="activeKeep.img" alt="" class="img-fluid" style="height: 100%;">
      </div>
      <!-- Information half RIGHT -->
      <div class="col-6">
        <!-- Header (views/kept) -->
        <section class="row">
          <div class="col-12">
            <p class="text-center"><i class="mdi mdi-eye"></i>{{ activeKeep.views }}
              <span class="ps-2">
                <i class="mdi mdi-lock"></i>{{ activeKeep.kept }}
              </span>
            </p>
          </div>
        </section>
        <!-- Body (title/description) -->
        <section class="row">
          <div class="col-12">
            <p class="fs-4 text-center">{{ activeKeep.name }}</p>
            <p>{{ activeKeep.description }}</p>
          </div>
        </section>
        <!-- Footer (saveForm, creator info) -->
        <section class="row">
          <div class="col-6"></div>

          <div class="col-6">
            <img class="profile-img" :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" :title="activeKeep.creator.name">
            <p>{{ activeKeep.creator.name }}</p>
          </div>
        </section>
      </div>
    </section>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState";

export default {
  setup(){
    return {
      activeKeep: computed(()=> AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img{
  height: 60px;
  width: 60px;
  border-radius: 50%;
}

</style>