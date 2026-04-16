<template>
    <div class="flex flex-col gap-y-2">
        <tw-show-lg>
            <div class="flex justify-between">
                <fugletur-titel :showTitle="false" :fugleturId="state.fugleturId"></fugletur-titel>
                <tw-button-group :caption="''" class="flex gap-x-5">
                    <tw-button :caption="'Liste'" :isSelected="!state.isForslagMode"
                        @click="setShowForslag"></tw-button>
                    <tw-button :caption="'Forslag'" :isSelected="state.isForslagMode"
                        @click="setShowForslag"></tw-button>
                </tw-button-group>
            </div>
        </tw-show-lg>
        <tw-show-md>
            <fugletur-titel :showTitle="false" :fugleturId="state.fugleturId"></fugletur-titel>
            <tw-bottom-nav-bar>
                <tw-button :caption="'Liste'" :isSelected="!state.isForslagMode" @click="setShowForslag"></tw-button>
                <tw-button :caption="'Forslag'" :isSelected="state.isForslagMode" @click="setShowForslag"></tw-button>
            </tw-bottom-nav-bar>
        </tw-show-md>
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