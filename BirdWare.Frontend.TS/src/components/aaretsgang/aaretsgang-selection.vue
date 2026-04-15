<template>
    <tw-show-lg>
        <tw-button-group class="flex gap-x-4">
            <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
        </tw-button-group>
    </tw-show-lg>
    <tw-show-md>
        <div class="fixed bottom-3 left-0 right-0 bg-white dark:bg-gray-900 px-2 w-full">
            <div class="flex justify-between mx-2">
                <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
            </div>
        </div>
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