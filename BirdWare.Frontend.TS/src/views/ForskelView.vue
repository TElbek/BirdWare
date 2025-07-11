<template>
    <div class="row">
        <div class="col birdware large-text">Forskel</div>
        <div class="col-lg-auto" v-if="hasData">
            <div class="btn-group mb-2">
                <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-on' : 'btn-off']" @click="switchIsByTrip">Tid
                    &
                    Sted</div>
                <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-off' : 'btn-on']" @click="switchIsByTrip">
                    Arter
                </div>
            </div>
            <div class="btn-group mb-2 ms-2" v-if="hasData">
                <div class="btn btn-sm" :class="[state.isThisYear ? 'btn-on' : 'btn-off']" @click="switchIsThisYear">
                    <span>{{ new Date().getFullYear() }}: {{ state.itemCountThisYear }}</span>
                </div>
                <div class="btn btn-sm" :class="[state.isThisYear ? 'btn-off' : 'btn-on']" @click="switchIsThisYear">
                    <span>{{ new Date().getFullYear() - 1 }}: {{ state.itemCountLastYear }}</span>
                </div>
                <div class="btn btn-sm btn-off">
                    <div class="forskel-indikator" :class="[forskel >= 0 ? 'forskel-success' : 'forskel-danger']">
                        {{ Math.abs(forskel) }}</div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div :class="[state.isThisYear ? '' : 'd-none']">
            <forskelAar :is-this-year="true" :is-by-trip="state.isByTrip"
                @item-count="setItemCountThisYear">
            </forskelAar>
        </div>
        <div :class="[state.isThisYear ? 'd-none' : '']">
            <forskelAar :is-this-year="false" :is-by-trip="state.isByTrip"
                @item-count="setItemCountLastYear">
            </forskelAar>
        </div>
    </div>
</template>

<script setup lang="ts">
import forskelAar from '@/components/forskel/forskelAar.vue';
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