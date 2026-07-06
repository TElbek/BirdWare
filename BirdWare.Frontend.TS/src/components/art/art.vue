<template>
    <div class="flex flex-col gap-y-2">
        <div class="flex flex-row justify-between">
            <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
            <tw-text-sizeable>{{ artSelectionStore.antalArter }} arter</tw-text-sizeable>
        </div>
        <art-selection />
        <art-list></art-list>
    </div>
</template>

<script setup lang="ts">
import artSelection from '@/components/art/art-selection.vue';
import artList from '@/components/art/art-list.vue';

import { useArtSelectionStore } from '@/stores/art-selection-store';
const artSelectionStore = useArtSelectionStore();

import { useRoute } from 'vue-router';
const route = useRoute();
</script>