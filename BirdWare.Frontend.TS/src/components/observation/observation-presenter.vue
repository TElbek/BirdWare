<template>
    <observationMap v-if="obsSelectionStore.isGropingByLocality" class="my-2" :grouped-data="groupedData"></observationMap>
    <observationList v-else class="my-2" :grouped-data="groupedData" @addtag="addTag"></observationList>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, onMounted, computed, watch } from 'vue';
import { storeToRefs } from 'pinia'
import { type observationType } from '@/types/observationType.ts';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import observationList from './observation-list.vue';
import observationMap from './leaflet/observationMap.vue';

const obsSelectionStore = useObsSelectionStore();
const { selectedTags } = storeToRefs(obsSelectionStore);

import { getMonthNameFromNumber } from '@/ts/dateandtime.ts';
import { valueIsNumber } from '@/ts/typechecks.ts';

const state = reactive({
    observationer: [] as observationType[]
});

const queryString = computed(() => { return JSON.stringify(obsSelectionStore.selectedTags) });

const groupedData = computed(() => {
    switch (obsSelectionStore.chosenGroupingId) {
        case 0: return groupByFunctions['byAaarstal']();
        case 1: return groupByFunctions['byMaaned']();
        case 2: return groupByFunctions['byArt']();
        case 3: return groupByFunctions['byLokalitet']();
        case 4: return groupByFunctions['byRegion']();
    }
});

const groupByFunctions = {
    byAaarstal: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.aarstal - b.aarstal).reverse(), ( one: observationType ) => one.aarstal);
    },

    byMaaned: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.maaned - b.maaned), (  one: observationType  ) => one.maaned);
    },

    byArt: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.artNavn.localeCompare(b.artNavn)), (  one: observationType  ) => one.artNavn);
    },

    byLokalitet: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.lokalitetNavn.localeCompare(b.lokalitetNavn)), (  one: observationType  ) => one.lokalitetNavn);
    },

    byRegion: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.regionNavn.localeCompare(b.regionNavn)), (  one: observationType  ) => one.regionNavn);
    }
}

onMounted(() => {
    if (obsSelectionStore.selectedTags.length > 0) {
        getObservations();
    }
});

function getObservations() {
    api.get("observationer/get/tags?tagListAsJson=" + queryString.value).then(response => {
        state.observationer = response.data;
    });
}

function addTag(text: string) {

    let tagText = text;
    if(obsSelectionStore.isGropingByMonth && valueIsNumber(text)) {
        tagText = getMonthNameFromNumber(+text);
    }

    api.get("tag/" + tagText).then(response => {
        obsSelectionStore.AddTag(response.data);
    });
}

watch(() => selectedTags.value, (newValue) => {
    if( newValue.length > 0) {
        getObservations();
    } else {
        state.observationer = [];
    }
});
</script>