<template>
    <div class="row">
        <div class="col-auto">
            <fugleturTitel></fugleturTitel>
        </div>
        <div class="col">
            <fugleturSelection></fugleturSelection>
        </div>
        <div class="col-auto mt-1">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="scroll">
        <div class="mt-2 row row-cols-1 row-cols-lg-2 row-cols-xl-6 g-2">
            <div v-for="[key, value] in groupedData">
                <div class="card h-100 p-1">
                    <div class="card-header birdware fw-semibold">
                        <span class="text-capitalize">{{ key }}</span>
                    </div>
                    <div class="card-body">
                        <div v-for="tur in value" class="row">
                            <fugleturDato :fugleturId="tur.id" :dato="tur.dato" class="col-auto"></fugleturDato>
                            <div class="col">{{ tur.lokalitetNavn }}</div>
                        </div>
                    </div>
                </div>  
            </div>
        </div>
    </div>
</template>

<script setup>
import { reactive, onMounted, computed, watch } from 'vue';
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import fugleturSelection from '@/components/fugletur/fugletur-selection.vue';
import { useFugleturSelectionStore } from '@/stores/fugletur-selection-store';
import fugleturDato from '../main/fugleturdato.vue';

import api from '@/api';
import { storeToRefs } from 'pinia'

const fugleturSelectionStore = useFugleturSelectionStore();
const { selectedTags } = storeToRefs(fugleturSelectionStore);
const queryString = computed(() => { return JSON.stringify(fugleturSelectionStore.selectedTags) });

const state = reactive({
    fugleture: [],
});

const groupedData = computed(() => {
    return Map.groupBy(state.fugleture, ({fugleturAarMaaned}) => fugleturAarMaaned);
});

onMounted(() => {
    if (fugleturSelectionStore.selectedTags.length > 0) {
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