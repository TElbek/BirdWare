<template>
    <tw-card>
        <tw-card-header :count="ankomstList.length" :caption="caption" :showCount="true"></tw-card-header>
        <div class="flex gap-x-3 gap-y-1 flex-wrap mb-2">
            <template v-for="art in ankomstList" :key="art.artId">
                <div class="flex gap-x-1">
                    <div :class="getIndicatorClass(art)"></div>
                    <art-navn :artId="art.artId" :artNavn="art.artNavn" :speciel="false" :su="false"></art-navn>
                </div>
            </template>
        </div>
    </tw-card>
</template>

<script setup lang="ts">
import { type ankomstDatoType } from '@/types/ankomstDatoType';
import ArtNavn from '../main/artNavn.vue';

const props = defineProps<{
    ankomstList: ankomstDatoType[];
    caption: string;
}>();

function getIndicatorClass(art: ankomstDatoType) {
    if(art.tidligereIAar) {
        return 'w-3 h-3 rounded-full bg-green-500 relative top-1.5';
    } else if(art.tidligereIAar === false && art.erSetIaar) {
        return 'w-3 h-3 rounded-full bg-red-500 relative top-1.5';
    } else {
        return 'w-3 h-3 rounded-full bg-gray-300 relative top-1.5';
    }
}
</script>