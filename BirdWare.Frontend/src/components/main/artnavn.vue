<template>
    <a @click="navigateToObservation">
        <span class="text-nowrap" :class="[props.su ? 'border-bottom border-danger border-2' : '']">{{ props.artNavn }}</span>
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
        obsSelectionStore.SetGroupingId(0);
        router.push({ path: '/art/observation' });
    });
}
</script>