<template>
    <div>
        <button @click="switchShow" class="w-27 ps-2 pe-1 rounded-sm border-gray-300 border dark:border-birdware-bright grid grid-cols-[max-content_1fr]">
            <svg class="w-5 h-5 text-birdware" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd"
                    d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                    clip-rule="evenodd" />
            </svg>
            <div class="mr-1 ms-1 text-nowrap text-start text-birdware dark:text-birdware-bright">{{ props.caption }}</div>
        </button>
        <div v-show="state.show" class="z-100 absolute right-1 py-2 mt-1 border border-gray-200 dark:border dark:border-birdware rounded-md shadow-xl w-35 bg-white dark:bg-gray-900 text-birdware" @click="switchShow">
            <div class="grid grid-cols-1 gap-3 mx-2">
                <slot></slot>
            </div>
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