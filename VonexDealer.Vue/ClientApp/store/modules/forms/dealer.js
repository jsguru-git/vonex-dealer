import { HTTP } from '../../../http-common';
import { Dealer } from '../../../models/Dealer';

import { getField, updateField } from 'vuex-map-fields';
import { createHelpers } from 'vuex-map-fields'
import { httpError } from '../../../models/httpError';

export const { mapFields: mapDealerFields } = createHelpers({
    getterType: `dealer/getField`,
    mutationType: `dealer/updateField`,
});



const state = () => ( {
    dealer: new Dealer(),
    dealers: [],
    message: new httpError(),
})


const actions = {
   
    getDealer({ commit }) {
        var response = HTTP.get('Order/getDealer/' + state.dealerID);
        response.then(function (data) {
            var resultData = data.data;
            commit('ADD_DEALER_MUTATION', resultData)

        })
            .catch((err) => {
                console.log('error in loading');
            });
    },
    addDealer({ state, commit }) {
        commit('setDealer', state.dealer);
    },
    listDealers({ commit }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.get('order/listDealers');
            response.then(data => {
                if (data.status !== 200)
                    resolve();
                var result = data.data;
                commit('addDealers', result);
                resolve(result);
            }).catch(err => {
                reject(err);
            })

        })
    },
    saveDealers({ commit, dispatch }) {
        return new Promise((resolve, reject) => {

        })
        
    },
    setMessage({ commit }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },


};


const mutations = {

    ADD_DEALER_MUTATION: function (state, dealer) {
        state.dealer = dealer;
    },
    setProgress(state, message) {
        state.message = message;

    },
    addDealers(state, dealers) {
        state.dealers = dealers;
    },
    updateField
};
const getters = {
    
    dealer: state => {
        return state.dealer;
    },
    hasDealer: state => state.dealer.dealerID > 0,
    getField
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
