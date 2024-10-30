<template>
    <div class="scroll">
        <div class="mt-2 row row-cols-1 row-cols-lg-2 row-cols-xl-3 g-2">
            <div v-for="[key, value] in groupedData" class="mt-1">
                <div class="card h-100 p-1">
                    <div class="card-header birdware fw-semibold">
                        <div class="row">
                            <a class="col" @click="addTag(key)">
                                <div v-if="!obsSelectionStore.isGropingByMonth">{{ key }}</div>
                                <div class="text-capitalize" v-else>{{ getMonthNameFromNumber(key) }}</div>
                            </a>
                            <div class="col-auto fw-semibold accelink">{{ value.length }}</div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div v-for="obs in obsSorted(value)" class="row">
                            <fugleturDato class="col-auto" :fugleturId="obs.fugleturId" :dato="obs.dato"></fugleturDato>
                            <span v-if="obsSelectionStore.showSpeciesNameInList" class="col-12 col-lg-4">{{ obs.artNavn }}</span>
                            <span :class="[obsSelectionStore.showSpeciesNameInList ? 'col-lg-3' : 'col-lg-4']" class="col"> {{
                                obs.lokalitetNavn }}</span>
                            <span class="col-12 col-lg d-inline-block text-truncate"> {{ obs.bem }}</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { reactive, onMounted, computed, watch } from 'vue';
import api from '@/api';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { storeToRefs } from 'pinia'

import fugleturDato from '@/components/main/fugleturdato.vue';

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

function getMonthNameFromNumber(value) {
    const options = {
        month: 'long',
    };
    return new Date('2024-' + value + '-01').toLocaleDateString('da-DK', options);
}

function addTag(text) {
    api.get("tag/" + (obsSelectionStore.isGropingByMonth ? getMonthNameFromNumber(text) : text)).then(response => {
        obsSelectionStore.AddTag(response.data);
    });
}

function obsSorted(value) {
    return value.sort((a, b) => a.dato.localeCompare(b.dato))
        .reverse()
        .slice(0, 10);
}

watch(() => selectedTags.value, (newValue) => {
    getObservations();
});
</script>

<style scoped>
a {
    cursor: pointer;
}
</style>