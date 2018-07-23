
import { HTTP } from '../../../http-common'
import { ISDN } from '../../../models/ISDN';

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
        var months = 0;
        var rateplan = 0;
        if (state.rows.length > 0) {
            months = payload.contractPeriod;
            rateplan = state.rows[0].ratePlanID;
        }
        state.rows.push(new ISDN({ addressID: payload.addressID, contractLengthMonths: months, ratePlanID: rateplan }));
    },
    deleteRow(state) {

        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.pop();
    },
    setProgress(state, message) {
        state.message = message;

    },
    setNewISDN(state, isdnData) {
        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.length = 0;
        isdnData.forEach(function (isdn) {
            delete isdn.numbersAsString;
            state.rows.push(isdn);
        })

    },
    removeIsdn(state, isdnID) {
        state.rows.filter(function (el) {
            return el.isdnid !== isdnID;
        });
    },
    updateField

}

//ACTIONS
const actions = {
    resetISDN({ commit }) {
        commit('setNewISDN', []);
    },
    getISDN({ commit, rootState, dispatch }) {
        return new Promise((resolve, reject) => {
            var orderID = rootState.orders.rows[0].orderID;
            var response = HTTP.get('Landline/GetISDNsOnOrder/' + orderID);
            response.then(function (data) {
                var resultData = data.data;
                if (typeof resultData !== 'undefined' && resultData.length > 0) {
                    delete resultData.numbersAsString;
                    commit('setNewISDN', resultData)
                    resolve(resultData);
                }
                resolve();
            })
                .catch((err) => {

                    new httpError({ message: "Error:" + err, httpStatus: 500 })
                    reject(err);
                });
        })
    },
    async addNewISDN({ commit, state, dispatch, rootState }) {
        // return new async Promise((resolve, reject) => {
        if (typeof rootState.orders.rows[0].landlineID == 'undefined' || rootState.orders.rows[0].landlineID == 0) {
            var landlineID = await dispatch('orders/landline/addLandline', null, { root: true });

        }

        var response = await HTTP.post('Landline/addISDNs/' + rootState.orders.rows[0].landlineID, state.rows).catch(error => { return Promise().reject });

        //response.then(function (data) {
        if (response) {
            var resultData = response.data;
            if (typeof resultData !== 'undefined' && resultData.length > 0) {
                delete resultData.numbersAsString;
                commit('setNewISDN', resultData);
                dispatch('setMessage', new httpError({ message: "Submitted OK", httpStatus: 200 }));
            }
            return; //resolve();
        };

    },
    updateNewISDN({ state, commit, dispatch }, isdnid) {
        return new Promise((resolve, reject) => {

            var isdn = state.rows.find(function (element) {
                return element.isdnid == isdnid;
            })

            var response = HTTP.put('UpdateISDNs/' + isdnid, isdn);
            response.then(function (data) {
                var resultData = data.data;
                delete resultData.numbersAsString;
                commit('setNewISDN', resultData);
                dispatch('setMessage', new httpError({ message: "Updated OK", httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 500 }));
                    rejetc(err);
                });
        })
    },
    removeNewISDN({ commit, dispatch }, isdnid) {
        return new Promise((resolve, reject) => {
            if (isdnid === 0) {
                commit('deleteRow');
                return;
            }
            var response = HTTP.delete('landline/RemoveISDN/' + isdnid)

            response.then(function (data) {
                var resultData = data.data;
                delete resultData.numbersAsString;
                commit('removeIsdn', isdnid);

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
    setMessage({ commit, }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    checkDuplicates({ state, commit, rootState }) {

        commit('setProgress', new httpError());

        return new Promise((resolve, reject) => {

            var counts = [];
            var hasDuplicates = false;
            var numbers = [];
            try {

                if (rootState.orders.newChurn.rows !== null) {

                    rootState.orders.newChurn.rows.forEach(el => {
                        if (el.serviceNumber !== null)
                            numbers.push(el.serviceNumber);
                    });

                    state.rows.forEach(row => {

                        row.numbers.forEach(num => {
                            if (num !== null)
                                numbers.push(num)
                        });
                    })
                }


                //var a = row.numbers;
                for (var i = 0; i <= numbers.length - 1; i++) {
                    if (numbers[i].length == 0) { continue; }
                    if (counts[numbers[i]] === undefined) {
                        counts[numbers[i]] = 1;
                    } else {
                        hasDuplicates = true;
                        resolve(hasDuplicates);
                        break;
                    }

                }
                resolve(hasDuplicates);
            } catch (err) {
                reject(err);
            }
        })

    },

}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
