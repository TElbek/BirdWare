<template>
    <div class="grid grid-rows-[max-content_1fr] gap-2">
        <tw-text-sizeable class="relative top-2">{{ route.meta.title }}</tw-text-sizeable>
        <div class="mt-2 rounded leaflet-narrow" id="map"></div>
    </div>
</template>

<script setup lang="ts">
import { onMounted, watch } from 'vue';
import { useGeolocation } from '@/composables/geolocation';
import { useObservationMapLogic } from '@/composables/observation-map-logic';
import type { baseGeoJson } from '@/types/geoJsonTypes';

import { useRoute } from 'vue-router';
const route = useRoute();

interface geoJson extends baseGeoJson {
    type: "FeatureCollection";
    features: Array<{
        type: "Feature";
        geometry: {
            type: "Point";
            coordinates: [number, number];
        };
        properties: {
            name: string;
        };
    }>;
}

const minLokation = {} as geoJson;
const geoLocation = useGeolocation();
const { initializeLeaflet, addSimplePointToMap } = useObservationMapLogic(null);

onMounted(() => {
    geoLocation.getLocation();
});

function setValues() {
    minLokation.features = [{
        type: "Feature",
        geometry: {
            type: "Point",
            coordinates: [geoLocation.obtainLocation.value?.coords.longitude ?? 0, geoLocation.obtainLocation.value?.coords.latitude  ?? 0]
        },
        properties: {
            name: "Nuv. position"
        }
    }];
}

watch(() => geoLocation.hasLocation.value, (newValue) => {
    if (newValue) {
        initializeLeaflet();
        setValues();
        addSimplePointToMap(minLokation);
    }
});
</script>