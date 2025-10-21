<template>
    <div v-if="props.analyseListe.length > 0">
        <tw-card>
            <span class="text-base font-semibold text-birdware dark:text-birdware-bright capitalize">{{
                props.analyseTypeTekst }}</span>
            <span class="text-base font-semibold text-birdware dark:text-birdware-bright float-end">{{
                props.analyseListe.length }}</span>
            <tw-flex>
                <template v-for="item in arterSorteret(props.analyseListe)">
                    <art-navn :art-navn="item.artNavn" :art-id="item.artId" :speciel="item.speciel" :su="item.su"></art-navn>
                </template>
            </tw-flex>
        </tw-card>
    </div>
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