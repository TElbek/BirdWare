<template>
    <div class="flex justify-end gap-2 mb-2">
        <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        <tw-button-dropdown
            :caption="isThisYear ? `${thisYear}: ${itemCountThisYear}` : `${lastYear}: ${itemCountLastYear}`">
            <button @click="$emit('switch-is-this-year')" :disabled="isThisYear"
                class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright">
                {{ thisYear }}: {{ itemCountThisYear }}
            </button>
            <button @click="$emit('switch-is-this-year')" :disabled="!isThisYear"
                class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright">
                {{ lastYear }}: {{ itemCountLastYear }}
            </button>
        </tw-button-dropdown>
        <tw-button class="h-8 rounded border border-gray-300 mt-2 font-bold text-nowrap" :isSelected="false"
            :caption="Math.abs(props.forskel).toString()"
            :class="[props.forskel >= 0 ? 'forskel-success' : 'forskel-danger']"></tw-button>
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