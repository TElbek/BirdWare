<template>
    <tw-button-responsive :caption="selectedGroupModeCaption">
        <tw-button v-for="mode in groupByModeList" :caption="mode.caption"
            :isSelected="mode.id == obsSelectionStore.chosenGroupingId" @click="setGroupByMode(mode.id)">
        </tw-button>
    </tw-button-responsive>
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