<template>
    <div class="d-flex justify-content-between mb-2">
        <fugleturTitel class="col" :fugleturId="fugleturStore.chosenFugleturId"></fugleturTitel>
        <fugleturNavigation class="col-auto"></fugleturNavigation>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2" v-if="state.hasData">
        <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
            <fugleturAnalyseType :fugleturId="fugleturStore.chosenFugleturId" :analysetype="analyseType"
                :analyseTypeTekst="getAnalyseTypeTekst(analyseType.analyseType)"></fugleturAnalyseType>
        </template>
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
import type { analyseTypeType } from '@/types/analyseTypeType';

const fugleturStore = useFugleturStore();
const { chosenFugleturId } = storeToRefs(fugleturStore)

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    fugletur: {} as fugleturType,
    hasData: false
});

onMounted(() => {
    getAnalyseTyper();
    getFugletur();
});

function getFugletur() {
    console.log("Her 1");
    api.get('fugletur/' + fugleturStore.chosenFugleturId).then((response) => {
        console.log("Her 2");
        state.fugletur = response.data;
        state.hasData = true;
    });
}

function getAnalyseTyper() {
    api.get('analyse/typer').then((response) => {
        state.analyseTyper = response.data;
    });
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