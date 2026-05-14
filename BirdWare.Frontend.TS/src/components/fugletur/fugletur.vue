<template>
    <div class="flex flex-col gap-y-2">
        <div class="flex justify-between">
            <fugletur-titel :fugletur-id="fugleturStore.chosenFugleturId"></fugletur-titel>
            <fugletur-navigation></fugletur-navigation>
        </div>
        <router-view>
        </router-view>
    </div>
</template>

<script setup lang="ts">
import { useFugleturStore } from '@/stores/fugletur-store.ts';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import fugleturNavigation from '@/components/fugletur/fugletur-navigation.vue';

const fugleturStore = useFugleturStore();
const router = useRouter();

onMounted(() => {
    fugleturStore.hasId ?
        navigateFugleturObservationer() :
        navigateFugleturOversigt();
});

function navigateFugleturObservationer() {
    router.replace({ name: 'fugletur-obs' });
}

function navigateFugleturOversigt() {
    router.replace({ name: 'fugletur-oversigt' });
}
</script>