<template>
    <div class="btn-group" role="group">
        <button v-for="route in route.matched[0].children" class="btn btn-sm" 
            @click="navigate(route.name)"
            :class="[isCurrentRoute(route.name) ? 'btn-on' : 'btn-off']">
            {{ route.meta.title }}
        </button>
    </div>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router';
const route = useRoute();
const router = useRouter();

function isCurrentRoute(routeName) {
    return route.name == routeName;
}

function navigate(routeName) {
    router.push({name:routeName});
}
</script>