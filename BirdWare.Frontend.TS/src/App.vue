<script setup lang="ts">
import { RouterView } from 'vue-router'
import navigation from '@/components/main/navigation/navigation.vue';
</script>

<template>
  <div class="max-w-full-xl mx-auto p-3">
    <div class="flex flex-col gap-y-1">
      <navigation />
      <RouterView />
    </div>
  </div>
</template>