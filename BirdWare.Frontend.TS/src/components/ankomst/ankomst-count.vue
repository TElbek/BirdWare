<template>
    <div class="gradient shadow-sm shadow-gray-600 rounded" :title="`${setFoerTid.length} før tid, ${setForsinket.length} forsinket, ${ikkeSetEndnu.length} ikke set`">
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
const setForsinketSlut = computed(() => (setFoerTid.value.length + setForsinket.value.length) / sum.value * 100 + '%');
const ikkeSetSlut = computed(() => (setFoerTid.value.length + setForsinket.value.length + ikkeSetEndnu.value.length) / sum.value * 100 + '%');

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
.gradient {
    height: 20px;
    width: 100px;
    background-image: linear-gradient(to right,
            var(--color-green-500),
            var(--color-green-500) v-bind(setFoerTidSlut),
            var(--color-red-500) v-bind(setFoerTidSlut),
            var(--color-red-500) v-bind(setForsinketSlut),
            var(--color-gray-300) v-bind(setForsinketSlut),
            var(--color-gray-300) v-bind(ikkeSetSlut));
}
</style>