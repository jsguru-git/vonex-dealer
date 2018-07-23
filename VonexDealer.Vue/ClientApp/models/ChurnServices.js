export class ChurnServices {
    constructor({
        NumberOfServiceChurn = null,
        isChurn = false,
        churnData = [ {
            id: 0,
            ContractPeriod: 0,
            ServiceNumber: null,
            RatePlan: 0,
            ServiceType: 0,
            Address: null
        }]
    } = {}) {
        this.NumberOfServiceChurn = NumberOfServiceChurn;
        this.isChurn = isChurn;
        this.churnData = churnData;
    }
}

export function CreateChurnServices(data) {
    return Object.freeze(new ChurnServices(data))
}
