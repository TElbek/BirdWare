<template>
    <div class="birdware large-text">Hvor kan jeg finde</div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2">
        <div v-for="[key, value] in byLokalitet">
            <div class="card h-100">
                <div class="card-header birdware">
                    <span>{{ key }} {{ Math.round(value[0].distance) }} km.</span>
                    <span class="float-end">{{ value.length }}</span>
                </div>
                <div class="card-body">
                    <div class="art-flex">
                        <div v-for="art in arterSorteret(value)">
                            <art-navn :artId="art.artId" :artNavn="art.artNavn" ></art-navn>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, computed, onMounted } from 'vue';
import artNavn from '@/components/main/artNavn.vue';
import { type hvorKanJegFindeType } from '@/types/hvorKanJegFindeType.ts';

const state = reactive({
    hvorKanJegFinde: [] as hvorKanJegFindeType[],
});

const byLokalitet = computed(() => {
    return Map.groupBy(state.hvorKanJegFinde, (one: hvorKanJegFindeType) => one.lokalitetNavn);
});

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
</script>