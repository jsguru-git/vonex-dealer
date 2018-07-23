import { createHelpers } from 'vuex-map-fields'
import { ISDNChurn } from '../../../models/ISDNChurn';
import { getField, updateField } from 'vuex-map-fields';

export const { mapFields: mapISDNFields } = createHelpers({
    getterType: `ISDN/getField`,
    mutationType: `ISDN/updateField`,
});

export const { mapMultiRowFields: mapISDNMultiRowFields } = createHelpers({
    getterType: 'ISDN/getField',
    mutationType: 'ISDN/updateField',
});

const state = () => ({
    ISDNChurn: new ISDNChurn()
})

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setISDNChurn(state, ISDNChurn) {
        if (!state.ISDNChurn.isISDN) {
            state.ISDNChurn.NumberOfISDN = 1;
            state.ISDNChurn.ISDNData.push({
                ISDNID: state.ISDNChurn.ISDNData.length + 1,
                ContractLengthMonths: 1,
                RatePlanID: 500,
                StartRange: 1000,
                EndRange: 1500,
                LandlineID: 1
            });
        }
        else {
            state.ISDNChurn.ISDNData = [];
            for (var i = 0; i < state.ISDNChurn.NumberOfISDN; i++) {
                state.ISDNChurn.ISDNData.push({
                    ISDNID: state.ISDNChurn.ISDNData.length + 1,
                    ContractLengthMonths: 1,
                    RatePlanID: 500,
                    StartRange: 1000,
                    EndRange: 1500,
                    LandlineID: 1
                });
            }
        }
    },
    updateField
}

//ACTIONS
const actions = {
    addISDNChurn({ state, commit }) {
        commit('setISDNChurn', state.ISDNChurn);
    },

    updateISDNChurn({ state, commit }) {
        commit('setISDNChurn', state.ISDNChurn);
    },

    resetISDNChurn({ state, commit }) {
        commit('setISDNChurn', new ISDNChurn());
    },
    setProgress(state, message) {
        state.message = message;
        if (message.httpStatus !== 500)
            setTimeout(() => {
                state.message = new httpError();
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
