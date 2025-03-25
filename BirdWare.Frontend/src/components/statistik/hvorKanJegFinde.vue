<template>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2">
        <div v-for="[key, value] in byLokalitet">
            <div class="card h-100">
                <div class="card-header birdware">
                    <span>{{ key }}</span>
                    <span class="float-end">{{ Math.round(value[0].distance) }} km.</span>
                </div>
                <div class="card-body">
                    <div class="art-flex">
                        <div v-for="art in arterSorteret(value)">
                            <artNavn :artId="art.artId" :artNavn="art.artNavn"></artNavn>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import api from '@/api';
import { reactive, computed, onMounted } from 'vue';

const state = reactive({
    hvorkanjegfinde: [],
    hasData: false
});

const byLokalitet = computed(() => {
    return Map.groupBy(state.hvorkanjegfinde, ({ lokalitetNavn }) => lokalitetNavn);
});

onMounted(() => {
    getHvorKanJegFinde();
});

function getHvorKanJegFinde() {
    api.get('hvorkanjegfinde/').then(res => {
        state.hvorkanjegfinde = res.data;
        state.hasData = true;
    });
}

function arterSorteret(trip) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>