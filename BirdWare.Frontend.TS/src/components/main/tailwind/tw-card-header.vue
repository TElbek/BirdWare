<template>
    <div class="flex justify-between font-medium text-birdware dark:text-birdware-bright text-lg">
        <span>{{ props.caption }}</span>
        <span v-if="showCount" class="text-right">{{ props.count }}</span>
    </div>
</template>

<script setup lang="ts">
interface twCardHeaderProps {
    caption: string,
    count: number,
    showCount: boolean
}

const props = withDefaults(defineProps<twCardHeaderProps>(), {
    showCount: false,
    count: 0
});
</script>