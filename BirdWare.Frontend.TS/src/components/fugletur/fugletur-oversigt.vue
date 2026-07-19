<template>
    <div class="flex flex-col gap-y-2">
        <fugletur-selection></fugletur-selection>
        <tw-grid-cols-generic :itemsPerRow=5 :count="groupedData?.size" :offset="5">
            <div v-for="[key, value] in groupedData">
                <tw-card>
                    <tw-card-header :caption="key" :count="value.length" :showCount="true">
                    </tw-card-header>
                    <div v-for="tur in value">
                        <div class="grid grid-cols-[max-content_1fr_max-content] gap-x-3">
                            <fugletur-dato :fugleturId="tur.id" :dato="tur.dato" :highlight="true" />
                            <span class="dark:text-white">{{ tur.lokalitetNavn }}</span>
                            <span class="pe-1">{{ tur.antalArter }} {{ tur.antalArter === 1 ? 'art' : 'arter' }}</span>
                        </div>
                    </div>
                </tw-card>
            </div>
        </tw-grid-cols-generic>
    </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, computed, watch } from 'vue';
import fugleturSelection from '@/components/fugletur/fugletur-selection.vue';
import { useFugleturSelectionStore } from '@/stores/fugletur-selection-store.ts';

import api from '@/api';
import { storeToRefs } from 'pinia'
import { type fugleturType } from '@/types/fugleturType.ts';

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