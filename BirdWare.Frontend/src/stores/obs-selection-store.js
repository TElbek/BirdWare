import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useObsSelectionStore = defineStore('obs-selection', () => {
  const selectedTags = ref([]);
  const chosenGroupingId = ref(0);
  const chosenViewMode = ref(0);
  const isGropingByMonth = computed(() => chosenGroupingId.value == 1);
  const isGropingByLocality = computed(() => chosenGroupingId.value == 3);
  const isGropingByNumber = computed(() => chosenGroupingId.value == 0 || chosenGroupingId.value == 1);
  const isGropingByText = computed(() => chosenGroupingId.value == 2 || chosenGroupingId.value == 3 || chosenGroupingId.value == 4);
  const showSpeciesNameInList = computed(() => selectedTags.value.filter((tag) => tag.tagType == 10 ).length != 1);

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

  function SetGroupingId(value) {
    chosenGroupingId.value = value;
  }

  function SetViewMode() {
    chosenViewMode.value = (chosenViewMode.value == 0 ? 1 : 0);
  }

  function FindTag(tag) {
    if(IsTagKnown(tag)) {
      return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType)[0];
    }
  }

  function IsTagKnown(tag) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType).length > 0;
  }

  return { chosenGroupingId,
           chosenViewMode,
           selectedTags, 
           isGropingByMonth,
           isGropingByLocality,
           isGropingByNumber,
           isGropingByText,
           showSpeciesNameInList,
           AddTag, 
           SetTag, 
           RemoveTag, 
           SetGroupingId,
           SetViewMode }
})