<template>
    <router-link :to="`/art/${props.artId}/observation`">
        <span :class="[props.su ? 'text-danger fw-bold' : '']" class="text-nowrap">{{ props.artNavn }}</span>
    </router-link>
</template>

<script setup>
const props = defineProps({
    artId: 0,
    artNavn: '',
    su: false
});
</script>