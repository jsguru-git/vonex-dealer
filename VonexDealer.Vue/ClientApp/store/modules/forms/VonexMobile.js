import { createHelpers } from 'vuex-map-fields'
import { VonexMobileService } from '../../../models/VonexMobileService';
import { getField, updateField } from 'vuex-map-fields';

export const { mapFields: mapVonexMobileFields } = createHelpers({
    getterType: `vonexmobile/getField`,
    mutationType: `vonexmobile/updateField`,
});

export const { mapMultiRowFields: mapVonexMobileMultiRowFields } = createHelpers({
    getterType: 'vonexmobile/getField',
    mutationType: 'vonexmobile/updateField',
});

const state = () => ({
    VonexMobileService: new VonexMobileService()
});

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setVonexMobileService(state, VonexMobileService) {
        if (!state.VonexMobileService.isNewService) {
            state.VonexMobileService.NumberOfService = 1;
            state.VonexMobileService.extensionData.push({
                id: state.VonexMobileService.extensionData.length + 1,
                mobno: 2,
                planname: 'xyz',
                planno: null,
                value: null,
                simsize: 'Standard',
                simcardno: null,
                state: 'QLD',
                currentcareer: null,
                acno: null
            });
        }
        else {
            state.VonexMobileService.extensionData = [];
            for (var i = 0; i < state.VonexMobileService.NumberOfService; i++) {
                state.VonexMobileService.extensionData.push({
                    id: state.VonexMobileService.extensionData.length + 1,
                    mobno: 2,
                    planname: 'xyz',
                    planno: null,
                    value: null,
                    simsize: 'Standard',
                    simcardno: null,
                    state: 'QLD',
                    currentcareer: null,
                    acno: null
                });
            }
        }
    },
    setProgress(state, message) {
        state.message = message;
        if (message.httpStatus !== 500)
            setTimeout(() => {
                state.message = new httpError();
            }, 2000);
    },
    updateField
}

//ACTIONS
const actions = {
    addVonexMobileService({ state, commit }) {
        commit('setVonexMobileService', state.VonexMobileService);
    },

    updateVonexMobileService({ state, commit }) {
        commit('setVonexMobileService', state.VonexMobileService);
    },

    resetVonexMobileService({ state, commit }) {
        commit('setVonexMobileService', new VonexMobileService());
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
