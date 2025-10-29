<template>
    <div class="grid grid-cols-[1fr_max-content]">
        <tw-text-sizeable>Tilføj Tur</tw-text-sizeable>
        <input class="p-2 border border-gray-200 rounded dark:text-white" type="search" placeholder="Søg..." v-model="state.searchValue" />
    </div>
    <tw-grid-cols-five :count="byDistance.size" class="mt-4 max-h-160 xl:max-h-180 overflow-auto">
        <div v-for="[key, value] in byDistance">
            <tw-card>
                <tw-card-header :caption="key + ' km.'" :show-count="false"></tw-card-header>
                <tw-flex>
                    <a v-for="lokalitet in value" @click="opretTur(lokalitet.id)" >
                        <span class="dark:text-white">{{ lokalitet.navn }}</span>
                    </a>
                </tw-flex>
            </tw-card>
        </div>
    </tw-grid-cols-five>
</template>

<script setup lang="ts">
import { reactive, onMounted, watch, computed } from 'vue';
import api from '@/api';
import { useRouter } from 'vue-router';
import { type lokalitetDistanceType } from '@/types/lokalitetDistanceType';

const router = useRouter();

const state = reactive({
    lokalitetListe: [] as lokalitetDistanceType[],
    position: {} as GeolocationPosition,
    searchValue: '' as string
});

onMounted(() => {
    getLocation();
});

const byDistance = computed(() => {
    return Map.groupBy(filteredLokalitetListe.value, ({ distance }) => distance);
});

const filteredLokalitetListe = computed(() => {
    return state.searchValue.length > 0 ?
        state.lokalitetListe.filter((lok) => lok.navn.toLowerCase().indexOf(state.searchValue.toLowerCase()) > -1) :
        state.lokalitetListe;
});

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setPosition);
    }
}

function setPosition(position: GeolocationPosition) {
    state.position = position;
}

function getLokaliteterForPosition() {
    api.get("lokalitet/" + state.position.coords.latitude + '/' + state.position.coords.longitude)
        .then((response) => { state.lokalitetListe = response.data; });
}

function opretTur(lokalitetId: number) {
    api.post("fugletur/oprettur/" + lokalitetId)
        .then(() => {
            router.push({ name: 'addobs' });
        });
}

watch(() => state.position, (newValue) => {
    getLokaliteterForPosition();
});
</script>