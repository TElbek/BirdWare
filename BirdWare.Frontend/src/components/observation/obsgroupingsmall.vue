<template>
    <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        {{ caption }}
    </button>
    <ul class="dropdown-menu">
        <li v-for="item in groupByModes">
            <a class="dropdown-item birdware" @click="setGroupByMode(item.id)">{{ item.caption }}</a>
        </li>
    </ul>
</template>

<script setup>
import { useObsSelectionStore } from '@/stores/obs-selection-store';
const obsSelectionStore = useObsSelectionStore();

const props = defineProps({
    caption: '',
    groupByModes: Array
});

function setGroupByMode(id) {
    obsSelectionStore.SetGroupingId(id);
}
</script>