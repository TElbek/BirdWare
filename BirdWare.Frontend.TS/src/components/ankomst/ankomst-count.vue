<template>
    <div class="flex flex-row gap-x-2 w-50 justify-end ms-2">
        <div v-if="setFoerTid.length > 0" class="bar foertid text-center text-sm text-white shadow-sm shadow-gray-400">{{ setFoerTid.length }}</div>
        <div v-if="setForsinket.length > 0"class="bar forsinket text-center text-sm text-white shadow-sm shadow-gray-400">{{ setForsinket.length }}</div>
        <div v-if="ikkeSetEndnu.length > 0"class="bar ikkeset text-center text-sm text-black shadow-sm shadow-gray-400">{{ ikkeSetEndnu.length }}</div>        
    </div>
</template>

<script setup lang="ts">
import type { ankomstDatoType } from '@/types/ankomstDatoType';
import { computed } from 'vue';

const props = defineProps<{
    ankomstList: ankomstDatoType[];
}>();

const sum = computed(() => ikkeSetEndnu.value.length + setFoerTid.value.length + setForsinket.value.length);

const setFoerTidSlut = computed(() => setFoerTid.value.length / sum.value * 100 + '%');
const setForsinketSlut = computed(() => setForsinket.value.length / sum.value * 100 + '%');
const ikkeSetSlut = computed(() => (ikkeSetEndnu.value.length) / sum.value * 100 + '%');

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

<style scoped>
.bar {
    display: inline-block;
    height: 22px;
    border-radius: var(--radius-md);
}

.foertid {
    background-color: var(--color-green-500);
    width: v-bind(setFoerTidSlut);
}

.forsinket {
    background-color: var(--color-red-500);
    width: v-bind(setForsinketSlut);
}

.ikkeset {
    background-color: var(--color-gray-300);
    width: v-bind(ikkeSetSlut);
}

</style>