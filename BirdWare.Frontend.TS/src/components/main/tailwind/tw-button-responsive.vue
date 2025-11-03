<template>
    <tw-show-lg>
        <tw-button-group class="flex gap-4 px-2 py-1">
            <slot></slot>
        </tw-button-group>
    </tw-show-lg>
    <tw-show-md>
        <tw-button-dropdown :caption="props.caption">
            <slot></slot>
        </tw-button-dropdown>
    </tw-show-md>
</template>

<script setup lang="ts">
interface twButtonResponsiveProps {
    caption: string;
}

const props = defineProps<twButtonResponsiveProps>();
</script>