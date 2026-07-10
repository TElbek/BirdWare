<template>
    <multiselect v-model="artSelectionStore.selectedTags" :options="state.tagList" track-by="name" label="name"
        :multiple="true" :showLabels="false" :allow-empty="true" @search-change="getTagList" placeholder="Vælg..."
        group-values="tags" group-label="name" :group-select="false">
    </multiselect>
</template>

<script setup lang="ts">
import api from '@/api';
import { onMounted, reactive } from 'vue';

import { Multiselect } from 'vue-multiselect';
import { useArtSelectionStore } from '@/stores/art-selection-store';
import type { tagGroupType } from '@/types/tagGroupType';

const emit = defineEmits(['search'])
const artSelectionStore = useArtSelectionStore();

const state = reactive({
    tagList: [] as tagGroupType[]
});

onMounted(() => {
    if (artSelectionStore.selectedTags.length == 0) {
        api.get("tag/" + new Date().getFullYear()).then(response => {
            artSelectionStore.addTag(response.data);
        });
    }
});

function getTagList(query: string) {
    if ((query != undefined) && (query.length >= 3)) {
        api.get('tags/arter?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}
</script>