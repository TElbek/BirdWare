<template>
    <label :for="props.for" class="text-nowrap mx-1 font-semibold">
        <slot></slot>
    </label>
</template> 

<script setup lang="ts">
const props = defineProps({
    for: {
        type: String,
        required: true
    }
})
</script>