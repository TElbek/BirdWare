<template>
    <div class="row">
        <div class="col h4 birdware">Tilføj Tur</div>
        <div class="col-auto">
            <input type="search" class="form-control form-control-sm" placeholder="Søg..." v-model="state.searchValue" />
        </div>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4 g-2">
        <div class="col" v-for="[key, value] in byDistance">
            <div class="card h-100">
                <div class="card-header ms-1 fw-semibold birdware">
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

<script setup>
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';

const state = reactive({
    searchValue: '',
    position: {},
    lokalitetListe: []
});

onMounted(() => {
    getLocation();
});

const byDistance = computed(() => {
    return Map.groupBy(filteredLokalitetListe.value, ({ distance }) => distance);
});

const filteredLokalitetListe = computed(() => {
    return state.searchValue.length > 0 ?
        state.lokalitetListe.filter((lok) => lok.navn.toLowerCase().indexOf(state.searchValue.toLowerCase()) > -1) : state.lokalitetListe;
});

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setPosition);
    }
}

function setPosition(position) {
    state.position = position;
}

function getLokaliteterForPosition() {
    api.get("lokalitet/" + state.position.coords.latitude + '/' + state.position.coords.longitude)
        .then((response) => { state.lokalitetListe = response.data });
}

function opretTur(lokalitetId) {
    api.post("fugletur/oprettur/" + lokalitetId);
}

watch(() => state.position, (newValue) => {
    getLokaliteterForPosition();
});
</script>

<style scoped>
.lokalitet-flex {
    display: flex;
    flex-wrap: wrap;
    gap: 2px 15px;
}
</style>