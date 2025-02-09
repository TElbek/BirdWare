<template>
    <nav class="navbar navbar-expand-lg fixed-top me-3 ms-3">
        <a class="navbar-brand birdware" href="#">BirdWare</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar"
            aria-controls="offcanvasNavbar" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasNavbar"
            aria-labelledby="offcanvasNavbarLabel">
            <div class="offcanvas-header">
                <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <ul class="navbar-nav justify-content-start flex-grow-1 pe-3">
                    <template v-for="route in visibleRoutes">
                        <li class="nav-item" data-bs-toggle="offcanvas" data-bs-target=".offcanvas-start.show">
                            <router-link 
                                class="nav-link" :to="route.path">{{ route.meta.title }}</router-link>
                        </li>
                    </template>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { computed } from 'vue';
const router = useRouter();

const visibleRoutes = computed(() => {
    return router.options.routes.filter((route) =>
        route.meta.showInNavBar == true &&
        route.meta.requireSSL == (location.protocol == 'https:') || location.hostname == 'localhost')
});
</script>