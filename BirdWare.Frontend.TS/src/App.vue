<script setup lang="ts">
import { RouterView } from 'vue-router'
import navigation from '@/components/main/navigation.vue';
</script>

<template>
  <div class="container-fluid">
    <navigation></navigation>
    <RouterView/>
  </div>
</template>

<style lang="scss">
$white: #fff;
$birdwareColor: #048baf;
$red: #ff0000;
$birdwareColor-light: #048baf50;
$black-30: #00000030;
$black-20: #00000020;
$black-10: #00000010;
$light-gray: #efefef20;
$sucolor: #ff6347;
$specielcolor: #90ee90;

body {
  font-size: 0.90rem;
  background-color: $light-gray;
}

.nav-link {
  font-size: 1.1em;
  border-bottom: 2px solid transparent;
}

.birdware {
  color: $birdwareColor;
}

.btn-on {
  background-color: $birdwareColor;
  color: $white;
}

.btn-on:hover {
  background-color: $birdwareColor;
  color: $white;
}

.btn-off {
  border: 1px solid $black-30;
}

.btn-off:hover {
  color: $birdwareColor;
  border: 1px solid $black-30;
}

.btn-equal-width {
  width: 70px;
}

.art-flex {
  display: flex;
  flex-wrap: wrap;
  gap: 2px 8px;
}

.lokalitet-flex {
    display: flex;
    flex-wrap: wrap;
    gap: 2px 15px;
}

.card {
  border: 1px solid $black-20;
  box-shadow: 0px 0px 5px $black-20;
  border-radius: 3px;
  padding: 2px;
}

.card-header {
  padding: 3px;
  background-color: transparent;
  border-color: $birdwareColor-light;
  font-weight: 500;
}

.card-body {
  padding: 3px;
}

.col-w-100 {
  width: 100px;
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
  background: $white;
  outline: none;
  color: $birdwareColor;
  font-weight: normal;
}

.multiselect__tag {
  color: $birdwareColor;
  font-weight: 600;
  background: $white;
  border-radius: 3px;
  border: 1px solid $birdwareColor-light;
}

.multiselect__option--selected.multiselect__option--highlight {
  background: $white;
  color: $birdwareColor;
  font-weight: normal;
}

.multiselect__tag-icon::after {
  color: $birdwareColor;
}

.multiselect__tag-icon:focus,
.multiselect__tag-icon:hover {
  background: $white;
  color: $birdwareColor;
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

  .large-text {
    font-size: 1.2rem;
  }
}

@media only screen and (min-width: 992px) {
  .router-link-exact-active {
    border-bottom: 2px solid $birdwareColor;
  }

  .router-link-active {
    border-bottom: 2px solid $birdwareColor;
  }

  .large-text {
    font-size: 1.4rem;
  }
}

.btn-birdware,
.btn-birdware:hover {
  background-color: $birdwareColor;
  color: $white;
}

.btn-outline-birdware,
.btn-outline-birdware:hover {
  border: 1px solid $birdwareColor;
}


.navbar {
  background-color: transparent;
}

.nav-link,
.nav-link:focus,
.nav-link:hover {
  color: $birdwareColor;
}

.navbar-brand {
  margin-bottom: 4px;
}

.navbar-brand:hover {
  color: $birdwareColor;
}

.navbar-brand:focus {
  color: $birdwareColor;
}

.navbar-toggler:focus {
  outline: none;
  box-shadow: none;
}

.btn-outline-birdware,
.btn-outline-birdware:hover {
  color: $birdwareColor;
  background-color: transparent;
  border-color: $black-20;
}

.btn-birdware,
.btn-birdware:hover {
  background-color: $birdwareColor;
  border-color: $black-20;
  color: $white;
}

.btn.show {
  color: $birdwareColor;
}

.dropdown-menu {
  font-size: inherit;
}

.dropdown-item:hover {
  color: $birdwareColor;
}

.offcanvas {
  --bs-offcanvas-width: 50%;
}

.forskel-indikator {
  border-radius: 50%;
  color: white;
  width: 20px;
  font-weight: bold;
}

.forskel-success {
  background-color: $birdwareColor;
}

.forskel-danger {
  background-color: $red;
}

.forekomst {
  border-width: 0px 0px 2px 0px;
  border-style: solid;
}

.forekomst-su {
  border-color: $sucolor;
}

.forekomst-speciel {
  border-color: $specielcolor;
}
</style>