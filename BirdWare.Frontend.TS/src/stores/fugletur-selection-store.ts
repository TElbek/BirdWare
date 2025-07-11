import { ref } from 'vue'
import { defineStore } from 'pinia'
import type { tagType } from '@/types/tagType';

export const useFugleturSelectionStore = defineStore('fugletur-selection', () => {
  const selectedTags = ref([] as tagType[]);

  function AddTag(value: tagType) {
    if(!IsTagKnown(value)) {
      selectedTags.value = [...selectedTags.value,value];  
    }
    else {
      RemoveTag(value);
    }
  }

  function SetTag(value: tagType) {
    selectedTags.value = [];
    AddTag(value);
  }

  function RemoveTag(value: tagType) {
    if (IsTagKnown(value)) {
      selectedTags.value.splice(selectedTags.value.indexOf(findTag(value)), 1);
      selectedTags.value = [...selectedTags.value];  
    }
  }

  function findTag(tag: tagType) {
      return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType)[0];
  }

  function IsTagKnown(tag: tagType) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType).length > 0;
  }

  return { 
           selectedTags,            
           AddTag, 
           SetTag, 
           RemoveTag}
})