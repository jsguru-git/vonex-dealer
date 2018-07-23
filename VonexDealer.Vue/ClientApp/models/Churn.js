export class Churn {
    constructor({
        churnID = 0,
        landlineID = 0,
        contractPeriod = null,
        serviceNumber = null,
        ratePlanID = null,
        addressID = 0
    } = {}) {
        this.churnID = churnID;
        this.landlineID = landlineID;
        this.contractPeriod = contractPeriod;
        this.serviceNumber = serviceNumber;
        this.ratePlanID = ratePlanID;
        this.addressID = addressID;
    }

}
export function createChurn(data) {
    return Object.freeze(new Churn(data));
}
