<template>
    <div class="relative mt-2">
        <button @click="switchShow" class="p-1 rounded-md border-gray-300 border dark:border-birdware-bright grid grid-cols-[80px_auto]">
            <div class="mr-1 ms-1 text-nowrap text-start text-birdware dark:text-birdware-bright">{{ props.caption }}</div>
            <svg class="w-5 h-5 text-birdware" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd"
                    d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                    clip-rule="evenodd" />
            </svg>
        </button>
        <div v-show="state.show" class="absolute right-0 py-2 mt-1 border border-gray-200 dark:border dark:border-birdware rounded-md shadow-xl w-35 bg-white dark:bg-gray-900 text-birdware" @click="switchShow">
            <slot></slot>
        </div>
    </div>
</template>

<script setup lang="ts">
import { reactive } from 'vue';

interface ForskelSelectionProps {
    caption: string;
}

const props = defineProps<ForskelSelectionProps>();

const state = reactive({
    show: false as boolean
});

function switchShow() {
    state.show = !state.show;
}
</script>