<template>
    <div class="rounded border border-gray-400 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import api from '@/api';
import { ref, onMounted, watch, computed, reactive } from 'vue';
import type { Feature } from 'geojson';
import * as L from 'leaflet';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();

const initialMap = ref();
const observationLayer = ref({} as L.Layer);
const hasLayer = ref(false);

const emit = defineEmits(['addtag']);
const queryString = computed(() => { return JSON.stringify(obsSelectionStore.selectedTags) });

const svgTemplate = `<svg xmlns="http://www.w3.org/2000/svg">
   <circle cx="10" cy="10" r="8" fill="blue" />
   Sorry, your browser does not support inline SVG.
</svg>`;

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
            pointToLayer: function (feature: Feature, latlng: L.LatLng) {
                return L.marker(latlng, {
                    icon: createMapIcon({ width: 22})
                });
            },
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

function createMapIcon({ width = 22, color = '#0000ff'} = {}) {
    const height = Math.round(width * (300 / 240)); // ratio 1.25
    const html = L.Util.template(svgTemplate, {
        width,
        height,
        color,
    });
    return L.divIcon({
        html,
        iconSize: [width, height]
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

watch(() => obsSelectionStore.selectedTags, (newValue) => {
    addPointsToMap();
});
</script>