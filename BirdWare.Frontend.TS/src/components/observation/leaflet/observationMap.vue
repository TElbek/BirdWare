<template>
    <div class="rounded border border-gray-400 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import api from '@/api';
import { ref, onMounted, watch, computed, reactive } from 'vue';
import "leaflet/dist/leaflet.css"
import * as L from 'leaflet';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();

const initialMap = ref();
const observationLayer = ref({} as L.Layer);
const hasLayer = ref(false);

const emit = defineEmits(['addtag']);
const queryString = computed(() => { return JSON.stringify(obsSelectionStore.selectedTags) });

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

function addPointsToMap() {

    resetGeoJson();

    api.get("observationer/get/tags/geojson?tagListAsJson=" + queryString.value).then(response => {
        observationLayer.value = L.geoJSON(response.data, {
            onEachFeature: function (feature, layer) {
                layer.on({
                    click: whenFeatureClicked
                });
            }
        }).addTo(initialMap.value);

        fitBounds();
        toggleHasLayer();
    });
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

function fitBounds() {
    if (observationLayer) {
        let layerList: L.Layer[] = [observationLayer.value as L.Layer];
        let featureGroup = new L.FeatureGroup<L.Layer>(layerList);
        let bounds = featureGroup.getBounds();
        initialMap.value.fitBounds(bounds, [50, 50]);
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

watch(() => obsSelectionStore.selectedTags, (newValue) => {
    addPointsToMap();
});
</script>

<style scoped>
.leaflet {
    height: 80vh;
    z-index: 50 !important;
}
</style>