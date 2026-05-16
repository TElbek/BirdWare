<template>
    <div class="flex flex-col gap-y-2">
        <fugletur-selection></fugletur-selection>
        <tw-grid-cols-three :count="groupedData?.size" :offset="5">
            <div v-for="[key, value] in groupedData">
                <tw-card>
                    <tw-text-sizeable class="text-birdware dark:text-birdware-bright capitalize">{{ key }}</tw-text-sizeable>
                    <tw-text-sizeable class="text-birdware dark:text-birdware-bright float-end">{{ value.length }}</tw-text-sizeable>
                    <div class="flex flex-row flex-wrap gap-2 mt-2 mb-2 justify-items-stretch">
                        <div v-for="tur in value">
                            <div class="flex flex-col border rounded-md border-gray-300 p-1 dark:border-gray-600 w-max lg:w-50 ps-2">
                                <div class="flex flex-row justify-between gap-x-2">
                                    <fugletur-dato :fugleturId="tur.id" :dato="tur.dato" :highlight="true"/>
                                    <span class="pe-1">{{ tur.antalArter }} {{ tur.antalArter === 1 ? 'art' : 'arter' }}</span>
                                </div>
                                <span>{{ tur.lokalitetNavn }}</span>
                            </div>
                        </div>
                    </div>
                </tw-card>
            </div>
        </tw-grid-cols-three>
    </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, computed, watch } from 'vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import fugleturSelection from '@/components/fugletur/fugletur-selection.vue';
import { useFugleturSelectionStore } from '@/stores/fugletur-selection-store.ts';

import api from '@/api';
import { storeToRefs } from 'pinia'
import { type fugleturType } from '@/types/fugleturType.ts';

import { useRoute } from 'vue-router';
import TwTextSizeable from '../main/tailwind/tw-text-sizeable.vue';
const route = useRoute();

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