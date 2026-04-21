<template>
    <div class="scrollbar-hide overflow-auto" :style="{ height: vh + 'vh' }">
        <div :class="getRowColClasses(props.count)">
            <slot></slot>
        </div>
    </div>
</template>

<script setup lang="ts">
import { getRowColClasses } from '@/ts/rowcols';
import { computed } from 'vue';

const props = defineProps({
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

const vh = computed(() => 80 - props.offset);
</script>