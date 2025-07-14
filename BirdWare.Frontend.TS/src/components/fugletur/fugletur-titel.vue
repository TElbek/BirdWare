<template>
    <span class="birdware large-text d-inline-block text-truncate">{{ title }}</span>
</template>

<script setup lang="ts">
import api from '@/api';
import { formatDate } from '@/ts/dateandtime';
import { onMounted, watch, reactive, computed } from 'vue';
import { type fugleturType } from '@/types/fugleturType.ts';

interface fugleturProps {
    fugleturId: number
}

const props = defineProps<fugleturProps>();

const state = reactive({
    fugletur: {} as fugleturType
});


onMounted(() => {
    if (props.fugleturId > 0) {
        getFugletur();
    }
});

const title = computed(() => {
    return formatDate(state.fugletur.dato) + ' ' + state.fugletur.lokalitetNavn + ' #' + state.fugletur.antalArter;
});

function getFugletur() {
    api.get('fugletur/' + props.fugleturId).then((response => {
        state.fugletur = response.data;
    }));
}

watch(() => props.fugleturId, (newValue) => {
    if (newValue != 0) {
        getFugletur();
    }
});
</script>