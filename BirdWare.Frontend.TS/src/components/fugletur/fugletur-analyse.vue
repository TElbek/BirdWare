<template>
    <div v-if="state.hasData">
        <tw-grid-cols-generic :itemsPerRow=5 :count="state.analyseTyper.length">
            <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
                <fugleturAnalyseType 
                    :analysetype="analyseType">
                </fugleturAnalyseType>
            </template>
        </tw-grid-cols-generic>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import fugleturAnalyseType from '@/components/fugletur/fugletur-analyse-type.vue';
import { reactive, onMounted } from 'vue';
import type { analyseTypeType } from '@/types/analyseTypeType.ts';

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    hasData: false
});

onMounted(() => {
    getAnalyseTyper();
});

function getAnalyseTyper() {
    api.get('analyse/typer').then((response) => {
        state.analyseTyper = response.data;
        state.hasData = true;
    });
}
</script>