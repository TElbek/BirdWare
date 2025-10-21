<template>
    <button v-if="tooManyItems || props.isShowingAllItems" @click="emitShowMore()" class="text-birdware dark:text-birdware-bright">
        <span v-if="props.tooManyItems">&#x25BC;</span>
        <span v-else-if="props.isShowingAllItems">&#x25B2;</span>
    </button>
</template>

<script setup lang="ts">
interface seMereProps {
    tooManyItems: boolean;
    isShowingAllItems: boolean;
}

const props = defineProps<seMereProps>();

const emit = defineEmits<{
    (e: 'show-more'): void;
}>();

function emitShowMore() {
    emit('show-more');
}
</script>