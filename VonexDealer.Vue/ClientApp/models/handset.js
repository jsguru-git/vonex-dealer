export class handset {
    constructor({

        handsetID = 0,
        model = null,
        description = null,
        imagePath = null,
        dealerEx = 0.00,
        rrpEx = 0.00,
        hardwareCategoryID = 0,
        manufacturerDetailsID = 0

    } = {}) {
        this.handsetID = handsetID,
            this.model = model,
            this.description = description,
            this.imagePath = imagePath,
            this.hardwareCategoryID = hardwareCategoryID,
            this.manufacturerDetailsID = manufacturerDetailsID,
            this.rrpEx = rrpEx,
            this.dealerEx = dealerEx

    }
}

export function CreateHandset(data) {
    return Object.freeze(new handset(data))
}
