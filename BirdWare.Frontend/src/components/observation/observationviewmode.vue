<template>
    <div class="btn btn-sm btn-outline-birdware ms-2" @click="obsSelectionStore.SetViewMode()"
        :class="[obsSelectionStore.chosenViewMode == 1 ? 'btn-birdware' : 'btn-off']">
        Plot
    </div>
</template>

<script setup>
import { useObsSelectionStore } from '@/stores/obs-selection-store';
const obsSelectionStore = useObsSelectionStore();
</script>