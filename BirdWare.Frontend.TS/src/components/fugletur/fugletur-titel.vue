<template>
    <div v-if="isOversigtRoute">
        <tw-text-sizeable>Oversigt</tw-text-sizeable>
    </div>
    <div v-else-if="state.hasdata" class="flex gap-x-2">
        <tw-text-sizeable>{{ formatDate(state.fugletur.dato) }}</tw-text-sizeable>
        <tw-text-sizeable>{{ state.fugletur.lokalitetNavn }}</tw-text-sizeable>
        <tw-text-sizeable>{{ state.fugletur.antalArter }} {{ state.fugletur.antalArter > 1 ? 'Arter' : 'Art' }}</tw-text-sizeable>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { formatDate } from '@/ts/dateandtime';
import { onMounted, watch, reactive, computed } from 'vue';
import { type fugleturType } from '@/types/fugleturType.ts';
import { useRoute } from 'vue-router';
import TwTextSizeable from '../main/tailwind/tw-text-sizeable.vue';

const route = useRoute();

interface fugleturProps {
    fugleturId: number
}

const props = defineProps<fugleturProps>();

const state = reactive({
    fugletur: {} as fugleturType,
    hasdata: false
});

const isOversigtRoute = computed(() => route.name === 'fugletur-oversigt');

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