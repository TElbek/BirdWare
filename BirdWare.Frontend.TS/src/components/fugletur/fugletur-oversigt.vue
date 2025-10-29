<template>
    <div class="grid grid-cols-[1fr_max-content]">
        <tw-text-sizeable class="relative top-2">{{ route.meta.title }}</tw-text-sizeable>
        <fugletur-navigation class="z-50"></fugletur-navigation>        
    </div>
    <fugletur-selection  class="mt-3 mb-2"></fugletur-selection>
    <tw-grid-cols-five :count="groupedData?.size">
        <div v-for="[key, value] in groupedData">
            <tw-card>
                <span class="text-base font-medium tracking-wide text-birdware dark:text-birdware-bright capitalize">{{ key
                    }}</span>
                <span class="text-base font-medium tracking-wide text-birdware dark:text-birdware-bright float-end">{{ value.length
                    }}</span>
                <table-birdware>
                    <template v-for="tur in value">
                        <table-row-birdware>
                            <table-cell-birdware>
                                <fugleturDato :fugleturId="tur.id" :dato="tur.dato" />
                            </table-cell-birdware>
                            <table-cell-birdware>
                                <span>{{ tur.lokalitetNavn }}</span>
                            </table-cell-birdware>
                        </table-row-birdware>
                    </template>
                </table-birdware>
            </tw-card>
        </div>
    </tw-grid-cols-five>
</template>

<script setup lang="ts">
import { reactive, onMounted, computed, watch } from 'vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import fugleturSelection from '@/components/fugletur/fugletur-selection.vue';
import fugleturDato from '@/components/main/fugleturdato.vue';
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