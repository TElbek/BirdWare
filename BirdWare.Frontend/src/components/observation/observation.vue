<template>
    <div class="row">
        <div class="col-auto birdware large-text d-none d-lg-block">
            Observation
        </div>
        <div class="col birdware large-text d-lg-none">
            Observation
        </div>
        <div class="col d-none d-lg-block">
            <observation-selection></observation-selection>
        </div>
        <div class="col-auto d-flex gap-2">
            <observation-group-by></observation-group-by>
            <observation-view-mode></observation-view-mode>
        </div>
    </div>
    <div class="row d-lg-none mt-2">
        <div class="col">
            <observation-selection></observation-selection>
        </div>
    </div>
    <observation-presenter></observation-presenter>
</template>

<script setup>
import { defineAsyncComponent } from 'vue';
const observationSelection = defineAsyncComponent(() => import('./observationselection.vue'));
const observationGroupBy = defineAsyncComponent(() => import('./observationgrouping.vue'));
const observationViewMode = defineAsyncComponent(() => import('./observationviewmode.vue'));
const observationPresenter = defineAsyncComponent(() => import('./observationpresenter.vue'));
</script>