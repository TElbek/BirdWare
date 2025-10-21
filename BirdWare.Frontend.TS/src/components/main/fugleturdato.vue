<template>
    <a @click="navigateToFugletur" class="cursor-pointer">
        <span class="fw-small-bold">{{ formatDate(props.dato) }}</span>
    </a>
</template>

<script setup lang="ts">
import { formatDate } from '@/ts/dateandtime';
import { useFugleturStore } from '@/stores/fugletur-store';
import { useRouter } from 'vue-router';

const fugleturStore = useFugleturStore();
const router = useRouter();

interface fugleturDatoProps {
    fugleturId: number,
    dato: string
}

const props = defineProps<fugleturDatoProps>();

function navigateToFugletur() {
    fugleturStore.setFugleturId(props.fugleturId);
    router.push({path: '/fugletur/observationer'});
}
</script>