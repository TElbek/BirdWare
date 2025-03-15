<template>
    <div class="scroll">
        <div v-for="[key, value] in groupedData" class="border-bottom p-1 mb-1">
            <div class="border-bottom">
                <a @click="addTag(key)">
                    <span class="birdware" v-if="!obsSelectionStore.isGropingByMonth">{{ key }}</span>
                    <span class="birdware text-capitalize" v-else>{{ getMonthNameFromNumber(key) }}</span>
                    <span class="birdware ms-2">({{ value.length }})</span>
                </a>
            </div>
            <div v-for="obs in obsSorted(value)" class="row row-space-below">
                <fugleturDato class="col-auto" :fugleturId="obs.fugleturId" :dato="obs.dato"></fugleturDato>
                <div class="col-5 col-md-2 text-nowrap">{{ obs.artNavn }}</div>
                <div class="col-3 col-md-3 text-nowrap">{{ obs.lokalitetNavn }}</div>
                <div class="col-12 col-md-6">{{ obs.bem }}</div>
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
    emit('addtag',text);
}

function obsSorted(value) {
    return value.sort((a, b) => a.dato.localeCompare(b.dato))
        .reverse()
        .slice(0, 10);
}
</script>