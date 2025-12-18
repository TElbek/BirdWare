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
