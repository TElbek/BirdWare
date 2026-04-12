<template>
    <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
    <tw-grid-cols-five :count="groupedByMaaned.size" class="mt-4">
        <template v-for="[key, value] in groupedByMaaned" :key="key">
            <ankomst-card :ankomstList="value" :caption="getNameOfMonth(key)"></ankomst-card>
        </template>
    </tw-grid-cols-five>
</template>

<script setup lang="ts">
import api from '@/api';
import { computed, onMounted, reactive } from 'vue';
import { useRoute } from 'vue-router';
import { type ankomstDatoType } from '@/types/ankomstDatoType';
import AnkomstCard from '@/components/ankomst/ankomst-card.vue';
import {getNameOfMonth} from '@/ts/dateandtime';

const route = useRoute();

const state = reactive({
    ankomst: [] as ankomstDatoType[],
});

onMounted(() => {
    api.get('ankomstdato/familie/5').then((response) => {
        state.ankomst = response.data;
    });
});

const groupedByMaaned = computed(() => {
    return Map.groupBy(state.ankomst.sort((a, b) => a.maaned - b.maaned), (one: ankomstDatoType) => one.maaned);
});
</script>