import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useFugleturStore = defineStore('fugletur', () => {
    const chosenFugleturId = ref(undefined);

    const hasId = computed(() => chosenFugleturId.value != undefined);

    function setFugleturId(value) {
        chosenFugleturId.value = value;
    }

    return {
        chosenFugleturId,
        hasId,
        setFugleturId
    }
});