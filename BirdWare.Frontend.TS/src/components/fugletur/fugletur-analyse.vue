<template>
    <div v-if="state.hasData">
        <span v-if="state.itemsWithData == 0">Ingen Analyse...</span>
        <tw-grid-cols-generic :itemsPerRow=itemsPerRow :count="itemsPerRow">
            <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
                <fugleturAnalyseType :analysetype="analyseType" @dataFound="increment()">
                </fugleturAnalyseType>
            </template>
        </tw-grid-cols-generic>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import fugleturAnalyseType from '@/components/fugletur/fugletur-analyse-type.vue';
import { reactive, onMounted, computed } from 'vue';
import type { analyseTypeType } from '@/types/analyseTypeType.ts';

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    hasData: false,
    itemsWithData: 0 as number
});

onMounted(() => {
    state.itemsWithData = 0;
    getAnalyseTyper();
});

const itemsPerRow = computed(() => Math.min(state.itemsWithData, 4))

function getAnalyseTyper() {
    api.get('analyse/typer').then((response) => {
        state.analyseTyper = response.data;
        state.hasData = true;
    });
}

function increment() {
    state.itemsWithData++;
}
</script>