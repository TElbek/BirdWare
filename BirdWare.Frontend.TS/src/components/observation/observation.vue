<template>
  <div class="grid grid-cols-[auto_110px] mb-1">
    <tw-text-sizeable class="mt-4">{{ route.meta.title }}</tw-text-sizeable>
    <observation-grouping class="z-50" />
  </div>
  <observation-selection />
  <observation-presenter class="max-h-140 xl:max-h-175 overflow-auto" />
</template>

<script setup lang="ts">
import observationGrouping from './observation-grouping.vue';
import observationPresenter from './observation-presenter.vue';
import observationSelection from './observation-selection.vue';

import { useRoute } from 'vue-router';
const route = useRoute();
</script>
