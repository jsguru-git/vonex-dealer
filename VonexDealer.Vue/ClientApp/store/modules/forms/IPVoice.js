import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { IPVoiceService } from '../../../models/IPVoiceService';
import { ipOrder } from '../../../models/ipOrder'

import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';
import handset from './hardware';


const state = {
    iporder: new ipOrder(),
    rows: [],
    handsets: [],
    ratePlans: [],
    message: new httpError()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setRatePlan(state, ratePlan) {
        if (ratePlan !== null)
            state.ratePlans = ratePlan;
    },
    addRow(state, months) {
        if(state.rows.length > 0)
            state.rows.push(new IPVoiceService({contractLengthMonths: months, ratePlanID: state.rows[0].ratePlanID}));
        else
            state.rows.push(new IPVoiceService({ contractLengthMonths: months }));
    },
    setOrder(state, order) {
      
        state.iporder = order;
    },
    deleteRow(state) {

        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.pop();
    },
    setProgress(state, message) {
        state.message = message;

    },
    setNewData(state, Data) {
        // To enable multi-row form handling
        // we make it possible to add new rows.
        state.rows.length = 0;
        Data.forEach(function (service) {
            state.rows.push(service);
        })

    },
    removeRow(state, rowID) {
        state.rows.filter(function (el) {
            return el.ipExtensionID !== rowID;
        });
    },
    setHandset(state, handsets) {
        if (handsets !== null)
            state.handsets = handsets;
    },
    updateField

}

//ACTIONS
const actions = {
    resetIPVoice({ commit }) {
        commit('setNewData', []);
    },
    async saveIPOrder({ commit, dispatch, rootState, state }) {

        try {
                
            var order = await HTTP.post('order/AddIPOrder/' + rootState.orders.rows[0].orderID, state.iporder);
            commit('setOrder', order.data);
            dispatch('setMessage', new httpError({ message: 'saved OK', httpStatus: 200 }));
            return Promise.resolve(order.data);

        } catch (err) {
            dispatch('setMessage', new httpError({ message: 'unable to save:' + err, httpStatus: 500 }));
            return Promise.reject(err);
        }
    },
    async getIPVoice({ commit, dispatch, rootState }) {
        try {
            var order = await HTTP.get('order/GetIPOrder/' + rootState.orders.rows[0].orderID)
            if (order.data == null || order.data.length == 0) {
                order.data = new ipOrder();
            }
            commit('setOrder', order.data);
            return Promise.resolve(order);

        } catch (err) {
            dispatch('setMessage', new httpError({ message: 'Error getting order:' + err, httpStatus: 500 }));
            return Promise.reject(err);
        }

    },
    async getIPExtensions({ commit, dispatch, rootState }) {
        try {
            var order = await HTTP.get('order/GetIPExtensions/' + rootState.orders.rows[0].orderID)
            if (order.data.length > 0) {
                
            commit('setNewData', order.data);
            }
            return Promise.resolve(order.data);

        } catch (err) {
            dispatch('setMessage', new httpError({ message: 'Error getting order:' + err, httpStatus: 500 }));
            return Promise.reject(err);
        }

    },

    async addIPVoice({ state, commit, rootState }) {
        try {
            var order = await HTTP.post('order/AddIPOrder/' + rootState.orders.rows[0].orderID, state.iporder )

            var ipOrder = await HTTP.post('order/AddIPExtensionsToOrder/' + rootState.orders.rows[0].orderID, state.rows);
            commit('setNewData', ipOrder.data);
            commit('setProgress', new httpError({ message: 'Saved OK', httpStatus: 200 }));
            return Promise.resolve(ipOrder.data);
        } catch (err) {
            commit('setProgress', new httpError({ message: "Error Saving: " + err, httpStatus: 500 }));
            return Promise.reject(err);

        }

    },

    updateIPVoice({ state, commit }) {
        //commit('setIPVoiceService', state.IPVoiceService);
    },

    async removeRows({ state, commit, rootState }, newVal) {
      //rowCount is the number of rows to finish with
        var oldVal = 0 || state.rows.length;
        var rows = [];
        for (var i = oldVal; i > newVal; i--) {
            rows.push(state.rows[i-1]);
            commit('deleteRow');
        }
        try {
            var ipexts = await HTTP.delete('order/DeleteIPExtsFromOrder/' + rootState.orders.rows[0].orderID, { data: rows });
            return Promise.resolve(ipexts);

        } catch (e) {
            return Promise.reject(err);
        }
    },
    async getRatePlans({ commit }) {

        return new Promise((resolve, reject) => {
            var filter = ['VoIP', 'DID'];
            var response = HTTP.post('Order/getRatePlans/', JSON.stringify(filter));
            response.then(function (data) {
                var resultData = data.data;
                commit('setRatePlan', resultData)
                resolve(resultData);
            })
                .catch((err) => {
                    reject(err);
                });


        })
    },
    async getHandsets({ commit, dispatch }) {
        try {
            var data = await HTTP.get('Order/getHandsets/', '');
            if (data) {
                var resultData = data.data;
                commit('setHandset', resultData)

                var rateplans = await dispatch('getRatePlans');
                return Promise.resolve(resultData);
            }
        } catch (err) {

            dispatch('setMessage', new httpError({ message: "Error:" + err, httpStatus: 500 }));
            return Promise.reject(err);
        }
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
    mutations
}
