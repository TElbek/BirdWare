<template>
    <div class="d-flex gap-1" :class="{ 'flex-wrap': props.hasWrap }">
        <slot></slot>
    </div>
</template>

<script setup lang="ts">
interface bsflexProps {
    hasWrap?: boolean;
}

const props = defineProps<bsflexProps>();
</script>