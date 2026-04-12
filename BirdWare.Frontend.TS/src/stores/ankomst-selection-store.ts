import { computed, ref } from 'vue'
import { defineStore } from 'pinia'
import type { tagType } from '@/types/tagType';

export const useAnkomstSelectionStore = defineStore('ankomst-selection', () => {
  const selectedTag = ref(null as tagType | null);

  function SetTag(value: tagType) {
    selectedTag.value = value;
  }

  const hasSelectedTag = computed(() => selectedTag.value !== null);

  return {
    selectedTag,
    SetTag,
    hasSelectedTag
  };
});