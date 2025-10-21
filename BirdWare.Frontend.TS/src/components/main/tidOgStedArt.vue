<template>
    <tw-button-dropdown :caption="props.isByTrip ? 'Tid & Sted' : 'Arter'">
        <button @click="switchIsByTrip" :disabled="isByTrip" class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright">Tid &
            Sted</button>
        <button @click="switchIsByTrip" :disabled="!isByTrip"
            class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright">Arter</button>
    </tw-button-dropdown></template>

<script setup lang="ts">
interface tidOgStedArtProps {
    isByTrip: boolean;
}

const props = defineProps<tidOgStedArtProps>();
const emit = defineEmits(['switch-is-by-trip']);

function switchIsByTrip() {
    emit('switch-is-by-trip');
};
</script>