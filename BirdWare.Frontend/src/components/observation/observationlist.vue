<template>
    <div class="scroll">
        <div class="row row-cols-1 row-cols-xl-2 g-2">
            <div v-for="[key, value] in groupedData" class="col">
                <div class="card h-100 mb-2">
                    <div class="card-header">
                        <a @click="addTag(key)">
                            <span class="birdware" v-if="!obsSelectionStore.isGropingByMonth">{{ key }}</span>
                            <span class="birdware text-capitalize" v-else>{{ getMonthNameFromNumber(key) }}</span>
                            <span class="birdware ms-1">({{ value.length }})</span>
                        </a>
                    </div>
                    <div class="card-body">
                        <div v-for="obs in obsSorted(value)" class="row">
                            <fugleturDato class="col-auto" :fugleturId="obs.fugleturId" :dato="obs.dato"></fugleturDato>
                            <div class="col-4 col-md-3 d-none d-lg-block  text-nowrap">{{ obs.artNavn }}</div>
                            <div class="col-4 col-md-3  text-nowrap text-truncate">{{ obs.lokalitetNavn }}</div>
                            <div class="col-4 col-md-4 text-truncate text-nowrap">{{ obs.bem }}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { getMonthNameFromNumber } from '@/js/dateandtime';

const obsSelectionStore = useObsSelectionStore();
const props = defineProps({
    groupedData: {}
});
const emit = defineEmits(['addtag']);

function addTag(text) {
    emit('addtag', text);
}

function obsSorted(value) {
    return value.sort((a, b) => a.dato.localeCompare(b.dato))
        .reverse()
        .slice(0, 10);
}
</script>