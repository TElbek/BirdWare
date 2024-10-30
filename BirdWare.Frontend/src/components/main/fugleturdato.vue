<template>
    <a @click="navigateToFugletur">
        <span class="fw-small-bold">{{ formatDate(props.dato) }}</span>
    </a>
</template>

<script setup>
import { formatDate } from '@/js/dateandtime';
import { useFugleturStore } from '@/stores/fugletur-store';
import { useRouter } from 'vue-router';

const fugleturStore = useFugleturStore();
const router = useRouter();

const props = defineProps({
    fugleturId: 0,
    dato: undefined
});

function navigateToFugletur() {
    fugleturStore.setFugleturId(props.fugleturId);
    router.push({path: '/fugletur/observationer'});
}
</script>