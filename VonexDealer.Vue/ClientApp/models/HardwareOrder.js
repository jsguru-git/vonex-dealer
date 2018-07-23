export class hardwareOrder {
    constructor({

        hardwareOrderID = 0,
        orderID = 0,
        addressID = 0,
        hardwareOrderDetails = null,
        subTotal = 0.00,
        freight = 0.00,
        totalInitialPayment = 0.00,
        

    } = {}) {
            this.hardwareOrderID = hardwareOrderID;
            this.orderID = orderID;
            this.addressID = addressID;
            this.hardwareOrderDetails = hardwareOrderDetails;
            this.subTotal = subTotal;
            this.freight = freight;
            this.totalInitialPayment = totalInitialPayment;

    }
}

export function CreateHardwareOrder(data) {
    return Object.freeze(new hardwareOrder(data))
}
