export class ISDNChurnService {
    constructor({
        NumberOfISDN = null,
        isISDN = false,
        ISDNData = []
    } = {}) {
        this.NumberOfISDN = NumberOfISDN;
        this.isISDN = isISDN;
        this.ISDNData = ISDNData;
    }
}

export function CreateISDNChurnService(data) {
    return Object.freeze(new ISDNChurnService(data))
}
