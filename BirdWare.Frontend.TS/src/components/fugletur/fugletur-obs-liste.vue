<template>
    <div class="scroll">
        <bs-row-cols class="row g2" :count="byFamilie.size">
            <div v-for="[key, value] in byFamilie">
                <bs-card>
                    <bs-card-header>
                        <span class="birdware ">{{ key }}</span>
                        <span class="float-end  birdware">{{ value.length }}</span>
                    </bs-card-header>
                    <bs-card-body>
                        <table-birdware>
                            <template v-for="obs in obsSorted(value)">
                                <table-row-birdware>
                                    <table-cell-birdware>
                                        <artNavn :artId="obs.artId" :artNavn="obs.artNavn" :su="obs.su" :speciel="obs.speciel"></artNavn>
                                    </table-cell-birdware>
                                    <table-cell-birdware>
                                        <div>{{ obs.bem }}</div>
                                    </table-cell-birdware>
                                </table-row-birdware>
                            </template>
                        </table-birdware>
                    </bs-card-body> 
                </bs-card>
            </div>
        </bs-row-cols>
    </div>
</template>

<script setup lang="ts">
import { reactive, computed, watch, onMounted } from 'vue';
import api from '@/api';
import { useFugleturStore } from '@/stores/fugletur-store';
import type { observationType } from '@/types/observationType';
import artNavn from '../main/artNavn.vue';
import { getRowColClasses } from '@/ts/rowcols';

const fugleturStore = useFugleturStore();

interface fugleturObsProps {
    fugleturId: number
}

const props = defineProps<fugleturObsProps>();

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
    api.get('fugletur/' + props.fugleturId + '/observationer').then((response => {
        state.obsListe = response.data;
    }));
}

function obsSorted(value: observationType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

watch(() => props.fugleturId, (newValue) => {
    getObservationer();
});
</script>