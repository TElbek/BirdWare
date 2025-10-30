<template>
    <div class="py-2">
        <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
        <tw-grid-cols-five :count="byLokalitet.size" class="mt-2 mb-3">
            <div v-for="[key, value] in byLokalitet">
                <tw-card>
                    <tw-card-header :caption="`${key} ${Math.round(value[0].distance)} km`" :count="value.length"
                        :show-count="true"></tw-card-header>
                    <tw-flex>
                        <template v-for="item in arterSorteret(value)">
                            <art-navn :art-navn="item.artNavn" :art-id="item.artId" :speciel="false"
                                :su="false"></art-navn>
                        </template>
                    </tw-flex>
                </tw-card>
            </div>
        </tw-grid-cols-five>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import { reactive, computed, onMounted } from 'vue';
import { type hvorKanJegFindeType } from '@/types/hvorKanJegFindeType';
import ArtNavn from '../main/artNavn.vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const state = reactive({
    hvorKanJegFinde: [] as hvorKanJegFindeType[]
});

onMounted(() => {
    getHvorKanJegFinde();
});

function getHvorKanJegFinde() {
    api.get('hvorkanjegfinde/').then(res => {
        state.hvorKanJegFinde = res.data;
    });
}

const byLokalitet = computed(() => {
    return Map.groupBy(state.hvorKanJegFinde, (one: hvorKanJegFindeType) => one.lokalitetNavn);
});

function arterSorteret(trip: hvorKanJegFindeType[]) {
    return trip.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>