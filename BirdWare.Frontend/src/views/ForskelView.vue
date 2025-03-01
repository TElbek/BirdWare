<template>
    <div>
        <div class="row">
            <div class="col" :class="[forskel >= 0 ? 'birdware' : 'text-danger']">
                <span class="large-text fw-semibold text-nowrap">Forskel {{ forskel }}</span>
            </div>
            <div class="col-auto">
                <div class="btn-group mb-2">
                    <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-on' : 'btn-off']" @click="switchIsByTrip">Tid
                        &
                        Sted</div>
                    <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-off' : 'btn-on']" @click="switchIsByTrip">
                        Arter
                    </div>
                </div>
                <div class="btn-group mb-2 ms-2">
                    <div class="btn btn-sm" :class="[state.isThisYear ? 'btn-on' : 'btn-off']"
                        @click="switchIsThisYear">
                        <span>{{ new Date().getFullYear() }}: {{ state.itemCountThisYear }}</span>
                    </div>
                    <div class="btn btn-sm" :class="[state.isThisYear ? 'btn-off' : 'btn-on']"
                        @click="switchIsThisYear">
                        <span>{{ new Date().getFullYear() - 1 }}: {{ state.itemCountLastYear }}</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col" :class="[state.isThisYear ? '' : 'd-none']">
                <forskelAar :lastYear=false :isByTrip="state.isByTrip" @item-count="setItemCountThisYear">
                </forskelAar>
            </div>
            <div class="col" :class="[isLastYear ? '' : 'd-none']">
                <forskelAar :lastYear=true :isByTrip="state.isByTrip" @item-count="setItemCountLastYear">
                </forskelAar>
            </div>
        </div>
    </div>
</template>

<script setup>
import forskelAar from '@/components/forskel/forskelaar.vue';
import { reactive, computed } from 'vue';

const state = reactive({
    isByTrip: true,
    isThisYear: true,
    itemCountThisYear: 0,
    itemCountLastYear: 0
});

const isLastYear = computed(() => { return !state.isThisYear });
const forskel = computed(() => {return state.itemCountThisYear - state.itemCountLastYear});

function switchIsByTrip() {
    state.isByTrip = !state.isByTrip;
}

function switchIsThisYear() {
    state.isThisYear = !state.isThisYear;
}

function setItemCountThisYear(count) {
    state.itemCountThisYear = count;
}

function setItemCountLastYear(count) {
    state.itemCountLastYear = count;
}
</script>