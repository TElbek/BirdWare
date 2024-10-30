<template>
    <a @click="navigateToObservation">
        <span :class="[props.su ? 'text-danger fw-bold' : '']" class="text-nowrap">{{ props.artNavn }}</span>
    </a>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { useObservationStore } from '@/stores/observation-store';

const observationStore = useObservationStore();
const router = useRouter();

const props = defineProps({
    artId: 0,
    artNavn: '',
    su: false
});

function navigateToObservation() {
    observationStore.setArtId(props.artId);
    router.push({path: '/art/observation'});
}
</script>