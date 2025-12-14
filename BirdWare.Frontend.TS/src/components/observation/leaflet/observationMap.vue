<template>
    <div class="rounded border border-gray-400 leaflet" id="map"></div>
</template>

<script setup lang="ts">
import api from '@/api';
import { ref, onMounted, watch, computed } from 'vue';
import type { Feature } from 'geojson';
import * as L from 'leaflet';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import type { birdwareGeoJson } from '@/types/birdwareGeoJsonType';

const obsSelectionStore = useObsSelectionStore();

const geoJsonObj = ref({} as birdwareGeoJson)

const map = ref();
const observationLayer = ref({} as L.Layer);
const hasLayer = ref(false);

const emit = defineEmits(['addtag']);
const queryString = computed(() => { return JSON.stringify(obsSelectionStore.selectedTags) });

const hasData = ref(false);

const svgTemplateBlue = `<svg xmlns="http://www.w3.org/2000/svg">
   <circle cx="10" cy="10" r="8" fill="#0000ff" />
   Sorry, your browser does not support inline SVG.
</svg>`;

const svgTemplateRed = `<svg xmlns="http://www.w3.org/2000/svg">
   <circle cx="10" cy="10" r="8" fill="#ff0000" />
   Sorry, your browser does not support inline SVG.
</svg>`;


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

function initializeLeaflet() {
    map.value = L.map('map');
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 15,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map.value);
}

function addPointsToMap() {
    resetGeoJson();
    observationLayer.value = L.geoJSON(geoJsonObj.value as any, {
        pointToLayer: function (feature: Feature, latlng: L.LatLng) {
            let marker = L.marker(latlng, {
                icon: createMapIcon({ width: 22, countIsAboveAverage: feature.properties?.countIsAboveAverage })
            });
            marker.on('mouseover', function (ev) {
                marker.openPopup();
            });
            marker.on('click', function (ev) {
                whenFeatureClicked(ev);
            });
            return marker;
        },
        onEachFeature: function (feature, layer) {
            layer.bindPopup('<strong>' + feature.properties?.name + '</strong><br/> ' + feature.properties?.count + ' observationer')
        }
    }).addTo(map.value);

    fitBounds();
    toggleHasLayer();
}

function createMapIcon({ countIsAboveAverage = false, width = 22, color = '#0000ff' } = {}) {
    const height = Math.round(width * (300 / 240)); // ratio 1.25
    const html = L.Util.template(countIsAboveAverage ? svgTemplateRed : svgTemplateBlue, {
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
        map.value.fitBounds(bounds, { padding: [20, 20] });
    }
}

watch(() => obsSelectionStore.selectedTags, (newValue) => {
    getGeoJSon();
});

watch(() => geoJsonObj.value, (newValue) => {
    if (hasData.value) addPointsToMap();
});
</script>