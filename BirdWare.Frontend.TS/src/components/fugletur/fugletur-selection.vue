<template>
    <multiselect v-model="fugleturSelectionStore.selectedTags" :options="state.tagList" track-by="name" label="name" :multiple="true"
        :showLabels="false" :allow-empty="true" @search-change="getTagList"></multiselect>
</template>

<script setup lang="ts">
import api from '@/api';
import { Multiselect } from 'vue-multiselect';
import { reactive } from 'vue';
import { useFugleturSelectionStore } from '@/stores/fugletur-selection-store.ts';
import { type tagType } from '@/types/tagType';

const fugleturSelectionStore = useFugleturSelectionStore();

const emit = defineEmits(['search']);
const state = reactive({
    tagList: [] as tagType[]
});

function getTagList(query: string) {
    if ((query != undefined) && (query.length >= 3)) {
        api.get('tags/fugletur?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}
</script>