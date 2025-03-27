<template>
    <div class="btn-group">
        <div v-for="mode in groupByModes" class="btn btn-sm birdware"
            :class="[mode.id == obsSelectionStore.chosenGroupingId ? 'btn-on' : 'btn-off']"
            @click="setGroupByMode(mode.id)">
            {{ mode.caption }}
        </div>
    </div>
</template>

<script setup>
import { useObsSelectionStore } from '@/stores/obs-selection-store';
const obsSelectionStore = useObsSelectionStore();

const props = defineProps({
    groupByModes: Array
});

function setGroupByMode(id) {
    obsSelectionStore.SetGroupingId(id);
}
</script>

<style scoped>
.btn-sm {
    width: 70px;
}
</style>