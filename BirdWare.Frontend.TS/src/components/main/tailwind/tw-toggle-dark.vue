<template>
    <button @click="toggleDark()" class="cursor-pointer">
        <span class="nav-item text-birdware dark:text-birdware-bright">
            {{ isDark ? '&#x263D Mørk' : '&#x263C Lys' }}
        </span>
    </button>
</template>

<script setup lang="ts">
import { useDark, useToggle } from '@vueuse/core'

const isDark = useDark();
const toggleDark = useToggle(isDark);
</script>
