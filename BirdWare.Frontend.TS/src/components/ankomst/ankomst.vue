<template>
    <div class="flex flex-col gap-y-3">
        <div class="flex flex-row justify-between">
            <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
            <ankomst-count :ankomstList="state.ankomst"></ankomst-count>
        </div>
        <ankomst-selection></ankomst-selection>
        <tw-grid-cols-five :count="groupedByMaaned.size">
            <template v-for="[key, value] in groupedByMaaned" :key="key">
                <ankomst-card :ankomstList="value" :caption="getNameOfMonth(key)"></ankomst-card>
            </template>
        </tw-grid-cols-five>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { computed, onMounted, reactive, watch } from 'vue';
import { useRoute } from 'vue-router';
import { type ankomstDatoType } from '@/types/ankomstDatoType';
import AnkomstCard from '@/components/ankomst/ankomst-card.vue';
import AnkomstSelection from '@/components/ankomst/ankomst-selection.vue';
import AnkomstCount from '@/components/ankomst/ankomst-count.vue';
import { getNameOfMonth } from '@/ts/dateandtime';
import { storeToRefs } from 'pinia';
import { useAnkomstSelectionStore } from '@/stores/ankomst-selection-store';
const ankomstSelectionStore = useAnkomstSelectionStore();

const { selectedTag } = storeToRefs(ankomstSelectionStore)

const route = useRoute();

const state = reactive({
    ankomst: [] as ankomstDatoType[],
});

onMounted(() => {
    if (ankomstSelectionStore.hasSelectedTag && ankomstSelectionStore.selectedTag) {
        getAnkomst(ankomstSelectionStore.selectedTag!.id);
    }
});

function getAnkomst(id: number) {
    api.get(`ankomstdato/familie/${id}`).then((response) => {
        state.ankomst = response.data;
    });
}

const groupedByMaaned = computed(() => {
    return Map.groupBy(state.ankomst.sort((a, b) => a.maaned - b.maaned), (one: ankomstDatoType) => one.maaned);
});

watch(selectedTag, () => {
    if (selectedTag.value) {
        getAnkomst(selectedTag.value.id);
    }
});
</script>