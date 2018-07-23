import { HTTP } from '../../../http-common'
import { httpError } from '../../../models/httpError';

import { Internet } from '../../../models/Internet';

import { getField, updateField } from 'vuex-map-fields';
import { createHelpers } from 'vuex-map-fields'



const state = {
    addresses: [],
    rows: [new Internet()],
    contacts: [],
    message: new httpError(),
}

//getters

const getters = {
    // customer: state => state.customer,

    getField
}

//Mutations
const mutations = {
    addRow(state, payload) {
        var months = 0;
        var rateplan = 0;
        if (state.rows.length > 0) {
            rateplan = state.rows[0].ratePlanID;
        }
        
            months = payload.months;
        
        state.rows.push(new Internet({
            addressID: payload.addressID, contractTerm: months, ratePlanID: rateplan,
            contactID: payload.contactID
        }));
    },
    delRow(state) {
        state.rows.pop();
    },
    setInternet(state, internet) {
        state.rows.length = 0;
        state.rows = internet;

    },
    setAddresses(state, addresses) {
        state.addresses = addresses
    },
    addAddresses(state, address) {
        state.addresses.push(address);
    },

    setProgress(state, message) {
        state.message = message;

    },
    setContacts(state, contacts) {
        //delete state.contacts;
        state.contacts = contacts;
    },
    addContact(state, contact) {
        state.contacts.push(contact);
    },
    updateField

}

//ACTIONS
const actions = {
    resetInternet({ commit }) {
        commit('setInternet', [new Internet()]);
        commit('setAddresses', []);
        commit('setContacts', []);
    },
    setMessage({ commit, }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    getInternet({ commit, rootState, dispatch }) {
        return new Promise((resolve, reject) => {
            var orderID = rootState.orders.rows[0].orderID;
            var response = HTTP.get('order/GetInternet/' + orderID);
            dispatch('setMessage', new httpError({ message: "Retrieving Internet orders", httpStatus: 200 }));
            response.then(function (data) {
                if (data.status === 200) {
                    var resultData = data.data;
                    if (typeof resultData !== 'undefined' && resultData.length > 0) {
                        commit('setInternet', resultData)
                        dispatch('setMessage', new httpError({ message: "Retrieved OK", httpStatus: 200 }));
                        resolve(resultData);
                    }
                } else {
                    commit('setInternet', []);
                    resolve();
                }
            })
                .catch((err) => {
                    reject(err);
                });
        })
    },
    addNewInternet({ commit, state, rootState, dispatch }) {
        return new Promise((resolve, reject) => {
            var orderID = rootState.orders.rows[0].orderID;

            var response = HTTP.post('order/AddInternetOrder/' + orderID, state.rows);
            dispatch('setMessage', new httpError({ message: "saving Internet orders", httpStatus: 200 }));
            response.then(function (data) {
                if (data.data) {
                    commit('setInternet', data.data);
                    resolve(data.data);
                }
            }).catch(function (err) {
                dispatch('setMessage', new httpError({ message: err, httpStatus: 500 }));
                reject(err);
            })
        })
    },
    getContacts({  commit,  rootState }) {
        return new Promise((resolve, reject) => {
            var orderID = rootState.orders.rows[0].orderID;

            var response = HTTP.get('order/getContacts/' + orderID);
            response.then(function (data) {
                if (data.status === 200) {
                    commit('setContacts', data.data);
                    resolve(data.data);
                }
                else {

                    commit('setContacts', []);
                    resolve(data.data);
                }

            }).
                catch(err => {
                    reject(err.message);
                })
        })
    },
    addContacts({ commit, state, rootState, dispatch }, contact) {
        return new Promise((resolve, reject) => {
            var orderID = rootState.orders.rows[0].orderID;
            commit('addContact', contact);
            var response = HTTP.post('order/addContact/' + orderID, state.contacts);
            dispatch('setMessage', new httpError({ message: "saving contacts", httpStatus: 200 }));
            response.then(function (data) {
                commit('setContacts', data.data);
                resolve(data.data);
            }).catch(err => {
                reject(err);
            })
        })
    },
    async removeRows({ state, commit, dispatch, rootState }, newVal) {
        //rowCount is the number of rows to finish with
        var oldVal = 0 || state.rows.length;
        var rows = [];
        for (var i = oldVal; i > newVal; i--) {
            rows.push(state.rows[i - 1]);
            commit('delRow');
        }
        try {
            console.log(rows);
            var cont = await HTTP.delete('order/DeleteInternetFromOrder/' + rootState.orders.rows[0].orderID, { data: rows });
            dispatch('setMessage', new httpError({ message: 'Rows deleted', httpStatus: 200 }));
            return Promise.resolve(cont);
        } catch (e) {
            dispatch('setMessage', new httpError({ message: 'error deleting rows: ' + e, httpStatus: 500 }));
            return Promise.reject(e);
        }

    }
}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
