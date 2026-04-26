import { ref, onMounted, onUnmounted } from 'vue';
 
export function useWindowSize() {
  const windowWidth = ref(window.innerWidth);
  const windowHeight = ref(window.innerHeight);
  const tailwindBreakpointLG = 1024;
 
  const handleResize = () => {
    windowWidth.value = window.innerWidth;
    windowHeight.value = window.innerHeight;
    isMobile.value = windowWidth.value <= tailwindBreakpointLG;
  };
 
  const isMobile = ref(false);

  onMounted(() => {
    handleResize();
    window.addEventListener('resize', handleResize);
  });
 
  onUnmounted(() => {
    window.removeEventListener('resize', handleResize);
  });
 
  return { windowWidth, windowHeight, isMobile };
}