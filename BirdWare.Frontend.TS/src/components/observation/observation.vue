<template>
  <div class="grid grid-cols-[1fr_max-content] py-2">
    <tw-text-sizeable >{{ route.meta.title }}</tw-text-sizeable>
    <observation-grouping class="z-50" />
  </div>
  <observation-selection />
  <observation-presenter />
</template>

<script setup lang="ts">
import observationGrouping from './observation-grouping.vue';
import observationPresenter from './observation-presenter.vue';
import observationSelection from './observation-selection.vue';

import { useRoute } from 'vue-router';
const route = useRoute();
</script>
