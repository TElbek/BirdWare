<template>
    <div v-if="state.hasData">
        <tw-grid-cols-generic :itemsPerRow=5 :count="analyseTyperCount">
            <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
                <fugleturAnalyseType :analyseListe="getAnalyseListeForType(analyseType.analyseType)"
                    v-if="getAnalyseListeForType(analyseType.analyseType).length > 0"
                    :analysetype="analyseType" :analyseTypeTekst="getAnalyseTypeTekst(analyseType.analyseType)">
                </fugleturAnalyseType>
            </template>
        </tw-grid-cols-generic>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import fugleturAnalyseType from '@/components/fugletur/fugletur-analyse-type.vue';
import { reactive, watch, onMounted, computed } from 'vue';
import { useFugleturStore } from '@/stores/fugletur-store';
import { storeToRefs } from 'pinia';
import { getNameOfMonth } from '@/ts/dateandtime';
import { type fugleturType } from '@/types/fugleturType';
import type { analyseTypeType } from '@/types/analyseTypeType.ts';
import type { analyseType } from '@/types/analyseType.ts';

const fugleturStore = useFugleturStore();
const { chosenFugleturId } = storeToRefs(fugleturStore)

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    analyseListe: [] as analyseType[],
    fugletur: {} as fugleturType,
    hasData: false
});

onMounted(() => {
    getAnalyseListe();
    getAnalyseTyper();
    getFugletur();
});

const analyseTyperCount = computed(() => [...new Set(state.analyseListe.map(item => item.analyseType))].length);

function getFugletur() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId).then((response) => {
        state.fugletur = response.data;
    });
}

function getAnalyseTyper() {
    api.get('analyse/typer').then((response) => {
        state.analyseTyper = response.data;
    });
}

function getAnalyseListe() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId + '/analyse').then((response) => {
        state.analyseListe = response.data;
        state.hasData = true;
    });
}

function getAnalyseListeForType(analyseType: number) {
    return state.analyseListe.filter((item) => item.analyseType == analyseType);
}

function getAnalyseTypeTekst(analyseType: number) {
    var analyseTypeTekst = state.analyseTyper.find((item) => item.analyseType == analyseType)?.analyseTypeTekst ?? '';

    if (analyseType == 3) {
        return analyseTypeTekst?.replaceAll('[Region]', state.fugletur.regionNavn);
    }
    if (analyseType == 4) {
        return analyseTypeTekst?.replaceAll('[Kommune]', state.fugletur.kommuneNavn + ' Kommune');
    }
    if (analyseType == 5) {
        return analyseTypeTekst?.replaceAll('[Lokalitet]', state.fugletur.lokalitetNavn);
    }
    if (analyseType == 6) {
        return analyseTypeTekst?.replaceAll('[Aar]', state.fugletur.aarstal.toString());
    }
    if (analyseType == 7) {
        return analyseTypeTekst?.replaceAll('[Maaned]', getNameOfMonth(state.fugletur.maaned));
    }
    
    return analyseTypeTekst;
}


watch(() => chosenFugleturId.value, (newValue) => {
    getFugletur();
});
</script>