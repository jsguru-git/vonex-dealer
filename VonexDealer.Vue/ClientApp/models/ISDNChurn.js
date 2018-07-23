export class ISDNChurn {
    constructor({
        NumberOfISDN = null,
        isISDN = false,
        contractPeriod = null,
        ratePlanID = null,
        hasRange = false,
        ISDNData = []
    } = {}) {
        this.NumberOfISDN = NumberOfISDN;
        this.isISDN = isISDN;
        this.ISDNData = ISDNData;
        this.contractPeriod = contractPeriod;
        this.ratePlanID = ratePlanID;
        this.hasRange = hasRange;
    }
}

export function CreateISDNChurn(data) {
    return Object.freeze(new ISDNChurn(data))
}
