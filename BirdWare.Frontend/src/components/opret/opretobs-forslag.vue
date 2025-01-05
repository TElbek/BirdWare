<template>
    <div class="scroll">
        <div class="row row-cols-12 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 g-2">
            <div v-for="[key, value] in byIndeks">
                <div class="card h-100 w-100">
                    <div class="card-header">
                        <span class="birdware fw-semibold">{{ key }}</span>
                    </div>
                    <div class="card-body p-1">
                        <div class="art-flex">
                            <div v-for="art in artSorted(value)">
                                <artNavn :artId="art.artId" :artNavn="art.artNavn"></artNavn>
                            </div>
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
    forslag: [],
});

const byIndeks = computed(() => { return Map.groupBy(state.forslag.sort((a, b) => a.index - b.index).reverse(), ({ indeks }) => indeks) });
const byGruppe = computed(() => { return Map.groupBy(state.forslag.sort((a, b) => a.gruppeNavn.localeCompare(b.gruppeNavn)), ({ gruppeNavn }) => gruppeNavn) });

onMounted(() => {
    getForslag();
});

function getForslag() {
    api.get("fugletur/foreslaaArter/")
        .then((response) => { state.forslag = response.data });
}

function artSorted(value) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>