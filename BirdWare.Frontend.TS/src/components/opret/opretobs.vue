<template>
    <div>
        <div class="grid grid-cols-[1fr_max-content] mb-2">
            <fugletur-titel :showTitle="false" :fugleturId="state.fugleturId"></fugletur-titel>
            <tw-button-dropdown :caption="state.isForslagMode ? 'Forslag' : 'Liste'">
                <button class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright" @click="setShowForslag">Liste</button>
                <button class="block px-4 py-1 text-sm cursor-pointer dark:text-birdware-bright" @click="setShowForslag">Forslag</button>
            </tw-button-dropdown>
        </div>
        <RouterView></RouterView>
    </div>
</template>

<script setup lang="ts">
import { reactive, onMounted } from 'vue';
import api from '@/api';
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import { useRoute, useRouter } from 'vue-router';

const router = useRouter();
const route = useRoute();

const state = reactive({
    fugleturId: 0 as number,
    isForslagMode: false as boolean
});

onMounted(() => {
    state.isForslagMode = (route.name == 'addobs-forslag');
    getSenesteFugleturId();
});

function setShowForslag() {
    state.isForslagMode = !state.isForslagMode;
    router.replace({ name: (state.isForslagMode ? 'addobs-forslag' : 'addobs-liste') })
}

function getSenesteFugleturId() {
    api.get("fugletur/seneste/id")
        .then((response) => { state.fugleturId = response.data });
}
</script>