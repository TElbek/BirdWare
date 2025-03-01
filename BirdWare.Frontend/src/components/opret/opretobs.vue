<template>    
    <div>
        <div class="row">
            <fugleturTitel :showTitle="false" class="col" :fugleturId="state.fugleturId"></fugleturTitel>
            <div class="col-auto">
                <button class="btn btn-sm" @click="setShowForslag" :class="state.showForslag ? 'btn-birdware' : 'btn-outline-birdware'">
                    <span v-if="state.showForslag">Forslag</span>
                    <span v-else>Liste</span>
                </button>
            </div>
        </div>
        <RouterView></RouterView>
    </div>
</template>

<script setup>
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';
import { useRoute, useRouter }  from 'vue-router';

const router = useRouter();
const route = useRoute();

const state = reactive({
    fugleturId: 0,
    showForslag: false
});

onMounted(() => {
    state.showForslag = (route.name == 'addobs-forslag');
    getSenesteFugleturId();
});

function setShowForslag() {
    state.showForslag = !state.showForslag;
    router.replace({ name: (state.showForslag ? 'addobs-forslag' : 'addobs-liste') })
}

function getSenesteFugleturId() {
    api.get("fugletur/seneste/id")
        .then((response) => { state.fugleturId = response.data });
}
</script>

<style scoped>
button {
    width: 70px;
}
</style>