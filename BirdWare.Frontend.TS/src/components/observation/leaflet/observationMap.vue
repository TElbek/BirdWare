<template>
    <div class="rounded border border-gray-300 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import "leaflet/dist/leaflet.css"
import * as L from 'leaflet';
import type { observationType } from '@/types/observationType';
import type { GeoJSON } from 'geojson';

const initialMap = ref();
const geoJson = ref([] as GeoJSON[]);
const observationLayer = ref({} as L.Layer);
const hasLayer = ref(false);

interface observationMapProps  {
    observationer: observationType[]
}

const props = defineProps<observationMapProps>();

const lokalitetIdList = computed(() => [...new Set(props.observationer.map(item => item.lokalitetId))]);

onMounted(() => {
    initializeLeaflet();
    addPointsToMap();
});

function initializeLeaflet() {
    initialMap.value = L.map('map');
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(initialMap.value);
}

function findFirstObservationByLokalitetId(id:number) {
    return props.observationer.find((item) => item.lokalitetId == id);
}

function addPointsToMap() {

    resetGeoJson();

    props.observationer && lokalitetIdList.value.forEach((id) => {
        let obs = findFirstObservationByLokalitetId(id);
        geoJson.value.push({
            type: 'Feature',
            geometry: { type: 'Point', coordinates: obs ? [obs.longitude, obs.latitude] : [0, 0] },
            properties: { name: obs?.lokalitetId, count: 1 }
        })
    });

    observationLayer.value = L.geoJSON(geoJson.value).addTo(initialMap.value);
    centerMap();
    toggleHasLayer();
}

function resetGeoJson() {
    geoJson.value = [];
    if (hasLayer.value) {
        removeLayer();
    }
}

function removeLayer() {
    observationLayer.value.remove();
    toggleHasLayer();
}

function toggleHasLayer() {
    hasLayer.value = !hasLayer.value;
}

function centerMap() {
    let minLat = 56,
        maxLat = 56,
        minLng = 11,
        maxLng = 11;

    props.observationer && props.observationer.forEach((item) => {
        minLat = Math.min(minLat, item.latitude);
        maxLat = Math.max(maxLat, item.latitude);
        minLng = Math.min(minLng, item.longitude);
        maxLng = Math.max(maxLng, item.longitude);
    });

    const mapCenter = [(minLat + maxLat) / 2, (minLng + maxLng) / 2];
    initialMap.value.setView(mapCenter, 7);
}

watch(() => props.observationer, (newValue) => {
    addPointsToMap();
});
</script>

<style scoped>
.leaflet {
    height: 80vh;
    z-index: 50 !important;
}
</style>