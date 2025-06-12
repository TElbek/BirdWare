<template>
    <div class="col">
        <div class="card h-100 w-100">
            <div class="card-header text-nowrap">
                <div class="row">
                    <div class="col d-inline-block text-truncate">
                        <span class="birdware ">{{ props.analyseTypeTekst }}</span>
                    </div>
                    <div class="col-auto birdware ">
                        {{ props.analyseListe.length }}
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="art-flex ms-1">
                    <div v-for="art in arterSorteret(props.analyseListe)">
                        <artNavn :artId="art.artId" :artNavn="art.artNavn" :su="art.su" :speciel="art.speciel"></artNavn>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import artNavn from '@/components/main/artnavn.vue';

const props = defineProps({
    analyseListe: Array,
    analyseTypeTekst: ''
});

function arterSorteret(value) {
    return value.sort((a, b) => a.artNavn.localeCompare(b.artNavn));
}
</script>
