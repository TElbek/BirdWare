<template>
    <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        <span>{{ props.caption }}</span>
    </button>
    <slot></slot>
</template>

<script setup lang="ts">
interface bsbuttondropdownProps {
    caption: string
}

const props = defineProps<bsbuttondropdownProps>();
</script>