<template>
    <div :class="getRowColClassesThree(props.count)">
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