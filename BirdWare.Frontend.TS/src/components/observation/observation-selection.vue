<template>
    <multiselect class="z-100" v-model="obsSelectionStore.selectedTags" :options="state.tagList" track-by="name" label="name"
        :multiple="true" :showLabels="false" :allow-empty="true" @search-change="getTagList" placeholder="Vælg...">
    </multiselect>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive } from 'vue';
import { type tagType } from '@/types/tagType.ts';

import { Multiselect } from 'vue-multiselect';
import { useObsSelectionStore } from '@/stores/obs-selection-store';

const emit = defineEmits(['search'])
const obsSelectionStore = useObsSelectionStore();

const state = reactive({
    tagList: [] as tagType[]
});

function getTagList(query: string) {
    if ((query != undefined) && (query.length >= 3)) {
        api.get('tags?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}
</script>