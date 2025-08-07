<template>
    <div class="row">
        <div class="col">
            <fugleturSelection></fugleturSelection>
        </div>
        <div class="col-auto">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="scroll">
        <bs-row-cols :count="groupedData.size">
            <div v-for="[key, value] in groupedData">
                <bs-card>
                    <bs-card-header>
                        <span class="birdware text-capitalize">{{ key }}</span>
                        <span class="birdware float-end">{{ value.length }}</span>
                    </bs-card-header>
                    <bs-card-body>
                        <table-birdware>
                            <template v-for="tur in value">
                                <table-row-birdware>
                                    <table-cell-birdware>
                                        <fugletur-dato :fugleturId="tur.id" :dato="tur.dato"></fugletur-dato>
                                    </table-cell-birdware>
                                    <table-cell-birdware>
                                        {{ tur.lokalitetNavn }}
                                    </table-cell-birdware>
                                </table-row-birdware>
                            </template>
                        </table-birdware>
                    </bs-card-body>
                </bs-card>
            </div>
        </bs-row-cols>
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
import { type fugleturType } from '@/types/fugleturType';

const fugleturSelectionStore = useFugleturSelectionStore();
const { selectedTags } = storeToRefs(fugleturSelectionStore);
const queryString = computed(() => { return JSON.stringify(fugleturSelectionStore.selectedTags) });

const state = reactive({
    fugleture: [] as fugleturType[]
});

const groupedData = computed(() => {
    return Map.groupBy(state.fugleture.slice(0, 30), (one: fugleturType) => one.fugleturAarMaaned);
});

onMounted(() => {
    if (fugleturSelectionStore.selectedTags.length == 0) {
        api.get("tag/" + new Date().getFullYear()).then(response => {
            fugleturSelectionStore.AddTag(response.data);
        });
    }
    else {
        getFugleture();
    }
});

watch(() => selectedTags.value, () => {
    getFugleture();
});

function getFugleture() {
    api.get("fugleture/get/tags?tagListAsJson=" + queryString.value).then(response => {
        state.fugleture = response.data;
    });
}
</script>