<template>
    <div class="scrollbar-hide overflow-auto" :style="{ height: vh + 'vh' }">
        <div :class="getRowColClassesGeneric()">
            <slot></slot>
        </div>
    </div>
</template>

<script setup lang="ts">
import { getRowColClassesFive, getRowColClassesFour, getRowColClassesThree, getRowColClassesTwo } from '@/ts/rowcols';
import { computed } from 'vue';

const props = defineProps({
    itemsPerRow: {
        type: Number,
        required: true
    },
    count: {
        type: Number,
        required: true
    },
    offset: {
        type: Number,
        default: 0,
        required: false
    }
})

function getRowColClassesGeneric() {
    if(props.itemsPerRow <= 2) return getRowColClassesTwo(props.count);
    if(props.itemsPerRow == 3) return getRowColClassesThree(props.count);
    if(props.itemsPerRow == 4) return getRowColClassesFour(props.count);
    else return getRowColClassesFive(props.count);
}

const vh = computed(() => 80 - props.offset);
</script>