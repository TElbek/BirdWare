<template>
    <tw-text-sizeable class="relative top-2">{{ route.meta.title }}</tw-text-sizeable>
    <div class="mt-3 rounded leaflet-small lg:w-220" id="map"></div>
    <div class="grid grid-cols[1fr_1fr_1fr_1fr] lg:grid-cols-[max-content_2fr_max-content_1fr] lg:w-220 gap-y-0.5 lg:gap-x-3 mt-2" v-focus>
        <tw-label>Lokalitet</tw-label>
        <tw-input v-model="lokalitet.navn" v-focus/>
        <tw-label>Region</tw-label>
        <tw-input-select v-model="lokalitet.regionId">
            <option v-for="region in regioner" :key="region.id" :value="region.id">{{ region.navn }}</option>
        </tw-input-select>
    </div>
    <div class="flex mt-3 gap-x-2">
        <tw-button class="bg-gray-100 text-black border shadow border-gray-200 p-2 rounded" @click="saveLokalitet" :caption="'Opret'"></tw-button>
        <tw-button class="bg-gray-100 border shadow border-gray-200 text-black p-2 rounded" @click="cancel" :caption="'Fortryd'"></tw-button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import api from '@/api';
import { onMounted, watch } from 'vue';
import { useGeolocation } from '@/composables/geolocation';
import { useObservationMapLogic } from '@/composables/observation-map-logic';
import type { baseGeoJson } from '@/types/geoJsonTypes';

import { useRoute, useRouter } from 'vue-router';
import type { regionType } from '@/types/regionType';
import type { lokalitetType } from '@/types/lokalitetType';

const route = useRoute();
const router = useRouter();

const regioner = ref([] as regionType[]);
const lokalitet = ref({} as lokalitetType);

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
    getNewLokalitet();
});

function getRegioner() {
    api.get('regioner').then((response) => {
        regioner.value = response.data;
    });
}

function getNewLokalitet() {
    api.get('lokalitet/ny').then((response) => {
        lokalitet.value = response.data;
    });
}

function saveLokalitet() {
    api.post('lokalitet/opret', lokalitet.value).then((response) => {
        router.push({ name: 'addtrip'});        
    });
}

function cancel() {
    router.push({ name: 'addtrip'});        
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
    lokalitet.value.latitude = geoLocation.obtainLocation.value?.coords.latitude ?? 0;
    lokalitet.value.longitude = geoLocation.obtainLocation.value?.coords.longitude ?? 0;
}

watch(() => geoLocation.hasLocation.value, (newValue) => {
    if (newValue) {
        initializeLeaflet();
        setValues();
        addSimplePointToMap(minLokation);
    }
});
</script>