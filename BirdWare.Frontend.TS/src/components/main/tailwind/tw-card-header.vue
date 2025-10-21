<template>
    <div class="grid grid-cols-[auto_15px] mb-1">
        <span class="text-base font-semibold text-birdware dark:text-birdware-bright">{{ props.caption }}</span>
        <span v-if="showCount" class="text-base font-semibold text-birdware text-right">{{ props.count }}</span>
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