<template>
    <div class="flex flex-col gap-y-2">
        <div class="flex gap-x-2 justify-between">
            <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
            <tw-show-lg>
                <forskel-selection :is-by-trip="state.isByTrip" :is-this-year="state.isThisYear"
                    :forskel="state.itemCountThisYear - state.itemCountLastYear"
                    :item-count-last-year="state.itemCountLastYear" :item-count-this-year="state.itemCountThisYear"
                    @switch-is-by-trip="switchIsByTrip" @switch-is-this-year="switchIsThisYear"></forskel-selection>
            </tw-show-lg>
            <tw-show-md>
                <div class="fixed pb-2 m-4 bottom-0 left-0 right-0 bg-white">
                    <forskel-selection :is-by-trip="state.isByTrip" :is-this-year="state.isThisYear"
                        :forskel="state.itemCountThisYear - state.itemCountLastYear"
                        :item-count-last-year="state.itemCountLastYear" :item-count-this-year="state.itemCountThisYear"
                        @switch-is-by-trip="switchIsByTrip" @switch-is-this-year="switchIsThisYear"></forskel-selection>
                </div>
            </tw-show-md>
        </div>

        <div :class="[state.isThisYear ? 'block' : 'hidden']">
            <forskel-aar :is-by-trip="state.isByTrip" :is-this-year="true" @item-count="setItemCountThisYear">
            </forskel-aar>
        </div>

        <div :class="[!state.isThisYear ? 'block' : 'hidden']">
            <forskel-aar :is-by-trip="state.isByTrip" :is-this-year="false" @item-count="setItemCountLastYear">
            </forskel-aar>
        </div>


    </div>
</template>

<script setup lang="ts">
import { reactive, computed } from 'vue';
import forskelAar from './forskel-aar.vue'
import forskelSelection from './forskel-selection.vue'
import { useRoute } from 'vue-router';

const route = useRoute();
const state = reactive({
    isByTrip: true,
    isThisYear: true,
    hasDataLastYear: false,
    hasDataThisYear: false,
    itemCountThisYear: 0,
    itemCountLastYear: 0
});

const forskel = computed(() => { return state.itemCountThisYear - state.itemCountLastYear });

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