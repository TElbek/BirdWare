<template>
    <div v-if="obsSelectionStore.chosenViewMode == 0">
        <observationList :grouped-data="groupedData" @addtag="addTag"></observationList>    
    </div>
    <div v-else>
        <observationPlot></observationPlot>
    </div>
</template>

<script setup>
import { reactive, onMounted, computed, watch } from 'vue';
import api from '@/api';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { storeToRefs } from 'pinia'
import { defineAsyncComponent } from 'vue';

const observationPlot = defineAsyncComponent(() => import('./observationplot.vue'));
const observationList = defineAsyncComponent(() => import('./observationlist.vue'));

const obsSelectionStore = useObsSelectionStore();
const { selectedTags } = storeToRefs(obsSelectionStore)

const state = reactive({
    observationer: [],
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
        return Map.groupBy(state.observationer.sort((a, b) => a.aarstal - b.aarstal).reverse(), ({ aarstal }) => aarstal);
    },

    byMaaned: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.maaned - b.maaned), ({ maaned }) => maaned);
    },

    byArt: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.artNavn.localeCompare(b.artNavn)), ({ artNavn }) => artNavn);
    },

    byLokalitet: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.lokalitetNavn.localeCompare(b.lokalitetNavn)), ({ lokalitetNavn }) => lokalitetNavn);
    },

    byRegion: function () {
        return Map.groupBy(state.observationer.sort((a, b) => a.regionNavn.localeCompare(b.regionNavn)), ({ regionNavn }) => regionNavn);
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

function addTag(text) {
    api.get("tag/" + (obsSelectionStore.isGropingByMonth ? getMonthNameFromNumber(text) : text)).then(response => {
        obsSelectionStore.AddTag(response.data);
    });
}

watch(() => selectedTags.value, (newValue) => {
    getObservations();
});
</script>