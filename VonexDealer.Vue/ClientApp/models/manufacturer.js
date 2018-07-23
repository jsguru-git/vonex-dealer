export class manufacturer {
    constructor({
        manufacturerID = 0,
        description = null
    } = {}) {
        this.manufacturerID = manufacturerID,
            this.description = description
    }

}

export function createManufacturer(data) {
    return Object.freeze(new manufacturer(data))
}
