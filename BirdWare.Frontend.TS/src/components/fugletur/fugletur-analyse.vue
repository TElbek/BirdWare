<template>
    <div v-if="state.hasData">
        <tw-grid-cols-generic :itemsPerRow=itemsPerRow :count="itemsPerRow">
            <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
                <fugleturAnalyseType :fugletur="state.fugletur" :analysetype="analyseType" 
                        @dataFound="incrementItemsWithData()">
                </fugleturAnalyseType>
            </template>
            <Fugletur-analyse-statistik></Fugletur-analyse-statistik>
        </tw-grid-cols-generic>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, onMounted, computed } from 'vue';
import fugleturAnalyseType from '@/components/fugletur/fugletur-analyse-type.vue';
import { type analyseTypeType } from '@/types/analyseTypeType.ts';
import { type fugleturType } from '@/types/fugleturType';
import { useFugleturStore } from '@/stores/fugletur-store';
import FugleturAnalyseStatistik from './fugletur-analyse-statistik.vue';

const fugleturStore = useFugleturStore();

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    fugletur: {} as fugleturType,
    hasFugletur: false as boolean,
    hasData: false as boolean,
    itemsWithData: 0 as number,
});

onMounted(() => {
    state.itemsWithData = 0;
    getAnalyseTyper();
    getFugletur();
});

const itemsPerRow = computed(() => Math.min(state.itemsWithData + 1, 4))

function getAnalyseTyper(): void {
    api.get('analyse/typer').then((response) => {
        state.analyseTyper = response.data;
        state.hasData = true;
    });
}

function getFugletur(): void {
    api.get('fugletur/' + fugleturStore.chosenFugleturId).then((response) => {
        state.fugletur = response.data;
        state.hasFugletur = true;
    });
}

function incrementItemsWithData(): void {
    state.itemsWithData++;
}
</script>