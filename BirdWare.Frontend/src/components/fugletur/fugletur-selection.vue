<template>
    <multiselect v-model="fugleturSelectionStore.selectedTags" :options="state.tagList" track-by="name" label="name" :multiple="true"
        :showLabels="false" :allow-empty="true" @search-change="getTagList"></multiselect>
</template>

<script setup>
import api from '@/api';
import { Multiselect } from 'vue-multiselect';
import { reactive, onMounted } from 'vue';
import { useFugleturSelectionStore } from '@/stores/fugletur-selection-store';

const fugleturSelectionStore = useFugleturSelectionStore();

const emit = defineEmits(['search']);
const state = reactive({
    tagList: []
});

function getTagList(query) {
    if ((query != undefined) & (query.length >= 3)) {
        api.get('tags/fugletur?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}
</script>