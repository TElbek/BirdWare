<script setup lang="ts">
import { RouterView } from 'vue-router'
import navigation from '@/components/main/navigation/navigation.vue';
</script>

<template>
  <div class="max-w-full-xl mx-auto ps-4 pe-4 pt-2 ">
    <navigation />
    <RouterView />
  </div>
</template>