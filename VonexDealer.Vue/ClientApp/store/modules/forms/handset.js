import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { handset } from '../../../models/handset';
import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';
import { Promise } from 'core-js';

const state = {
    rows: [new handset()],
    message: new httpError(),
    manufacturers: [],
    categories: [],

}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setRows(state, rows) {
        state.rows.length = 0;
        rows.forEach(data => {
            state.rows.push(data);
        });
    },
    setManufacturers(state, rows) {
        state.manufacturers = rows;
    },
    setCategories(state, rows) {
        state.categories = rows;
    },
    setProgress(state, message) {
        state.message = message;
    },
    addRow(state, row) {
        state.rows.push(new handset());
    },
    deleteRow(state) {
        state.rows.pop();
    },
    removeHandset(state, handset) {
        if (state.rows.length > 0) {
            for (var index = state.rows.length; index--;) {
                if (state.rows[index].handsetID === handset.handsetID) {
                    state.rows.splice(index, 1);
                }

            }
        }
    },
    updateField

}

//ACTIONS
const actions = {
    reset({ commit }) {
        commit('setRows', new handset());
        commit('setProgress', new httpError());
    },
    setMessage({ commit, }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    getHandsets({ commit, dispatch }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/getHandsets/', '');
            response.then(data => {
                var resultData = data.data;
                commit('setRows', resultData)
                 dispatch('getCategories');
                 dispatch('getManufacturers');
                commit('setProgress', new httpError({ message: "Handsets Retrieved", httpStatus: 200 }));
                resolve(resultData);
                return;
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 200 }));
                    reject(err);
                });
        })
    },
    getCategories({ commit, dispatch }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/GetHardwareCategories/', '');
            response.then(function (data) {
                var resultData = data.data;
                commit('setCategories', resultData)
                resolve(resultData);
                return;
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 200 }));
                    reject(err);
                });
        })
    },
    getManufacturers({ commit, dispatch }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/GetHardwareManufacturers/', '');
            response.then(function (data) {
                var resultData = data.data;
                commit('setManufacturers', resultData)
                resolve(resultData);
                return;
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 200 }));
                    reject(err);
                });
        })
    },
    saveHandsets({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.post('support/UpdateHandsets/', state.rows);
            response.then(data => {
                if (data.status === 200) {
                    commit('setRows', data.data);
                    dispatch('setMessage', new httpError({ message: 'Saved OK', httpStatus: 200 }));
                }
                resolve(data.data);
            }).catch(err => {
                dispatch('setMessage', new httpError({ message: err.response.message, httpStatus: 500 }));
                reject(err);
            });
        })
    },
    removeHandset({ commit, dispatch, state }, handset) {
        return new Promise((resolve, reject) => {
            if (handset.handsetID == 0) {
                commit('removeHandset', handset);
                resolve();
                return;
            }
            var response = HTTP.delete('support/deleteHandsets/'+ handset.handsetID);
            response.then(data => {
                if (data.status === 200) {
                    commit('removeHandset', handset);
                    dispatch('setMessage', new httpError({ message: 'handset deleted', httpStatus: 200 }));
                    resolve(data.status);
                }
            }).catch(err => {
                dispatch('setMessage', new httpError({ message: 'cant delete', httpStatus: 500 }))
                reject(err.status);
            })
        })
    },

    setMessage({ commit, dispatch }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
