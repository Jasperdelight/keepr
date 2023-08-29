<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-white px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center text-dark selectable rounded">
        Home
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <div class="dropdown">
  <button v-if="account?.id" class="btn btn-outline dropdown-toggle selectable" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Create <i class="mdi mdi-triangle-down-outline"></i>
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
    <a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#newKeepModal">New Keep </a>
    <a class="dropdown-item" href="#"  data-bs-toggle="modal" data-bs-target="#newVaultModal">New Vault</a>
  </div>
</div>
      <ul class="navbar-nav me-auto">
        <li>
          
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed } from "vue";
import Login from './Login.vue';
import { AppState } from "../AppState";
export default {
  setup() {
    return {
      account:computed(()=> AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
