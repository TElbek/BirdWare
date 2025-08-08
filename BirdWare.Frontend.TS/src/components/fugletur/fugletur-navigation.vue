<template>
    <bs-show-md>
        <bs-button-dropdown :caption="route.meta.title">
            <ul class="dropdown-menu">
                <li v-for="route in availableRoutes">
                    <a class="dropdown-item birdware" @click="navigate(route.name)">{{ route.meta?.title }}</a>
                </li>
            </ul>
        </bs-button-dropdown>
    </bs-show-md>
    <bs-show-lg>
        <bs-button-group>
            <template v-for="item in availableRoutes">
                <bs-button :isOn="item.name == route.name" class="btn-equal-width" @click="navigate(item.name)">
                    {{ item.meta?.title }}
                </bs-button>
            </template>
        </bs-button-group>
    </bs-show-lg>
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