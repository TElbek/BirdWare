export type analyseType = {
    analyseType: number
    artId: number
    su: boolean
    speciel: boolean
    artNavn: string
}

export interface analyseListe {
    analyser: analyseType[]
}

export interface enAnalyse {
    analyse: analyseType
}
