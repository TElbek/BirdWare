<template>
    <div v-if="state.hasdata" class="flex flex-wrap gap-x-2 me-2">
        <tw-text-sizeable>{{ formatDate(state.fugletur.dato) }}</tw-text-sizeable>
        <tw-text-sizeable>{{ state.fugletur.lokalitetNavn }}</tw-text-sizeable>
        <tw-text-sizeable>#{{ state.fugletur.antalArter }}</tw-text-sizeable>
    </div>
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
    fugletur: {} as fugleturType,
    hasdata: false
});

onMounted(() => {
    if (props.fugleturId > 0) {
        getFugletur();
    }
});

function getFugletur() {
    api.get('fugletur/' + props.fugleturId).then((response => {
        state.fugletur = response.data;
        state.hasdata = true;
    }));
}

watch(() => props.fugleturId, (newValue) => {
    if (newValue != 0) {
        state.hasdata = false;
        getFugletur();
    }
});
</script>