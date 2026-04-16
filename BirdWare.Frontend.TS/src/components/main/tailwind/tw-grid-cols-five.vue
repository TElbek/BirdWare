<template>
    <div class="scrollbar-hide overflow-auto" :class="`max-h-[${vh}vh]`">
        <div :class="getRowColClasses(props.count)">
            <slot></slot>
        </div>
    </div>
</template>

<script setup lang="ts">
import { getRowColClasses } from '@/ts/rowcols';
import { computed } from 'vue';
interface rowcolsProps {
    count: number;
    offset: number
}

const vh = computed(() => 80 - props.offset);

const props = withDefaults(defineProps<rowcolsProps>(), {
    offset: 0
});
</script>