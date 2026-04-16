<template>
    <tw-show-lg>
        <tw-button-group class="flex gap-x-4">
            <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        </tw-button-group>
    </tw-show-lg>
    <tw-show-md>
        <tw-bottom-nav-bar>
            <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        </tw-bottom-nav-bar>
    </tw-show-md>
</template>

<script setup lang="ts">
import tidOgStedArt from '@/components/main/tidOgStedArt.vue';

const emit = defineEmits(['switch-is-by-trip', 'switch-is-this-year']);
interface AaretsgangSelectionProps {
    isByTrip: boolean;
}

const props = defineProps<AaretsgangSelectionProps>();

function switchIsByTrip() {
    emit('switch-is-by-trip');
};
</script>