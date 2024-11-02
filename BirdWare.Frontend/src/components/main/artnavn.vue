<template>
    <a @click="navigateToObservation">
        <span :class="[props.su ? 'text-danger fw-bold' : '']" class="text-nowrap">{{ props.artNavn }}</span>
    </a>
</template>

<script setup>
import api from '@/api';
import { useRouter } from 'vue-router';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();
const router = useRouter();

const props = defineProps({
    artId: 0,
    artNavn: '',
    su: false
});

function navigateToObservation() {
    api.get("tag/art/" + props.artId).then(response => {
        obsSelectionStore.SetTag(response.data);
        router.push({ path: '/art/observation' });
    });
}
</script>