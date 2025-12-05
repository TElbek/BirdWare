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
const layer = ref({} as L.Layer);
const hasLayer = ref(false);

const props = defineProps({
    groupedData: Map<string | number, observationType[]>
});

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

function addPointsToMap() {

    resetGeoJson();

    props.groupedData && props.groupedData.forEach((value, key) => {
        geoJson.value.push({
            type: 'Feature',
            geometry: { type: 'Point', coordinates: value[0] ? [value[0]?.longitude, value[0]?.latitude] : [0, 0] },
            properties: { name: key, count: value.length }
        })
    });

    layer.value = L.geoJSON(geoJson.value).addTo(initialMap.value);
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
    layer.value.remove();
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

    props.groupedData && props.groupedData.forEach((value, key) => {
        minLat = Math.min(minLat, value[0] && value[0].latitude);
        maxLat = Math.max(maxLat, value[0] && value[0].latitude);
        minLng = Math.min(minLng, value[0] && value[0].longitude);
        maxLng = Math.max(maxLng, value[0] && value[0].longitude);
    });

    const mapCenter = [(minLat + maxLat) / 2, (minLng + maxLng) / 2];
    initialMap.value.setView(mapCenter, 7);
}

watch(() => props.groupedData, (newValue) => {
    addPointsToMap();
});
</script>

<style scoped>
.leaflet {
    height: 80vh;
    z-index: 50 !important;
}
</style>