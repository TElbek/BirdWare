<template>
    <div class="row">
        <div class="col">
            <fugleturTitel :fugleturId="fugleturStore.chosenFugleturId"></fugleturTitel>
        </div>
        <div class="col-auto">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2">
        <template v-for="analyseType in state.analyseTyper" :key="analyseType.analyseType">
            <fugleturAnalyseType :fugleturId="fugleturStore.chosenFugleturId" :analysetype="analyseType"></fugleturAnalyseType>
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
import type { analyseType } from '@/types/analyseType';

const fugleturStore = useFugleturStore();
const { chosenFugleturId } = storeToRefs(fugleturStore)

const state = reactive({
    analyseTyper: [] as analyseTypeType[],
    fugletur: {} as fugleturType,
    hasData: false
});

onMounted(() => {
    getAnalyseTyper();
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

watch(() => chosenFugleturId.value, (newValue) => {
    getFugletur();
});
</script>