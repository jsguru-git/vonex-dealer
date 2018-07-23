import { HTTP } from '../../../http-common'
import { httpError } from '../../../models/httpError';

import { Address } from '../../../models/Address';

import { getField, updateField } from 'vuex-map-fields';
import { createHelpers } from 'vuex-map-fields'



export const { mapFields: mapAddressFields } = createHelpers({
    getterType: `address/getField`,
    mutationType: `address/updateField`,
});

export const { mapMultiRowFields: mapAddressMultiRowFields } = createHelpers({
    getterType: `address/getField`,
    mutationType: `address/updateField`,
});



const state = {
    address: new Address(),
    addresses: [],
    message: new httpError(),
    results: []
}

//getters

const getters = {
    // customer: state => state.customer,

    getField
}

//Mutations
const mutations = {
    setAddresses(state, addresses) {
        state.addresses.length = 0;
        addresses.forEach(addr => {
            state.addresses.push(addr);
        })
    },
    addAddresses(state, address) {
        state.addresses.push(address);
    },
    setAddress(state, address) {

        state.address = address;
    },
    setResults(state, results) {
        state.results = results;
    },
    setProgress(state, message) {
        state.message = message;
       
    },
    updateField

}

//ACTIONS
const actions = {
    resetAddress({ commit }) {
        commit('setAddress', new Address());
        commit('setAddresses', [new Address()]);
    },
    setMessage({ commit, }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    getAllAddresses({ commit, dispatch }, customerID) {
        return new Promise((resolve, reject) => {
            HTTP.get('Customer/GetAllCustomerAddress/' + customerID).then(function (data) {
                var resultData = data.data;
                commit('setAddresses', resultData);
                dispatch('setMessage', new httpError({ message: "Details OK", httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error in Get: " + err, httpStatus: 500 }));
                    reject(err);
                });
        })
    },
    getAddress({ commit , dispatch}, addressID) {
        var response = HTTP.get('Customer/getAddress/' + addressID);
        response.then(function (data) {
            var resultData = data.data;
            commit('setAddress', resultData);
            dispatch('setMessage', new httpError({ message: "Details OK", httpStatus: 200 }));

        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: "Error in Get: " + err, httpStatus: 500 }));
            });
    },
    async addAddressToCust({ commit, state, dispatch, rootState }) {
        var data = await HTTP.post('Customer/addAddresstoCustomer/' + rootState.orders.customer.customer.customerID, state.address);

        if (data) {
            var resultData = data.data;
            commit('setAddress', resultData);
            commit('addAddresses', resultData); // add to array for commiting to new order
            dispatch('setMessage', new httpError({ message: "Address added OK", httpStatus: 200 }));
            return Promise.resolve(resultData);
        }
        else {
            (err) => {
                dispatch('setmessage', new httpError({ message: "Error Adding Address :" + err, httpStatus: 500 }));
                return Promise.reject(err);
            }
        }
    },
    async updateAddressToCust({ commit, state, dispatch, rootState }, customerID) {

        var response = HTTP.put('Customer/updateAddresstoCustomer/' + rootState.orders.customer.customer.customerID, state.address);
        dispatch('setMessage', new httpError({ message: "updating Address", httpStatus: 200 }));

        response.then(async function (data) {
            var resultData = data.data;
            await commit('setAddress', resultData);
            await dispatch('setMessage', new httpError({ message: "Address updated", httpStatus: 200 }));
            return Promise.resolve(resultData)
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: "Error updating Address: " + err, httpStatus: 500 }));
                return Promise.reject(err);

            });
    },
    addAddress({ commit, dispatch, state }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.post('Customer/addAddress/', state.address);

            response.then(function (data) {
                var resultData = data.data;
                commit('setAddress', resultData);
                commit('addAddresses', resultData);
                dispatch('setMessage', new httpError({ message: "added Address", httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error updating Address: " + err, httpStatus: 500 }));
                    reject(err);
                })
        })
    },
    async  updateAddress({ commit, dispatch, state }) {
        var response = HTTP.put('Customer/updateAddress/', state.address);

        response.then(function (data) {
            var resultData = data.data;
            console.log(resultData);
            commit('setAddress', resultData);
            dispatch('setMessage', new httpError({ message: "updated Address: " + err, httpStatus: 200 }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: "Error updating Address: " + err, httpStatus: 500 }));
                return Promise.reject(err);
            });
    },
    async searchAddress({ commit, dispatch }, streetAddress) {
        if (streetAddress != null && streetAddress.length >= 5) {
            var response = HTTP.post('validateAddress/',
                {
                    "country": "AU",
                    "formatted_address": streetAddress
                }
            );
        
            response.then(function (data) {
                var results = data.data.results;
                commit('setResults', results);
                return Promise.resolve(results);
            })
                .catch(function (err) {
                    dispatch('setMessage', new httpError({ message: 'Error getting address', httpStatus: 500 }));
                    return Promise.reject(err)
                });
        }
    },
    completeAddress({ commit, state}, e) {
        var match = state.results.filter(x => x.formatted_address === e);
        if (match.length > 0) {
            var splitStreetAddress = e.split(" ");
            let addr = new Address();
            addr.suburb = match[0].suburb;
            addr.state = splitStreetAddress[splitStreetAddress.length - 3];
            addr.postCode = match[0].postcode;
            addr.streetAddress = e.substring(0, e.indexOf(','));
            commit('setAddress', addr);
            commit('setResults', []);
        }
        return null;
    },
}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
