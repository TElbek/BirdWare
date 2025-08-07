<template>
    <div class="mt-1">
        <multiselect v-model="state.selectedTag" :options="state.tagList" track-by="name" label="name"
        :multiple="false" :showLabels="false" :allow-empty="true" @search-change="getTagList"></multiselect>
    </div>
    <bs-row-cols :count="groupByFamilie.size" class="mt-2">
        <div v-for=" [key, value] in groupByFamilie">
            <bs-card>
                <bs-card-header>
                    <span class="birdware">{{ key }}</span>
                </bs-card-header>
                <bs-card-body>
                    <bs-flex hasWrap=""true>
                        <template v-for="obs in value">
                            <a @click="removeObservation(obs.observationId)">
                                <span class="me-2">{{ obs.artNavn }}</span>
                            </a>
                        </template>
                    </bs-flex>
                </bs-card-body>
            </bs-card>
        </div>
    </bs-row-cols>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted, watch, computed } from 'vue';
import api from '@/api';
import { Multiselect } from 'vue-multiselect';
import { type opretObsListeType } from '@/types/opretObsListeType';
import { type tagType } from '@/types/tagType';

const state = reactive({
    observationer: [] as opretObsListeType[],
    tagList: [] as tagType[],
    selectedTag: {} as tagType,
});

const hasData = ref(false as boolean);
const groupByFamilie = computed(() => Map.groupBy(state.observationer, ({ familieNavn }) => familieNavn));

onMounted(() => {
    getObservationer();
});

function getObservationer() {
    api.get("fugletur/seneste/observationer")
        .then((response) => {
            state.observationer = response.data;
            hasData.value = true;
        });
}

function removeObservation(id: number) {
    api.post("fugletur/observation/" + id + "/slet")
        .then(() => {
            getObservationer();
        })
}

function getTagList(query: string) {
    if ((query != undefined) && (query.length >= 3)) {
        api.get('tags/arter?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}

watch(() => state.selectedTag, (newValue) => {
    api.post("observation/opretobs/" + newValue.id).then(() => {
        getObservationer();            
    });
});
</script>