<template>
  <router-link :to="{name: 'Vault', params:{vaultId: vault.id}}">
    <button>{{ vault.name }}</button>
  </router-link>
</template>


<script>
import { Vault } from "../models/Vault";

export default {
  props: {
    vault:{type: Vault, required: true}
  },
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>