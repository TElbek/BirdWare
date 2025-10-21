<template>
    <div class="grid grid-cols-[auto_110px]">
        <fugletur-titel class="relative top-2" :fugletur-id="fugleturStore.chosenFugleturId"></fugletur-titel>
        <fugletur-navigation></fugletur-navigation>        
    </div>
    <fugletur-obs-liste  class="max-h-160 xl:max-h-180 overflow-auto mt-3 mb-2"></fugletur-obs-liste>
</template> 

<script setup lang="ts">
import fugleturTitel from './fugletur-titel.vue';
import fugleturObsListe from './fugletur-obs-liste.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import { useFugleturStore } from '@/stores/fugletur-store';
const fugleturStore = useFugleturStore();
</script>