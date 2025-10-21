<template>
    <div :class="getRowColClasses(props.count)">
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