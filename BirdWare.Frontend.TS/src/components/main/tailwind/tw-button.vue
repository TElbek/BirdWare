<template>
    <button type="button"
        :disabled="isSelected"
        class="px-2 py-1 text-sm text-gray-900 dark:border dark:border-birdware-bright dark:text-white"
        :class="[props.isSelected ? 'bg-gray-300 rounded dark:bg-gray-600': 'cursor-pointer']">
        {{ props.caption }}
    </button>
</template>

<script lang="ts" setup>
interface TwButtonProps {
    isSelected: boolean;
    caption: string;
}

const props = defineProps<TwButtonProps>();
</script>