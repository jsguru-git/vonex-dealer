import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { Company } from '../../../models/Company';
import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';
import { convertEnum } from '../../../models/entityType';

export const { mapFields: mapCompanyFields } = createHelpers({
    getterType: `company/getField`,
    mutationType: `company/updateField`,
});



const state = {
    company: new Company(),
    message: new httpError()
}

//getters

const getters = {
    soletrader(state) {
        if (state.company.entityType != null) {
            if (state.company.entityType.includes('T') || state.company.entityType === 'IND' || state.company.entityType === 'LGL')
                return true;
            else
                return false;
        }
    },
    getField
}

//Mutations
const mutations = {
    setCompany(state, company) {
        state.company = company;
    },
    setProgress(state, message) {
        state.message = message;
       
    },
    updateField

}

//ACTIONS
const actions = {
    resetCompany({ commit }) {
        commit('setCompany', new Company());
        commit('setProgress', new httpError());
    },
    getCompany({ commit, dispatch }, companyID) {
        var response = HTTP.get('Order/getCompany/' + companyID);
        response.then(async function (data) {
            var resultData = data.data;
          
            commit('setCompany', resultData)
            commit('orders/setResidential', false, { root: true });
            dispatch('setMessage', new httpError({ httpStatus: 200, message: "Retreived OK" }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ httpStatus: 500, message: "Error Retreiving:" + err }));
                return Promise.reject(err);

            });
    },
    addCompany({ state, commit, dispatch }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.post('Order/AddCompany', state.company);
            response.then(function (data) {
                var resultData = data.data;
                console.log(resultData);
                commit('setCompany', resultData);
                dispatch('setMessage', new httpError({ httpStatus: 200, message: "Company added OK" }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ httpStatus: 500, message: "Error Adding:" + err }));
                    reject(err);
                });
        })
    },
    async updateCompany({ state, commit, dispatch }) {
        var response = HTTP.put('Order/updateCompany/', state.company);
        response.then(function (data) {
            var resultData = data.data;
            console.log(resultData);
            commit('setCompany', resultData);
            dispatch('setMessage', new httpError({ httpStatus: 200, message: "Updated OK" }));
            return Promise.resolve(resultData);
        })
            .catch((err) => {
                dispatch('setMessage', "Error updating company :" + err);
                return Promise.reject(err);
            });
    },

    validateACN({ state, commit, dispatch }) {
        commit('setProgress', new httpError());
        var abn = state.company.acn.replace(/\s+/g, '');
        var response = HTTP.get('TotalCheck/ValidateABN/' + abn);
        response.then(function (result) {
            var data = result.data;
            console.log(data);
            if (data.result_count > 0) {
                var result = data.results[0];
                var companyResult = new Company();
                companyResult.otherOrganisation = false;
                companyResult.companyName = result.entity_name;
                if (result.other_names !== null ) {
                    companyResult.tradingName = result.other_names[result.other_names.length-1].other_name;
                    companyResult.entityType = result.entity_type_code || result.entity_name_type;
                }
                else {
                    companyResult.companyName = result.entity_name;
                    companyResult.entityType = result.entity_type_code;
                }
                if (!(companyResult.entityType.includes('T') || companyResult.entityType === 'IND' )) {

                    companyResult.abn = result.abn;
                    companyResult.acn = result.acn || result.abn;
                    companyResult.tradingName = result.entity_name;
                    companyResult.isdirectorSurnameRequired = false;
                    companyResult.isdateOfBirthRequired = false;
                    companyResult.otherOrganisation = false;
                }

                else {
                    companyResult.otherOrganisation = true;
                    companyResult.abn = result.abn;
                    companyResult.acn = result.acn || result.abn;
                    companyResult.companyName = result.entity_name;
                    companyResult.tradingName = result.other_names[0].other_name;
                    companyResult.isdirectorSurnameRequired = true;
                    companyResult.isdateOfBirthRequired = true;
                    companyResult.otherOrganisation = true;
                }
                commit('setCompany', companyResult);
                
                dispatch('setMessage', new httpError({ message: "Company retreived OK", httpStatus: 200 }));
                return Promise.resolve(companyResult);
            }
        })
            .catch((err) => {
                dispatch('setMessage', new httpError({ message: "Company Error:" + err, httpStatus: 500 }));
                return Promise.reject(err);
            });
        return response;
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
