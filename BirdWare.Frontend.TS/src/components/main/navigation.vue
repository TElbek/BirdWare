<template>
    <nav class="mb-1">
        <div class="grid grid-cols-[125px_auto] text-birdware dark:text-birdware-bright">
            <a href="#" class="mr-4 cursor-pointer text-xl relative top-[2px]">
                <span>Birdware</span>
            </a>
            <button @click="isOpen = !isOpen"
                class="relative ml-auto h-6 max-h-[40px] w-6 max-w-[40px] select-none text-center align-middle text-xs font-medium uppercase text-inherit transition-all hover:bg-transparent focus:bg-transparent active:bg-transparent disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none lg:hidden p-4 border border-gray-300 rounded"
                type="button">
                <span class="absolute transform -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" stroke="currentColor"
                        stroke-width="2">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16"></path>
                    </svg>
                </span>
            </button>

            <div :class="['w-full md:w-auto', isOpen ? '' : 'hidden', 'lg:block']" @click="toggleIsOpen">
                <ul class="flex flex-col gap-2 mt-2 mb-4 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
                    <li class="flex items-center text-sm gap-x-2 lg:text-base" v-for="route in visibleRoutes"
                        :key="route.path">
                        <router-link :to="route.path" class="text-nowrap pb-2" >
                            <span class="nav-item">{{ route.meta?.title }}</span>
                        </router-link>
                    </li>
                    <li>
                        <tw-toggle-dark></tw-toggle-dark>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';
import TwToggleDark from '@/components/main/tailwind/tw-toggle-dark.vue';

const router = useRouter();
const isOpen = ref(false);

const matchLG = window.matchMedia('(min-width: 1024px)');

function toggleIsOpen() {
    if (!matchLG.matches) {
        isOpen.value = !isOpen.value;
    }
}

const visibleRoutes = computed(() => {
    return router.options.routes.filter((route) =>
        route.meta?.showInNavBar == true &&
        (route.meta?.requireSSL == true &&
            location.protocol == 'https:' ||
            location.hostname == 'localhost' ||
            route.meta.requireSSL == false))
});
</script>

<style scoped>
.nav-item {
    position: relative;
    top: 4px;
}
</style>