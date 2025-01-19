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
                    <div class="btn btn-sm" @click="navigateList()" :class="[obsSelectionStore.chosenViewMode == 0 ? 'btn-birdware' : 'btn-off']">
                        Liste
                    </div>
                    <div class="btn btn-sm" @click="navigatePlot()"  :class="[obsSelectionStore.chosenViewMode == 1 ? 'btn-birdware' : 'btn-off']">
                        Plot
                    </div>
                </div>
            </div>
            <div class="col-auto">
                <observation-group-by></observation-group-by>
            </div>
        </div>
        <router-view></router-view>
    </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { defineAsyncComponent } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { useRouter } from 'vue-router';

const obsSelectionStore = useObsSelectionStore();
const observationSelection = defineAsyncComponent(() => import('./observationselection.vue'));
const observationGroupBy = defineAsyncComponent(() => import('./observationgrouping.vue'));
const router = useRouter();

onMounted(() => {
    if(obsSelectionStore.chosenViewMode == 0) {
        navigateList();
    } else {
        navigatePlot();
    }
});

function navigateList() {
    obsSelectionStore.SetViewMode(0);
    router.push({name:'art-observation-liste'});
};

function navigatePlot() {
    obsSelectionStore.SetViewMode(1);
    router.push({name:'art-observation-plot'});
};
</script>

<style scoped>
.btn-group {
    position: relative;
    top: 6px;
}
</style>