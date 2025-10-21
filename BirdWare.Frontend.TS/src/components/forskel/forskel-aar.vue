<template>
    <tw-grid-cols-five :count="listOfItems.size">
        <div v-for="[key, value] in listOfItems">
            <tw-card>
                <tw-card-header :caption="key" :count="value.length" :show-count="true"></tw-card-header>
                <tw-flex>
                    <template v-for="item in arterSorteret(value)">
                        <art-navn :art-navn="item.artNavn" :art-id="item.artId" :speciel="item.speciel"
                            :su="item.su"></art-navn>
                    </template>
                </tw-flex>
            </tw-card>
        </div>
    </tw-grid-cols-five>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, computed, onMounted } from 'vue';
import { type forskelType } from '@/types/forskelType.ts';
import ArtNavn from '../main/artNavn.vue';

interface forskelAarProps {
    isThisYear: boolean,
    isByTrip: boolean
}

const props = defineProps<forskelAarProps>();
const apiPath = computed(() => { return props.isThisYear ? 'iaar' : 'sidsteaar' });
const emit = defineEmits(['item-count']);

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

onMounted(() => {
    getForskelIAar();
});

const state = reactive({
    forskelAar: [] as forskelType[]
});

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