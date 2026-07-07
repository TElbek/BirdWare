<template>
    <tw-grid-cols-five :count="groupedData.size">
        <div v-for="[groupName, arts] in groupedData" :key="groupName">
            <tw-card>
                <tw-card-header :caption="groupName" :count="arts.length" :showCount="true">
                </tw-card-header>
                <tw-flex>
                    <template v-for="item in arterSorteret(arts)" :key="item.artId">
                        <art-navn :art-navn="item.artNavn" :art-id="item.artId" :speciel="item.speciel"
                            :su="item.su"></art-navn>
                    </template>
                </tw-flex>
            </tw-card>
        </div>
    </tw-grid-cols-five>
</template>

<script setup lang="ts">
import api from '@/api';
import type { artType } from '@/types/artType';
import { computed, onMounted, reactive, watch } from 'vue';

import { useArtSelectionStore } from '@/stores/art-selection-store';
import { storeToRefs } from 'pinia';

const state = reactive({
    arter: [] as artType[]
});

const queryString = computed(() => { return JSON.stringify(artSelectionStore.selectedTags) });
const artSelectionStore = useArtSelectionStore();
const { selectedTags } = storeToRefs(artSelectionStore);

const harFlereFamilier = computed(() => {
    const uniqueFamilies = new Set(state.arter.map(art => art.familieNavn));
    return uniqueFamilies.size > 1;
});

const groupedData = computed(() =>
    harFlereFamilier.value ?
        Map.groupBy(state.arter.sort((a, b) => a.familieNavn.localeCompare(b.familieNavn)), (one: artType) => one.familieNavn) :
        Map.groupBy(state.arter.sort((a, b) => a.gruppeNavn.localeCompare(b.gruppeNavn)), (one: artType) => one.gruppeNavn)
);

onMounted(() => {
    if (artSelectionStore.selectedTags.length > 0) {
        getArter();
    }
});

function getArter() {
    api.get("arter/get/tags?tagListAsJson=" + queryString.value).then(response => {
        state.arter = response.data;
    });
}

watch(() => selectedTags.value, (newValue) => {
    if (newValue.length > 0) {
        getArter();
    } else {
        state.arter = [];
    }
});

watch(() => state.arter, (newValue) => {
    artSelectionStore.setAntalArter(newValue.length);
});

function arterSorteret(art: artType[]) {
    return art.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>