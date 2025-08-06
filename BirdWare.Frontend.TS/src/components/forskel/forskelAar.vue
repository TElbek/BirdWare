<template>
    <bs-row-cols :count="listOfItems.size">
        <div v-for="[key, value] in listOfItems">
            <bs-card>
                <bs-card-header>
                    <span class="birdware">{{ key }}</span>
                    <span class="float-end  birdware">{{ value.length }}</span> 
                </bs-card-header>
                <bs-card-body>
                    <bs-flex :hasWrap="true">
                        <div v-for="art in arterSorteret(value)">
                            <art-navn :artId="art.artId" :artNavn="art.artNavn" :su="art.su"
                                :speciel="art.speciel"></art-navn>
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
import { type forskelType} from '@/types/forskelType.ts';

interface forskelAarProps {
    isThisYear: boolean,
    isByTrip: boolean
}

const props = defineProps<forskelAarProps>();

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