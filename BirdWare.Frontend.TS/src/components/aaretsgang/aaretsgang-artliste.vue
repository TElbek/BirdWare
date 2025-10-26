<template>
    <show-more-ui 
        :too-many-items="showMoreHandler.tooManyItems.value" 
        :is-showing-all-items="showMoreHandler.isShowingAllItems.value" 
        @show-more="showMoreHandler.toggleShowMore()"/>
        
    <template v-for="item in props.value.slice(0,showMoreHandler.takeCount.value)" :key="item.artId">
        <art-navn :art-navn="item.artNavn" :art-id="item.artId" :speciel="item.speciel" :su="item.su"></art-navn>
    </template>
</template>

<script setup lang="ts">
import type { aaretsGangType } from '@/types/aaretsGangType';
import { useShowMoreHandler } from '@/composables/show-more-handler';
import { watch } from 'vue';

interface aaretsgangArtlisteProps {
    value: aaretsGangType[];
}

const props = defineProps<aaretsgangArtlisteProps>();
const showMoreHandler = useShowMoreHandler(props.value.length);

watch(() => props.value.length, (newLength) => {
    showMoreHandler.updateItemCount(newLength);
});
</script>