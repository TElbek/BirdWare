<template>
    <tw-button-dropdown :caption="route.meta?.title">
        <button class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright" v-for="route in availableRoutes" @click="navigate(route.name)">
            {{ route.meta?.title }}
        </button>
    </tw-button-dropdown>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { useRoute, useRouter, type RouteRecordNameGeneric, type RouteRecordRaw } from 'vue-router';
import { useFugleturStore } from '@/stores/fugletur-store';

const fugleturStore = useFugleturStore();
const route = useRoute();
const router = useRouter();

function navigate(routeName: RouteRecordNameGeneric) {
    router.replace({ name: routeName });
}

const availableRoutes = computed(() => {
    return route.matched[0].children.filter((item) => item.meta?.requireId && fugleturStore.hasId || !item.meta?.requireId);
});
</script>