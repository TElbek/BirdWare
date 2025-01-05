<template>    
    <div>
        <fugleturTitel :fugleturId="state.fugleturId"></fugleturTitel>
        <RouterView></RouterView>
    </div>
</template>

<script setup>
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';
import fugleturTitel from '@/components/fugletur/fugletur-titel.vue';

const state = reactive({
    fugleturId: 0
});

onMounted(() => {
    getSenesteFugleturId();
});

function getSenesteFugleturId() {
    api.get("fugletur/seneste/id")
        .then((response) => { state.fugleturId = response.data });
}
</script>