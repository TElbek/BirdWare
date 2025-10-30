<template>
    <div :class="getRowColClassesThree(props.count)" class="scrollbar-hide max-h-[75vh] overflow-auto">
        <slot></slot>
    </div>
</template> 
 
<script setup lang="ts">
import { getRowColClassesThree } from '@/ts/rowcols';
interface rowcolsProps {
    count: number;
}

const props = defineProps<rowcolsProps>();
</script>