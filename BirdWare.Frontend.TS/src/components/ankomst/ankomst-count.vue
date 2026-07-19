<template>
    <div class="flex flex-row gap-x-2 justify-end ms-2">
        <tw-badge v-if="setFoerTid.length > 0" :caption="'set'" :color-class="'bg-green-500'"
            :count="setFoerTid.length"></tw-badge>
        <tw-badge v-if="setForsinket.length > 0" :caption="'forsinket'" :color-class="'bg-red-500'"
            :count="setForsinket.length"></tw-badge>
        <tw-badge v-if="ikkeSetEndnu.length > 0" :caption="'ikke set'" :color-class="'bg-gray-300'"
            :count="ikkeSetEndnu.length"></tw-badge>
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
</script>