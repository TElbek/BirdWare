import { ref, onMounted, onUnmounted, computed } from 'vue';
 
export function useWindowSize() {
  // Reactive size refs
  const windowWidth = ref(window.innerWidth);
  const windowHeight = ref(window.innerHeight);
 
  // Resize handler
  const handleResize = () => {
    windowWidth.value = window.innerWidth;
    windowHeight.value = window.innerHeight;
    isMobile.value = windowWidth.value <= 1024;
  };
 
  const isMobile = ref(false);

  // Lifecycle hooks
  onMounted(() => {
    handleResize();
    window.addEventListener('resize', handleResize);
  });
 
  onUnmounted(() => {
    window.removeEventListener('resize', handleResize);
  });
 
  // Return the reactive values
  return { windowWidth, windowHeight, isMobile };
}