export type baseGeoJson = {
  type: string
  name: string
  crs: Crs
  features: baseFeature[]
}

export type baseFeature = {
  type: string
  geometry: Geometry
}

export type Crs = {
  type: string
  properties: CrsProperties
}

export type CrsProperties = {
  name: string
}

export type Geometry = {
  type: string
  coordinates: number[]
}
