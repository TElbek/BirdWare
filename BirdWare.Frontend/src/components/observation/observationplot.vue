<template>
    <div id="plotDiv" class="mt-2"></div>
</template>

<script setup>
import Plotly from 'plotly.js-dist-min';
import { reactive, onMounted, computed, watch } from 'vue';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();

const props = defineProps({
    groupedData: {}
});

const state = reactive({
    data: [
        {
            x: [],
            y: [],
            type: 'bar',
            orientation: ''
        }
    ],
    layoutNumeric: {
        margin: {
            l: 30,
            r: 30,
            b: 30,
            t: 10,
            pad: 5
        }
    },
    layoutText: {
        margin: {
            l: 130,
            r: 30,
            b: 30,
            t: 10,
            pad: 5
        }
    }
});

onMounted(() => {
    transferData();
});

function transferData() {
    state.data[0].orientation = (obsSelectionStore.isGropingByText ? 'h' : 'v');

    state.data[0].x = [];
    state.data[0].y = [];
    for (let [key, value] of props.groupedData) {
        state.data[0].x.push(obsSelectionStore.isGropingByText ? value.length : key);
        state.data[0].y.push(obsSelectionStore.isGropingByText ? key : value.length);
    }
    Plotly.newPlot('plotDiv', state.data,
        (obsSelectionStore.isGropingByText ? state.layoutText : state.layoutNumeric),
        { displayModeBar: false, responsive: true, scrollZoom: false });
}

watch(() => props.groupedData, (newValue) => {
    transferData();
});
</script>