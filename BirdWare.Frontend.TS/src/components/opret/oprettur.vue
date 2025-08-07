<template>
    <div class="row">
        <div class="col h4 birdware">Tilføj Tur</div>
        <div class="col-auto">
            <input type="search" class="form-control form-control-sm" placeholder="Søg..."
                v-model="state.searchValue" />
        </div>
    </div>
    <bs-row-cols :count="byDistance.size">
        <div v-for="[key, value] in byDistance">
            <bs-card>
                <bs-card-header>
                    <span class="birdware">{{ key }} km.</span>
                </bs-card-header>
                <bs-card-body>
                    <bs-flex hasWrap="true">
                        <div v-for="lokalitet in value">
                            <a @click="opretTur(lokalitet.id)">{{ lokalitet.navn }}</a>
                        </div>
                    </bs-flex>
                </bs-card-body>
            </bs-card>
        </div>
    </bs-row-cols>    
</template>

<script setup lang="ts">
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';
import { useRouter } from 'vue-router';
import { type lokalitetDistanceType } from '@/types/lokalitetDistanceType';

const router = useRouter();

const state = reactive({
    lokalitetListe: [] as lokalitetDistanceType[],
    position: {} as GeolocationPosition,
    searchValue: '' as string
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
        .then((response) => { state.lokalitetListe = response.data; });
}

function opretTur(lokalitetId: number) {
    api.post("fugletur/oprettur/" + lokalitetId)
        .then(() => {
            router.push({ name: 'addobs' });
        });
}

watch(() => state.position, (newValue) => {
    getLokaliteterForPosition();
});
</script>