<script setup>
import navigation from '@/components/main/navigation.vue'
import { RouterView } from 'vue-router'
</script>

<template>
  <div class="container-fluid">
    <navigation></navigation>
    <div class="mb-2 routerview">
      <RouterView />
    </div>
  </div>
</template>

<style>
body {
  font-size: 0.90rem;
}

.nav-link {
  font-size: 1.1em;
}

.birdware {
  color: #048baf;
}

.btn-on {
  background-color: #048baf;
  color: #fff;
}

.btn-on:hover {
  background-color: #048baf;
  color: #fff;
}

.btn-off {
  border: 1px solid #00000030;
}

.btn-off:hover {
  color: #000;
  border: 1px solid #00000050;
}

.art-flex {
  display: flex;
  flex-wrap: wrap;
  gap: 2px 8px;
}

.card {
  border: 1px solid rgba(0, 0, 0, 0.1);
  box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.05);
  border-radius: 3px;
}

.card-header {
  padding: 3px;
  background-color: transparent;
  border-width: 0px;
}

.card-body {
  padding: 3px;
}

.col-w-100 {
  width: 100px;
}

.large-text {
  font-size: 1.4rem;
}

a {
  color: inherit;
  text-decoration: none;
  cursor: pointer;
}

.multiselect__input,
.multiselect__single {
  min-height: 25px;
}

.multiselect__option {
  background: white;
  outline: none;
  color: #6392b0;
  font-weight: normal;
}

.multiselect__tag {
  color: #6392b0;
  font-weight: bold;
  background: #fff;
  border-radius: 0px;
  border: 1px solid #ddd;
}

.multiselect__option--selected.multiselect__option--highlight {
  background: white;
  color: #6392b0;
  font-weight: normal;
}

.multiselect__tag-icon::after {
  color: #6392b0;
}

.multiselect__tag-icon:focus,
.multiselect__tag-icon:hover {
  background: #fff;
  color: #6392b0;
}

.scroll {
  -ms-overflow-style: none;
  height: 88vh;
  overflow-y: auto;
  overflow-x: hidden;
  padding-bottom: 1px;
  scrollbar-width: none;
}

.scroll::-webkit-scrollbar {
  display: none;
}

@media only screen and (max-width: 992px) {
  .row-space-below {
    margin-bottom: 5px;
  }

  .router-link-exact-active {
    font-weight: 600;
  }

  .router-link-active {
    font-weight: 600;
  }
}

@media only screen and (min-width: 992px) {
  .router-link-exact-active {
    border-bottom: 2px solid #048baf;
  }

  .router-link-active {
    border-bottom: 2px solid #048baf;
  }
}

.btn-birdware,
.btn-birdware:hover {
  background-color: #048baf;
  color: white;
}

.btn-outline-birdware,
.btn-outline-birdware:hover {
  border: 1px solid #048baf;
}


.navbar {
  background-color: white;
}

.nav-link, .nav-link:focus, .nav-link:hover {
  color: #048baf;
}

.navbar-brand {
  padding-top: 0px;
  font-size: 1.3rem;
  font-weight: 600;
  letter-spacing: .1rem;
}

.navbar-brand:hover {
  color: #048baf;
}

.navbar-brand:focus {
  color: #048baf;
}

.navbar-toggler:focus {
  outline: none;
  box-shadow: none;
}

.routerview {
  margin-top: 60px;
}
</style>