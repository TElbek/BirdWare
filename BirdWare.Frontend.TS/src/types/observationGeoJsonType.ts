import type { baseFeature, baseGeoJson } from "./geoJsonTypes"

export interface observationGeoJson extends baseGeoJson {
  features: Feature[]
}

export interface Feature extends baseFeature {
  properties: Properties
}

export type Properties = {
  id: number
  name: string
  count: number
  countIsAboveAverage: boolean
  latestDate: Date
}
