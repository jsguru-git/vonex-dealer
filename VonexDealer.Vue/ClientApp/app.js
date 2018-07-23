import Vue from 'vue'
import Vuetify from 'vuetify'
import axios from 'axios'
import store from './store'
import router from './router'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import auth from './auth/auth'
import 'vuetify/dist/vuetify.min.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import colors from 'vuetify/es5/util/colors'
// import BootstrapVue from 'bootstrap-vue'

// Vue.use(BootstrapVue);

// import 'bootstrap/dist/css/bootstrap.css'
// import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(auth)
Vue.use(Vuetify, {
    theme:
        {
            primary: "#2f5578",
            secondary: "#00c2f3",
            accent: "#7986CB",
            error: "#f44336",
            warning: "#44d9ff",
            info: "#2196f3",
            success: "#4caf50"
        }
})

sync(store, router)




const app = new Vue({
    store,
    auth,
    router,
    ...App,
})

export {
    store,
    auth,
    app,
    router,
}
