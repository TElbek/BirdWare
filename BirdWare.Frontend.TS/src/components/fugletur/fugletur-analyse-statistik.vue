<template>
    <tw-card>
        <tw-card-header-slot>
            <span class="text-semibold text-lg">Statistik</span>
        </tw-card-header-slot>
        <span>{{ formattedObservationCount }} observationer analyseret.</span>
    </tw-card>
</template>

<script setup lang="ts">
import api from '@/api';
import { useFugleturStore } from '@/stores/fugletur-store';
import { computed, onMounted, reactive } from 'vue';
const fugleturStore = useFugleturStore();

const state = reactive({
    observationCount: 0 as number,
});

onMounted(() => {
    getObservationCount();
});

function getObservationCount(): void {
    api.get('fugletur/' + fugleturStore.chosenFugleturId + '/analyse/statistik').then((response) => {
        state.observationCount = response.data;
    })
}

const formattedObservationCount = computed(() => {
    return new Intl.NumberFormat('da-DK').format(state.observationCount);
});
</script>