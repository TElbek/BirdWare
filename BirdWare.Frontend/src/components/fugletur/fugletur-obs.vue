<template>    
    <div class="row">
        <fugleturTitel class="col" :fugleturId="fugleturStore.chosenFugleturId"></fugleturTitel>
        <div class="col-auto">
            <fugleturNavigation></fugleturNavigation>
        </div>
    </div>
    <div class="scroll mt-2">
        <fugleturObsListe :fugleturId="fugleturStore.chosenFugleturId"></fugleturObsListe>
    </div>
</template>

<script setup>
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';
import fugleturObsListe from '@/components/fugletur/fugletur-obs-liste.vue';
import { useFugleturStore } from '@/stores/fugletur-store';

const fugleturStore = useFugleturStore();
</script>