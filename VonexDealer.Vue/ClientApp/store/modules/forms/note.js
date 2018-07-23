import { HTTP } from '../../../http-common'
import { createHelpers } from 'vuex-map-fields'
import { Note } from '../../../models/note';
import { getField, updateField } from 'vuex-map-fields';
import { httpError } from '../../../models/httpError';


const state = {
    rows: [new Note()],
    message: new httpError()
}

//getters

const getters = {
    getField
}

//Mutations
const mutations = {
    setNote(state, notes) {
        state.rows = notes;
    },
    setProgress(state, message) {
        state.message = message;
    },
    addRow(state, component) {
        state.rows.push(new Note(component));
    },
    deleteNote(state, note) {
        var removeIndex = state.rows.map(function (row) { return row.noteID; }).indexOf(note);
        if (removeIndex > -1)
            state.rows.splice(removeIndex, 1);
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
    resetNotes({ commit }) {
        commit('setNote', [new Note()]);
    },
    addNote({ commit, rootState }, component) {
        commit('addRow', {
            component: component,
            orderID: rootState.orders.rows[0].orderID
        });
    },
    async getNotes({ commit, state, rootState }, component) {

        return new Promise((resolve, reject) => {
            var response = HTTP.get(`Order/getNotes/${rootState.orders.rows[0].orderID}/${component}`);
            response.then(function (data) {
                var resultData = data.data;
                if (resultData.length > 0)
                    commit('setNote', resultData)
                else
                    commit('setNote', []);
                resolve(resultData);
            })
                .catch((err) => {
                    reject(err);
                });
        })
    },
    saveNote({ state, commit, dispatch, rootState }) {
        return new Promise((resolve, reject) => {
            var response = HTTP.post('Order/AddNotesOrder/' + rootState.orders.rows[0].orderID, state.rows);
            response.then(function (data) {
                var resultData = data.data;
                commit('setNote', resultData);
                dispatch('setMessage', new httpError({ message: "Notes Added", httpStatus: 200 }));
                resolve(resultData);
            })
                .catch((err) => {
                    dispatch('setMessage', new httpError({ message: "Error Adding Note :" + err, httpStatus: 500 }));
                    reject(err);
                });
        })
    },
    delNote({ state, commit, dispatch, rootState }, note) {
        return new Promise((resolve, reject) => {
            if (note === 0) {
                commit('deleteNote', note);
                resolve(note);
                return;
            }

            var response = HTTP.delete('Order/DeleteNotesFromOrder/' + rootState.orders.rows[0].orderID, { data: note });
            response.then(function (data) {
                var resultData = data.data;
                if (resultData == true)
                    commit('deleteNote', note);
                resolve(resultData);

            }).catch(function (err) {
                dispatch('setMessage', new httpError({ messag: err, httpStatus: 500 }));
                reject(err);
            })
        })
    }


}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
