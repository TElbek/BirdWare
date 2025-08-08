<template>
    <div class="btn btn-sm" :class="[props.isOn ? 'btn-on' : 'btn-off']" @click="sendClickEvent">
        <slot></slot>
    </div>
</template> 

<script setup lang="ts">
interface bsbuttonProps {
    isOn?: boolean;
}

const props = withDefaults(defineProps<bsbuttonProps>(), {
  isOn: false
});

const emit = defineEmits(['clicked']);

function sendClickEvent() {
    emit('clicked');
}
</script>