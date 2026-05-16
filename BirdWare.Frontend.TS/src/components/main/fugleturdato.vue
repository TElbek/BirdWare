<template>
    <a @click="navigateToFugletur" class="cursor-pointer">
        <span :class="highlight ? 'text-birdware dark:text-birdware-bright' : 'text-black dark:text-white'"  >
            {{ formatDate(props.dato) }}
        </span>
    </a>
</template>

<script setup lang="ts">
import { formatDate } from '@/ts/dateandtime';
import { useFugleturStore } from '@/stores/fugletur-store';
import { useRouter } from 'vue-router';
import { useRouteLogic } from '@/composables/route-logic';

const { turRoute } = useRouteLogic();

const fugleturStore = useFugleturStore();
const router = useRouter();

interface fugleturDatoProps {
    fugleturId: number,
    dato: string,
    highlight?: boolean
}

const props = withDefaults(defineProps<fugleturDatoProps>(), {
    highlight: false
});

function navigateToFugletur() {
    fugleturStore.setFugleturId(props.fugleturId);
    router.push(turRoute!.path);
}
</script>