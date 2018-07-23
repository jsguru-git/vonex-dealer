import Vue from 'vue'
import Vuex from 'vuex'

import createPersistedState from 'vuex-persistedstate'
import * as Cookies from 'js-cookie'
import orders from './modules/orders'
//import dealer from './modules/forms/dealer'
//import customer from './modules/forms/customer'
//import address from './modules/forms/address'
//import company from './modules/forms/company'
//import newservice from './modules/forms/newservice'
//import ISDN from './modules/forms/ISDN'
//import vonexmobile from './modules/forms/VonexMobile'
//import IPVoice from './modules/forms/IPVoice'
//import auth from './modules/forms/auth'
//import NewISDN from './modules/forms/NewISDN'
//import NewPSTN from './modules/forms/NewPSTN'
//import NewChurn from './modules/forms/NewChurn'
//import Landline from './modules/forms/Landline'


Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    strict: debug,
    plugins: [
           createPersistedState()
    ],
    modules: {
        //auth,
        orders,
        //dealer,
        //customer,
        //address,
        //company,
        //newservice,
        //ISDN,
        //vonexmobile,
        //IPVoice,
        //NewISDN,
        //NewPSTN,
        //NewChurn,
        //Landline,
    },
});
