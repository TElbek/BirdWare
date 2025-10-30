<template>
    <div :class="getRowColClasses(props.count)" class="scrollbar-hide max-h-[80vh] overflow-auto">
        <slot></slot>
    </div>
</template> 
 
<script setup lang="ts">
import { getRowColClasses } from '@/ts/rowcols';
interface rowcolsProps {
    count: number;
}

const props = defineProps<rowcolsProps>();
</script>