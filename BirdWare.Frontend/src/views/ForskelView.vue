<template>
    <div class="row">
        <div class="col-12 col-sm-6">
            <forskelAar :lastYear="false" :isByTrip="state.isByTrip">
                <div class="btn-group mb-2">
                    <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-on' : 'btn-off']" @click="switchIsByTrip">Tid & Sted</div>
                    <div class="btn btn-sm" :class="[state.isByTrip ? 'btn-off' : 'btn-on']" @click="switchIsByTrip">Arter</div>
                </div>                
            </forskelAar>
        </div>
        <div class="col-12 col-sm-6">
            <forskelAar :lastYear="true" :isByTrip="state.isByTrip">
            </forskelAar>
        </div>
    </div>
</template>

<script setup>
import forskelAar from '@/components/forskel/forskelaar.vue';
import { reactive } from 'vue';

const state = reactive({
    isByTrip: true,
});

function switchIsByTrip() {
    state.isByTrip = !state.isByTrip;
}
</script>