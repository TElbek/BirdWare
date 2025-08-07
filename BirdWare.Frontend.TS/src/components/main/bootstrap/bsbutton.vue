<template>
    <div class="btn btn-sm" :class="[props.isOn ? 'btn-on' : 'btn-off']" @click="sendClickEvent">
        <slot></slot>
    </div>
</template> 

<script setup lang="ts">
interface bsbuttonProps {
    isOn: boolean;
}

const emit = defineEmits(['clicked']);
const props = defineProps<bsbuttonProps>();

function sendClickEvent() {
    emit('clicked');
}
</script>