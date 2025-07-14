<template>
    <div class="col" v-if="state.analyseListe.length > 0 && !state.loading">
        <div class="card h-100 w-100">
            <div class="card-header text-nowrap">
                <div class="row">
                    <div class="col d-inline-block text-truncate">
                        <span class="birdware ">{{ props.analyseTypeTekst }}</span>
                    </div>
                    <div class="col-auto birdware ">
                        {{ state.analyseListe.length }}
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="art-flex ms-1">
                    <div v-for="art in arterSorteret(state.analyseListe)">
                        <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su" :speciel="art.speciel">
                        </artNavn>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col" v-if="state.loading">
        <div class="spinner-border birdware">
            <span class="visually-hidden">Loading...</span>
        </div>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import artNavn from '@/components/main/artNavn.vue';
import type { analyseType } from '@/types/analyseType';
import type { analyseTypeType } from '@/types/analyseTypeType';
import { reactive, watch, onMounted, computed } from 'vue';

interface fugleturAnalyseProps {
    analysetype: analyseTypeType,
    fugleturId: number,
    analyseTypeTekst: string
}

const props = defineProps<fugleturAnalyseProps>();

const state = reactive({
    analyseListe: [] as analyseType[],
    loading: true as boolean
});

onMounted(() => {
    getAnalyseForType();
});

function getAnalyseForType() {
    api.get('fugletur/' + props.fugleturId + '/analyse/' + props.analysetype.analyseType)
        .then((response) => {
            state.analyseListe = response.data;
        })
        .finally(() => {
            state.loading = false;
        })
}

function arterSorteret(value: analyseType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>
