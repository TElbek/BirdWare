<template>
    <div>
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
                    <div class="btn btn-sm" :class="[state.isThisYear ? 'btn-on' : 'btn-off']"
                        @click="switchIsThisYear">
                        <span>{{ new Date().getFullYear() }}: {{ state.itemCountThisYear }}</span>
                    </div>
                    <div class="btn btn-sm" :class="[state.isThisYear ? 'btn-off' : 'btn-on']"
                        @click="switchIsThisYear">
                        <span>{{ new Date().getFullYear() - 1 }}: {{ state.itemCountLastYear }}</span>
                    </div>
                    <div class="btn btn-sm btn-off">
                        <div class="forskel-indikator" :class="[forskel >= 0 ? 'forskel-success' : 'forskel-danger']">{{Math.abs(forskel)}}</div>
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
    hasDataLastYear: false,
    hasDataThisYear: false,
    itemCountThisYear: 0,
    itemCountLastYear: 0
});

const isLastYear = computed(() => { return !state.isThisYear });
const forskel = computed(() => { return state.itemCountThisYear - state.itemCountLastYear });
const hasData = computed(() => { return state.hasDataLastYear && state.hasDataThisYear });

function switchIsByTrip() {
    state.isByTrip = !state.isByTrip;
}

function switchIsThisYear() {
    state.isThisYear = !state.isThisYear;
}

function setItemCountThisYear(count) {
    state.hasDataThisYear = true;
    state.itemCountThisYear = count;
}

function setItemCountLastYear(count) {
    state.hasDataLastYear = true;
    state.itemCountLastYear = count;
}
</script>