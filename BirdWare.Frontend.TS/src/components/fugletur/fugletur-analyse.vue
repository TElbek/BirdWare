<template>
    <div v-if="state.hasData" :class="[isVisible ? 'visible' : 'hidden']">
        <span class="text-semibold text-lg" v-if="isVisible && state.itemsWithData == 0">Ingen analyse</span>
        <tw-grid-cols-generic :itemsPerRow=itemsPerRow :count="itemsPerRow">
            <template v-for="analyseType in state.analyseTyper.slice().reverse()" :key="analyseType.analyseType">
                <fugleturAnalyseType :fugletur="state.fugletur" :analysetype="analyseType" 
                        @dataFound="incrementItemsWithData()" 
                        @finished="incrementFinished()">
                </fugleturAnalyseType>
            </template>
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

const fugleturStore = useFugleturStore();

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    fugletur: {} as fugleturType,
    hasFugletur: false as boolean,
    hasData: false as boolean,
    itemsWithData: 0 as number,
    finishedCount: 0 as number
});

onMounted(() => {
    state.itemsWithData = 0;
    state.finishedCount = 0;
    getAnalyseTyper();
    getFugletur();
});

const itemsPerRow = computed(() => Math.min(state.itemsWithData, 4))

const isVisible = computed(() => state.hasData && state.finishedCount == state.analyseTyper.length);

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

function incrementFinished(): void {
    state.finishedCount++;
}
</script>