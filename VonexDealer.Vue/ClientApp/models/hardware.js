export class hardware {
    constructor({

        hardwareID = 0,
        hardwareOrderID = 0,
        quantity = 1,
        cost = 0.00,
        handsetID = 0,
        comment = null,
        dealerSupplied = false,
        lineTotal = 0.00,
        fromIPOrder = false,
    } = {}) {
        this.hardwareID = hardwareID;
        this.hardwareOrderID = hardwareOrderID;
        this.quantity = quantity;
        this.handsetID = handsetID;
        this.comment = comment;
        this.dealerSupplied = dealerSupplied;
        this.cost = cost;
        this.lineTotal = lineTotal;
        this.fromIPOrder = fromIPOrder
    }
  

}

export function CreateHardware(data) {
    return Object.freeze(new hardware(data))
}
