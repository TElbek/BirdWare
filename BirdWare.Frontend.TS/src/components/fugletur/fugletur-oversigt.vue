<template>
    <div class="flex flex-col gap-y-2">
        <div class="flex justify-between">
            <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
            <fugletur-navigation></fugletur-navigation>
        </div>
        <fugletur-selection></fugletur-selection>
        <tw-grid-cols-five :count="groupedData?.size" :offset="5">
            <div v-for="[key, value] in groupedData">
                <tw-card>
                    <span class="text-base font-medium  text-birdware dark:text-birdware-bright capitalize">{{ key
                        }}</span>
                    <span class="text-base font-medium  text-birdware dark:text-birdware-bright float-end">{{
                        value.length
                        }}</span>
                    <div class="grid grid-cols-[max-content_1fr] gap-x-3 dark:text-white">
                        <template v-for="tur in value">
                            <fugletur-dato :fugleturId="tur.id" :dato="tur.dato" />
                            <span>{{ tur.lokalitetNavn }}</span>
                        </template>
                    </div>
                </tw-card>
            </div>
        </tw-grid-cols-five>
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