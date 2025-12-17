<template>
    <div class="rounded border border-gray-400 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import api from '@/api';
import { ref, onMounted, watch, computed } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import type { birdwareGeoJson } from '@/types/birdwareGeoJsonType';
import { useObservationMapLogic } from '@/composables/observation-map-logic';

const geoJsonObj = ref({} as birdwareGeoJson)
const obsSelectionStore = useObsSelectionStore();
const { initializeLeaflet, addPointsToMap } = useObservationMapLogic(emitAddTag);

const emit = defineEmits(['addtag']);
const queryString = computed(() => { return JSON.stringify(obsSelectionStore.selectedTags) });

const hasData = ref(false);

onMounted(() => {
    initializeLeaflet();
    getGeoJSon();
});

function getGeoJSon() {
    hasData.value = false;
    api.get("observationer/get/tags/geojson?tagListAsJson=" + queryString.value).then(response => {
        geoJsonObj.value = response.data;
        hasData.value = true;
    });
}

function emitAddTag(name: string) {
    if (!obsSelectionStore.hasTagWithName(name)) {
        emit('addtag', name);
        obsSelectionStore.SetGroupingId(0);
    }
}

watch(() => obsSelectionStore.selectedTags, (newValue) => {
    getGeoJSon();
});

watch(() => geoJsonObj.value, (newValue) => {
    if (hasData.value) addPointsToMap(geoJsonObj.value);
});
</script>