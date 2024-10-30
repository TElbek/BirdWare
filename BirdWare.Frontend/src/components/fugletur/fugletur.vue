<template>
    <router-view>
    </router-view>
</template>

<script setup>
import { useFugleturStore } from '@/stores/fugletur-store';
import { onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const fugleturStore = useFugleturStore();
const router = useRouter();
const route = useRoute();

onMounted(() => {
    if (fugleturStore.hasId) {
        navigateFugleturObservationer();
    }
    else {
        navigateFugleturOversigt();
    }
});

function navigateFugleturObservationer() {
    router.replace({ name: 'fugletur-obs' });
}

function navigateFugleturOversigt() {
    router.replace({ name: 'fugletur-oversigt' });
}
</script>