<template>
    <div class="birdware large-text d-inline-block text-truncate">
        <span class="me-2">{{ route.meta.title }}</span>
        <span v-if="state.fugletur != undefined">{{ formatDate(state.fugletur.dato) }} {{ state.fugletur.lokalitetNavn
            }}
            #{{ state.fugletur.antalArter }}</span>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { formatDate } from '@/ts/dateandtime';
import { onMounted, watch, reactive } from 'vue';
import { useRoute } from 'vue-router';
import { type enFugletur } from '@/types/fugleturType.ts';

const route = useRoute();

interface fugleturDatoInterface {
    fugleturId: number
}

const props = defineProps<fugleturDatoInterface>();

const state = reactive<enFugletur>({
    fugletur: {
        id: 0,
        dato: '',
        lokalitetId: 0,
        lokalitetNavn: '',
        regionId: 0,
        regionNavn: '',
        aarstal: 0,
        maaned: 0,
        antalArter: 0,
        fugleturAarMaaned: ''
    }
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