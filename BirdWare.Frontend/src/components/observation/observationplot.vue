<template>
    <div id="plotDiv" class="mt-2"></div>
</template>

<script setup>
import Plotly from 'plotly.js-dist-min';
import { reactive, onMounted, watch } from 'vue';
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
            text: null,
            textposition: 'auto',
            hoverinfo: 'none',
            orientation: '',
            marker: {
                color: 'rgb(4, 139, 175)',
            }
        }
    ],
    layoutNumeric: {
        xaxis: {
            tickmode: 'linear',
            tick0: 0,
            dtick: 1.0,
        },
        yaxis: {
            visible: false,
            tickmode: 'linear',
            dtick: 1.0,
        },
        margin: {
            l: 30,
            r: 30,
            b: 30,
            t: 10,
            pad: 5
        }
    },
    layoutText: {
        type: 'category',
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
    setLayoutHeight();
    transferData();
});

function setLayoutHeight() {
    state.layoutText.height = window.innerHeight - 200;
    state.layoutNumeric.height = window.innerHeight - 200;
}

function transferData() {
    state.data[0].x = [];
    state.data[0].y = [];
    for (let [key, value] of props.groupedData) {
        state.data[0].x.push(obsSelectionStore.isGropingByText ? value.length : key);
        state.data[0].y.push(obsSelectionStore.isGropingByText ? key : value.length);
    }

    state.data[0].orientation = (obsSelectionStore.isGropingByText ? 'h' : 'v');
    state.layoutNumeric.yaxis.dtick = Math.round(Math.log2(Math.max(...state.data[0].y)));
    state.data[0].text = (obsSelectionStore.isGropingByText ? state.data[0].x.map(String) : state.data[0].y.map(String));

    Plotly.newPlot('plotDiv', state.data,
        (obsSelectionStore.isGropingByText ? state.layoutText : state.layoutNumeric),
        { displayModeBar: false, responsive: true, scrollZoom: false });
}

watch(() => props.groupedData, (newValue) => {
    transferData();
});
</script>