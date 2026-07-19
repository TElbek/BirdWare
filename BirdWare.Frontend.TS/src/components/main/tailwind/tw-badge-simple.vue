<template>
    <div class="text-xs lg:text-md gap-x-1 w-7 text-center text-birdware dark:text-birdware-bright h-5 font-semibold border-gray-300 border rounded-lg px-1 shadow-xs mt-1">{{ props.count }}</div>
</template>

<script setup lang="ts">
interface simpleBadgeProps {
    count: Number
}

const props = defineProps<simpleBadgeProps>();
</script>
