<template>
    <div class="row row-cols-1 row-cols-lg-3 row-cols-xl-4 g-2">
        <div class="col" v-for="[key, value] in listOfItems">
            <div class="card h-100 p-1">
                <div class="card-header">
                    <span class="birdware fw-semibold">{{ key }}</span>
                    <span class="float-end fw-semibold birdware">{{ value.length }}</span>
                </div>
                <div class="card-body">
                    <div class="art-flex">
                        <div v-for="art in arterSorteret(value)">
                            <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su"></artNavn>
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

const props = defineProps({
    lastYear: false,
    isByTrip: true
});

const emit = defineEmits(['item-count'])

const state = reactive({
    forskel: [],
    hasData: false
});

onMounted(() => {
    getForskel();
});

function getForskel() {
    api.get('forskel/' + (props.lastYear ? 'sidsteaar' : 'iaar'))
        .then(res => {
            state.forskel = res.data;
            state.hasData = true;
            emit('item-count', state.forskel.length);
        })
}

function arterSorteret(trip) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

const listOfItems = computed(() => {
    return props.isByTrip ? byTrip.value : byFamilie.value;
});

const byTrip = computed(() => {
    return Map.groupBy(state.forskel.sort((a, b) => a.dato.localeCompare(b.dato)).reverse(), ({ titel }) => titel);
});

const byFamilie = computed(() => {
    return Map.groupBy(state.forskel.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), ({ familieNavn }) => familieNavn);
});
</script>