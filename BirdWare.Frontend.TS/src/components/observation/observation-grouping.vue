<template>
    <tw-show-lg>
        <tw-button-responsive :caption="selectedGroupModeCaption">
            <tw-button v-for="mode in groupByModeList" :caption="mode.caption"
                :isSelected="mode.id == obsSelectionStore.chosenGroupingId" @click="setGroupByMode(mode.id)">
            </tw-button>
        </tw-button-responsive>
    </tw-show-lg>
    <tw-show-md>
        <div class="fixed pb-2 m-4 bottom-0 left-0 right-0 bg-white">
            <tw-button-group class="flex justify-between gap-2 w-full px-2 py-1" :caption="selectedGroupModeCaption">
                <tw-button v-for="mode in groupByModeList" :caption="mode.caption"
                    :isSelected="mode.id == obsSelectionStore.chosenGroupingId" @click="setGroupByMode(mode.id)">
                </tw-button>
            </tw-button-group>
        </div>
    </tw-show-md>
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