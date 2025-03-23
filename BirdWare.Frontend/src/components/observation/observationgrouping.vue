<template>
    <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        {{ selectedGroupModeCaption }}
    </button>
    <ul class="dropdown-menu">
        <li v-for="item in groupByModesSorted">
            <a class="dropdown-item birdware" @click="setGroupByMode(item.id)">{{ item.caption }}</a>
        </li>
    </ul>
</template>

<script setup>
const emit = defineEmits(['groupby'])
import { useObsSelectionStore } from '@/stores/obs-selection-store';
const obsSelectionStore = useObsSelectionStore();
import { computed } from 'vue';

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

const groupByModesSorted = computed(() => {
    return groupByModes.sort((a,b) => a.caption.localeCompare(b.caption));
});

function setGroupByMode(id) {
    obsSelectionStore.SetGroupingId(id);
}
</script>