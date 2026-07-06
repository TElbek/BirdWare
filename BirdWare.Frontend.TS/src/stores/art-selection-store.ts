import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type tagType } from '@/types/tagType.ts'

export const useArtSelectionStore = defineStore('art-selection', () => {
  const selectedTags = ref([] as tagType[]);
  const antalArter = ref(0);

  function setAntalArter(antal: number) {
    antalArter.value = antal;
  }

  return {
    selectedTags,
    antalArter,
    setAntalArter
  }
},
  {
    persist: true,
  })