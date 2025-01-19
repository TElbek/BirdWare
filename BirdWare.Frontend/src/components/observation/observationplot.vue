<template>
    <div class="h1">Hejsa</div>
</template>

<script setup>
import Plotly from 'plotly.js-dist-min';
import { reactive, onMounted, computed, watch } from 'vue';
import api from '@/api';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const obsSelectionStore = useObsSelectionStore();
// const { selectedTags } = storeToRefs(obsSelectionStore)

const state = reactive({
    observationer: [],
});
</script>