<template>
    <multiselect v-model="obsSelectionStore.selectedTags" :options="state.tagList" track-by="name" label="name" :multiple="true"
        :showLabels="false" :allow-empty="true" @search-change="getTagList"></multiselect>
</template>

<script setup>
import { Multiselect } from 'vue-multiselect';
import { reactive } from 'vue';
import { useRoute } from 'vue-router';

import { useObsSelectionStore } from '@/stores/obs-selection-store';
import api from '@/api';

const emit = defineEmits(['search'])
const route = useRoute();
const obsSelectionStore = useObsSelectionStore();

const state = reactive({
    tagList: []
});

function getTagList(query) {
    if ((query != undefined) & (query.length >= 3)) {
        api.get('tags?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}
</script>