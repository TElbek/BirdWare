<template>
    <div class="birdware large-text">Hvor kan jeg finde</div>
    <bs-row-cols :count="byLokalitet.size">
        <div v-for="[key, value] in byLokalitet">
            <bs-card>
                <bs-card-header>
                    <span class="birdware">{{ key }} {{ Math.round(value[0].distance) }} km.</span>
                    <span class="birdware float-end">{{ value.length }}</span>
                </bs-card-header>
                <bs-card-body>
                    <bs-flex :hasWrap="true">
                        <div v-for="art in arterSorteret(value)">
                            <art-navn :artId="art.artId" :artNavn="art.artNavn"></art-navn>
                        </div>
                    </bs-flex>
                </bs-card-body>
            </bs-card>
        </div>
    </bs-row-cols>
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