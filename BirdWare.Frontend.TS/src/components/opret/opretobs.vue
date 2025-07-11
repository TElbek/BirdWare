<template>
    <div>
        <div class="row">
            <fugleturTitel :showTitle="false" class="col" :fugleturId="state.fugleturId"></fugleturTitel>
            <div class="col-auto">
                <button class="btn btn-sm" @click="setShowForslag"
                    :class="state.isForslagMode ? 'btn-birdware' : 'btn-outline-birdware'">
                    <span v-if="state.isForslagMode">Forslag</span>
                    <span v-else>Liste</span>
                </button>
            </div>
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

interface opretObsStateInterface {
    fugleturId: number,
    isForslagMode: boolean
}

const state = reactive<opretObsStateInterface>({
    fugleturId: 0,
    isForslagMode: false
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