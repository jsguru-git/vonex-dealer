import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { handset } from '../../../models/handset';
import { hardware } from '../../../models/hardware';
import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';
import { hardwareOrder } from '../../../models/HardwareOrder';

const state = {
    hardwareOrder: new hardwareOrder(),
    handsets: [new handset()],
    rows: [],
    message: new httpError(),

}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setHardwareOrder(state, order) {
        state.hardwareOrder = order;
    },
    setHandset(state, handsets) {
        state.handsets = handsets;
    },
    clearRows(state) {
       // state.rows.length = 0;
        state.rows = [];
    },
    setRows(state, rows) {
        state.rows.length = 0;
        rows.forEach(data => {
            state.rows.push(data);
        });
    },
    setProgress(state, message) {
        state.message = message;
    },
    addRow(state, row) {
        state.rows.push(new hardware({ quantity: 1, fromIPOrder: false }));
    },
    deleteRow(state) {
        state.rows.pop();
    },
    removeIPRows(state) {
        if (state.rows.length > 0) {
            for (var index = state.rows.length ; index--;) {
                if (state.rows[index].fromIPOrder === true) {
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
        commit('setHardwareOrder', new hardwareOrder());
        commit('clearRows');
        commit('setProgress', new httpError());
    },
    setMessage({ commit, }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    removeIPRows({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {
            var rows = state.rows.filter(e => e.fromIPOrder === true);

            var response = HTTP.delete('order/RemoveHardwareOnOrderAsync/' + rootState.orders.rows[0].orderID, {data: rows});
            response.then(data => {
                commit('removeIPRows');
                
                resolve(data.data);
            }).catch(err => {
             //   commit('setRows', [new hardware()]);
                reject(err);
            })


        })
    },
    getHandsetsFromIPVoice({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('order/GetHandsetsOffOrder/' + rootState.orders.rows[0].orderID);
            response.then(async data => {
                //await dispatch('removeIPRows');
                commit('setRows', data.data);
                resolve(data.data);
            }).catch(err => {
                commit('setRows', [new hardware()]);
                reject(err);
            })


        })
    },
    getHandsetsOnOrder({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/getHardwareOnOrder/' + rootState.orders.rows[0].orderID);
            response.then(function (data) {
                if (data.status === 200) {
                    var resultData = data.data;
                    commit('setRows', resultData);
                    dispatch('setMessage', new httpError({ message: "Hardware Retreived", httpStatus: 200 }));
                    resolve(resultData);
                }
                else {
                    if (typeof state.rows === 'undefined') {
                        commit('setRows', [new hardware()]);
                        resolve();
                    }
                }
                resolve();

            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 200 }));
                    reject(err);
                });
        })
    },

    getHardware({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/getHardwareOrder/' + rootState.orders.rows[0].orderID);
            response.then(async function (data) {
                var resultData = data.data;
                if (data.status === 204) {
                    commit('setHardwareOrder', new hardware());
                  //  commit('setRows', [new hardware({ quantity: 1, fromIPOrder: false })])
                }
                else
                    commit('setHardwareOrder', resultData);
                await dispatch('getHandsetsOnOrder');
                //await dispatch('getHandsetsFromIPVoice');

                dispatch('setMessage', new httpError({ message: "Hardware Retreived", httpStatus: 200 }));
                resolve(resultData);

            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 200 }));
                    reject(err);
                });
        })
    },
    getHandsets({ commit, dispatch }) {

        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/getHandsets/', '');
            response.then(function (data) {
                var resultData = data.data;
                commit('setHandset', resultData)
                resolve(resultData);
                return;
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 200 }));
                    reject(err);
                });
        })
    },
    saveHardware({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {

            var response = HTTP.post('order/CreateHardwareOrder/' + rootState.orders.rows[0].orderID, state.hardwareOrder);
            response.then(data => {
                if (data.status === 200) {
                    commit('setHardwareOrder', data.data);
                    commit('setHardware', data.data.hardwareOrderID, { root: true });
                    dispatch('setMessage', new httpError({ message: 'Saved OK', httpStatus: 200 }));
                }
                resolve(data.data);
            }).catch(err => {
                dispatch('setMessage', new httpError({ message: err.response.message, httpStatus: 500 }));
                reject(err);
            });
        })
    },
    saveHandsets({ commit, dispatch, state, rootState }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.post('order/AddHardwareToOrder/' + state.hardwareOrder.hardwareOrderID, state.rows);
            response.then(data => {
                if (data.status === 200) {
                    commit('clearRows');
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
