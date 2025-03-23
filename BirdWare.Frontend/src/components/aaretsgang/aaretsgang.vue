<template>
    <div class="row" v-if="state.hasData">
        <div class="col birdware large-text ">{{ year }}: {{ state.aaretsgang.length }} Årsarter</div>
        <div class="col-auto btn-group mb-2">
            <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-on' : 'btn-off']" @click="switchIsByTrip">Tid & Sted
            </div>
            <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-off' : 'btn-on']" @click="switchIsByTrip">Arter</div>
        </div>
    </div>
    <div class="scroll">
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2">
            <div class="col" v-for="([key, value], index) in listOfItems">
                <div class="card h-100 p-1" v-if="index <= 30 && state.isByTrip || !state.isByTrip">
                    <div class="card-header">
                        <span class="birdware">{{ key }}</span>
                        <span class="float-end birdware">{{ value.length }}</span>
                    </div>
                    <div class="card-body">
                        <div class="art-flex">
                            <template v-for="art in arterSorteret(value)">
                                <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su"></artNavn>
                            </template>
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
    aaretsgang: [],
    isByTrip: true,
    hasData: false
});

onMounted(() => {
    getAaretsGang();
});

function getAaretsGang() {
    api.get('aaretsgang/').then(res => {
        state.aaretsgang = res.data;
        state.hasData = true;
    });
}

function arterSorteret(trip) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

function switchIsByTrip() {
    state.isByTrip = !state.isByTrip;
}

const listOfItems = computed(() => {
    return state.isByTrip ? byTrip.value : byFamilie.value;
});

const byFamilie = computed(() => {
    return Map.groupBy(state.aaretsgang.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), ({ familieNavn }) => familieNavn);
});

const byTrip = computed(() => {
    return Map.groupBy(state.aaretsgang.sort((a, b) => a.dato.localeCompare(b.dato)).reverse(), ({ titel }) => titel);
});

const year = computed(() => { return new Date().getFullYear() });
</script>