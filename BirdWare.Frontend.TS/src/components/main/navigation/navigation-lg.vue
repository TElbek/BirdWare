<template>
    <div class="max-lg:hidden text-birdware dark:text-birdware-bright">
        <ul class="flex gap-x-8 text-lg cursor-pointer tracking-wide">
            <li>
                <router-link v-if="homeRoute != undefined" :to="homeRoute.path">
                    <span>{{ homeRoute?.meta?.title }}</span>
                </router-link>
            </li>
            <li v-for="route in visibleRoutes" :key="route.path">
                <router-link :to="route.path">
                    <span>{{ route.meta?.title }}</span>
                </router-link>
            </li>
            <li>
                <tw-toggle-dark></tw-toggle-dark>
            </li>
        </ul>
    </div>
</template>

<script setup lang="ts">
import TwToggleDark from '@/components/main/tailwind/tw-toggle-dark.vue';
import { useRouteLogic } from '@/composables/route-logic'

const { homeRoute, visibleRoutes } = useRouteLogic();
</script>