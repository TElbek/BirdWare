import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import { type tagType } from '@/types/tagType.ts'

export const useObsSelectionStore = defineStore('obs-selection', () => {
  const selectedTags = ref([] as tagType[]);
  const chosenGroupingId = ref(0);
  const chosenViewMode = ref(0);
  const isGropingByMonth = computed(() => chosenGroupingId.value == 1);
  const isGropingByLocality = computed(() => chosenGroupingId.value == 3);
  const isGropingByNumber = computed(() => chosenGroupingId.value == 0 || chosenGroupingId.value == 1);
  const isGropingByText = computed(() => chosenGroupingId.value == 2 || chosenGroupingId.value == 3 || chosenGroupingId.value == 4);
  
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

  function setGroupingId(value: number) {
    chosenGroupingId.value = value;
  }

  function setViewMode() {
    chosenViewMode.value = (chosenViewMode.value == 0 ? 1 : 0);
  }

  function findTag(tag: tagType) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType)[0];
  }

  function isTagKnown(tag: tagType) {
    return selectedTags.value.filter((item) => item.id == tag.id && tag.tagType == item.tagType).length > 0;
  }

  function hasTagWithName(name:string) {
    return selectedTags.value.some((item) => item.name == name);
  }

  return {
    chosenGroupingId,
    chosenViewMode,
    selectedTags,
    isGropingByMonth,
    isGropingByLocality,
    isGropingByNumber,
    isGropingByText,
    AddTag: addTag,
    SetTag: setTag,
    RemoveTag: removeTag,
    SetGroupingId: setGroupingId,
    SetViewMode: setViewMode,
    hasTagWithName
  }
})