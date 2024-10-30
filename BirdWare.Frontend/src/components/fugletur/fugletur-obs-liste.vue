<template>
    <div class="scroll">
        <div class="row row-cols-12 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 g-2">
            <div v-for="[key, value] in byFamilie">
                <div class="card h-100 w-100">
                    <div class="card-header">
                        <span class="birdware fw-semibold">{{ key }}</span>
                        <span class="float-end fw-semibold birdware">{{ value.length }}</span>
                    </div>
                    <div class="card-body p-1">
                        <div v-for="obs in obsSorted(value)" class="row">
                            <artNavn class="col-6" :artId="obs.artId" :artNavn="obs.artNavn" :su="obs.su"></artNavn>
                            <div class="col-6 d-inline-block text-truncate">{{ obs.bem }}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { reactive, computed, watch, onMounted } from 'vue';
import api from '@/api';
import artNavn from '@/components/main/artnavn.vue';
import { useFugleturStore } from '@/stores/fugletur-store';
const fugleturStore = useFugleturStore();

const props = defineProps({
    fugleturId: 0
});

const state = reactive({
    obsListe: [],
});

const byFamilie = computed(() => { return Map.groupBy(state.obsListe, ({ familieNavn }) => familieNavn) });

onMounted(() => {
    if (fugleturStore.hasId) {
        getObservationer();
    }
});

function getObservationer() {
    api.get('fugletur/' + props.fugleturId + '/observationer').then((response => {
        state.obsListe = response.data;
    }));
}

function obsSorted(value) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

watch(() => props.fugleturId, (newValue) => {
    getObservationer();
});
</script>