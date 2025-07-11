<template>
    <div class="d-none d-lg-block">
        <div class="btn-group">
            <div v-for="mode in groupByModeList" class="btn btn-sm birdware btn-equal-width"
                :class="[mode.id == obsSelectionStore.chosenGroupingId ? 'btn-on' : 'btn-off']"
                @click="setGroupByMode(mode.id)">
                {{ mode.caption }}
            </div>
        </div>
    </div>
    <div class="d-lg-none">
        <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
            aria-expanded="false">
            {{ selectedGroupModeCaption }}
        </button>
        <ul class="dropdown-menu">
            <li v-for="item in groupByModeList">
                <a class="dropdown-item birdware" @click="setGroupByMode(item.id)">{{ item.caption }}</a>
            </li>
        </ul>
    </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { type groupByMode } from '@/types/obsGroupingType.ts';

const obsSelectionStore = useObsSelectionStore();

const groupByModeList: groupByMode[] = [
    { caption: 'Årstal', id: 0 },
    { caption: 'Måned', id: 1 },
    { caption: 'Art', id: 2 },
    { caption: 'Lokalitet', id: 3 },
    { caption: 'Region', id: 4 },
];

const selectedGroupModeCaption = computed(() => {
    return groupByModeList.filter((item) => item.id == obsSelectionStore.chosenGroupingId)[0].caption;
});

function setGroupByMode(id: number) {
    obsSelectionStore.SetGroupingId(id);
}
</script>