<template>
    <input :type="type" :name="name" @input="updateInputValue" :value="modelValue" :placeholder="placeholder"
        class="focus:outline-0 rounded border border-gray-300 h-8 focus:border-2 focus:border-blue-500"
        :class="[type == 'number' ? 'text-end pe-2' : 'ps-2']" />
</template>

<script setup lang="ts">
defineOptions({
    inheritAttrs: false
})

defineProps({
    name: {
        type: String,
        required: true
    },
    type: {
        type: String,
        default: 'text'
    },
    modelValue: {
        type: [String, Number, Date, Boolean],
        default: ''
    },
    placeholder: {
        type: String,
        default: ''
    }
})
const emit = defineEmits(['update:modelValue'])
function updateInputValue(event: Event) {
    const input = event.target as HTMLInputElement;
    emit('update:modelValue', input.value);
}
</script>