import type { Feature } from 'geojson';
import type { birdwareGeoJson } from '@/types/birdwareGeoJsonType';
import * as L from 'leaflet';
import { ref } from 'vue';

export function useObservationMapLogic(emitTagCallback: any) {
    const map = ref();
    const observationLayer = ref({} as L.Layer);
    const hasLayer = ref(false);

    const svgTemplateBlue = `<svg xmlns="http://www.w3.org/2000/svg">
                <circle cx="10" cy="10" r="8" fill="#0000ff" />
            </svg>`;

    const svgTemplateRed = `<svg xmlns="http://www.w3.org/2000/svg">
                <circle cx="10" cy="10" r="8" fill="#ff0000" />
            </svg>`;

    function initializeLeaflet() {
        map.value = L.map('map');
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 15,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map.value);
    }

    function addPointsToMap(geoJson: birdwareGeoJson) {
        if (map) {
            resetGeoJson();
            observationLayer.value = L.geoJSON(geoJson as any, {
                pointToLayer: function (feature: Feature, latlng: L.LatLng) {
                    let marker = L.marker(latlng, {
                        icon: createMapIcon({ width: 22, countIsAboveAverage: feature.properties?.countIsAboveAverage })
                    });
                    marker.on('click', function (ev) {
                        whenFeatureClicked(ev);
                    });
                    return marker;
                },
                onEachFeature: function (feature, layer) {
                    layer.bindPopup('<strong>' + feature.properties?.name + '</strong><br/> ' + feature.properties?.count + ' observationer')
                }
            }).addTo(map.value);

            fitBounds();
            toggleHasLayer();
        }
    }

    function createMapIcon({ countIsAboveAverage = false, width = 22, color = '#0000ff' } = {}) {
        const height = Math.round(width * (300 / 240)); // ratio 1.25
        const html = L.Util.template(countIsAboveAverage ? svgTemplateRed : svgTemplateBlue, {
            width,
            height,
            color,
        });
        return L.divIcon({
            html,
            iconSize: [width, height]
        });
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
                map.value.setView([56,11],7);
                return;
            }
        }
    }

    return {
        initializeLeaflet,
        addPointsToMap
    }
}