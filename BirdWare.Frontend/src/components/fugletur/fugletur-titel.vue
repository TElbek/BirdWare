<template>
    <div class="birdware large-text fw-semibold text-nowrap">
        <span>{{ route.meta.title }}</span>
        <span class="ms-2" v-if="state.fugletur != undefined">{{ formatDate(state.fugletur.dato) }} - {{
            state.fugletur.lokalitetNavn }}</span>
    </div>
</template>

<script setup>
import api from '@/api';
import { formatDate } from '@/js/dateandtime';
import { onMounted, watch, reactive } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const props = defineProps({
    fugleturId: 0
});

const state = reactive({
    fugletur: undefined,
});

onMounted(() => {
    if(props.fugleturId > 0 ) {
        getFugletur();
    }
});

function getFugletur() {
    api.get('fugletur/' + props.fugleturId).then((response => {
        state.fugletur = response.data;
    }));
}

watch(() => props.fugleturId, (newValue) => {
    if(newValue != 0) {
        getFugletur();
    }
});
</script>