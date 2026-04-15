<template>
  <div class="flex flex-col gap-y-2">
    <div class="flex flex-row justify-between">
    <tw-text-sizeable >{{ route.meta.title }}</tw-text-sizeable>
    <observation-grouping />
  </div>
  <observation-selection />
  <observation-presenter />
  </div>
</template>

<script setup lang="ts">
import observationGrouping from './observation-grouping.vue';
import observationPresenter from './observation-presenter.vue';
import observationSelection from './observation-selection.vue';

import { useRoute } from 'vue-router';
const route = useRoute();
</script>
