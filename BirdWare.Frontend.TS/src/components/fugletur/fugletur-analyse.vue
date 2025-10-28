<template>
    <div class="grid grid-cols-[auto_110px]">
        <fugletur-titel class="relative top-2" :fugletur-id="fugleturStore.chosenFugleturId"></fugletur-titel>
        <fugletur-navigation></fugletur-navigation>
    </div>
    <div class="max-h-160 xl:max-h-180 overflow-auto mt-3 mb-2">
        <div v-if="state.hasData" class="mt-3 mb-2">
            <tw-grid-cols-five :count="analyseTyperCount">
                <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
                    <fugleturAnalyseType :analyseListe="getAnalyseListeForType(analyseType.analyseType)"
                        :analysetype="analyseType" :analyseTypeTekst="getAnalyseTypeTekst(analyseType.analyseType)">
                    </fugleturAnalyseType>
                </template>
            </tw-grid-cols-five>
        </div>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
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
    getAnalyseTyper();
    getFugletur();
});

const analyseTyperCount = computed(() => [...new Set(state.analyseListe.map(item => item.analyseType))].length);

function getFugletur() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId).then((response) => {
        state.fugletur = response.data;
        getAnalyseListe();
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
    var analyseTypeTekst = state.analyseTyper.filter((item) => item.analyseType == analyseType)[0].analyseTypeTekst;
    if (analyseType == 3) {
        return analyseTypeTekst.replaceAll('[Region]', state.fugletur.regionNavn);
    }
    if (analyseType == 4) {
        return analyseTypeTekst.replaceAll('[Lokalitet]', state.fugletur.lokalitetNavn);
    }
    if (analyseType == 5) {
        return analyseTypeTekst.replaceAll('[Aar]', state.fugletur.aarstal.toString());
    }
    if (analyseType == 6) {
        return analyseTypeTekst.replaceAll('[Maaned]', getNameOfMonth(state.fugletur.maaned));
    }
    return analyseTypeTekst;
}


watch(() => chosenFugleturId.value, (newValue) => {
    getFugletur();
});
</script>