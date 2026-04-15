<template>
    <tw-show-lg>
        <tw-button-responsive :caption="currentRoute.meta?.title">
            <tw-button v-for="route in availableRoutes" :caption="route.meta?.title"
                :isSelected="currentRoute.name == route.name" @click="navigate(route.name)">
            </tw-button>
        </tw-button-responsive>
    </tw-show-lg>
    <tw-show-md>
        <div class="fixed pb-2 bottom-0 left-0 right-0 bg-white dark:bg-black">
            <tw-button-group class="flex justify-between gap-2 w-full px-2 py-1" :caption="''">
                <tw-button v-for="route in availableRoutes" :caption="route.meta?.title"
                    :isSelected="currentRoute.name == route.name" @click="navigate(route.name)">
                </tw-button>
            </tw-button-group>
        </div>
    </tw-show-md>
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