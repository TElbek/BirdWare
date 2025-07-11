export type tagType = {
    tagType: number
    id: number
    parentId: number
    name: string
}

export interface tagListe {
    tagList: tagType[]
}
