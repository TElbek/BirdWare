<template>
    <div class="grid grid-rows-[max-content_1fr_max-content] gap-y-2 lg:w-220">
        <tw-text-sizeable class="relative top-2">{{ route.meta.title }}</tw-text-sizeable>
        <div class="mt-2 rounded leaflet-narrow" id="map"></div>
        <div class="grid grid-cols-[70px_1fr] gap-1" v-focus>
            <tw-label>Lokalitet</tw-label>
            <tw-input/>
        </div>
        <div class="grid grid-cols-[70px_1fr] gap-1">
            <tw-label>Region</tw-label>
            <tw-input-select>
                <option v-for="region in regioner" :key="region.id" :value="region.id">{{ region.navn }}</option>
            </tw-input-select>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import api from '@/api';
import { onMounted, watch } from 'vue';
import { useGeolocation } from '@/composables/geolocation';
import { useObservationMapLogic } from '@/composables/observation-map-logic';
import type { baseGeoJson } from '@/types/geoJsonTypes';

import { useRoute } from 'vue-router';
import type { regionType } from '@/types/regionType';
const route = useRoute();

const regioner = ref([] as regionType[]);

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
    getRegioner();
});

function getRegioner() {
    api.get('regioner').then((response) => {
        regioner.value = response.data;
    });
}

function setValues() {
    minLokation.features = [{
        type: "Feature",
        geometry: {
            type: "Point",
            coordinates: [geoLocation.obtainLocation.value?.coords.longitude ?? 0, geoLocation.obtainLocation.value?.coords.latitude ?? 0]
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