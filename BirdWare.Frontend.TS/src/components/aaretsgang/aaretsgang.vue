<template>
    <div class="row mb-2" v-if="hasData">
        <div class="col birdware large-text ">{{ year }}: {{ state.aaretsGang.length }} Årsarter</div>
        <bs-show-lg class="col-auto">
            <bs-button-group>
                <bs-button :isOn="isByTrip" @clicked="setIsByTrip">Tid & Sted</bs-button>
                <bs-button :isOn="isByFamilie" @clicked="setIsByFamilie">Arter</bs-button>
                <bs-button :isOn="isByMaaned" @clicked="setIsByMaaned">Maaned</bs-button>
            </bs-button-group>
        </bs-show-lg>
        <bs-show-md class="col-auto">
            <bs-button-dropdown :caption="isByTrip ? 'Tid & Sted' : isByFamilie ? 'Arter' : 'Maaned'">
                <ul class="dropdown-menu">
                    <a class="dropdown-item birdware" @click="setIsByTrip">Tid & Sted</a>
                    <a class="dropdown-item birdware" @click="setIsByFamilie">Arter</a>
                    <a class="dropdown-item birdware" @click="setIsByMaaned">Maaned</a>
                </ul>
            </bs-button-dropdown>
        </bs-show-md>
    </div>
    <div class="scroll mb-2">
        <bs-row-cols :count="listOfItems.size">
            <div v-for="([key, value], index) in listOfItems">
                <bs-card v-if="index < 30 && isByTrip || index < 5 && isByMaaned || isByFamilie">
                    <bs-card-header>
                        <span class="birdware">{{ key }}</span>
                        <span class="float-end birdware">{{ value.length }}</span>
                    </bs-card-header>
                    <bs-card-body>
                        <bs-flex :hasWrap="true">
                            <template v-for="art in arterSorteret(value)">
                                <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su" :speciel="art.speciel">
                                </artNavn>
                            </template>
                        </bs-flex>
                    </bs-card-body>
                </bs-card>
            </div>
        </bs-row-cols>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, ref, computed, onMounted } from 'vue';
import artNavn from '@/components/main/artNavn.vue';
import { type aaretsGangType } from '@/types/aaretsGangType.ts';

const state = reactive({
    aaretsGang: [] as aaretsGangType[],
});

const isByTrip = ref(false);
const isByFamilie = ref(false);
const isByMaaned = ref(false);
const hasData = ref(false);

onMounted(() => {
    setIsByTrip();
    getAaretsGang();
});

function getAaretsGang() {
    api.get('aaretsgang/').then(res => {
        state.aaretsGang = res.data;
        hasData.value = true;
    });
}

function arterSorteret(trip: aaretsGangType[]) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

function setIsByTrip() {
    setIsByNone();
    isByTrip.value = true;
}

function setIsByFamilie() {
    setIsByNone();
    isByFamilie.value = true;
}

function setIsByMaaned() {
    setIsByNone();
    isByMaaned.value = true;
}

function setIsByNone() {
    isByTrip.value = false;
    isByFamilie.value = false;
    isByMaaned.value = false
}

const listOfItems = computed(() => {
    return isByTrip.value ? byTrip.value : isByFamilie.value ? byFamilie.value : byMaaned.value;
});

const byFamilie = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: aaretsGangType) => one.familieNavn);
});

const byTrip = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.dato.localeCompare(b.dato)).reverse(), (one: aaretsGangType) => one.titel);
});

const byMaaned = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.dato.localeCompare(b.dato)).reverse(), (one: aaretsGangType) => one.maaned);
});

const year = computed(() => { return new Date().getFullYear() });
</script>