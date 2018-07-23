import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';
import { Summary } from '../../../models/summary';
import axios from 'axios'

const state = {
    summary: new Summary(),
    message: new httpError(),
    orderStatus: 0,
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setSummary(state, summary) {
        state.summary = summary;
    },
    setProgress(state, message) {
        state.message = message;
    },
    updateField

}

//ACTIONS
const actions = {
    resetSummary({ commit }) {
        commit('setSummary', new Summary());
    },
    downloadPDF({ state, rootState }, html) {

        HTTP.defaults.responseType = 'blob';
        HTTP.post('Order/OutputPDF/', JSON.stringify({ orderID: rootState.orders.rows[0].orderID, htmlContent: html }))
            .then(function (data) {

                const url = window.URL.createObjectURL(new Blob([data.data], { type: 'application/pdf' }));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', rootState.orders.rows[0].orderID + '.pdf');
                document.body.appendChild(link);
                link.click();
            }).catch(function (err) {
                console.log('unable to download');
            })

    },
    sendEmail({ commit, state, dispatch, rootState }, emailOptions) {
        debugger;
        emailOptions.orderID = rootState.orders.rows[0].orderID;
        commit('setSummary', emailOptions);
        return new Promise((resolve, reject) => {


            var response = HTTP.post('Order/SendEmail/', emailOptions);
            response.then(function (data) {
                if (data.status === 200) {
                    dispatch('setMessage', new httpError({ message: 'sent successfully', httpStatus: 200 }));
                    commit('orders/setOrderStatus', 1, { root: true });
                    resolve(data.data);
                    return;
                }

            }).catch(function (err) {
                dispatch('setMessage', new httpError({ message: 'Error' + err, httpStatus: 500 }));
                reject(err);
                return;
            })

        })
    },
    populateSummary({ commit, dispatch, rootState }, guid) {
        return new Promise((resolve, reject) => {
            var orderID = 0;
            var url = 'Order/OrderReport/';
            if (typeof guid === 'undefined')
                orderID = rootState.orders.rows[0].orderID;
            else {
                orderID = guid;
                url = 'Order/OrderReportByGuid/';
            }

            var response = HTTP.get(url + orderID); //, {responseType: 'document'});
            response.then(function (data) {
                if (data.status === 200) {
                    
                    commit('orders/setOrder', data.data.order, { root: true });
                    resolve(data.data.htmlContent);
                }
                else
                    reject(data);

            }).catch(function (err) {
                dispatch('setMessage', new httpError({ message: 'Error' + err.message, httpStatus: 500 }));
                if (err.response.status == 401) {
                    reject(401);
                }
                reject(err.response.status);
            })

        })
    },
    setMessage({ commit }, message) {
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
