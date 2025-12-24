<template>
    <div class="rounded border border-gray-400 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import api from '@/api';
import { useObservationMapLogic } from '@/composables/observation-map-logic';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import type { observationGeoJson } from '@/types/observationGeoJsonType';
import { computed, onMounted, ref, watch } from 'vue';

const geoJsonObj = ref({} as observationGeoJson)
const obsSelectionStore = useObsSelectionStore();
const { initializeLeaflet, addPointsToMap, resetGeoJson } = useObservationMapLogic(emitAddTag);

const emit = defineEmits(['addtag']);
const queryString = computed(() => { return JSON.stringify(obsSelectionStore.selectedTags) });

const hasData = ref(false);

onMounted(() => {
    initializeLeaflet();
    getGeoJSon();
});

function getGeoJSon() {
    if (obsSelectionStore.selectedTags.length > 0) {
        hasData.value = false;
        api.get("observationer/get/tags/geojson?tagListAsJson=" + queryString.value).then(response => {
            geoJsonObj.value = response.data;
            hasData.value = true;
        });
    }
    else {
        geoJsonObj.value = {} as observationGeoJson;
        hasData.value = false;
        resetGeoJson();
    }
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