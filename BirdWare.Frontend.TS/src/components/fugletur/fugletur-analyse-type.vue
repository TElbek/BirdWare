<template>
    <div class="col" v-if="props.analyseListe.length > 0">
        <bs-card>
            <bs-card-header>
                <div class="row">
                    <div class="col d-inline-block text-truncate">
                        <span class="birdware ">{{ props.analyseTypeTekst }}</span>
                    </div>
                    <div class="col-auto birdware ">
                        {{ props.analyseListe.length }}
                    </div>
                </div>
            </bs-card-header>
            <bs-card-body>
                <bs-flex :hasWrap="true">
                    <div v-for="art in arterSorteret(props.analyseListe)">
                        <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su" :speciel="art.speciel">
                        </artNavn>
                    </div>
                </bs-flex>
            </bs-card-body>
        </bs-card>
    </div>
</template>

<script setup lang="ts">
import artNavn from '@/components/main/artNavn.vue';
import type { analyseType } from '@/types/analyseType';
import type { analyseTypeType } from '@/types/analyseTypeType';

interface fugleturAnalyseProps {
    analysetype: analyseTypeType,
    analyseListe: analyseType[],
    analyseTypeTekst: string
}

const props = defineProps<fugleturAnalyseProps>();

function arterSorteret(value: analyseType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>
