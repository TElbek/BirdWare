<template>
    <tw-grid-cols-three :count="props.groupedData?.size">
        <div v-for="[key, value] in props.groupedData">
            <tw-card>
                <div class="grid grid-cols-[auto_15px] mb-1 font-medium tracking-wide">
                    <a @click="addTag(key)" class="cursor-pointer">
                        <span class="text-base text-birdware dark:text-birdware-bright">
                            {{ obsSelectionStore.isGropingByMonth && valueIsNumber(key) ? getNameOfMonth(key) :
                                key }}
                        </span>
                    </a>
                    <span class="text-base text-birdware text-right">{{ value.length }}</span>
                </div>
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
                                obs.lokalitetNavn }}</table-cell-birdware>
                            <table-cell-birdware>
                                <div>{{ obs.bem }}</div>
                            </table-cell-birdware>
                        </table-row-birdware>
                    </template>
                </table-birdware>
            </tw-card>
        </div>
    </tw-grid-cols-three>
</template>

<script setup lang="ts">
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { type observationType } from '@/types/observationType';
import { valueIsNumber } from '@/ts/typechecks';
import { getNameOfMonth } from '@/ts/dateandtime.ts'
import fugleturDato from '../main/fugleturdato.vue';
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