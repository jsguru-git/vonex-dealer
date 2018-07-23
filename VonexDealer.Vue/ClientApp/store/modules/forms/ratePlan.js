import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { ratePlan } from '../../../models/ratePlan';
import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';

export const { mapFields: mapRatePlanFields } = createHelpers({
    getterType: `ratePlan/getField`,
    mutationType: `ratePlan/updateField`,
});



const state = {
    rows: [],
    count: 0,
    message: new httpError()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setRatePlan(state, ratePlan) {
        state.rows = ratePlan.data;
        state.count = ratePlan.count;
    },
    setProgress(state, message) {
        state.message = message;
       
    },
    addRow(state) {
        state.rows.push(new ratePlan());
    },
    removeRatePlan(state, ratePlanID) {
        if (state.rows.length > 0) {
            for (var index = state.rows.length; index--;) {
                if (state.rows[index].ratePlanID === ratePlanID) {
                    state.rows.splice(index, 1);
                }

            }
        }
    },
    updateField

}

//ACTIONS
const actions = {

    async getRatePlans({ commit }, filter) {
        return new Promise((resolve, reject) => {
            var response = HTTP.post('support/getRatePlans/', filter == null ? {} : filter);
            response.then(function (data) {
                var resultData = data.data;
                commit('setRatePlan', resultData);
                
                resolve(resultData);
            })
                .catch((err) => {
                    reject(err);
                });
        })
    },
    removeRatePlan({ state, commit, dispatch }, ratePlanID) {
        var response = HTTP.delete('support/DeleteRatePlan/'+ ratePlanID );
        response.then(function (data) {
            var resultData = data.data;
            commit('removeRatePlan', ratePlanID)
            dispatch('setMessage', new httpError({ httpStatus: 200, message: "deleted OK" }));
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: "Error deleting ratePlan :" + err, httpStatus: 500 }));

            });
    },
  
    saveRatePlans({ state, commit, dispatch }) {
        var response = HTTP.post('support/UpdateRatePlans/', state.rows);
        response.then(function (data) {
            var resultData = data.data;
            console.log(resultData);
            commit('setRatePlan', resultData);
            dispatch('setMessage', new httpError({ httpStatus: 200, message: "Updated OK" }));
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: "Error updating ratePlan :" + err, httpStatus: 500 }));

            });
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
