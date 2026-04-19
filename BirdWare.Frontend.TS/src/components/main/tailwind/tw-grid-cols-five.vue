<template>
    <div class="scrollbar-hide overflow-auto" :class="`max-h-[80vh]`">
        <div :class="getRowColClasses(props.count)">
            <slot></slot>
        </div>
    </div>
</template>

<script setup lang="ts">
import { getRowColClasses } from '@/ts/rowcols';
interface rowcolsProps {
    count: number;
}

const props = defineProps<rowcolsProps>();
</script>