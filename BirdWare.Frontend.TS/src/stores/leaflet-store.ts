import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useLeafletStore = defineStore('leaflet', () => {
    const zoomLevel = ref(7);
    const centerLatitude = ref(56);
    const centerLongitude = ref(11);

    function setCenter(latitude: number, longitude: number) {
        centerLatitude.value = latitude;
        centerLongitude.value = longitude;
    }

    function setZoomLevel(level: number) {
        zoomLevel.value = level;
    }

      return {
        zoomLevel,
        setZoomLevel,
        centerLatitude,
        centerLongitude,
        setCenter
    }
});