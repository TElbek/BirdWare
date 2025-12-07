<template>
    <div class="rounded border border-gray-400 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue';
import "leaflet/dist/leaflet.css"
import * as L from 'leaflet';
import type { observationType } from '@/types/observationType';
import type { GeoJSON } from 'geojson';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();

const initialMap = ref();
const geoJson = ref([] as GeoJSON[]);
const observationLayer = ref({} as L.Layer);
const hasLayer = ref(false);

interface observationMapProps {
    observationer: observationType[]
}

const props = defineProps<observationMapProps>();
const lokalitetIdList = computed(() => [...new Set(props.observationer.map(item => item.lokalitetId))]);
const emit = defineEmits(['addtag']);

onMounted(() => {
    initializeLeaflet();
    addPointsToMap();
});

function initializeLeaflet() {
    initialMap.value = L.map('map');
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 15,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(initialMap.value);
}

function findFirstObservationByLokalitetId(id: number) {
    return props.observationer.find((item) => item.lokalitetId == id);
}

function addPointsToMap() {

    resetGeoJson();

    props.observationer && lokalitetIdList.value.forEach((id) => {
        let obs = findFirstObservationByLokalitetId(id);
        geoJson.value.push({
            type: 'Feature',
            id: id,
            geometry: { type: 'Point', coordinates: obs ? [obs.longitude, obs.latitude] : [0, 0] },
            properties: { name: obs?.lokalitetNavn, id: obs?.lokalitetId }
        })
    });

    observationLayer.value = L.geoJSON(geoJson.value, {
        onEachFeature: function (feature, layer) {
            layer.on({
                click: whenFeatureClicked
            });
        }
    }).addTo(initialMap.value);

    centerMap();
    toggleHasLayer();
}

function whenFeatureClicked(e: L.LeafletMouseEvent) {
    if (obsSelectionStore.hasTagWithName(e.target.feature.properties.name)) {
        emit('addtag', e.target.feature.properties.name);
    }
    else {
        emit('addtag', e.target.feature.properties.name);
        obsSelectionStore.SetGroupingId(0);
    }
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
    if (props.observationer && props.observationer.length > 0) {

        let minLat = Math.min(...props.observationer.map(o => o.latitude)),
            maxLat = Math.max(...props.observationer.map(o => o.latitude)),
            minLng = Math.min(...props.observationer.map(o => o.longitude)),
            maxLng = Math.max(...props.observationer.map(o => o.longitude));

        initialMap.value.setView([(minLat + maxLat) / 2, (minLng + maxLng) / 2], 7);
        initialMap.value.fitBounds([[minLat, minLng], [maxLat, maxLng]], {padding: [20,20]});

    }
}

//function setupEvents() {
//    initialMap.value.on({ zoomend: whenZoomEnd });
//    initialMap.value.on({ move: whenMove });
//}

//function whenZoomEnd(e:L.LeafletEvent) {
//    leafletStore.setZoomLevel(e.target._zoom);
//}

//function whenMove(e:L.LeafletEvent) {
//    let centerLatLong = e.target.getCenter();
//    leafletStore.setCenter(centerLatLong.lat, centerLatLong.lng);
//}

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