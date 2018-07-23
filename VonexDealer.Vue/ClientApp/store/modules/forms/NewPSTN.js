import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { NewPSTN } from '../../../models/NewPSTN';

import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';



const state = {
    rows: [new NewPSTN()],
    message: new httpError()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
  
    addRow(state, payload) {
        state.rows.push(new NewPSTN({ addressID: payload.addressID, contractPeriod: payload.contractPeriod }));
    },
    deleteRow(state) {

        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.pop();
    },
    setProgress(state, message) {
        state.message = message;
       
    },
    setNewPSTN(state, pstnData) {
        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.length = 0;
        if (pstnData.length > 0) {
            pstnData.forEach(function (pstn) {
                state.rows.push(pstn);
            })
        }

    },
    removePstn(state, pstnID) {
        state.rows.filter(function (el) {
            return el.ISDNID !== isdnID;
        });
    },
    updateField

}

//ACTIONS
const actions = {
    resetPstn({ commit }) {
        commit('setNewPSTN', new NewPSTN());

    },
    setMessage({ commit, dispatch }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    async removeRows({ state, commit, rootState }, newVal) {
        //rowCount is the number of rows to finish with
        var oldVal = 0 || state.rows.length;
        var rows = [];
        for (var i = oldVal; i > newVal; i--) {
            rows.push(state.rows[i - 1]);
            commit('deleteRow');
        }
        try {
            var pstns = await HTTP.delete('order/DeleteNewPSTNFromOrder/' + rootState.orders.rows[0].orderID, { data: rows });
            return Promise.resolve(pstns);

        } catch (err) {
            return Promise.reject(err);
        }
    },
    resetPSTN({ commit }) {
        commit('setNewPSTN', new NewPSTN());
    },
    getNewPSTN({ commit, dispatch,rootState }) {
        return new Promise((resolve, reject) => {

            var response = HTTP.get('landline/GetNewPSTNsOnOrder/' + rootState.orders.rows[0].orderID);
            response.then(function (data) {
                var resultData = data.data;
                commit('setNewPSTN', resultData)
                dispatch('setMessage', new httpError({ message: 'retrieved PSTN OK', httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: 'Error retrieving:' + err, httpStatus: 500 }));
                    rreject(err);
                });
        })
    },
    async addNewPSTN({ state, commit, dispatch, rootState }) {
        await dispatch('orders/landline/addLandline', null, { root: true });
        var response = HTTP.post('Landline/AddNewPSTNs/' + rootState.orders.rows[0].landlineID, state.rows);
        response.then(function (data) {
            var resultData = data.data;
            commit('setNewPSTN', resultData);

            dispatch('setMessage', new httpError({ message: 'Saved Successfully', httpStatus: 200 }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: 'Error Saving:' + err, httpStatus: 500 }));
                return Promise.reject(err);
            });
        return;
    },
    updateNewPSTN({ state, commit, dispatch }, NewPSTNID) {
        var response = HTTP.put('UpdateNewPSTN/' + NewPSTNID, state.PSTN);
        response.then(function (data) {
            var resultData = data.data;
            //commit('setCustomer', resultData);
            commit('setNewPSTN', resultData);

            dispatch('setMessage', new httpError({ message: 'Updated OK', httpStatus: 200 }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
                dispatch('setMessage', new httpStatus({ message: 'Error Updating' + err, httpStatus: 500 }));
                return Promise.reject(err);
            });
    },
    removeNewPSTN({ commit, dispatch }, NewPSTNID) {
        var progress = new httpError();

        var response = HTTP.get('RemoveNewPSTN/' + NewPSTNID);

        response.then(function (data) {
            var resultData = data.data;
            commit('removePSTN', NewPSTNID)
            dispatch('setMessage', new httpError({ message: 'Removed OK', httpStatus: 200 }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: 'Error:' + err, httpStatus: 500 }));
                return Promise.reject(err);
            });
    },

}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
