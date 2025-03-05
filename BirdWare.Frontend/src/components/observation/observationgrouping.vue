<template>
    <div class="btn-group d-none d-lg-block">
        <div v-for="item in groupByModes" class="btn btn-sm" @click="setGroupByMode(item.id)"
            :class="[obsSelectionStore.chosenGroupingId == item.id ? 'btn-on' : 'btn-off']">
            {{ item.caption }}
        </div>
    </div>
    <div class="btn-group d-lg-none">
        <div v-for="item in groupModesSmall" class="btn btn-sm" @click="setGroupByMode(item.id)"
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
    { caption: 'Årstal', id: 0, onlyLarge: false },
    { caption: 'Måned', id: 1, onlyLarge: false },
    { caption: 'Art', id: 2, onlyLarge: true },
    { caption: 'Lokalitet', id: 3, onlyLarge: false },
    { caption: 'Region', id: 4, onlyLarge: true },
];

const groupModesSmall = groupByModes.filter((mode) => mode.onlyLarge == false)

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