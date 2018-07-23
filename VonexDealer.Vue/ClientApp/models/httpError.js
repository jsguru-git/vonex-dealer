export class httpError {
    constructor({
        httpStatus = 0,
        message = null

    } = {}) {
        this.httpStatus = httpStatus;
        this.message = message;
    }
}

export function createhttpError(data) {
    return Object.freeze(new httpError(data));
}
