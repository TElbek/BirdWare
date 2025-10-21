import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useThemeStore = defineStore('themeStore', () => {
  const isDarkMode = ref(false)
  function toggleDarkMode() {
    isDarkMode.value = !isDarkMode.value
  }

  return { isDarkMode, toggleDarkMode }
})
