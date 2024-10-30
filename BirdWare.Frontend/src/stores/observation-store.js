import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useObservationStore = defineStore('observation', () => {
    const chosenArtId = ref(undefined);

    const hasId = computed(() => chosenArtId.value != undefined);

    function setArtId(value) {
        chosenArtId.value = value;
    }

    return {
        chosenArtId,
        hasId,
        setArtId
    }
});