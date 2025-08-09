<template>
    <div class="row mb-1">
        <div class="col birdware large-text">Forskel</div>
        <forskel-selection class="col-auto" v-if="hasData" :forskel="forskel" :is-by-trip="state.isByTrip"
            :is-this-year="state.isThisYear" :item-count-last-year="state.itemCountLastYear"
            :item-count-this-year="state.itemCountThisYear" @switch-is-by-trip="switchIsByTrip"
            @switch-is-this-year="switchIsThisYear">
        </forskel-selection>
    </div>
    <div :class="[state.isThisYear ? 'mt-2' : 'd-none']">
        <forskel-aar :is-this-year="true" :is-by-trip="state.isByTrip" @item-count="setItemCountThisYear">
        </forskel-aar>
    </div>
    <div :class="[state.isThisYear ? 'd-none' : 'mt-2']">
        <forskel-aar :is-this-year="false" :is-by-trip="state.isByTrip" @item-count="setItemCountLastYear">
        </forskel-aar>
    </div>
</template>

<script setup lang="ts">
import forskelAar from '@/components/forskel/forskelaar.vue';
import forskelSelection from '@/components/forskel/forskelselection.vue';
import { reactive, computed } from 'vue';

const forskel = computed(() => { return state.itemCountThisYear - state.itemCountLastYear });
const hasData = computed(() => { return state.hasDataLastYear && state.hasDataThisYear });

const state = reactive({
    isByTrip: true,
    isThisYear: true,
    hasDataLastYear: false,
    hasDataThisYear: false,
    itemCountThisYear: 0,
    itemCountLastYear: 0
});

function switchIsByTrip() {
    state.isByTrip = !state.isByTrip;
}

function switchIsThisYear() {
    state.isThisYear = !state.isThisYear;
}

function setItemCountThisYear(count: number) {
    state.hasDataThisYear = true;
    state.itemCountThisYear = count;
}

function setItemCountLastYear(count: number) {
    state.hasDataLastYear = true;
    state.itemCountLastYear = count;
}
</script>