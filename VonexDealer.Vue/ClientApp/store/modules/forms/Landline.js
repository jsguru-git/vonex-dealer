import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { Landline } from '../../../models/Landline';

import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';

export const { mapFields: mapLandlineFields } = createHelpers({
    getterType: `landline/getField`,
    mutationType: `landline/updateField`,
});

const state = {
    Landline: new Landline(),
    message: new httpError()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setLandline(state, Landline) {
        state.Landline = Landline;

        //state.Landline.Churns.push(new churn());
    },
    setProgress(state, message) {
        state.message = message;
       
    },
    updateField

}

//ACTIONS
const actions = {
    resetLandline({ commit }) {
        commit('setLandline', new Landline());
    },
    async getLandline({ commit,dispatch, rootState }) {
        return new Promise((resolve, reject) => {

            var orderID = rootState.orders.rows[0].orderID;
            if (orderID === 0)
                reject('no order ID');

            var response = HTTP.get('Landline/GetLandlinesOnOrder/' + orderID);
            response.then(function (data) {
                var resultData = data.data;
                if (resultData,length > 0) {
                    commit('setLandline', resultData, rootState);
                   // rootState.orders.rows[0].landlineID = resultData.landlineID;
                   dispatch('setMessage', new httpError({ message: 'landline retrieved', httpStatus: 200 }));
                    resolve(resultData);
                }
                else {
                    commit('setLandline', new Landline())
                    resolve(resultData);
                }
            })
                .catch((err) => {
                   dispatch('setMessage', new httpError({ message: "Error in loading" + err, httpStatus: 500 }));
                    reject(err);
                });
        })
    },
    async addLandline({ state, commit, dispatch, rootState }) {
        return new Promise((resolve, reject) => {
            if (rootState.orders.rows[0].landlineID > 0) {
                resolve(rootState.orders.rows[0].landlineID);
                return;
            }
            //new landline
            var landline = new Landline();
            landline.orderID = rootState.orders.rows[0].orderID;

            var response = HTTP.post('Landline/AddLandlines', landline);
            response.then(function (data) {
                var resultData = data.data;
                commit('setLandline', resultData);
                dispatch('orders/addLandline', resultData, { root: true });

               dispatch('setMessage', new httpError({ message: "Processed", httpStatus: 200 }));
                resolve(resultData)
            })
                .catch((err) => {
                   dispatch('setMessage', new httpError({ message: "Error: " + err, httpStatus: 500 }));
                    reject(err)
                });
        }
        )

    },
    async updateLandline({ state, commit, dispatch }, LandlineID) {

        var response = HTTP.put('UpdateLandline/' + LandlineID, state.Landline);
        response.then(function (data) {
            var resultData = data.data;
            //commit('setCustomer', resultData);

           dispatch('setMessage', new httpError({ message: "Processed", httpStatus: 200 }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
               dispatch('setMessage', new httpError({ message: "Error: " + err, httpStatus: 500 }));
                return Prommise.reject(err);
            });
    },
    async removeLandline({ commit, dispatch }, LandlineID) {

        var response = HTTP.get('RemoveLandline/' + LandlineID);
        response.then(function (data) {
            var resultData = data.data;
            //commit('setLandline', resultData)
           dispatch('setMessage', new httpError({ message: "Removed OK", httpStatus: 200 }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
               dispatch('setMessage', new httpError({ message: "Error: " + err, httpStatus: 200 }));
                return Promise.reject(err);
            });
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
