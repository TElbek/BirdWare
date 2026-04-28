<template>
    <div ref="map-wrapper-ref" class="map-wrapper">
        <div class="rounded border border-gray-300 h-full" id="map">
        </div>
    </div>
</template>

<script setup lang="ts">
import type { observationType } from '@/types/observationType';
import { computed, onMounted, onUnmounted, ref, shallowRef, useTemplateRef, watch } from 'vue';

import "leaflet/dist/leaflet.css"
import * as L from 'leaflet';

import 'leaflet.markercluster/dist/MarkerCluster.css';
import 'leaflet.markercluster/dist/MarkerCluster.Default.css';
import "leaflet.markercluster";

import { useWindowSize } from '@/composables/windowSize';
import { useDebounce } from '@/composables/debounce';
import { formatDate } from '@/ts/dateandtime';

const { windowHeight, windowWidth, isMobile } = useWindowSize();
const mapRef = useTemplateRef('map-wrapper-ref');
const mapYPos = ref(0);
const heightExpr = computed(() => 'calc(100vh - ' + Math.round(mapYPos.value) + 'px  - ' + (isMobile.value == true ? '50px' : '20px') + ')');

const { debounce } = useDebounce();
const debounceWindowEventHandler = debounce(() => windowEventHandler(), 500);

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
    initialMap.value = L.map('map');
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 14,
        minZoom: 7,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(initialMap.value);

    updateMapYPos();
    addMarkers();
});

onUnmounted(() => {
    if (initialMap.value) {
        initialMap.value.remove();
    }
});

function updateMapYPos() {
    if (mapRef.value) {
        mapYPos.value = mapRef.value.getBoundingClientRect().top;
    }
}

function windowEventHandler() {
    updateMapYPos();
    fitBounds();
}

function addMarkers() {
    markers.clearLayers();
    for (const [lokalitetId, observationer] of groupedData.value) {
        const firstObservation = observationer[0];
        if (firstObservation.latitude && firstObservation.longitude && firstObservation.regionId > 0) {
            const marker = L.circleMarker([firstObservation.latitude, firstObservation.longitude]);
            marker.setStyle({ color: observationer.length > averageCount.value ? 'green' : 'blue' });
            marker.bindPopup(`<b>${firstObservation.lokalitetNavn}</b><br>Observationer: ${observationer.length}</br>Seneste: ${formatDate(firstObservation.dato)} `);
            markers.addLayer(marker);
        }
    }
    initialMap.value.addLayer(markers);
    fitBounds();
}

function fitBounds() {
    if (markers && initialMap.value) {
        let layerList: L.Layer[] = [markers];
        let featureGroup = new L.FeatureGroup<L.Layer>(layerList);
        let bounds = featureGroup.getBounds();
        try {
            initialMap.value.fitBounds(bounds, { padding: [100, 100] });
        }
        catch {
            initialMap.value.setView([56, 11], 7);
            return;
        }
    }
}

watch(() => props.observationer, (newVal) => {
    addMarkers();
});

watch([windowHeight, windowWidth, isMobile], () => {
    debounceWindowEventHandler();
});
</script>

<style scoped>
.map-wrapper {
    height: v-bind(heightExpr);
    z-index: 0 !important;
}
</style>