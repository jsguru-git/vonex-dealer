
export class Internet {
    constructor({ internetID = 0,
        orderID = 0,
        internetType = null,
        ratePlanID = 0,
        usageAllowance = 0,
        speedRequested = null,
        monthlyPrice = 0.00,
        connectionFee = 0.00,
        contractTerm = 0,
        addressID = null,
        serviceNumber = null,
        contactID = null
    } = {}) {
        this.internetID = internetID;
        this.orderID = orderID;
        this.internetType = internetType;
        this.ratePlanID = ratePlanID;
        this.usageAllowance = usageAllowance;
        this.speedRequested = speedRequested;
        this.monthlyPrice = monthlyPrice;
        this.connectionFee = connectionFee;
        this.contractTerm = contractTerm;
        this.addressID = addressID;
        this.serviceNumber = serviceNumber;
        this.contactID = contactID;
    }
}

export function createInternet(data) {
    return Object.freeze(new Internet(data));
}
