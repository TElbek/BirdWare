<template>
    <div class="d-block">
        <div class="btn btn-sm btn-outline-birdware" @click="obsSelectionStore.SetViewMode()"
            :class="[obsSelectionStore.chosenViewMode == 1 ? 'btn-birdware' : 'btn-off']">
            Plot
        </div>
    </div>
</template>

<script setup>
import { useObsSelectionStore } from '@/stores/obs-selection-store';
const obsSelectionStore = useObsSelectionStore();
</script>