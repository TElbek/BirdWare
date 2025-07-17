<template>
    <div class="row mb-1">
        <div class="col birdware large-text">Forskel</div>
        <forskelSelection class="col-auto" v-if="hasData" :forskel="forskel" :is-by-trip="state.isByTrip"
            :is-this-year="state.isThisYear" :item-count-last-year="state.itemCountLastYear"
            :item-count-this-year="state.itemCountThisYear" @switch-is-by-trip="switchIsByTrip"
            @switch-is-this-year="switchIsThisYear">
        </forskelSelection>
    </div>
    <div class="row">
        <div :class="[state.isThisYear ? '' : 'd-none']">
            <forskelAar :is-this-year="true" :is-by-trip="state.isByTrip" @item-count="setItemCountThisYear">
            </forskelAar>
        </div>
        <div :class="[state.isThisYear ? 'd-none' : '']">
            <forskelAar :is-this-year="false" :is-by-trip="state.isByTrip" @item-count="setItemCountLastYear">
            </forskelAar>
        </div>
    </div>
</template>

<script setup lang="ts">
import forskelAar from '@/components/forskel/forskelAar.vue';
import forskelSelection from '@/components/forskel/forskelSelection.vue';
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