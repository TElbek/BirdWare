<template>
    <div class="scroll mt-1">
        <bs-row-cols :count="byFamilie.size" v-if="hasData">
            <div v-for="[key, value] in byFamilie">
                <bs-card>
                    <bs-card-header>
                        <span class="birdware ">{{ key }}</span>
                    </bs-card-header>
                    <bs-card-body>
                        <div class="art-flex">
                            <div v-for="art in artSorted(value)">
                                 <a @click="addObs(art.artId)">{{ art.artNavn }}</a>
                            </div>
                        </div>
                    </bs-card-body>
                </bs-card>
            </div>
        </bs-row-cols>
    </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted, computed } from 'vue';
import api from '@/api';
import type { opretObsForslagType } from '@/types/opretObsForslagType';

const state = reactive({
    forslag: [] as opretObsForslagType[],
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