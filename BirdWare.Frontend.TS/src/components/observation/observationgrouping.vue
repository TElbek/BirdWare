<template>
    <bs-show-lg>
        <bs-button-group>
            <template v-for="mode in groupByModeList">
                <bs-button class="btn-equal-width" :isOn="mode.id == obsSelectionStore.chosenGroupingId"
                    @click="setGroupByMode(mode.id)">
                    {{ mode.caption }}
                </bs-button>
            </template>
        </bs-button-group>
    </bs-show-lg>
    <bs-show-md>
        <bs-button-dropdown :caption="selectedGroupModeCaption">
            <ul class="dropdown-menu">
                <li v-for="item in groupByModeList">
                    <a class="dropdown-item birdware" @click="setGroupByMode(item.id)">{{ item.caption }}</a>
                </li>
            </ul>
        </bs-button-dropdown>
    </bs-show-md>
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
}
</script>