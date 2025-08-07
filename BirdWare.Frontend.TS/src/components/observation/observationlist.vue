<template>
    <div class="scroll">
        <div class="row g-2" :class="getRowColClassesThree(props.groupedData?.size ?? 0)">
            <div v-for="[key, value] in props.groupedData" class="col">
                <bs-card>
                    <bs-card-header>
                        <a @click="addTag(key)">
                            <span class="birdware" v-if="obsSelectionStore.isGropingByMonth && valueIsNumber(key)">{{
                                getMonthNameFromNumber(+key) }}</span>
                            <span class="birdware text-capitalize" v-else>{{ key }}</span>
                            <span class="birdware ms-1">({{ value.length }})</span>
                        </a>
                    </bs-card-header>
                    <bs-card-body>
                        <table-birdware>
                            <template v-for="obs in obsSorted(value)" :key="obs.observationId">
                                <table-row-birdware>
                                    <table-cell-birdware>
                                        <fugleturDato :fugleturId="obs.fugleturId" :dato="obs.dato" />
                                    </table-cell-birdware>
                                    <table-cell-birdware class="text-nowrap" v-if="showSpeciesNameInList">
                                        {{ obs.artNavn }}
                                    </table-cell-birdware>
                                    <table-cell-birdware class="text-nowrap">{{
                                        obs.lokalitetNavn}}</table-cell-birdware>
                                    <table-cell-birdware>
                                        <div class="truncate">{{ obs.bem }}</div>
                                    </table-cell-birdware>
                                </table-row-birdware>
                            </template>
                        </table-birdware>
                    </bs-card-body>
                </bs-card>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { getMonthNameFromNumber } from '@/ts/dateandtime';
import { type observationType } from '@/types/observationType';
import { valueIsNumber } from '@/ts/typechecks';
import fugleturDato from '../main/fugleturdato.vue';
import { getRowColClassesThree, getRowColClassesTwo } from '@/ts/rowcols';
import type { tagType } from '@/types/tagType';
import { computed } from 'vue';

const obsSelectionStore = useObsSelectionStore();

const props = defineProps({
    groupedData: Map<string | number, observationType[]>
});

const showSpeciesNameInList = computed(() => obsSelectionStore.selectedTags.filter((tag: tagType) => tag.tagType == 12).length != 1);

const emit = defineEmits(['addtag']);

function addTag(value: string | number) {
    emit('addtag', value);
}

function obsSorted(value: observationType[]) {
    return value.sort((a, b) => a.dato.localeCompare(b.dato))
        .reverse()
        .slice(0, 10);
}
</script>