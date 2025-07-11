<template>
    <div class="scroll mt-1">
        <div class="row row-cols-2 row-cols-md-2 row-cols-lg-3 row-cols-xl-6 g-2" v-if="hasData">
            <div v-for="[key, value] in byFamilie">
                <div class="card h-100 w-100">
                    <div class="card-header">
                        <span class="birdware ">{{ key }}</span>
                    </div>
                    <div class="card-body p-1">
                        <div class="art-flex">
                            <div v-for="art in artSorted(value)">
                                 <a @click="addObs(art.artId)">{{ art.artNavn }}</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted, computed } from 'vue';
import api from '@/api';
import type { opretObsForslagType } from '@/types/opretObsForslagType';

interface opretObsListeStateInterface {
    forslag: opretObsForslagType[]
}

const state = reactive<opretObsListeStateInterface>({
    forslag: [],
});

const hasData = ref(false);
const byFamilie = computed(() => { return Map.groupBy(state.forslag.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: opretObsForslagType) => one.familieNavn) });

onMounted(() => {
    getForslag();
});

function getForslag() {
    api.get("fugletur/foreslaaArter/")
        .then((response) => { 
            state.forslag = response.data;
            hasData.value = true;
        });
}

function artSorted(value: opretObsForslagType[]) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}

function addObs(artId: number) {    
    api.post("observation/opretobs/" + artId).then((response) => {
        getForslag();            
    });
}
</script>