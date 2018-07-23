import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { Customer } from '../../../models/customer';

import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';

export const { mapFields: mapCustomerFields } = createHelpers({
    getterType: `customer/getField`,
    mutationType: `customer/updateField`,
});


const state = {
    customer: new Customer(),
    dealerCustomers: [],
    message: new httpError()
}

//getters

const getters = {
    hasCustomer: state => state.customer.customerID > 0,
    getField
}

//Mutations
const mutations = {
    setCustomer(state, customer) {
        state.customer = customer;
    },
    setUB(state, UBAccountNo) {
        state.customer.ubAccountNo = UBAccountNo;
    },
    setDealerCustomers(state, customers) {
        state.dealerCustomers.length = 0;
        customers.forEach(function (cust) {

            state.dealerCustomers.push(cust);
        });
    },
    setProgress(state, message) {
        state.message = message;

    },
    updateField

}

//ACTIONS
const actions = {
    addCustomerToUB({ commit, state, dispatch }) {
        return new Promise((resolve, reject) => {
            if (state.customer.ubAccountNo > 0) {
                resolve();
                return;
            }

            var response = HTTP.post('Customer/AddCustomerToUB', state.customer);
            response.then(data => {
                var result = data.data;
                commit('setUB', result.ubAccountNo);
                dispatch('setMessage', new httpError({ message: 'updated Customer', httpStatus: 200 }));
                resolve();
            }).catch(err => {
                dispatch('setMessage', new httpError({ message: 'unable to create UB ' + err, httpStatus: 500 }));
                reject(err);
            })
        })
    },

    resetCustomer({ commit }) {
        commit('setCustomer', new Customer());
    },
    async getCustomer({ commit, dispatch }, customerID) {
        var response = HTTP.get('Customer/GetCustomer/' + customerID);
        response.then(function (data) {
            var resultData = data.data;
            commit('setCustomer', resultData)
            dispatch('setMessage', new httpError({ message: "Retrieved OK", httpStatus: 200 }));
            return Promise.resolve();
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: 'Error:' + err, httpStatus: 500 }));
                return Promise.reject(err);
            });
    },
    async listCustomers({ commit }, dealerID) {
        try {
            var customers = await HTTP.get('Customer/GetDealerCustomers/' + dealerID);
            if (customers.data != null) {
                commit('setDealerCustomers', customers.data);
                return Promise.resolve(customers);
            }
        } catch (e) {
            return Promise.reject(e);
        }

    },
    async addCustomer({ state, commit, dispatch }, dealerID) {
        return new Promise((resolve, reject) => {

            var response = HTTP.post('Customer', state.customer);
            response.then(function (data) {
                var resultData = data.data;
                commit('setCustomer', resultData);
                dispatch('setMessage', new httpError({ message: "Submitted OK", httpStatus: 200 }));
                dispatch('orders/addOrder', state.customer.customerID, { root: true });
                //create order.
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error in Saving", httpStatus: 500 }));
                    reject(err);
                });
        })
    },
    async updateCustomer({ state, commit, dispatch, rootState }, customerID) {

        try {
            var response = await HTTP.put('Customer/' + customerID, state.customer);
            var resultData = response.data;
            commit('setCustomer', resultData);
            dispatch('setMessage', new httpError({ message: "Updated OK", httpStatus: 200 }));
            if (rootState.orders.rows[0].orderID === 0) {
                await dispatch('orders/addOrder', state.customer.customerID, { root: true });
                await dispatch('setMessage', new httpError({ message: 'Updated OK', httpStatus: 200 }));
                return Promise.resolve(resultData);
            }

        } catch (err) {

            dispatch('setMessage', new httpError({ message: "Error: " + err, httpStatus: 500 }));
            return Promise.reject(err);
        }
    },
    setMessage({ commit, }, message) {
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
