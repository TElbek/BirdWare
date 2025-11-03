<template>
    <button type="button"
        :disabled="isSelected"
        class="text-sm text-birdware dark:text-white cursor-pointer"
        :class="[props.isSelected ? '': '']">
        <div class="dark:text-birdware-bright text-start">
            <span v-if="props.isSelected" class="mr-1 hidden lg:inline">✓</span>
            <span>{{ props.caption }}</span>
        </div>
    </button>
</template>

<script lang="ts" setup>
interface TwButtonProps {
    isSelected: boolean;
    caption: string;
}

const props = defineProps<TwButtonProps>();
</script>