export class IPVoiceService {
    constructor({

        ipExtensionID = 0,
        ipOrderID = 0,
        extensionName = null,
        extensionNo = 0,
        handsetID = 0,
        ratePlanID = 0,
        outboundANI = null,
        voicemail = false,
        voicemailEmail = null,
        mobileTwinning = null,
        contractLengthMonths = 0,
        dealerSupplied = false,

    } = {}) {
        this.ipExtensionID = ipExtensionID;
        this.ipOrderID = ipOrderID;
        this.extensionName = extensionName;
        this.extensionNo = extensionNo;
        this.handsetID = handsetID;
        this.ratePlanID = ratePlanID;
        this.outboundANI = outboundANI;
        this.voicemailEmail = voicemailEmail;
        this.mobileTwinning = mobileTwinning;
        this.voicemail = voicemail;
        this.contractLengthMonths = contractLengthMonths;
        this.dealerSupplied = dealerSupplied;

    }
}

export function CreateNewIPVoiceService(data) {
    return Object.freeze(new IPVoiceService(data))
}
