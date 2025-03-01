<template>
    <multiselect v-model="state.selectedTags" :options="state.tagList" track-by="name" label="name"
        :multiple="false" :showLabels="false" :allow-empty="true" @search-change="getTagList"></multiselect>
    <div class="row row-cols-12 row-cols-md-2 row-cols-lg-3 row-cols-xl-6 g-2 mt-2">
        <div v-for=" [key, value] in groupByFamilie">
            <div class="card">
                <div class="card-header birdware fw-semibold">
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

<script setup>
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';
import { Multiselect } from 'vue-multiselect';

const state = reactive({
    observationer: [],
    tagList: [],
    selectedTags: [],
    hasData: false
});

const groupByFamilie = computed(() => Map.groupBy(state.observationer, ({ familieNavn }) => familieNavn));

onMounted(() => {
    getObservationer();
});

function getObservationer() {
    api.get("fugletur/seneste/observationer")
        .then((response) => {
            state.observationer = response.data;
            state.hasData = true;
        });
}

function removeObservation(id) {
    api.post("fugletur/observation/" + id + "/slet")
        .then((response) => {
            getObservationer();
        })
}

function getTagList(query) {
    if ((query != undefined) & (query.length >= 3)) {
        api.get('tags/arter?query=' + query).then(response => {
            state.tagList = response.data;
        });
    }
}

watch(() => state.selectedTags, (newValue) => {
    api.post("observation/opretobs/" + newValue.id).then((response) => {
        getObservationer();            
    });
});
</script>