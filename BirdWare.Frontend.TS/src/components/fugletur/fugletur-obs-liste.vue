<template>
    <tw-grid-cols-five :count="byFamilie.size">
        <div v-for="[key, value] in byFamilie">
            <tw-card>
                <span class="text-base font-medium tracking-wide text-birdware dark:text-birdware-bright capitalize">{{ key}}</span>
                <span class="text-base font-medium tracking-wide text-birdware dark:text-birdware-bright float-end">{{ value.length }}</span>
                <table-birdware>
                    <template v-for="obs in value">
                        <table-row-birdware>
                            <table-cell-birdware>
                                <art-navn :art-id="obs.artId" :art-navn="obs.artNavn" :speciel="obs.speciel" :su="obs.su"></art-navn>
                            </table-cell-birdware>
                            <table-cell-birdware>
                                {{ obs.bem }}
                            </table-cell-birdware>  
                        </table-row-birdware>
                    </template>
                </table-birdware>
            </tw-card>
        </div>
    </tw-grid-cols-five>
</template>

<script setup lang="ts">
import { reactive, computed, watch, onMounted } from 'vue';
import api from '@/api';
import { useFugleturStore } from '@/stores/fugletur-store';
import type { observationType } from '@/types/observationType';
import artNavn from '../main/artNavn.vue';

import { storeToRefs } from 'pinia'
const fugleturStore = useFugleturStore();

const { chosenFugleturId } = storeToRefs(fugleturStore);

const state = reactive({
    obsListe: [] as observationType[],
});

const byFamilie = computed(() => { return Map.groupBy(state.obsListe, (one: observationType) => one.familieNavn) });
const byFamilieCount = computed(() => [...new Set(state.obsListe.map(item => item.familieId))].length);

onMounted(() => {
    if (fugleturStore.hasId) {
        getObservationer();
    }
});

function getObservationer() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId + '/observationer').then((response => {
        state.obsListe = response.data;
    }));
}

function obsSorted(value: observationType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

watch(() => chosenFugleturId, (newValue) => {
    getObservationer();
});
</script>