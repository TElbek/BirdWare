<template>
    <div ref="map-wrapper-ref" class="map-wrapper">
        <div class="rounded border border-gray-300 h-full" id="map">
        </div>
    </div>
</template>

<script setup lang="ts">
import type { observationType } from '@/types/observationType';
import { computed, onMounted, ref, useTemplateRef, watch } from 'vue';

import "leaflet/dist/leaflet.css"

import { useWindowSize } from '@/composables/windowSize';
import { useDebounce } from '@/composables/debounce';
import { useObservationMapLogicCluster } from '@/composables/observation-map-logic-cluster';

const { initializeLeaflet, addMarkers, fitBounds } = useObservationMapLogicCluster();
const { windowHeight, windowWidth, isMobile } = useWindowSize();
const mapRef = useTemplateRef('map-wrapper-ref');
const mapYPos = ref(0);
const heightExpr = computed(() => 'calc(100vh - ' + Math.round(mapYPos.value) + 'px  - ' + (isMobile.value == true ? '50px' : '20px') + ')');

const { debounce } = useDebounce();
const debounceWindowEventHandler = debounce(() => windowEventHandler(), 500);

const props = defineProps({
    observationer: {
        type: Array as () => observationType[],
        required: true
    }
});

onMounted(() => {
    initializeLeaflet();
    updateMapYPos();
    addMarkers(props.observationer);
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

watch(() => props.observationer, (newVal) => {
    addMarkers(props.observationer);
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