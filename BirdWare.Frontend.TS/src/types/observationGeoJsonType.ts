import type { Crs, Geometry } from "./geoJsonTypes"

export type observationGeoJson = {
  type: string
  name: string
  crs: Crs
  features: Feature[]
}

export type Feature = {
  type: string
  properties: Properties
  geometry: Geometry
}

export type Properties = {
  id: number
  name: string
  count: number
  countIsAboveAverage: boolean
  latestDate: Date
}
