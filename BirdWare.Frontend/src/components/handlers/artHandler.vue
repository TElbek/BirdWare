<template>
    <div></div>
</template>

<script setup>
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import api from '@/api';

import { onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const obsSelectionStore = useObsSelectionStore();
const router = useRouter();
const route = useRoute();

onMounted(() => {
    redirect();
});

function redirect() {
    api.get("tag/art/" + route.params['id']).then(response => {
        obsSelectionStore.SetTag(response.data);
        obsSelectionStore.SetGroupingId(0);
        router.replace({ path: '/art/observation' });        
    });
};
</script>