<template>
    <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        {{ route.meta.title }}
    </button>
    <ul class="dropdown-menu">
        <li v-for="route in route.matched[0].children">
            <a class="dropdown-item birdware" @click="navigate(route.name)">{{ route.meta.title }}</a>
        </li>
    </ul>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router';
const route = useRoute();
const router = useRouter();

function navigate(routeName) {
    router.push({name:routeName});
}
</script>