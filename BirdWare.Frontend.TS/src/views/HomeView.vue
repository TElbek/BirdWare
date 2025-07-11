<template>
    <img :src="requireImg('BirdWare_logo_farve_RGB.svg')" />
</template>

<script setup>
function requireImg(imgName) {
    return new URL(`../assets/${imgName}`, import.meta.url).href
}
</script>

<style scoped>
img {
  position: absolute;
  margin: auto;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}
</style>

