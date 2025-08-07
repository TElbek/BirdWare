<template>
    <button class="d-lg-none btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        {{ route.meta.title }}
    </button>
    <ul class="dropdown-menu d-lg-none">
        <li v-for="route in availableRoutes">
            <a class="dropdown-item birdware" @click="navigate(route.name)">{{ route.meta?.title }}</a>
        </li>
    </ul>
    <bs-button-group class="d-none d-lg-block">
        <template v-for="item in availableRoutes">
            <bs-button :isOn="item.name == route.name" class="btn-equal-width" @click="navigate(item.name)">
                {{ item.meta?.title }}
            </bs-button>
        </template>
    </bs-button-group>
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