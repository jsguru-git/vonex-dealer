export class Auth {
    constructor({
        //accessToken = localStorage.getItem('access_token') || null,
        //idToken = localStorage.getItem('id_token') || null,
        //expiresAt = localStorage.getItem('expires_at') || null,
        //status = null,
        //name = JSON.parse(localStorage.getItem('user')).name || null,
        //nickname = JSON.parse(localStorage.getItem('user')).nickname || null,
        //pictureURL = JSON.parse(localStorage.getItem('user')).picture || null,
        //user = JSON.parse(localStorage.getItem('user')) ||  null} = {}) {
        accessToken = null,
        idToken =  null,
        expiresAt =  null,
        status = null,
        name = null,
        nickname = null,
        pictureURL = null,
        user = null } = {}) {
        this.accessToken = accessToken
        this.idToken = idToken;
        this.expiresAt = expiresAt;
        this.status = status;
        this.name = name;
        this.nickname = nickname;
        this.pictureURL = pictureURL;
        this.user = user;

    }
}

export function createAuth(data) {
    return Object.freeze(new Auth(data));
}

