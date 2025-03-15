<template>
    <div class="birdware large-text ">
        <div class="row">
            <div class="col-12 col-lg-auto" v-if="props.showTitle">{{ route.meta.title }}</div>
            <div class="col-12 col-lg-auto text-nowrap" v-if="state.fugletur != undefined">
                {{ formatDate(state.fugletur.dato) }} - {{state.fugletur.lokalitetNavn }}
            </div>
        </div>
    </div>
</template>

<script setup>
import api from '@/api';
import { formatDate } from '@/js/dateandtime';
import { onMounted, watch, reactive } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const props = defineProps({
    fugleturId: 0,
    showTitle: {default: true}
});

const state = reactive({
    fugletur: undefined,
});

onMounted(() => {
    if(props.fugleturId > 0 ) {
        getFugletur();
    }
});

function getFugletur() {
    api.get('fugletur/' + props.fugleturId).then((response => {
        state.fugletur = response.data;
    }));
}

watch(() => props.fugleturId, (newValue) => {
    if(newValue != 0) {
        getFugletur();
    }
});
</script>

<style scoped>
.row > * {
	padding-right: 0px;
    
}</style>