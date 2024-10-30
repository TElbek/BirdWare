<template>
    <div class="row">
        <div class="col">
            <fugleturTitel :fugleturId="fugleturStore.chosenFugleturId"></fugleturTitel>
        </div>
        <div class="col-auto">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="scroll mt-2">
        <div class="row row-cols-12 g-2">
            <template v-for="[key, value] in groupedByAnalyseType">
                <fugleturAnalyseType :analyseListe="value" :analyseTypeTekst="getAnalyseTypeTekst(key)"></fugleturAnalyseType>
            </template>
        </div>
    </div>
</template>

<script setup>
import api from '@/api';
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import fugleturAnalyseType from './fugletur-analyse-type.vue';
import { reactive, watch, onMounted, computed } from 'vue';
import { useFugleturStore } from '@/stores/fugletur-store';
import { storeToRefs } from 'pinia';
import { getNameOfMonth } from '@/js/dateandtime';

const fugleturStore = useFugleturStore();
const { chosenFugleturId } = storeToRefs(fugleturStore)

const state = reactive({
    analyseListe: [],
    analyseTyper: [],
    fugletur: {}
});

const groupedByAnalyseType = computed(() => {
    return Map.groupBy(state.analyseListe, ({analyseType }) => analyseType);
});

function getAnalyseTypeTekst(analyseType) {
    var analyseTypeTekst = state.analyseTyper.filter((item) => item.analyseType == analyseType)[0].analyseTypeTekst;    
    if(analyseType == 1 || analyseType == 2) {
        return analyseTypeTekst;
    }
    if(analyseType == 3) {
        return analyseTypeTekst.replaceAll('[Region]', state.fugletur.regionNavn);
    }
    if(analyseType == 4) {
        return analyseTypeTekst.replaceAll('[Lokalitet]', state.fugletur.lokalitetNavn);
    }
    if(analyseType == 5) {
        return analyseTypeTekst.replaceAll('[Aar]', state.fugletur.aarstal);
    }
    if(analyseType == 6) {
        return analyseTypeTekst.replaceAll('[Maaned]', getNameOfMonth(state.fugletur.maaned));
    }
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
    }));
}

watch(() => chosenFugleturId.value, (newValue) => {
    getAnalyse();
});
</script>