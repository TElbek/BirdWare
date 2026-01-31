import { reactive, computed } from 'vue';

export function useGeolocation() {
    const state = reactive({
        location: {} as GeolocationPosition,
    });

    const hasLocation = computed(() => {
        return state.location.coords !== undefined;
    });

    const obtainLocation = computed(() => {
        return hasLocation.value ? state.location : null;
    });

    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(setLocation);
        }
    }

    function setLocation(position: GeolocationPosition) {
        state.location = position;
    }

    return {
        hasLocation,
        getLocation,
        obtainLocation,
    };
}