<template>
    <div class="row">
        <div class="col">
            <fugleturTitel :fugleturId="fugleturStore.chosenFugleturId"></fugleturTitel>
        </div>
        <div class="col-auto">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="scroll mt-2" v-if="state.analyseListe.length > 0">
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2">
            <template v-for="[key, value] in groupedByAnalyseType">
                <fugleturAnalyseType :analyseListe="value" :analyseTypeTekst="getAnalyseTypeTekst(key)">
                </fugleturAnalyseType>
            </template>
        </div>
    </div>
    <div v-else v-if="state.hasData">
        <span class="h5">Ingen Analyse</span>
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
import type { analyseType } from '@/types/analyseType';

const fugleturStore = useFugleturStore();
const { chosenFugleturId } = storeToRefs(fugleturStore)

interface xInterface {
    fugletur: fugleturType,
    hasData: boolean,
    analyseTyper: analyseTypeType[],
    analyseListe: analyseType[]
}

const state = reactive<xInterface>({
    analyseListe: [],
    analyseTyper: [],
    fugletur: {
        id: 0,
        dato: '',
        lokalitetId: 0,
        lokalitetNavn: '',
        regionId: 0,
        regionNavn: '',
        aarstal: 0,
        maaned: 0,
        antalArter: 0,
        fugleturAarMaaned: ''
    },
    hasData: false
});

const groupedByAnalyseType = computed(() => {
    return Map.groupBy(state.analyseListe, ({ analyseType }) => analyseType);
});

function getAnalyseTypeTekst(analyseType: number) {
    var analyseTypeTekst = state.analyseTyper.filter((item) => item.analyseType == analyseType)[0].analyseTypeTekst;
    if (analyseType == 1 || analyseType == 2) {
        return analyseTypeTekst;
    }
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
    return 'ukendt';
}

onMounted(() => {
    getAnalyseTyper();
    if (fugleturStore.hasId) {
        getAnalyse();
        getFugletur();
    }
});

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

function getAnalyse() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId + '/analyse').then((response => {
        state.analyseListe = response.data;
        state.hasData = true;
    }));
}

watch(() => chosenFugleturId.value, (newValue) => {
    getAnalyse();
});
</script>