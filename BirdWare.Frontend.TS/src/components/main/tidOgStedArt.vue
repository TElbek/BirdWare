<template>
    <tw-button-responsive :caption="props.isByTrip ? 'Tid & Sted' : 'Arter'">
        <tw-button @click="switchIsByTrip" :caption="'Tid & Sted'" :isSelected="isByTrip"></tw-button>
        <tw-button @click="switchIsByTrip" :caption="'Arter'" :isSelected="!isByTrip"></tw-button>
    </tw-button-responsive>
</template>

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