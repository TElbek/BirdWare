<template>
    <div class="flex justify-end gap-2">
        <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        <tw-button-responsive
            :caption="isThisYear ? `${thisYear}: ${itemCountThisYear}` : `${lastYear}: ${itemCountLastYear}`">
            <tw-button :caption="thisYear + ' : ' + itemCountThisYear"
                @click="$emit('switch-is-this-year')" :isSelected="isThisYear">
            </tw-button>
            <tw-button :caption="lastYear + ' : ' + itemCountLastYear"
                @click="$emit('switch-is-this-year')" :isSelected="!isThisYear">
            </tw-button>
        </tw-button-responsive>
        <button class="border border-gray-200 rounded-sm shadow-xs dark:border-birdware-bright w-10 mb-0.5"
           :class="[props.forskel >= 0 ? 'forskel-success' : 'forskel-danger']">{{
                Math.abs(props.forskel)}}</button>
    </div>
</template>

<script setup lang="ts">
import tidOgStedArt from '@/components/main/tidOgStedArt.vue';

interface ForskelSelectionProps {
    itemCountThisYear: number;
    itemCountLastYear: number;
    forskel: number;
    isByTrip: boolean;
    isThisYear: boolean;
}

const props = defineProps<ForskelSelectionProps>();
const emit = defineEmits(['switch-is-by-trip', 'switch-is-this-year']);

const thisYear = new Date().getFullYear();
const lastYear = thisYear - 1;

function switchIsByTrip() {
    emit('switch-is-by-trip');
};
</script>