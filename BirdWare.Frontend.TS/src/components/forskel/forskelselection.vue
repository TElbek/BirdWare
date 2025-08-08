<template>
    <div>
        <bs-show-lg>
            <bs-button-group>
                <bs-button :isOn="props.isByTrip" @click="switchIsByTrip">{{ tidCaption }}</bs-button>
                <bs-button :isOn="!props.isByTrip" @click="switchIsByTrip">{{ artCaption }}</bs-button>
            </bs-button-group>
            <bs-button-group class="ms-2">
                <bs-button :isOn="props.isThisYear" @click="switchIsThisYear">{{ thisYearCaption }}</bs-button>
                <bs-button :isOn="!props.isThisYear" @click="switchIsThisYear">{{ lastYearCaption }}</bs-button>
                <bs-button :isOn="false" :class="[forskel >= 0 ? 'forskel-success' : 'forskel-danger']">{{
                    Math.abs(forskel) }}</bs-button>
            </bs-button-group>
        </bs-show-lg>
        <bs-show-md>
            <bs-flex>
                <bs-button-dropdown :caption="tidArtCaption">
                    <ul class="dropdown-menu">
                        <a class="dropdown-item birdware" @click="switchIsByTrip">{{ tidCaption }}</a>
                        <a class="dropdown-item birdware" @click="switchIsByTrip">{{ artCaption }}</a>
                    </ul>
                </bs-button-dropdown>
                <bs-button-dropdown :caption="aarstalCaption">
                    <ul class="dropdown-menu">
                        <a class="dropdown-item birdware" @click="switchIsThisYear">{{ thisYearCaption }}</a>
                        <a class="dropdown-item birdware" @click="switchIsThisYear">{{ lastYearCaption }}</a>
                    </ul>
                </bs-button-dropdown>
                <bs-button>
                    <div class="forskel-indikator" :class="[forskel >= 0 ? 'forskel-success' : 'forskel-danger']">
                        {{ Math.abs(forskel) }}</div>
                </bs-button>
            </bs-flex>
        </bs-show-md>
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

const tidCaption = "Tid & Sted";
const artCaption = "Arter";

const tidArtCaption = computed(() => {
    return props.isByTrip ? tidCaption : artCaption;
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