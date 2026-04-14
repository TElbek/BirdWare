<template>
    <div class="flex flex-col gap-y-2">
        <div class="grid grid-cols-[1fr_max-content]">
            <fugletur-titel :fugletur-id="fugleturStore.chosenFugleturId"></fugletur-titel>
            <fugletur-navigation></fugletur-navigation>
        </div>

        <fugletur-obs-liste></fugletur-obs-liste>
    </div>
</template>

<script setup lang="ts">
import fugleturTitel from './fugletur-titel.vue';
import fugleturObsListe from './fugletur-obs-liste.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import { useFugleturStore } from '@/stores/fugletur-store';
const fugleturStore = useFugleturStore();
</script>