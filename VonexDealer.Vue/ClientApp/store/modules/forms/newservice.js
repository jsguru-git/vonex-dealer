import { createHelpers } from 'vuex-map-fields'
import { NewServices } from '../../../models/NewServices';
import { getField, updateField } from 'vuex-map-fields';

export const { mapFields: mapNewServiceFields } = createHelpers({
    getterType: `newservice/getField`,
    mutationType: `newservice/updateField`,
});

export const { mapMultiRowFields: mapNewServiceMultiRowFields } = createHelpers({
    getterType: 'newservice/getField',
    mutationType: 'newservice/updateField',
});

const state = {
    NewServices: new NewServices()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setNewServices(state, NewServices) {
        if (!state.NewServices.isNewService) {
            state.NewServices.NumberOfService = 1;
            state.NewServices.serviceData.push({
                NewPSTNID: state.NewServices.serviceData.length + 1,
                Address: 1,
                Termination: 2,
                Fee: 2
            });
        }
        else {
            state.NewServices.serviceData = [];
            for (var i = 0; i < state.NewServices.NumberOfService; i++) {
                state.NewServices.serviceData.push({
                    NewPSTNID: state.NewServices.serviceData.length + 1,
                    Address: 1,
                    Termination: 2,
                    Fee: 2
                });
            }
        }
    },
    setProgress(state, message) {
        state.message = message;
       
    },
    updateField
}

//ACTIONS
const actions = {
    setMessage({ commit }, message) {
        commit('setProgress', message);
        if (message.httpStatus !== 500)
            setTimeout(() => {
                commit('setProgress', new httpError());
            }, 2000);
    },
    addNewServices({ state, commit }) {
        commit('setNewServices', state.NewServices);
    },

    updateNewServices({ state, commit }) {
        commit('setNewServices', state.NewServices);
    },

    resetNewServices({ state, commit }) {
        commit('setNewServices', new NewServices());
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
