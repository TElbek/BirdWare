<template>
    <div class="row">
        <div class="col h4 birdware">Tilføj Tur</div>
        <div class="col-auto">
            <input type="search" class="form-control form-control-sm" placeholder="Søg..."
                v-model="state.searchValue" />
        </div>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-xl-6 g-2">
        <div class="col" v-for="[key, value] in byDistance">
            <div class="card h-100">
                <div class="card-header ms-1  birdware">
                    <span>{{ key }} km.</span>
                </div>
                <div class="card-body ms-1">
                    <div class="lokalitet-flex">
                        <div v-for="lokalitet in value">
                            <a @click="opretTur(lokalitet.id)">
                                {{ lokalitet.navn }}
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';
import { useRouter } from 'vue-router';
import type { lokalitetDistanceType } from '@/types/lokalitetDistanceType';

const router = useRouter();

interface opretTurStateInterface {
    searchValue: string,
    position: GeolocationPosition,
    lokalitetListe: lokalitetDistanceType[]
}

const state = reactive<opretTurStateInterface>({
    lokalitetListe: [],
    position: {
        coords: {
            accuracy: 0,
            altitude: null,
            altitudeAccuracy: null,
            heading: null,
            latitude: 0,
            longitude: 0,
            speed: null,
            toJSON: function () {
                throw new Error('Function not implemented.');
            }
        },
        timestamp: 0,
        toJSON: function () {
            throw new Error('Function not implemented.');
        }
    },
    searchValue: ''
});

onMounted(() => {
    getLocation();
});

const byDistance = computed(() => {
    return Map.groupBy(filteredLokalitetListe.value, ({ distance }) => distance);
});

const filteredLokalitetListe = computed(() => {
    return state.searchValue.length > 0 ?
        state.lokalitetListe.filter((lok) => lok.navn.toLowerCase().indexOf(state.searchValue.toLowerCase()) > -1) : 
        state.lokalitetListe;
});

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setPosition);
    }
}

function setPosition(position: GeolocationPosition) {
    state.position = position;
}

function getLokaliteterForPosition() {
    api.get("lokalitet/" + state.position.coords.latitude + '/' + state.position.coords.longitude)
        .then((response) => { state.lokalitetListe = response.data });
}

function opretTur(lokalitetId: number) {
    api.post("fugletur/oprettur/" + lokalitetId)
        .then((response) => {
            router.push({ name: 'addobs' });
        });
}

watch(() => state.position, (newValue) => {
    getLokaliteterForPosition();
});
</script>