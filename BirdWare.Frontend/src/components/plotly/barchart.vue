<template>
    <div id="plotDiv" class="mt-1"></div>
</template>

<script setup>
import Plotly from 'plotly.js-basic-dist-min';
import { reactive, onMounted, watch } from 'vue';

const props = defineProps({
    isVertical: false,
    index: -1,
    x: Array,
    y: Array
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
            l: 0,
            r: 0,
            b: 45,
            t: 10,
            pad: 0
        }
    },
    layoutText: {
        xaxis: {
            visible: false,
            tickmode: 'linear',
            dtick: 1.0,
        },
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
    makePlot();
});

function setLayoutHeight() {
    state.layoutText.height = window.innerHeight - 250;
    state.layoutNumeric.height = window.innerHeight - 250;
}

function makePlot() {

    state.data[0].x = props.x;
    state.data[0].y = props.y;

    state.data[0].orientation = (props.isVertical ? 'h' : 'v');
    state.layoutNumeric.yaxis.dtick = Math.round(Math.log2(Math.max(...props.y)));
    state.data[0].text = (props.isVertical ? props.x.map(String) : props.y.map(String));

    Plotly.newPlot('plotDiv', state.data,
        (props.isVertical ? state.layoutText : state.layoutNumeric),
        { displayModeBar: false, responsive: true, scrollZoom: false });
}

watch(() => props.index, (newValue) => {
    makePlot();
});
</script>