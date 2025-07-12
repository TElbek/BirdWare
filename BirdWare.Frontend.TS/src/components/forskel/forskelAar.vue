<template>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-6 g-2">
        <div v-for="[key, value] in listOfItems">
            <div class="card h-100">
                <div class="card-header">
                    <span class="birdware">{{ key }}</span>
                    <span class="float-end  birdware">{{ value.length }}</span>
                </div>
                <div class="card-body">
                    <div class="art-flex">
                        <div v-for="art in arterSorteret(value)">
                            <art-navn :artId="art.artId" :artNavn="art.artNavn" :su="art.su"
                                :speciel="art.speciel"></art-navn>
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
import { type forskelType} from '@/types/forskelType.ts';

interface forskelAarInterface {
    isThisYear: boolean,
    isByTrip: boolean
}

const props = defineProps<forskelAarInterface>();

const state = reactive({
    forskelAar: [] as forskelType[]
});

const emit = defineEmits(['item-count']);

onMounted(() => {
    getForskelIAar();
});

const listOfItems = computed(() => {
    return props.isByTrip ? byTrip.value : byFamilie.value;
});

const byTrip = computed(() => {
    const listSorted = state.forskelAar.sort((a, b) => a.dato.localeCompare(b.dato)).reverse();
    return Map.groupBy(listSorted, (one: forskelType) => one.titel);
});

const byFamilie = computed(() => {
    const listSorted = state.forskelAar.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn));
    return Map.groupBy(listSorted, (one: forskelType) => one.familieNavn);
});

const apiPath = computed(() => { return props.isThisYear ? 'iaar' : 'sidsteaar' });

function arterSorteret(trip: forskelType[]) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

function getForskelIAar() {
    api.get('forskel/' + apiPath.value).then(res => {
        state.forskelAar = res.data;
        emit('item-count', state.forskelAar.length);
    });
}
</script>