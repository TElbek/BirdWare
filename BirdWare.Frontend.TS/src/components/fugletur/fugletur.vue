<template>
    <router-view>
    </router-view>
</template>

<script setup lang="ts">
import { useFugleturStore } from '@/stores/fugletur-store.ts';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

const fugleturStore = useFugleturStore();
const router = useRouter();

onMounted(() => {
    fugleturStore.hasId ? 
        navigateFugleturObservationer() : 
        navigateFugleturOversigt();
});

function navigateFugleturObservationer() {
    router.replace({ name: 'fugletur-obs' });
}

function navigateFugleturOversigt() {
    router.replace({ name: 'fugletur-oversigt' });
}
</script>