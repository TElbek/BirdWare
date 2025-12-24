import type { Feature } from 'geojson';
import type { observationGeoJson } from '@/types/observationGeoJsonType';
import * as L from 'leaflet';
import { ref, shallowRef } from 'vue';
import { formatDate } from '@/ts/dateandtime';

export function useObservationMapLogic(emitTagCallback: any) {
    const map = shallowRef();
    const observationLayer = ref({} as L.Layer);
    const hasLayer = ref(false);

    function initializeLeaflet() {
        map.value = L.map("map")
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 15,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map.value);
    }

    function addPointsToMap(geoJson: observationGeoJson) {
        if (map) {
            resetGeoJson();
            observationLayer.value = L.geoJSON(geoJson as any, {
                pointToLayer: function (feature: Feature, latlng: L.LatLng) {
                    let marker = L.circleMarker(latlng).addTo(map.value);
                    marker.setStyle({color: feature.properties?.countIsAboveAverage ? 'green' : 'blue'});
                    marker.on('click', function (ev) {
                        whenFeatureClicked(ev);
                    });
                    return marker;
                },
                onEachFeature: function (feature, layer) {
                    layer.bindTooltip('<strong>' + feature.properties?.name + '</strong>: ' + feature.properties?.count + ' obs<br/><strong>Seneste </strong>' + formatDate( feature.properties?.latestDate))
                }
            }).addTo(map.value);

            fitBounds();
            toggleHasLayer();
        }
    }

    function whenFeatureClicked(e: L.LeafletMouseEvent) {
        emitTagCallback(e.target.feature.properties.name)
    }

    function resetGeoJson() {
       if (hasLayer.value) {
           removeLayer();
       }
    }

    function removeLayer() {
        observationLayer.value.remove();
        toggleHasLayer();
    }

    function toggleHasLayer() {
        hasLayer.value = !hasLayer.value;
    }

    function fitBounds() {
        if (observationLayer && map) {
            let layerList: L.Layer[] = [observationLayer.value as L.Layer];
            let featureGroup = new L.FeatureGroup<L.Layer>(layerList);
            let bounds = featureGroup.getBounds();
            try {
                map.value.fitBounds(bounds, { padding: [20, 20] });
            }
            catch {
                map.value.setView([56, 11], 7);
                return;
            }
        }
    }

    return {
        initializeLeaflet,
        addPointsToMap,
        resetGeoJson
    }
}