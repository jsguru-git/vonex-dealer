
import { HTTP } from '../../http-common';
import { createHelpers } from 'vuex-map-fields'
import { Order } from '../../models/Order'
import { httpError } from '../../models/httpError';
import axios from 'axios';
import { getField, updateField } from 'vuex-map-fields';

export const { mapFields: mapOrderFields } = createHelpers({
    getterType: `orders/getField`,
    mutationType: `orders/updateField`,
});

export const { mapMultiRowFields: mapOrderMultiRowFields } = createHelpers({
    getterType: `orders/getField`,
    mutationType: `orders/updateField`,
});



//import order from './forms/order';
import auth from './forms/auth';
import customer from './forms/customer';
import dealer from './forms/dealer';
import company from './forms/company';
import newChurn from './forms/NewChurn';
import landline from './forms/Landline';
import address from './forms/address';
import ratePlan from './forms/ratePlan';
import summary from './forms/summary';
import internet from './forms/Internet';
import isdn from './forms/NewISDN';
import pstn from './forms/NewPSTN';
import ipVoice from './forms/IPVoice';
import notes from './forms/note';
import hardware from './forms/hardware';
import handset from './forms/handset';


const actions = {
    setMessage({ commit, dispatch }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    updateSignature({ state, commit, dispatch }, signature) {
        return new Promise((resolve, reject) => {
            
            let header = HTTP.defaults.headers.common.Authorization;
            delete HTTP.defaults.headers.common.Authorization;
            var response = HTTP.post('/Order/addSignature/', signature);
            response.then(function (data) {
                HTTP.defaults.headers.common.Authorization = header;
                var resultData = data.data;
                commit('setSignature', signature.data);
                dispatch('setMessage', new httpError({ message: "Updated OK", httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error: " + err, httpStatus: 500 }));
                    reject(err);
                })
        })
    },
    getDealerFromEmail({ commit, state }) {
        return new Promise((resolve, reject) => {
            if (typeof state.auth.auth.name === 'undefined' || state.auth.auth.name === "Guest") {
                reject();
            }
            var response = HTTP.get('Order/getDealerFromEmail?email=' + state.auth.auth.name);
            response.then(function (data) {
                var resultData = data.data;
                commit('orders/dealer/ADD_DEALER_MUTATION', resultData, { root: true })
                resolve(data);
            })
                .catch((err) => {
                    console.log('error in loading');
                    reject(err);
                });
        });
    },
    async resetOrder({ commit, dispatch }) {
        await dispatch('customer/resetCustomer');
        await dispatch('newChurn/resetNewChurn');
        await dispatch('address/resetAddress');
        await dispatch('company/resetCompany');
        await dispatch('landline/resetLandline');
        await dispatch('pstn/resetPstn');
        await dispatch('isdn/resetISDN');
        await dispatch('ipVoice/resetIPVoice');
        await dispatch('summary/resetSummary');
        await dispatch('notes/resetNotes');
        await dispatch('ratePlan/getRatePlans');
        await dispatch('internet/resetInternet');
        await dispatch('hardware/reset');
        await dispatch('setMessage', new httpError());
        await commit('setOrder', new Order());
    },
    addOrder({ state, commit, dispatch, rootState }, customerID) {
        return new Promise((resolve, reject) => {

            if (customerID === 0) {
                reject('no customerID');
                return;
            }
            var dealerID = rootState.orders.dealer.dealer.dealerID;
            var order = new Order();
            order.dealerID = dealerID;
            order.customerID = customerID;

            var response = HTTP.post('Order/CreateOrder', order);
            response.then(function (data) {
                var resultData = data.data;
                commit('setOrder', resultData);
                resolve(resultData);
            })
                .catch((err) => {
                    
                    console.log('error in saving :' + err);
                    commit('setError');
                    reject(err);
                });
        })
    },
    listOrders({ commit }, orderID) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('Order/GetOrders/' + orderID);
            response.then(function (data) {
                var resultData = data.data;
                resolve([resultData]);
            })
                .catch((err) => {
                    console.log('error in loading :' + err);
                    reject(err);
                });
        })
    },
    async getOrder({ commit, dispatch }, orderID) {
        await dispatch('resetOrder');

        try {
            var data = await HTTP.get('Order/GetOrders/' + orderID);
        }
        catch (err) {
            console.log('error in loading :' + err);
            dispatch('setMessage', new httpError({ message: 'Error: ' + err, httpStatus: 500 }));
            return Promise.reject(err);
        };
        if (data.data) {
            var resultData = data.data[0];
            var order = await commit('setOrder', resultData);
            var cust = await dispatch('customer/getCustomer', resultData.customerID);
            if (resultData.customerDetails.companyID > 0) {
                var comp = await dispatch('company/getCompany', resultData.customerDetails.companyID);
            }
            dispatch('setMessage', new httpError({ message: 'Order retreived', httpStatus: 200 }));
            return Promise.resolve(resultData);
        }
    },
    addLandline({ commit, state, rootState }, landline) {
        commit('setLandline', landline.landlineID);
        return new Promise((resolve, reject) => {

            var response = HTTP.post('Order/addLandlineOrder/' + state.rows[0].orderID, landline);
            response.then(function (data) {
                var resultData = data.data;
                //commit('orders/landline/ADD_DEALER_MUTATION', resultData, { root: true })
                resolve(data);
            })
                .catch((err) => {
                    console.log('error in loading');
                    reject(err);
                });
        })

    }
};
const mutations = {
    setOrder(state, order) {
        if (typeof order !== 'undefined')
            state.rows.splice(0, 1, order);
    },
    setOrderStatus(state, status) {
        debugger;
        state.rows[0].orderStatus = status;
    },
    setProgress(state, message) {
        state.message = message;
    },
    setLandline(state, landlineID) {
        state.rows[0].landlineID = landlineID;
    },
    setSignature(state, signature) {
        state.rows[0].signature = signature;
        state.rows[0].orderStatus = 2;
    },
    setResidential(state, flag) {
        state.isResidential = flag;
    },
    setHardware(state, hardwareID) {
        state.rows[0].hardwareID = hardwareID;
    },
    updateField,
};

const getters = {
    hasOrder: (state, getters, rootState, rootGetters) => (
        rootGetters['orders/auth/isLoggedIn'] && state.rows[0].orderID > 0
    ),
    hasSignature: (state, getters, rootState, rootGetters) => (rootGetters['orders/auth/isLoggedIn'] && state.rows[0].signature),
    hasDealer: (state, getters, rootState, rootGetters) => (rootGetters['orders/auth/isLoggedIn'] && typeof state.dealer.dealer.dealerID != 'undefined' && state.dealer.dealer.dealerID > 0),
    hasAddress: (state, getters, rootState, rootGetters) => (rootGetters['orders/auth/isLoggedIn'] && state.address.address.addressID > 0),
    hasCustomer: (state, getters, rootState, rootGetters) => (rootGetters['orders/auth/isLoggedIn'] && state.customer.customer.customerID > 0),
    isSigned: (state, getters, rootState, rootGetters) => (rootGetters['orders/auth/isLoggedIn'] && state.rows[0].orderStatus > 0),
    getField
};

const state = {
    rows: [new Order()],
    message: new httpError(),
    isResidential: false,

}

const modules = {
    // order, auth, 
    auth, dealer, address, company, customer, newChurn, landline, ratePlan, summary, isdn, pstn, ipVoice, notes, internet, hardware, handset,
};

export const { mapFields: mapRatePlanFields } = createHelpers({
    getterType: `orders/ratePlan/getField`,
    mutationType: `orders/ratePlan/updateField`,
});

export const { mapFields: mapAuthFields } = createHelpers({
    getterType: `orders/auth/getField`,
    mutationType: `orders/auth/updateField`,
});

export const { mapFields: mapDealerFields } = createHelpers({
    getterType: `orders/dealer/getField`,
    mutationType: `orders/dealer/updateField`,
});


export const { mapFields: mapCompanyFields } = createHelpers({
    getterType: `orders/company/getField`,
    mutationType: `orders/company/updateField`,
});

export const { mapFields: mapCustomerFields } = createHelpers({
    getterType: `orders/customer/getField`,
    mutationType: `orders/customer/updateField`,
});
export const { mapFields: mapNewChurnFields } = createHelpers({
    getterType: `orders/newChurn/getField`,
    mutationType: `orders/newChurn/updateField`,
});

export const { mapMultiRowFields: mapNewChurnMultiRowFields } = createHelpers({
    getterType: 'orders/newChurn/getField',
    mutationType: 'orders/newChurn/updateField',
});

export const { mapFields: mapLandlineFields } = createHelpers({
    getterType: `orders/landline/getField`,
    mutationType: `orders/landline/updateField`,
});

export const { mapMultiRowFields: mapLandlineMultiRowFields } = createHelpers({
    getterType: 'orders/landline/getField',
    mutationType: 'orders/landline/updateField',
});

export const { mapFields: mapAddressFields } = createHelpers({
    getterType: `orders/address/getField`,
    mutationType: `orders/address/updateField`,
});
export const { mapMultiRowFields: mapAddressMultiRowFields } = createHelpers({
    getterType: 'orders/address/getField',
    mutationType: 'orders/address/updateField',
});

export const { mapMultiRowFields: mapRatePlanMultiRowFields } = createHelpers({
    getterType: 'orders/ratePlan/getField',
    mutationType: 'orders/ratePlan/updateField',
});
export const { mapFields: mapSummaryFields } = createHelpers({
    getterType: `orders/summary/getField`,
    mutationType: `orders/summary/updateField`,
});
export const { mapMultiRowFields: mapNewISDNMultiRowFields } = createHelpers({
    getterType: `orders/isdn/getField`,
    mutationType: `orders/isdn/updateField`,
});

export const { mapFields: mapNewISDNFields } = createHelpers({
    getterType: `orders/isdn/getField`,
    mutationType: `orders/isdn/updateField`,
});

export const { mapFields: mapNewPSTNFields } = createHelpers({
    getterType: `orders/pstn/getField`,
    mutationType: `orders/pstn/updateField`,
});

export const { mapMultiRowFields: mapNewPSTNMultiRowFields } = createHelpers({
    getterType: 'orders/pstn/getField',
    mutationType: 'orders/pstn/updateField',
});

export const { mapFields: mapIPVoiceFields } = createHelpers({
    getterType: `orders/ipVoice/getField`,
    mutationType: `orders/ipVoice/updateField`,
});

export const { mapMultiRowFields: mapIPVoiceMultiRowFields } = createHelpers({
    getterType: 'orders/ipVoice/getField',
    mutationType: 'orders/ipVoice/updateField',
});

export const { mapFields: mapNoteFields } = createHelpers({
    getterType: `orders/notes/getField`,
    mutationType: `orders/notes/updateField`,
});

export const { mapMultiRowFields: mapNoteMultiRowFields } = createHelpers({
    getterType: 'orders/notes/getField',
    mutationType: 'orders/notes/updateField',
});

export const { mapMultiRowFields: mapDealerMultiRowFields } = createHelpers({
    getterType: 'orders/dealer/getField',
    mutationType: 'orders/dealer/updateField',
});

export const { mapMultiRowFields: mapInternetMultiRowFields } = createHelpers({
    getterType: 'orders/internet/getField',
    mutationType: 'orders/internet/updateField',
});

export const { mapMultiRowFields: mapHardwareMultiRowFields } = createHelpers({
    getterType: 'orders/hardware/getField',
    mutationType: 'orders/hardware/updateField',
});

export const { mapFields: mapHardwareFields } = createHelpers({
    getterType: `orders/hardware/getField`,
    mutationType: `orders/hardware/updateField`,
});

export const { mapFields: mapInternetFields } = createHelpers({
    getterType: `orders/internet/getField`,
    mutationType: `orders/internet/updateField`,
});

//handset
export const { mapMultiRowFields: mapHandsetMultiRowFields } = createHelpers({
    getterType: 'orders/handset/getField',
    mutationType: 'orders/handset/updateField',
});

export const { mapFields: mapHandsetFields } = createHelpers({
    getterType: `orders/handset/getField`,
    mutationType: `orders/handset/updateField`,
});

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
    modules
}
