<template>
    <select
        class="px-1 focus:outline-0 rounded border border-gray-300 h-8 focus:border-2 focus:border-blue-500"
        @input="updateInputValue" :value="modelValue">
        <slot></slot>
    </select>
</template>

<script setup lang="ts">
defineOptions({
    inheritAttrs: false
})

const props = defineProps({
    name: {
        type: String,
        required: true
    },
    modelValue: {
        type: [String, null, Number],
        default: '',
        required: true
    }
})
const emit = defineEmits(['update:modelValue'])
function updateInputValue(event: Event) {
    const input = event.target as HTMLInputElement;
    emit('update:modelValue', input.value);
}
</script>