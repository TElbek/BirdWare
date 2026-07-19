<template>
    <div class="flex text-xs lg:text-md gap-x-1 text-gray-600 dark:text-white h-5 font-normal border-gray-300 border rounded-lg px-1 shadow-xs">
        <div class="w-2 h-2 lg:w-2.5 lg:h-2.5 rounded-full relative top-1.25 lg:top-1" :class="props.colorClass"></div>
        <tw-show-lg>
            <div class="capitalize">{{ props.caption }}</div>
        </tw-show-lg>
        <div>{{ props.count}}</div>
    </div>
</template>

<script setup lang="ts">
interface badgeProps {
    caption: String,
    colorClass: String,
    count: Number
}

const props = defineProps<badgeProps>();
</script>
