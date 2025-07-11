import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useFugleturStore = defineStore('fugletur', () => {
    const chosenFugleturId = ref(0);

    const hasId = computed(() => chosenFugleturId.value != 0);

    function setFugleturId(value: number) {
        chosenFugleturId.value = value;
    }

    return {
        chosenFugleturId,
        hasId,
        setFugleturId
    }
});