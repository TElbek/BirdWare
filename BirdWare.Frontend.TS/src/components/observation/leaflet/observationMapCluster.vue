<template>
    <div class="rounded border border-gray-300 h-full" id="map" style="height:75vh;">        
    </div>
</template>

<script setup lang="ts">
import type { observationType } from '@/types/observationType';
import { computed, onMounted, ref, shallowRef, watch } from 'vue';

import "leaflet/dist/leaflet.css"
import * as L from 'leaflet';

import 'leaflet.markercluster/dist/MarkerCluster.css';
import 'leaflet.markercluster/dist/MarkerCluster.Default.css';
import "leaflet.markercluster";

const initialMap = shallowRef();
const props = defineProps({
    observationer: {
        type: Array as () => observationType[],
        required: true
    }
});

const markers = L.markerClusterGroup();

const groupedData = computed(() => Map.groupBy(
            props.observationer.sort((a, b) => a.lokalitetNavn.localeCompare(b.lokalitetNavn)),
            (one: observationType) => (one.lokalitetId)));

const averageCount = computed(() => {
    const totalObservations = props.observationer.length;
    const uniqueLokalitetCount = new Set(props.observationer.map(obs => obs.lokalitetId)).size;
    return uniqueLokalitetCount > 0 ? (totalObservations / uniqueLokalitetCount) : 0.00;
});

onMounted(() => {
  initialMap.value = L.map('map').setView([56, 11], 7);
  L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
  }).addTo(initialMap.value);

  addMarkers();
});      

function addMarkers() {
    markers.clearLayers();
    for (const [lokalitetId, observationer] of groupedData.value) {
        const firstObservation = observationer[0];
        if (firstObservation.latitude && firstObservation.longitude) {
            const marker = L.circleMarker([firstObservation.latitude, firstObservation.longitude]);
            marker.setStyle({ color: observationer.length > averageCount.value ? 'green' : 'blue' });
            marker.bindPopup(`<b>${firstObservation.lokalitetNavn}</b><br>Antal observationer: ${observationer.length}`);
            markers.addLayer(marker);
        }
    }
    initialMap.value.addLayer(markers);
}

watch(() => props.observationer, (newVal) => {
    addMarkers();
});
</script>