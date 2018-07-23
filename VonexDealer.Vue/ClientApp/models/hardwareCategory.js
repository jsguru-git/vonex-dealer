export class hardwareCategory {
    constructor({
        hardwareCategoryID = 0,
        description = null
    } = {}) {
        this.hardwareCategoryID = hardwareCategoryID,
            this.description = description
    }

}

export function createHardwareCategory(data) {
    return Object.freeze(new hardwareCategory(data))
}
