<template>
    <div class="mt-1">
        <multiselect v-model="state.selectedTag" :options="state.tagList" track-by="name" label="name"
        :multiple="false" :showLabels="false" :allow-empty="true" @search-change="getTagList"></multiselect>
    </div>
    <div class="row row-cols-12 row-cols-md-2 row-cols-lg-3 row-cols-xl-6 g-2 mt-1">
        <div v-for=" [key, value] in groupByFamilie">
            <div class="card h-100">
                <div class="card-header birdware ">
                    {{ key }}
                </div>
                <div class="card-body">
                    <div class="d-flex flex-wrap">
                        <template v-for="obs in value">
                            <a @click="removeObservation(obs.observationId)">
                                <span class="me-2">{{ obs.artNavn }}</span>
                            </a>
                        </template>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted, watch, computed } from 'vue';
import api from '@/api';
import { Multiselect } from 'vue-multiselect';
import type { opretObsListeType } from '@/types/opretObsListeType';
import type { tagType } from '@/types/tagType';

interface opretObsListeStateInterface {
    observationer: opretObsListeType[],
    tagList: tagType[],
    selectedTag: tagType,
}

const state = reactive<opretObsListeStateInterface>({
    observationer: [],
    tagList: [],
    selectedTag: {
        tagType: 0,
        id: 0,
        parentId: 0,
        name: ''
    },
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
        .then((response) => {
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