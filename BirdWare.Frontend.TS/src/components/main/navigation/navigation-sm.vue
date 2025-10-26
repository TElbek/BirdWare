<template>
    <div class="grid grid-cols-[45px_auto] text-birdware dark:text-birdware-bright">

            <button
                class="relative mr-auto h-6 max-h-10 w-6 max-w-10 select-none text-center align-middle text-xs font-medium uppercase text-inherit transition-all hover:bg-transparent focus:bg-transparent active:bg-transparent disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none p-4 border border-gray-300 rounded"
                type="button" @click="toggleIsOpen">
                <span class="absolute transform -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" stroke="currentColor"
                        stroke-width="2">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16"></path>
                    </svg>
                </span>
            </button>

            <router-link v-if="!isAtHomeRoute && homeRoute != undefined" :to="homeRoute.path" >
                <span class="text-2xl tracking-wide">{{ homeRoute?.meta?.title }}</span>
            </router-link>

            <div>
                <div v-show="isOpen" class="fixed inset-0 z-40 transition-opacity duration-200" @click="closeMenu"
                    aria-hidden="true"></div>

                <aside @click="closeMenu"
                    :class="['fixed top-0 left-0 h-full z-50 w-64 bg-white dark:bg-gray-800 p-4 transform transition-transform duration-300', isOpen ? 'translate-x-0 rounded shadow-xl shadow-gray-400 dark:shadow-gray-100' : '-translate-x-full']">
                    <ul class="flex flex-col gap-4 text-base tracking-wide">
                        <li v-for="route in visibleRoutes" :key="route.path" class="cursor-pointer">
                            <router-link :to="route.path">
                                <span>{{ route.meta?.title }}</span>
                            </router-link>
                        </li>
                        <li>
                            <tw-toggle-dark></tw-toggle-dark>
                        </li>
                    </ul>
                </aside>
            </div>
        </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import TwToggleDark from '@/components/main/tailwind/tw-toggle-dark.vue';
import {useRouteLogic} from '@/composables/route-logic'

const { homeRoute, isAtHomeRoute, visibleRoutes } = useRouteLogic();
const isOpen = ref(false);

onMounted(() => {
});

function toggleIsOpen() {
    isOpen.value = !isOpen.value;
}

function closeMenu() {
    isOpen.value = false;
}
</script>