export type hvorKanJegFindeType = {
    artId: number
    artNavn: string
    lokalitetNavn: string
    latitude: number
    longitude: number
    distance: number
}

export interface hvorKanJegFindeListe {
    hvorKanJegFinde: hvorKanJegFindeType[]
}
