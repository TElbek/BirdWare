export type observationType = {
    observationId: number
    gruppeNavn: string
    gruppeId: number
    artNavn: string
    artId: number
    bem: string
    dato: string
    generelt: string
    lokalitetId: number
    lokalitetNavn: string
    latitude: number
    longitude: number
    fugleturId: number
    familieId: number
    familieNavn: string
    regionId: number
    regionNavn: string
    su: boolean
    speciel: boolean
    aarstal: number
    maaned: number
}

export interface observationListe {
    observationer: observationType[]
}