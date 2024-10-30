<template>
    <nav class="navbar navbar-expand-lg">
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto">
                <template v-for="route in visibleRoutes">
                    <li class="nav-item">
                        <router-link class="nav-link" :to="route.path">{{route.meta.title}}</router-link>
                    </li>
                </template>
            </ul>
        </div>
        <ul class="navbar-nav ms-auto">
            <li class="nav-item">
                <span class="nav-link fw-semibold">
                    <div class="h5">Birdware.dk</div>
                </span>
            </li>
        </ul>
    </nav>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { computed } from 'vue';
const router = useRouter();

const visibleRoutes = computed(() => {return router.options.routes.filter((route) => route.meta.showInNavBar == true)});
</script>

<style scoped>
.nav-link {
    color: #048baf;
}

.navbar {
    --bs-navbar-padding-y: 0;
}
</style>