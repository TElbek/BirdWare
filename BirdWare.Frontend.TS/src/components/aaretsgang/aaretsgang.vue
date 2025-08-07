<template>
    <div class="row" v-if="hasData">
        <div class="col birdware large-text ">{{ year }}: {{ state.aaretsGang.length }} Årsarter</div>
        <div class="col-auto btn-group mb-2">
            <div class="btn btn-sm" :class="[isByTrip ? 'btn-on' : 'btn-off']" @click="switchIsByTrip">Tid & Sted
            </div>
            <div class="btn btn-sm" :class="[isByTrip ? 'btn-off' : 'btn-on']" @click="switchIsByTrip">Arter</div>
        </div>
    </div>
    <div class="scroll">
        <bs-row-cols :count="listOfItems.size">
            <div v-for="([key, value], index) in listOfItems">
                <bs-card v-if="index < 30 && isByTrip || !isByTrip">
                    <bs-card-header>
                        <span class="birdware">{{ key }}</span>
                        <span class="float-end birdware">{{ value.length }}</span>
                    </bs-card-header>
                    <bs-card-body>
                        <bs-flex hasWrap="true">
                            <template v-for="art in arterSorteret(value)">
                                <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su" :speciel="art.speciel"></artNavn>
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

const isByTrip = ref(true);
const hasData = ref(false);

onMounted(() => {
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

function switchIsByTrip() {
    isByTrip.value = !isByTrip.value;
}

const listOfItems = computed(() => {
    return isByTrip.value ? byTrip.value : byFamilie.value;
});

const byFamilie = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: aaretsGangType) => one.familieNavn);
});

const byTrip = computed(() => {
    return Map.groupBy(state.aaretsGang.sort((a, b) => a.dato.localeCompare(b.dato)).reverse(), (one: aaretsGangType) => one.titel);
});

const year = computed(() => { return new Date().getFullYear() });
</script>