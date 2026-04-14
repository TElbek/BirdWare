<template>
    <tw-button-group class="flex lg:gap-x-5 justify-between w-full px-1"
        :caption="isThisYear ? `${thisYear}: ${itemCountThisYear}` : `${lastYear}: ${itemCountLastYear}`">
        <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        <tw-button :caption="thisYear + ':' + itemCountThisYear" @click="$emit('switch-is-this-year')"
            :isSelected="isThisYear">
        </tw-button>
        <tw-button :caption="lastYear + ':' + itemCountLastYear" @click="$emit('switch-is-this-year')"
            :isSelected="!isThisYear">
        </tw-button>
        <tw-show-lg>
            <button class="rounded-sm shadow-xs dark:border-birdware-bright w-10"
                :class="[props.forskel >= 0 ? 'forskel-success' : 'forskel-danger']">{{
                    Math.abs(props.forskel) }}</button>
        </tw-show-lg>
    </tw-button-group>
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