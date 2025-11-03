<template>
    <tw-button-responsive :caption="currentRoute.meta?.title">
        <tw-button v-for="route in availableRoutes" :caption="route.meta?.title"
            :isSelected="currentRoute.name == route.name" @click="navigate(route.name)">
        </tw-button>
    </tw-button-responsive>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { useRoute, useRouter, type RouteRecordNameGeneric, type RouteRecordRaw } from 'vue-router';
import { useFugleturStore } from '@/stores/fugletur-store';

const fugleturStore = useFugleturStore();
const currentRoute = useRoute();
const router = useRouter();

function navigate(routeName: RouteRecordNameGeneric) {
    router.replace({ name: routeName });
}

const availableRoutes = computed(() => {
    return currentRoute.matched[0].children.filter((item) => item.meta?.requireId && fugleturStore.hasId || !item.meta?.requireId);
});
</script>