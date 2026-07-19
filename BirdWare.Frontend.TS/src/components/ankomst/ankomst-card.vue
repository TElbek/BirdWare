<template>
    <tw-card>
        <tw-card-header-slot>
            <div class="flex justify-between text-birdware dark:text-birdware-bright text-lg">
                <span class="text-lg font-semibold">{{ caption }}</span>
                <ankomst-count class="mt-1" :ankomstList="ankomstList"></ankomst-count>
            </div>
        </tw-card-header-slot>
        <div class="mb-1 mt-1">
            <div class="grid grid-cols-[auto_1fr_auto] gap-x-2">
                <template v-for="art in sortArtList(ankomstList)" :key="art.artId">
                    <div :class="getIndicatorClass(art)"></div>
                    <art-navn :artId="art.artId" :artNavn="art.artNavn" :speciel="false" :su="false"></art-navn>
                    <div class="text-end" :class="[art.forskel < 0 ? 'text-red-500' : 'text-gray-500 dark:text-white']">
                        <span v-if="art.erSetIaar">{{ Math.trunc(Math.abs(art.forskel)) }} dage</span>
                    </div>
                </template>
            </div>
        </div>
    </tw-card>
</template>

<script setup lang="ts">
import { type ankomstDatoType } from '@/types/ankomstDatoType';
import AnkomstCount from './ankomst-count.vue';
import ArtNavn from '../main/artNavn.vue';

const props = defineProps<{
    ankomstList: ankomstDatoType[];
    caption: string;
}>();

function getIndicatorClass(art: ankomstDatoType) {
    if (art.tidligereIAar) {
        return 'w-3 h-3 rounded-full bg-green-500 relative top-1.5';
    } else if (art.tidligereIAar === false && art.erSetIaar) {
        return 'w-3 h-3 rounded-full bg-red-500 relative top-1.5';
    } else {
        return 'w-3 h-3 rounded-full bg-gray-300 relative top-1.5';
    }
}

function sortArtList(artList: ankomstDatoType[]) {
    return artList.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>