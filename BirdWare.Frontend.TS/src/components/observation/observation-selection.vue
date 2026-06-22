<template>
    <multiselect v-model="obsSelectionStore.selectedTags" :options="state.tagList" track-by="name" label="name"
        :multiple="true" :showLabels="false" :allow-empty="true" @search-change="getTagList" placeholder="Vælg..."
        group-values="tags" group-label="name" :group-select="false">
    </multiselect>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive } from 'vue';

import { Multiselect } from 'vue-multiselect';
import { useObsSelectionStore } from '@/stores/obs-selection-store';
import type { tagGroupType } from '@/types/tagGroupType';

const emit = defineEmits(['search'])
const obsSelectionStore = useObsSelectionStore();

const state = reactive({
    tagList: [] as tagGroupType[]
});

function getTagList(query: string) {
    if ((query != undefined) && (query.length >= 3)) {
        api.get('tags?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}
</script>