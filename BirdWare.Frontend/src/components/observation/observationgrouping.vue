<template>
    <div class="d-none d-lg-block">
        <obsGrouping :groupByModes="groupByModes" :caption="selectedGroupModeCaption">
        </obsGrouping>
    </div>
    <div class="d-lg-none">
        <obsGroupingSmall :groupByModes="groupByModes" :caption="selectedGroupModeCaption">
        </obsGroupingSmall>
    </div>
</template>

<script setup>
import { defineAsyncComponent } from 'vue';
import { computed } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsGrouping = defineAsyncComponent(() => import('./obsgrouping.vue'));
const obsGroupingSmall = defineAsyncComponent(() => import('./obsgroupingsmall.vue'));
const obsSelectionStore = useObsSelectionStore();

const groupByModes = [
    { caption: 'Årstal', id: 0 },
    { caption: 'Måned', id: 1 },
    { caption: 'Art', id: 2 },
    { caption: 'Lokalitet', id: 3 },
    { caption: 'Region', id: 4 },
];

const selectedGroupModeCaption = computed(() => {
    return groupByModes.filter((item) => item.id == obsSelectionStore.chosenGroupingId)[0].caption;
});
</script>