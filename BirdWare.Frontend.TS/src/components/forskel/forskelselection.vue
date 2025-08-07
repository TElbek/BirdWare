<template>
    <div>
        <div class="d-none d-lg-block">
            <bs-button-group>
                <bs-button :isOn="props.isByTrip" @click="switchIsByTrip">{{ tidCaption }}</bs-button>
                <bs-button :isOn="!props.isByTrip" @click="switchIsByTrip">{{ artCaption }}</bs-button>
            </bs-button-group> 
            <bs-button-group class="ms-2">
                <bs-button :isOn="props.isThisYear" @click="switchIsThisYear">{{ thisYearCaption }}</bs-button>
                <bs-button :isOn="!props.isThisYear" @click="switchIsThisYear">{{ lastYearCaption }}</bs-button>
                <bs-button :isOn="false" :class="[forskel >= 0 ? 'forskel-success' : 'forskel-danger']">{{ Math.abs(forskel) }}</bs-button>
            </bs-button-group>
        </div>
        <div class="d-lg-none d-flex gap-2">
            <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
                aria-expanded="false">
                <span>{{ tidArtCaption }}</span>
            </button>
            <ul class="dropdown-menu">
                <a class="dropdown-item birdware" @click="switchIsByTrip">{{ tidCaption }}</a>
                <a class="dropdown-item birdware" @click="switchIsByTrip">{{ artCaption }}</a>
            </ul>
            <button class="btn btn-outline-birdware btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
                aria-expanded="false">
                <span>{{ aarstalCaption }}</span>
            </button>
            <ul class="dropdown-menu">
                <a class="dropdown-item birdware" @click="switchIsThisYear">{{ thisYearCaption }}</a>
                <a class="dropdown-item birdware" @click="switchIsThisYear">{{ lastYearCaption }}</a>
            </ul>
                <div class="btn btn-sm btn-off">
                    <div class="forskel-indikator" :class="[forskel >= 0 ? 'forskel-success' : 'forskel-danger']">
                        {{ Math.abs(forskel) }}</div>
                </div>

        </div>
    </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

interface ForskelSelectionProps {
    itemCountThisYear: number;
    itemCountLastYear: number;
    forskel: number;
    isByTrip: boolean;
    isThisYear: boolean;
}

const aarstalCaption = computed(() => {
    return props.isThisYear ? thisYearCaption.value : lastYearCaption.value;
});

const tidArtCaption = computed(() => {
    return props.isByTrip ? tidCaption.value : artCaption.value;
});

const tidCaption = computed(() => {
    return "Tid & Sted";
});

const artCaption = computed(() => {
    return "Arter";
});

const thisYearCaption = computed(() => {
    return new Date().getFullYear() + ": " + props.itemCountThisYear;
});

const lastYearCaption = computed(() => {
    return new Date().getFullYear() - 1 + ": " + props.itemCountLastYear;
});

const props = defineProps<ForskelSelectionProps>();
const emit = defineEmits(['switch-is-by-trip', 'switch-is-this-year']);

function switchIsByTrip() {
    emit('switch-is-by-trip');
};

function switchIsThisYear() {
    emit('switch-is-this-year');
}
</script>