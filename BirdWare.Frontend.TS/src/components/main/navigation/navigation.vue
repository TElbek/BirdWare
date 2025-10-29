<template>
    <tw-show-md>
        <navigation-sm />
    </tw-show-md>
    <tw-show-lg>
        <navigation-lg />
    </tw-show-lg>
</template>

<script setup lang="ts">
import NavigationSm from '@/components/main/navigation/navigation-sm.vue';
import NavigationLg from '@/components/main/navigation/navigation-lg.vue';
</script>