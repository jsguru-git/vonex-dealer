export class NewPSTN {
    constructor({
        newPSTNID = 0,
        contractPeriod = null,
        addressID = 0,
        termination = null,
        fee = 0.0,
        ratePlanID = 0
    } = {}) {
        this.newPSTNID = newPSTNID;
        this.addressID = addressID;
        this.termination = termination;
        this.fee = fee;
        this.ratePlanID = ratePlanID;
        this.contractPeriod = contractPeriod;
        this.ratePlanID = ratePlanID;
    }

}
export function createNewPSTN(data) {
    return Object.freeze(new NewPSTN(data));
}
