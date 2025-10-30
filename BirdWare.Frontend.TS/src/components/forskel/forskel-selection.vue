<template>
    <div class="flex justify-end gap-2">
        <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        <tw-button-dropdown
            :caption="isThisYear ? `${thisYear}: ${itemCountThisYear}` : `${lastYear}: ${itemCountLastYear}`">
            <button @click="$emit('switch-is-this-year')" :disabled="isThisYear"
                class="block px-2 py-1 text-sm cursor-pointer dark:text-birdware-bright">
                {{ thisYear }}: {{ itemCountThisYear }}
            </button>
            <button @click="$emit('switch-is-this-year')" :disabled="!isThisYear"
                class="block px-2 py-1 text-sm cursor-pointer dark:text-birdware-bright">
                {{ lastYear }}: {{ itemCountLastYear }}
            </button>
        </tw-button-dropdown>
        <button class="relative top-2 rounded-sm border border-gray-300 font-bold text-nowrap h-6 px-2"
            :class="[props.forskel >= 0 ? 'forskel-success' : 'forskel-danger']">{{ Math.abs(props.forskel) }}</button>
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