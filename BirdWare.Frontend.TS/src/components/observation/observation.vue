<template>
    <div class="row">
        <bs-show-lg class="col-auto birdware large-text">
            Observation
        </bs-show-lg>
        <bs-show-md class="col birdware large-text">
            Observation
        </bs-show-md>
        <bs-show-lg class="col">
            <observation-selection></observation-selection>
        </bs-show-lg>
        <div class="col-auto d-flex gap-2">
            <observation-group-by></observation-group-by>
        </div>
    </div>
    <bs-show-md class="mt-2">
            <observation-selection></observation-selection>
    </bs-show-md>
    <observation-presenter></observation-presenter>
</template>

<script setup lang="ts">
import observationSelection from './observationselection.vue';
import observationGroupBy from './observationgrouping.vue';
import observationPresenter from './observationpresenter.vue';
</script>