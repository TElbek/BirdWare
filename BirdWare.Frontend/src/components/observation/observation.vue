<template>
    <div class="d-none d-lg-block">
        <div class="row">
            <div class="col-auto birdware large-text">
                Observation
            </div>
            <div class="col">
                <observation-selection></observation-selection>
            </div>
            <div class="col-auto">
                <observation-group-by></observation-group-by>
                <observation-view-mode></observation-view-mode>
            </div>
        </div>
    </div>
    <div class="d-lg-none">
        <div class="row">
            <div class="col-auto birdware large-text">
                Observation
            </div>
            <div class="col">
                <observation-selection></observation-selection>
            </div>
        </div>
        <div class="row mt-2">
            <div class="col"></div>
            <div class="col-auto">
                <observation-group-by></observation-group-by>
                <observation-view-mode></observation-view-mode>
            </div>
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