<template>
    <div class="btn-group">
        <div v-for="item in groupByModes" class="btn btn-sm" @click="setGroupByMode(item.id)"
            :class="[obsSelectionStore.chosenGroupingId == item.id ? 'btn-on' : 'btn-off']">
            {{ item.caption }}
        </div>
    </div>
</template>

<script setup>
const emit = defineEmits(['groupby'])
import { useObsSelectionStore } from '@/stores/obs-selection-store';
const obsSelectionStore = useObsSelectionStore();

const groupByModes = [
    { caption: 'Årstal', id: 0 },
    { caption: 'Måned', id: 1 },
    { caption: 'Art', id: 2 },
    { caption: 'Lokalitet', id: 3 },
    { caption: 'Region', id: 4 },
];

function setGroupByMode(id) {
    obsSelectionStore.SetGroupingId(id);
}
</script>

<style scoped>
.btn-group {
    position: relative;
    top: 6px;
}

.btn-sm {
    width: 70px;
}

.btn-secondary {
    background-color: #6392b0;
}

.btn-outline-secondary {
    border-color: #6392b050;
}

.btn-outline-secondary:hover {
    background-color: #6392b030;
    color: black;
}
</style>