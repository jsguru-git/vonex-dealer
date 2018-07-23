export class ipOrder {
    constructor({
        iPOrderID = 0,
        orderID = 0,
        ratePlanID = 0,
        currentModem = null,
        currentSwitch = null,
        uploadSpeed = null,
        downloadSpeed = null,
        callHandline = null,
        isCreatedByAMPT = false,
        amptDomainName = null,
        dealerSupplied = false,
        minMonthly = false

    } = {}) {
        this.iPOrderID = iPOrderID;
        this.orderID = orderID;
        this.ratePlanID = ratePlanID;
        this.currentModem = currentModem;
        this.currentSwitch = currentSwitch;
        this.uploadSpeed = uploadSpeed;
        this.downloadSpeed = downloadSpeed;
        this.callHandline = callHandline;
        this.isCreatedByAMPT = isCreatedByAMPT;
        this.amptDomainName = amptDomainName;
        this.dealerSupplied = dealerSupplied;
        this.minMonthly = minMonthly;
    }
}

export function CreateIPOrder(data) {
    return Object.freeze(new ipOrder(data))
}
