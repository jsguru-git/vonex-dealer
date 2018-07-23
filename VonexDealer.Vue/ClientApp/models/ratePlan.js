export class ratePlan {
    constructor({
iD = null,
        ratePlanID = null,
        ratePlanDescription = null,
        groupName = null,
        usageType = null,
        handset = null,
        monthlyRetail = null,
        monthlyProfitPercent = null,
        ratePlanPDF = null,
        dataAllowance = null,
        contractMonths = null,
        payInitialComms = null,
        hasSpecialOverride = null,
        dealerSupplied = null,
        mixnMatch = null,
        additionalServices = null,
        residential = null,
    } = {}) {
        this.iD = iD;
        this.ratePlanID = ratePlanID;
        this.ratePlanDescription = ratePlanDescription;
        this.groupName = groupName;
        this.usageType = usageType;
        this.handset = handset;
        this.monthlyRetail = monthlyRetail;
        this.monthlyProfitPercent = monthlyProfitPercent;
        this.ratePlanPDF = ratePlanPDF;
        this.dataAllowance = dataAllowance;
        this.contractMonths = contractMonths;
        this.payInitialComms = payInitialComms;
        this.hasSpecialOverride = hasSpecialOverride;
        this.additionalServices = additionalServices;
        this.residential = residential;
        this.dealerSupplied = dealerSupplied;
        this.mixnMatch = mixnMatch;
    }
}

export function createRatePlan(data) {
    return Object.freeze(new ratePlan(data));
}
