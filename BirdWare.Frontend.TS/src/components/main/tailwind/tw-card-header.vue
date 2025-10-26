<template>
    <div class="grid grid-cols-[auto_15px] mb-1 tracking-wider">
        <span class="text-base font-medium text-birdware dark:text-birdware-bright">{{ props.caption }}</span>
        <span v-if="showCount" class="text-base font-medium text-birdware text-right">{{ props.count }}</span>
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