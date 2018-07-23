export class ISDN {
    constructor({
        isdnid = 0,
        landlineID = 0,
        contractLengthMonths = 0,
        ratePlanID = 0,
        hasRange = false,
        rangeSize = null,
        addressID = 0,
        numbers = [],
        isdnType = null
    } = {}) {
        this.isdnid = isdnid;
        this.landlineID = landlineID;
        this.contractLengthMonths = contractLengthMonths;
        this.ratePlanID = ratePlanID;
        this.hasRange = hasRange;
        this.rangeSize = rangeSize;
        this.addressID = addressID;
        this.numbers = numbers;
        this.isdnType = isdnType;
            
    }

}
export function createISDN(data) {
    return Object.freeze(new ISDN(data));
}
