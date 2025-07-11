<template>
    <div class="row">
        <div class="col">
            <fugleturSelection></fugleturSelection>
        </div>
        <div class="col-auto mt-1">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="scroll">
        <div class="mt-2 row row-cols-1 row-cols-lg-2 row-cols-xl-3 row-cols-xxl-4 g-2">
            <div v-for="[key, value] in groupedData">
                <div class="card h-100 p-1">
                    <div class="card-header birdware ">
                        <span class="text-capitalize">{{ key }}</span>
                        <span class="float-end">{{ value.length }}</span>
                    </div>
                    <div class="card-body">
                        <div v-for="tur in value" class="row">
                            <fugletur-dato :fugleturId="tur.id" :dato="tur.dato" class="col-auto"></fugletur-dato>
                            <div class="col">{{ tur.lokalitetNavn }}</div>
                        </div>
                    </div>
                </div>  
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, computed, watch } from 'vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import fugleturSelection from '@/components/fugletur/fugletur-selection.vue';
import fugleturDato from '../main/fugleturdato.vue';
import { useFugleturSelectionStore } from '@/stores/fugletur-selection-store';

import api from '@/api';
import { storeToRefs } from 'pinia'
import { type fugleturListe, type fugleturType } from '@/types/fugleturType';

const fugleturSelectionStore = useFugleturSelectionStore();
const { selectedTags } = storeToRefs(fugleturSelectionStore);
const queryString = computed(() => { return JSON.stringify(fugleturSelectionStore.selectedTags) });

const state = reactive<fugleturListe>({
    fugleture: [],
});

const groupedData = computed(() => {
    return Map.groupBy(state.fugleture.slice(0,30), (one: fugleturType) => one.fugleturAarMaaned);
});

onMounted(() => {
    if (fugleturSelectionStore.selectedTags.length == 0) {
        api.get("tag/" + new Date().getFullYear())   .then(response => {
            fugleturSelectionStore.AddTag(response.data);
        });
    }
    else {
        getFugleture();
    }
});

watch(() => selectedTags.value, (newValue) => {
    getFugleture();
});

function getFugleture() {
    api.get("fugleture/get/tags?tagListAsJson=" + queryString.value).then(response => {
        state.fugleture = response.data;
    });
}
</script>