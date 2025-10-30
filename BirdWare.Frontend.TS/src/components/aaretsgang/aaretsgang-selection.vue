<template>
    <div class="flex justify-end gap-2">
        <tidOgStedArt :isByTrip="props.isByTrip" @switch-is-by-trip="switchIsByTrip"></tidOgStedArt>
    </div>
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