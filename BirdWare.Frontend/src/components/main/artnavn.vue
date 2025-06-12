<template>
    <a @click="navigateToObservation">
        <span class="text-nowrap" :class="[forekomst]">{{ props.artNavn }}</span>
    </a>
</template>

<script setup>
import api from '@/api';
import { reactive, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();
const router = useRouter();

const props = defineProps({
    artId: 0,
    artNavn: '',
    speciel: false,
    su: false
});

const forekomst = computed(() => {
    if(props.speciel == true) return 'forekomst forekomst-speciel';
    if(props.su == true) return 'forekomst forekomst-su';
    return '';
});

function navigateToObservation() {
    api.get("tag/art/" + props.artId).then(response => {
        obsSelectionStore.SetTag(response.data);
        obsSelectionStore.SetGroupingId(0);
        router.push({ path: '/art/observation' });
    });
}
</script>