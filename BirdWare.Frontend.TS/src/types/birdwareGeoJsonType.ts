export interface birdwareGeoJson {
  type: string
  name: string
  crs: Crs
  features: Feature[]
}

export interface Crs {
  type: string
  properties: CrsProperties
}

export interface CrsProperties {
  name: string
}

export interface Feature {
  type: string
  properties: Properties
  geometry: Geometry
}

export interface Properties {
  id: number
  name: string
  count: number
  countIsAboveAverage: boolean
}

export interface Geometry {
  type: string
  coordinates: number[]
}
