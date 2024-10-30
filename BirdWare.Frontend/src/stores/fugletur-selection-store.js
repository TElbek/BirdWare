import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useFugleturSelectionStore = defineStore('fugletur-selection', () => {
  const selectedTags = ref([]);

  function AddTag(value) {
    if(!IsTagKnown(value)) {
      selectedTags.value = [...selectedTags.value,value];  
    }
    else {
      RemoveTag(value);
    }
  }

  function SetTag(value) {
    selectedTags.value = [];
    AddTag(value);
  }

  function RemoveTag(value) {
    if (IsTagKnown(value)) {
      selectedTags.value.splice(selectedTags.value.indexOf(FindTag(value)), 1);
      selectedTags.value = [...selectedTags.value];  
    }
  }

  function FindTag(tag) {
    if(IsTagKnown(tag)) {
      return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType)[0];
    }
  }

  function IsTagKnown(tag) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType).length > 0;
  }

  return { 
           selectedTags,            
           AddTag, 
           SetTag, 
           RemoveTag}
})