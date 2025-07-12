<template>
    <div class="birdware large-text d-inline-block text-truncate">
        <span class="me-2">{{ route.meta.title }}</span>
        <span>{{ formatDate(state.fugletur.dato) }} {{ state.fugletur.lokalitetNavn }} #{{ state.fugletur.antalArter
            }}</span>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { formatDate } from '@/ts/dateandtime';
import { onMounted, watch, reactive } from 'vue';
import { useRoute } from 'vue-router';
import { type fugleturType } from '@/types/fugleturType.ts';

const route = useRoute();

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

<style scoped>
.row>* {
    padding-right: 0px;

}
</style>