import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { Churn } from '../../../models/Churn';

import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';



const state = {
    rows: [],
    message: new httpError()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    addRow(state, payload) {
        state.rows.push(new Churn({ addressID: payload.addressID, contractPeriod: payload.contractPeriod }));
    },
    deleteRow(state) {

        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.pop();
    },
    setNewChurn(state, churnData) {
        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.length = 0;
        churnData.forEach(function (churn) {
            state.rows.push(churn);
        })

    },
    setProgress(state, message) {
        state.message = message;
       
    },
    removeChurn(state, churnID) {
        state.rows.filter(function (el) {
            return el.churnID !== churnID;
        });
    },
    updateField

}

//ACTIONS
const actions = {
    async removeRows({ state, commit, dispatch, rootState }, newVal) {
        //rowCount is the number of rows to finish with
        var oldVal = 0 || state.rows.length;
        var rows = [];
        for (var i = oldVal; i > newVal; i--) {
            rows.push(state.rows[i-1]);
            commit('deleteRow');
        }
        try {
            console.log(rows);
            var churns = await HTTP.delete('order/DeletePSTNChurnFromOrder/' + rootState.orders.rows[0].orderID, { data: rows });
            dispatch('setMessage', new httpError({ message: 'Rows deleted', httpStatus: 200 }));
            return Promise.resolve(churns);
        } catch (e) {
            dispatch('setMessage', new httpError({ message: 'error deleting rows: ' + e, httpStatus: 500 }));
            return Promise.reject(e);
        }

    },
    resetNewChurn({ commit }) {
        commit('setNewChurn', []);
    },
    async getNewChurn({ commit, rootState }) {
        var orderID = rootState.orders.rows[0].orderID;

        try {

            var data = await HTTP.get('Landline/GetChurnsOnOrder/' + orderID);

            if (data) {
                var resultData = data.data;
                if (typeof resultData !== 'undefined' && resultData.length > 0) {
                    commit('setNewChurn', resultData)
                    return Promise.resolve(resultData);
                }
                return Promise.resolve();
            }
        } catch (err) {

            commit('setProgress', new httpError({ message: "Error:" + err, httpStatus: 500 }));
            return Promise.reject(err);
        }
    },
    async addNewChurn({ commit, state, dispatch, rootState }) {

        await dispatch('orders/landline/addLandline', null, { root: true });

        var response = await HTTP.post('Landline/addChurns/' + rootState.orders.rows[0].landlineID, state.rows);

        var resultData = response.data;

        if (typeof resultData !== 'undefined' && resultData.length > 0) {
            commit('setNewChurn', resultData);
            dispatch('setMessage', new httpError({ message: "Submitted OK", httpStatus: 200 }));
            return Promise.resolve(resultData);
        }
        return Promise.reject();
    },
    updateNewChurn({ state, dispatch, commit }, ChurnID) {
        return new Promise((resolve, reject) => {

            var churn = state.rows.find(function (element) {
                return element.churnID == ChurnID;
            })

            var response = HTTP.put('UpdateChurn/' + ChurnID, churn);
            response.then(function (data) {
                var resultData = data.data;
                commit('setNewChurn', resultData);
                dispatch('setMessage', new httpError({ message: "Updated OK", httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 500 }));
                    reject(err);
                });
        })
    },
    removeNewChurn({ state, dispatch, commit }, churnID) {
        return new Promise((resolve, reject) => {

            if (churnID === 0) {
                commit('deleteRow');
                return;
            }
            var response = HTTP.delete('landline/RemoveChurn/' + churnID)

            response.then(function (data) {
                var resultData = data.data;
                commit('removeChurn', churnID);

                dispatch('setMessage', new httpError({ message: "Processed OK", httpStatus: 200 }));
                resolve();
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 500 }));
                    console.log('error in saving');
                    reject(err);
                });
        })

    },
    async checkDuplicates({ state, commit }) {

        const names = [];
        state.rows.forEach(function (el) {
            names.push(el.serviceNumber);
        })

        const count = names =>
            names.reduce((a, b) =>
                Object.assign(a, { [b]: (a[b] || 0) + 1 }), {})

        const duplicates = dict =>
            Object.keys(dict).filter((a) => dict[a] > 1)
        console.log(count(names)) // { Mike: 1, Matt: 1, Nancy: 2, Adam: 1, Jenny: 1, Carl: 1 }
        console.log(duplicates(count(names))) // [ 'Nancy' ]
        if (duplicates(count(names)).length == 0)
            return Promise.resolve(names);
        else
            return Promise.reject(count(names));
    },
    setMessage({ commit, dispatch }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    }

}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
}
