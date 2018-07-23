import { HTTP } from '../../../http-common';
import { getField, updateField } from 'vuex-map-fields';
import { Order } from '../../../models/Order'
import { httpError } from '../../../models/httpError';
import { Dealer } from '../../../models/Dealer';
import { createHelpers } from 'vuex-map-fields'


export const { mapMultiRowFields: mapOrderMultiRowFields } = createHelpers({
    getterType: `orders/order/getField`,
    mutationType: `orders/order/updateField`,
});



const mutations = {
    setOrder(state, order) {
        //state.rows.push(order);
        state.rows.splice(0, 1, order);

    },
    setProgress(state, message) {
        state.message = message;
        if (message.httpStatus !== 500)
            setTimeout(() => {
                state.message = new httpError();
            }, 2000);
    },
    updateField
};


const getters = {

    getField
};

const state = {
    rows: [new Order()],
    message: new httpError()
}

//ACTIONS
const actions = {
    resetOrder({ commit }) {
        commit('setOrder', new Order());
    },
    getOrder({ commit }, orderID) {
        var response = HTTP.get('Order/GetOrders/' + orderID);
        response.then(function (data) {
            var resultData = data.data;
            commit('setOrder', resultData)
            commit('setProgress', "Retrieved OK");

        })
            .catch((err) => {
                console.log('error in loading :' + err);
            });
    },
    addOrder({ state, commit, rootState }) {
        var dealerID = rootState.orders.dealer.dealer.dealerID;
        var order = new Order();
        order.dealerID = dealerID;
        return new Promise((resolve, reject) => {
            var response = HTTP.post('Order', order);
            response.then(function (data) {
                var resultData = data.data;
                commit('setOrder', resultData);
                commit('setProgress', "Submitted OK");
                resolve(resultData);
            })
                .catch((err) => {
                    console.log('error in saving :' + err);
                    commit('setProgress', "Error in Saving");
                    reject(err);
                });
        })
    },
    updateOrder({ state, commit, dispatch }, orderID) {
        var response = HTTP.put('Order/' + orderID, state.order);
        response.then(function (data) {
            var resultData = data.data;
            commit('setOrder', resultData);
            commit('setProgress', "Updated OK");
        })
            .catch((err) => {
                commit('setProgress', "Error: " + err);
                console.log('error in saving');
            });
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions,
}
