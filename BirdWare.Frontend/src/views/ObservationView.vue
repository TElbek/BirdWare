<template>
    <observation></observation>
</template>

<script setup>
import { defineAsyncComponent } from 'vue';
const observation = defineAsyncComponent(() => import('@/components/observation/observation.vue'));
</script>