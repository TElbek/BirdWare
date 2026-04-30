import * as L from 'leaflet';
import { shallowRef } from 'vue';
import { formatDate } from '@/ts/dateandtime';

import 'leaflet.markercluster/dist/MarkerCluster.css';
import 'leaflet.markercluster/dist/MarkerCluster.Default.css';
import "leaflet.markercluster";

import type { observationType } from '@/types/observationType';

const initialMap = shallowRef();
const markers = shallowRef<L.MarkerClusterGroup>(L.markerClusterGroup());

export function useObservationMapLogicCluster(emitTagCallback: any) {

    function initializeLeaflet() {
        initialMap.value = L.map('map');

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 14,
            minZoom: 6,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(initialMap.value);
    }

    function addMarkers(observationList: observationType[]) {
        cleanUpMarkersAndClusters();
        let averageCount = calculateAverageCount(observationList);

        for (const [lokalitetId, observationer] of groupObservationsByLokalitet(observationList)) {
            const firstObservation = observationer[0];
            if (firstObservation.latitude && firstObservation.longitude && firstObservation.regionId > 0) {
                const marker = L.circleMarker([firstObservation.latitude, firstObservation.longitude]);
                marker.setStyle({ color: observationer.length > averageCount ? 'green' : 'blue' });
                marker.on('click', function (ev) {
                        whenFeatureClicked(ev, firstObservation.lokalitetNavn);
                    });
                marker.bindTooltip(`<b>${firstObservation.lokalitetNavn}</b><br>Observationer: ${observationer.length}</br>Seneste: ${formatDate(firstObservation.dato)} `);
                markers.value.addLayer(marker);
            }
        }
        initialMap.value.addLayer(markers.value);
        fitBounds();
    }

    function cleanUpMarkersAndClusters() {
        initialMap.value.removeLayer(markers.value);
        markers.value = L.markerClusterGroup();
    }

    function fitBounds() {
        if (markers && initialMap.value) {
            let layerList: L.Layer[] = [markers.value];
            let featureGroup = new L.FeatureGroup<L.Layer>(layerList);
            let bounds = featureGroup.getBounds();
            try {
                initialMap.value.fitBounds(bounds, { padding: [120, 120] });
            }
            catch {
                initialMap.value.setView([56, 11], 7);
                return;
            }
        }
    }

    function groupObservationsByLokalitet(observationer: observationType[]) {
        const grouped = new Map<number, observationType[]>();
        for (const obs of observationer) {
            if (!grouped.has(obs.lokalitetId)) {
                grouped.set(obs.lokalitetId, []);
            }
            grouped.get(obs.lokalitetId)?.push(obs);
        }
        return grouped;
    }

    function calculateAverageCount(observationer: observationType[]) {
        const totalObservations = observationer.length;
        const uniqueLokalitetCount = new Set(observationer.map(obs => obs.lokalitetId)).size;
        return uniqueLokalitetCount > 0 ? (totalObservations / uniqueLokalitetCount) : 0.00;
    }

    function whenFeatureClicked(ev: L.LeafletMouseEvent, lokalitetNavn: string) {
        emitTagCallback(lokalitetNavn);
    }

    return {
        initializeLeaflet,
        addMarkers,
        fitBounds
    };
}

