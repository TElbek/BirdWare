<template>
    <tw-card>
        <tw-card-header :caption="props.analyseTypeTekst" :count="props.analyseListe.length"
            :show-count="true"></tw-card-header>
        <tw-flex>
            <art-navn v-for="item in arterSorteret(props.analyseListe)" :art-navn="item.artNavn" :art-id="item.artId"
                :speciel="item.speciel" :su="item.su"></art-navn>
        </tw-flex>
    </tw-card>
</template>

<script setup lang="ts">
import artNavn from '@/components/main/artNavn.vue';
import type { analyseType } from '@/types/analyseType.ts';
import type { analyseTypeType } from '@/types/analyseTypeType.ts';

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