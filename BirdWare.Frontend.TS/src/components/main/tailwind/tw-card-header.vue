<template>
    <div class="flex justify-between text-[1.03rem] pb-0.5 font-semibold text-birdware dark:text-birdware-bright">
        <a @click="emit('click', props.caption)" class="cursor-pointer">
            <span class="capitalize">{{ props.caption }}</span>
        </a>
        <tw-badge-simple :count="props.count"></tw-badge-simple>
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

const emit = defineEmits(['click'])
</script>