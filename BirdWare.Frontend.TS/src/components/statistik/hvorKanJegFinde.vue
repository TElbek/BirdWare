<template>
    <div class="flex flex-col gap-y-2">
        <div class="flex justify-between">
            <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
            <tw-action-bar>
                <tw-button :caption="'Familie'" :isSelected="!isByLokalitet" @click="toggleGrouping()"></tw-button>
                <tw-button :caption="'Lokalitet'" :isSelected="isByLokalitet" @click="toggleGrouping()"></tw-button>
            </tw-action-bar>
        </div>
        <tw-grid-cols-three :count="byResult.value.size">
            <div v-for="[key, value] in byResult.value">
                <tw-card>
                    <tw-card-header v-if="isByLokalitet" :caption="`${key} ${Math.round(value[0].distance)} km`"
                        :count="value.length" :show-count="false"></tw-card-header>
                    <tw-card-header v-if="isByFamilie" :caption="key" :count="value.length"
                        :show-count="false"></tw-card-header>

                    <div v-if="isByLokalitet">
                        <div class="grid grid-cols-[max-content_1fr] gap-x-3">
                            <template v-for="[keyFamilie, valueArter] in groupArterByFamilie(value)">
                                <div class="text-birdware dark:text-birdware-bright">{{ keyFamilie }}</div>
                                <div class="flex gap-x-2 flex-wrap">
                                    <art-navn v-for="item in valueArter" :art-navn="item.artNavn" :art-id="item.artId"
                                        :speciel="false" :su="false"></art-navn>
                                </div>
                            </template>
                        </div>
                    </div>

                    <div v-if="isByFamilie">
                        <div class="grid grid-cols-[max-content_max-content_1fr] gap-x-3">
                            <template v-for="[keyLokalitet, valueArter] in groupArterByLokalitet(value)">
                                <div class="text-birdware dark:text-birdware-bright">{{ keyLokalitet }}</div>
                                <div class="text-end text-gray-900 dark:text-white">{{
                                    Math.round(valueArter[0].distance) }} km.</div>
                                <div class="flex gap-x-2 flex-wrap">
                                    <art-navn v-for="item in valueArter" :art-navn="item.artNavn" :art-id="item.artId"
                                        :speciel="false" :su="false"></art-navn>
                                </div>
                            </template>
                        </div>
                    </div>
                </tw-card>
            </div>
        </tw-grid-cols-three>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, computed, onMounted, ref } from 'vue';
import { type hvorKanJegFindeType } from '@/types/hvorKanJegFindeType';
import ArtNavn from '../main/artNavn.vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const isByLokalitet = ref(true);

const state = reactive({
    hvorKanJegFinde: [] as hvorKanJegFindeType[]
});

const isByFamilie = computed(() => !isByLokalitet.value);

const byLokalitet = computed(() => {
    return Map.groupBy(state.hvorKanJegFinde.sort((a,b) => a.distance - b.distance), (one: hvorKanJegFindeType) => one.lokalitetNavn);
});

const byFamilie = computed(() => {
    return Map.groupBy(state.hvorKanJegFinde.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: hvorKanJegFindeType) => one.familieNavn);
});

const byResult = computed(() => isByLokalitet.value ? byLokalitet : byFamilie);

onMounted(() => {
    getHvorKanJegFinde();
});

function getHvorKanJegFinde() {
    api.get('hvorkanjegfinde/').then(res => {
        state.hvorKanJegFinde = res.data;
    });
}

function arterSorteret(trip: hvorKanJegFindeType[]) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

function groupArterByLokalitet(value: hvorKanJegFindeType[]) {
    return Map.groupBy(value.slice(), (one: hvorKanJegFindeType) => one.lokalitetNavn)
}

function groupArterByFamilie(value: hvorKanJegFindeType[]) {
    return Map.groupBy(value.slice().sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: hvorKanJegFindeType) => one.familieNavn)
}

function toggleGrouping() {
    isByLokalitet.value = !isByLokalitet.value;
}
</script>