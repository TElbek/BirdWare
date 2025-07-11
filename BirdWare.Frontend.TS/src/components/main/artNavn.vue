<template>
    <a @click="navigateToObservation">
        <span class="text-nowrap" :class="[forekomst]">{{ props.artNavn }}</span>
    </a>
</template>

<script setup lang="ts">
import api from '@/api';
import { computed } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import { useRouter } from 'vue-router';

const obsSelectionStore = useObsSelectionStore();
const router = useRouter();

interface artNavnInterface {
    artId: number,
    artNavn: string,
    speciel?: boolean,
    su?: boolean
}

const props = defineProps<artNavnInterface>();

const forekomst = computed(() => {
    if (props.speciel == true) return 'forekomst forekomst-speciel';
    if (props.su == true) return 'forekomst forekomst-su';
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