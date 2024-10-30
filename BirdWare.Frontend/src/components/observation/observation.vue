<template>
    <div>
        <div class="row">
            <div class="col-auto birdware large-text">
                Observation
            </div>
            <div class="col">
                <observation-selection></observation-selection>
            </div>
            <div class="col-auto">
                <observation-group-by></observation-group-by>
            </div>
        </div>
        <observation-list></observation-list>
    </div>
</template>

<script setup>
import { defineAsyncComponent, onMounted } from 'vue';

const observationSelection = defineAsyncComponent(() => import('./observationselection.vue'));
const observationGroupBy = defineAsyncComponent(() => import('./observationgrouping.vue'));
const observationList = defineAsyncComponent(() => import('./observationlist.vue'));
</script>