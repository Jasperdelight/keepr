<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
   <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer>
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
<ModalComponent id="accountModal">
    <template #modalBody>
      <AccountForm/>
    </template>
</ModalComponent>
</template>

<script>

import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
