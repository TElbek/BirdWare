<template>
    <nav class="navbar navbar-expand-lg">
        <a class="navbar-brand birdware" href="#">BirdWare</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar"
            aria-controls="offcanvasNavbar" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
            <div class="offcanvas-body">
                <a class="large-text birdware d-lg-none" href="#">Navigation</a>
                <ul class="navbar-nav justify-content-start flex-grow-1 pe-3">
                    <li class="nav-item" data-bs-dismiss="offcanvas" v-for="route in visibleRoutes">
                        <router-link class="nav-link" :to="route.path">{{ route.meta?.title }}</router-link>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { computed } from 'vue';
const router = useRouter();

const visibleRoutes = computed(() => {
    return router.options.routes.filter((route) =>
        route.meta?.showInNavBar == true &&
        (route.meta?.requireSSL == true && 
         location.protocol == 'https:' || 
         location.hostname == 'localhost' || 
         route.meta.requireSSL == false))
});
</script>