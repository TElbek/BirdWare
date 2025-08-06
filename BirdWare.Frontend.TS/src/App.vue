<script setup lang="ts">
import { RouterView } from 'vue-router'
import navigation from '@/components/main/navigation.vue';
</script>

<template>
  <div class="container-fluid">
    <navigation></navigation>
    <RouterView />
  </div>
</template>