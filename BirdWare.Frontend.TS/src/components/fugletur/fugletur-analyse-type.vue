<template>
    <tw-card v-if="state.hasData && state.hasAnalyseTyper && state.hasFugletur">        
        <tw-card-header 
            v-if="state.hasAnalyseTyper"
            :caption="getAnalyseTypeTekst(props.analysetype.analyseType)" :count="state.analyseListe.length" :show-count="true"></tw-card-header>
        <tw-flex>
            <art-navn v-for="item in arterSorteret(state.analyseListe)" :art-navn="item.artNavn" :art-id="item.artId"
                :speciel="item.speciel" :su="item.su"></art-navn>
        </tw-flex>
    </tw-card>
</template>

<script setup lang="ts">
import api from '@/api';
import artNavn from '@/components/main/artNavn.vue';
import type { analyseType } from '@/types/analyseType.ts';
import type { analyseTypeType } from '@/types/analyseTypeType.ts';
import { type fugleturType } from '@/types/fugleturType';
import { onMounted, reactive } from 'vue';
import { useFugleturStore } from '@/stores/fugletur-store';
import { getNameOfMonth } from '@/ts/dateandtime';

const fugleturStore = useFugleturStore();

const state = reactive({
    analyseListe: [] as analyseType[],
    analyseTyper: [] as analyseTypeType[],
    fugletur: {} as fugleturType,
    hasData: false as boolean,
    hasFugletur: false as boolean,
    hasAnalyseTyper: false as boolean
});

interface fugleturAnalyseProps {
    analysetype: analyseTypeType,
}

const props = defineProps<fugleturAnalyseProps>();

onMounted(() => {
    getAnalyseTyper();
    getFugletur();
    getAnalyseListe();
});

function getAnalyseListe() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId + '/analyse/' + props.analysetype.analyseType).then((response) => {
        state.analyseListe = response.data;
        state.hasData = state.analyseListe.length > 0;
    });
}

function getAnalyseTyper() {
    api.get('analyse/typer').then((response) => {
        state.analyseTyper = response.data;
        state.hasAnalyseTyper = true;
    });
}

function getFugletur() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId).then((response) => {
        state.fugletur = response.data;
        state.hasFugletur = true;
    });
}

function arterSorteret(value: analyseType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
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
</script>