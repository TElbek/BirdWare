<template>
    <div class="col">
        <div class="card h-100 w-100">
            <div class="card-header text-nowrap">
                <div class="row">
                    <div class="col d-inline-block text-truncate">
                        <span class="birdware ">{{ props.analyseTypeTekst }}</span>
                    </div>
                    <div class="col-auto birdware ">
                        {{ props.analyseListe.length }}
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="art-flex ms-1">
                    <div v-for="art in arterSorteret(props.analyseListe)">
                        <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su" :speciel="art.speciel">
                        </artNavn>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import artNavn from '@/components/main/artNavn.vue';
import type { analyseType } from '@/types/analyseType';

interface fugleturAnalyseTypeInterface {
    analyseListe: analyseType[],
    analyseTypeTekst: string
}

const props = defineProps<fugleturAnalyseTypeInterface>();

function arterSorteret(value: analyseType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>
