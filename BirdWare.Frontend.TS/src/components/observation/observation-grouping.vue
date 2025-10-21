<template>
    <tw-button-dropdown :caption="selectedGroupModeCaption">
        <button class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright" v-for="mode in groupByModeList" @click="setGroupByMode(mode.id)">
            {{ mode.caption }}
        </button>
    </tw-button-dropdown>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { type groupByModeType } from '@/types/obsGroupingType.ts';

const obsSelectionStore = useObsSelectionStore();

const groupByModeList: groupByModeType[] = [
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
}</script>