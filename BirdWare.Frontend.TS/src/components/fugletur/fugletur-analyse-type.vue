<template>
    <tw-card v-if="state.hasData">
        <tw-card-header :caption="getAnalyseTypeTekst(props.analysetype.analyseType)"
            :count="state.analyseListe.length" :show-count="true"></tw-card-header>
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
const emit = defineEmits(['dataFound']);

const state = reactive({
    analyseListe: [] as analyseType[],
    hasData: false as boolean,
});

interface fugleturAnalyseProps {
    analysetype: analyseTypeType,
    fugletur: fugleturType
}

const props = defineProps<fugleturAnalyseProps>();

onMounted(() => {
    getAnalyseListe();
});

function getAnalyseListe() {
    api.get('fugletur/' + fugleturStore.chosenFugleturId + '/analyse/' + props.analysetype.analyseType).then((response) => {
        state.analyseListe = response.data;
        updateStateAndEmit();
    });
}

function updateStateAndEmit() {
    if (state.analyseListe.length > 0) {
        state.hasData = true;
        emit('dataFound');
    }
}

function arterSorteret(value: analyseType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

function getAnalyseTypeTekst(analyseType: number) {
    var analyseTypeTekst = props.analysetype.analyseTypeTekst;

    if (analyseType == 3) {
        return analyseTypeTekst?.replaceAll('[Region]', props.fugletur.regionNavn);
    }
    if (analyseType == 4) {
        return analyseTypeTekst?.replaceAll('[Kommune]', props.fugletur.kommuneNavn + ' Kommune');
    }
    if (analyseType == 5) {
        return analyseTypeTekst?.replaceAll('[Lokalitet]', props.fugletur.lokalitetNavn);
    }
    if (analyseType == 6) {
        return analyseTypeTekst?.replaceAll('[Aar]', props.fugletur.aarstal.toString());
    }
    if (analyseType == 7) {
        return analyseTypeTekst?.replaceAll('[Maaned]', getNameOfMonth(props.fugletur.maaned));
    }

    return analyseTypeTekst;
}
</script>