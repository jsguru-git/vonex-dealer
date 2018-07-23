import { Auth } from '../../../models/auth'
import { httpError } from '../../../models/httpError'
import { createHelpers } from 'vuex-map-fields'
import { getField, updateField } from 'vuex-map-fields';
import { HTTP } from '../../../http-common';
import axios from 'axios'
export const { mapFields: mapAuthFields } = createHelpers({
    getterType: `orders/auth/getField`,
    mutationType: `orders/auth/updateField`,
});

const state = {
    auth: new Auth(),
    message: new httpError()
}


//getters

const getters = {
    isAuthenticated: state => !!state.auth.idToken,
    isLoggedIn: state => (state.auth.idToken && (state.auth.expiresAt > new Date().getTime())),
    authStatus: state => state.status,
    auth: state => state.auth,
    getField
}

//Mutations
const mutations = {
    setAuth(state, authResult) {
        state.auth.accessToken = authResult.accessToken;
        state.auth.idToken = authResult.idToken;

        let expiresAt = JSON.stringify(authResult.expiresIn * 1000 + new Date().getTime())

        state.auth.expiresAt = expiresAt;
        if (authResult.idTokenPayload) {
            state.auth.name = authResult.idTokenPayload.name;
            state.auth.nickname = authResult.idTokenPayload.nickname;
            state.auth.pictureURL = authResult.idTokenPayload.picture;
        }

        HTTP.defaults.headers.common['Authorization'] = 'Bearer ' + state.auth.idToken;

    },
    setProgress(state, message) {
        state.message = message;
       
    },
    updateField

}

//ACTIONS
const actions = {
    login() {
        $auth.login()
    },
    setMessage({ commit, }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    async setSession({ commit, dispatch, state }, authResult) {
        return new Promise((resolve, reject) => {
            commit('setAuth', authResult);
            dispatch('setMessage', new httpError({ httpStatus: 200, message: "logged in" }));
            HTTP.interceptors.response.use(function (response) {
                return response;
            }, function (error) {
                const originalRequest = error.config;
                if (typeof error.response !== 'undefined' && error.response.status === 401 && !originalRequest._retry) {
                    originalRequest._retry = true;

                    const token = authResult.idToken;

                    if (token != null) {
                        originalRequest.headers.Authorization = `Bearer ${token}`;
                        return axios(originalRequest);
                    }
                }
                return error;
                });

            resolve();
        })
    },
    checkLocal({ commit, dispatch, state }) {
        return new Promise((resolve, reject) => {
            var authResult = new Auth();
            authResult.idToken = localStorage.getItem('id_token');
            authResult.accessToken = localStorage.getItem('access_token')
            authResult.expiresAt = localStorage.getItem('expires_at')
            authResult.user = JSON.parse(localStorage.getItem('user'))
            commit('setAuth', authResult);
            resolve();
        })
    },
    clearSession({ commit }) {
        return new Promise((resolve, reject) => {
            var authResult = new Auth();
            commit('setAuth', authResult);
            resolve();
        })
    },
    checkToken({ rootState }) {
        HTTP.defaults.headers.common['Authorization'] = 'Bearer ' + rootState.orders.auth.auth.idToken;
    },
    handleAuthentication(authResult) {
        return new Promise((resolve, reject) => {
            webAuth.parseHash((err, authResult) => {
                if (authResult && authResult.accessToken && authResult.idToken) {
                    this.expiresAt = authResult.expiresIn
                    this.accessToken = authResult.accessToken
                    this.token = authResult.idToken
                    this.user = authResult.idTokenPayload

                    resolve(authResult)

                } else if (err) {
                    this.logout()
                    reject(err)
                }

            })
        })
    }
}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
