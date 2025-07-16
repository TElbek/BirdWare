<template>
    <div class="scroll">
        <div class="row g-2" :class="getRowColClassesTwo(props.groupedData?.size ?? 0)">
            <div v-for="[key, value] in props.groupedData" class="col">
                <div class="card h-100 mb-2">
                    <div class="card-header">
                        <a @click="addTag(key)">
                            <span class="birdware" v-if="obsSelectionStore.isGropingByMonth && valueIsNumber(key)">{{
                                getMonthNameFromNumber(+key) }}</span>
                            <span class="birdware text-capitalize" v-else>{{ key }}</span>
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

<script setup lang="ts">
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { getMonthNameFromNumber } from '@/ts/dateandtime';
import { type observationType } from '@/types/observationType';
import { valueIsNumber } from '@/ts/typechecks';
import fugleturDato from '../main/fugleturdato.vue';
import { getRowColClassesTwo } from '@/ts/rowcols';

const obsSelectionStore = useObsSelectionStore();

const props = defineProps({
    groupedData: Map<string | number, observationType[]>
});

const emit = defineEmits(['addtag']);

function addTag(value: string | number) {
    emit('addtag', value);
}

function obsSorted(value: observationType[]) {
    return value.sort((a, b) => a.dato.localeCompare(b.dato))
        .reverse()
        .slice(0, 10);
}
</script>