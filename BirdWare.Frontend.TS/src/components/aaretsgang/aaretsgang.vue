<template>
    <div>
        <div class="grid grid-cols-[auto_130px] mb-2">
            <tw-text-scaleable class="mt-4">{{ route.meta.title }}: {{ state.aaretsGang.length }} Arter</tw-text-scaleable>
            <aaretsgang-selection :is-by-trip="isByTrip" @switch-is-by-trip="swtichIsByTrip"></aaretsgang-selection>
        </div>
        <tw-grid-cols-five :count="listOfItems.size"  class="max-h-160 xl:max-h-175 overflow-auto">
            <div v-for="([key, value], index) in listOfItems">
                <tw-card v-if="index < 30 && isByTrip || !isByTrip">
                    <tw-card-header :caption="key" :count="value.length" :show-count="true"></tw-card-header>
                    <tw-flex>
                        <aaretsgang-artliste :value="arterSorteret(value)"></aaretsgang-artliste>
                    </tw-flex>
                </tw-card>
            </div>
        </tw-grid-cols-five>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, computed, onMounted, ref } from 'vue';
import { type aaretsGangType } from '@/types/aaretsGangType.ts';
import aaretsgangSelection from './aaretsgang-selection.vue';
import aaretsgangArtliste from './aaretsgang-artliste.vue';

import { useRoute } from 'vue-router';
const route = useRoute();

const isByTrip = ref(true);

const state = reactive({
    aaretsGang: [] as aaretsGangType[]
});

onMounted(() => {
    getAaretsGang();
});

function getAaretsGang() {
    api.get('aaretsgang/').then(res => {
        state.aaretsGang = res.data;
    });
}

function swtichIsByTrip() {
    isByTrip.value = !isByTrip.value;
}

const listOfItems = computed(() => {
    if (isByTrip.value) return byTrip.value;
    return byFamilie.value;
});

const byTrip = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.dato.localeCompare(b.dato)).reverse(), (one: aaretsGangType) => one.titel);
});

const byFamilie = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: aaretsGangType) => one.familieNavn);
});

function arterSorteret(trip: aaretsGangType[]) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>