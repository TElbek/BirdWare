<template>
    <tw-action-bar>
        <tw-button v-for="route in availableRoutes" :caption="route.meta?.title"
            :isSelected="currentRoute.name == route.name" @click="navigate(route.name)">
        </tw-button>
    </tw-action-bar>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { useRoute, useRouter, type RouteRecordNameGeneric, type RouteRecordRaw } from 'vue-router';
import { useFugleturStore } from '@/stores/fugletur-store';

const fugleturStore = useFugleturStore();
const currentRoute = useRoute();
const router = useRouter();

const availableRoutes = computed(() => {
    return currentRoute.matched[0].children.filter((item) => item.meta?.requireId && fugleturStore.hasId || !item.meta?.requireId);
});

function navigate(routeName: RouteRecordNameGeneric): void {
    router.replace({ name: routeName });
}
</script>