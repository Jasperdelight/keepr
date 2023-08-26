<template>
  <div  class="modal fade modal-lg" tabindex="-1" role="dialog" aria-labelledby="keepModalLabel" aria-hidden="true" style="min-width: 90%;">
  <div class="modal-dialog" role="document">
    <div  class="modal-content">
      <!-- <div  class="modal-header modal-md">
        
          <slot name="modalTitle"></slot>
        <slot name="modalHeader"></slot>
        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div> -->
      <div class="modal-body modal-lg p-0">
        <slot name="modalBody"></slot>

      </div>
      <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>  -->
      <!-- <div class="modal-footer">
      </div> -->
    </div>
  </div>
</div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState";




export default {
  setup(){
    return {
      activeRecipe: computed(()=>AppState.activeRecipe)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>