<template>
    <div>
        <div class="row">
            <div class="col-auto birdware large-text">
                Observation
            </div>
            <div class="col-12 col-lg">
                <observation-selection></observation-selection>
            </div>
            <div class="col-auto">
                <div class="btn-group">
                    <div class="btn btn-sm" @click="setViewMode(0)" :class="[obsSelectionStore.chosenViewMode == 0 ? 'btn-birdware' : 'btn-off']">
                        Liste
                    </div>
                    <div class="btn btn-sm" @click="setViewMode(1)"  :class="[obsSelectionStore.chosenViewMode == 1 ? 'btn-birdware' : 'btn-off']">
                        Plot
                    </div>
                </div>
            </div>
            <div class="col-auto">
                <observation-group-by></observation-group-by>
            </div>
        </div>
        <observation-presenter></observation-presenter>
    </div>
</template>

<script setup>
import { defineAsyncComponent } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();
const observationSelection = defineAsyncComponent(() => import('./observationselection.vue'));
const observationGroupBy = defineAsyncComponent(() => import('./observationgrouping.vue'));
const observationPresenter = defineAsyncComponent(() => import('./observationpresenter.vue'));

function setViewMode(mode) {
    obsSelectionStore.SetViewMode(mode);
}
</script>

<style scoped>
.btn-group {
    position: relative;
    top: 6px;
}
</style>