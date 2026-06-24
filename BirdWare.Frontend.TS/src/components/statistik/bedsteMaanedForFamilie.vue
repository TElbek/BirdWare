<template>
    <div class="flex flex-col gap-y-2">
        <div class="flex justify-between">
            <tw-text-sizeable>{{ route.meta.title }}</tw-text-sizeable>
        </div>
        <tw-grid-cols-five :count="state.liste.length">
            <div v-for="[key, value] in byMaaned">
                <tw-card>
                    <tw-card-header :caption="getNameOfMonth(key)" :show-count="false"></tw-card-header>
                    <tw-flex>
                        <template v-for="familie in sortValues(value)">
                            <span>{{ familie.familieNavn }}</span>
                        </template>
                    </tw-flex>
                </tw-card>
            </div>
        </tw-grid-cols-five>
    </div>
</template>

<script setup lang="ts">
import api from '@/api';
import type { bedsteMaanedForFamilieType } from '@/types/bedsteMaanedForFamilieType';
import { computed, onMounted, reactive } from 'vue';
import { useRoute } from 'vue-router';
import {getNameOfMonth} from '@/ts/dateandtime'

const route = useRoute();

const state = reactive({
    liste: [] as bedsteMaanedForFamilieType[]
});

onMounted(() => {
    getBedsteMaanedForFamilie();
});

const byMaaned = computed(() => {
    return Map.groupBy(state.liste.sort((a, b) => a.maaned - b.maaned), (one: bedsteMaanedForFamilieType) => one.maaned);
});

function getBedsteMaanedForFamilie() {
    api.get('bedstemaanedforfamilie').then(res => {
        state.liste = res.data;
    });
}

function sortValues(value: bedsteMaanedForFamilieType[]) {
    return value.sort((a,b) => a.familieNavn.localeCompare(b.familieNavn));
}
</script>