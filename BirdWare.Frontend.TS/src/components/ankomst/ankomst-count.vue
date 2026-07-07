<template>
    <div class="flex flex-row gap-x-2 font-semibold text-xs">
        <div v-if="setFoerTidAny" class="w-6 h-6 shadow rounded-full border-3 border-green-500 text-center text-black dark:text-white">{{ setFoerTid.length }}</div>
        <div v-if="setForsinketAny" class="w-6 h-6 shadow rounded-full border-3 border-red-500 text-center text-black dark:text-white">{{ setForsinket.length }}</div>
        <div v-if="ikkeSetEndnuAny" class="w-6 h-6 shadow rounded-full border-3 border-gray-300 text-center text-black dark:text-white">{{ ikkeSetEndnu.length }}</div>
    </div>
</template>

<script setup lang="ts">
    import type { ankomstDatoType } from '@/types/ankomstDatoType';
import { computed } from 'vue';

    const props = defineProps<{
        ankomstList: ankomstDatoType[];
    }>();

    const ikkeSetEndnu = computed(() => {
        return props.ankomstList.filter((one) => !one.erSetIaar);
    });

    const setFoerTid = computed(() => {
        return props.ankomstList.filter((one) => one.erSetIaar && one.forskel >= 0);
    });

    const setForsinket = computed(() => {
        return props.ankomstList.filter((one) => one.erSetIaar && one.forskel < 0);
    });

    const ikkeSetEndnuAny = computed(() => {
        return ikkeSetEndnu.value.length > 0;
    });

    const setFoerTidAny = computed(() => {
        return setFoerTid.value.length > 0;
    });

    const setForsinketAny = computed(() => {
        return setForsinket.value.length > 0;
    });
</script>