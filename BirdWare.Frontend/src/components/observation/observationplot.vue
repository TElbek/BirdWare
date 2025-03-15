<template>
    <barChart :isVertical="obsSelectionStore.isGropingByText" :x="state.x" :y="state.y"
        :index="state.index">
    </barChart>
</template>

<script setup>
import { defineAsyncComponent, reactive, onMounted, onBeforeUnmount, watch } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const barChart = defineAsyncComponent(() => import('@/components/plotly/barchart.vue'));
const obsSelectionStore = useObsSelectionStore();

const props = defineProps({
    groupedData: {}
});

const state = reactive({
    x: [],
    y: [],
    index: 0
});

onMounted(() => {
    transferData();
});

onBeforeUnmount(() => {
    state.index = 0;
});

function transferData() {
    state.x = [];
    state.y = [];
    for (let [key, value] of props.groupedData) {
        state.x.push(obsSelectionStore.isGropingByText ? value.length : key);
        state.y.push(obsSelectionStore.isGropingByText ? key : value.length);
    }
    state.index++;
}

watch(() => props.groupedData, (newValue) => {
    transferData();
});
</script>