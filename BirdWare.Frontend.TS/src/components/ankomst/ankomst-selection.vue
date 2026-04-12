<template>
   <multiselect v-model="ankomstSelectionStore.selectedTag" :options="state.tag" track-by="name" label="name"
        :multiple="false" :showLabels="false" :allow-empty="true" @search-change="getTagList" placeholder="Vælg...">
    </multiselect>
</template>

<script setup lang="ts">
import api from '@/api';
import { useAnkomstSelectionStore } from '@/stores/ankomst-selection-store';
import type { tagType } from '@/types/tagType';
import { reactive } from 'vue';
import { Multiselect } from 'vue-multiselect';

const ankomstSelectionStore = useAnkomstSelectionStore();

const state = reactive({
    tag: [] as tagType[],
});

const getTagList = (query: string) => {
    if (query.length >= 3) {
      api.get(`tags/familie?query=${query}`).then((response) => {
          state.tag = response.data;
      });
    }
};
</script>