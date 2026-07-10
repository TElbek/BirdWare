import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type tagType } from '@/types/tagType.ts'

export const useArtSelectionStore = defineStore('art-selection', () => {
  const selectedTags = ref([] as tagType[]);
  const antalArter = ref(0);

  function setAntalArter(antal: number) {
    antalArter.value = antal;
  }

  function addTag(value: tagType) {
    if (!isTagKnown(value)) {
      selectedTags.value = [...selectedTags.value, value];
    }
    else {
      removeTag(value);
    }
  }

  function setTag(value: tagType) {
    selectedTags.value = [];
    addTag(value);
  }

  function removeTag(value: tagType) {
    if (isTagKnown(value)) {
      selectedTags.value.splice(selectedTags.value.indexOf(findTag(value)), 1);
      selectedTags.value = [...selectedTags.value];
    }
  }

  function findTag(tag: tagType) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType)[0];
  }

  function isTagKnown(tag: tagType) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType).length > 0;
  }

  return {
    selectedTags,
    antalArter,
    setAntalArter,
    addTag,
    setTag,
    removeTag,
  }
},
  {
    persist: true,
  })