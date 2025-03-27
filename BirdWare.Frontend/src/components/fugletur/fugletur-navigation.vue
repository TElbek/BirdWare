<template>
    <button class="d-lg-none btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        {{ route.meta.title }}
    </button>
    <ul class="dropdown-menu d-lg-none">
        <li v-for="route in availableRoutes">
            <a class="dropdown-item birdware" @click="navigate(route.name)">{{ route.meta.title }}</a>
        </li>
    </ul>
    <div class="btn-group d-none d-lg-block">
        <div class="btn btn-sm" v-for="item in availableRoutes"
            :class="[item.name == route.name ? 'btn-on' : 'btn-off']" @click="navigate(item.name)">
            {{ item.meta.title }}
        </div>
    </div>
</template>

<script setup>
import { computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useFugleturStore } from '@/stores/fugletur-store';

const fugleturStore = useFugleturStore();
const route = useRoute();
const router = useRouter();

function navigate(routeName) {
    router.replace({ name: routeName });
}

const availableRoutes = computed(() => {
    return route.matched[0].children.filter((item) => item.meta.requireId && fugleturStore.hasId || !item.meta.requireId);
});
</script>