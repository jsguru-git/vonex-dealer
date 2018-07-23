import Home from 'components/home'
import Callback from 'components/Callback'
import CustomerDetails from 'components/customer-details'
import CreateCompany from 'components/create-company'
import CreateCustomer from 'components/create-customer'
import CustomerOrders from 'components/customer-orders'
import OrderCreate from 'components/order-create'
import CreatePstn from 'components/create-pstn'
import CreateIPVoice from 'components/create-ipvoice'
import CreateInternet from 'components/create-internet'
import CreateMobile from 'components/create-mobile'
import CreateInbound from 'components/create-inbound'
import CreatePorting from 'components/create-porting'
import CreateHardware from 'components/create-hardware'
import Signature from 'components/signature-pad'
import OrderSummary from 'components/order-summary'
import EditDealers from 'components/edit-dealers'
import AppStatus from 'components/app-status'
import Login from 'components/login'
import store from './store'
import MaintainHandsets from 'components/maintain-handsets'
import MaintainRatePlans from 'components/maintain-rateplans'
import OrderApprove from 'components/order-approve'

const isLoggedIn = (to, from, next) => {

    if (store) {
        if (store.getters['orders/auth/isLoggedIn']) {
            next();
            return;
        }
        else
            next('/');
    }
}
const hasCustomer = (to, from, next) => {

    if (store) {
        if (store.getters['orders/customer/hasCustomer']) {
            next()
            return
        }
        else
            next('/')
    }
}
const hasDealer = (to, from, next) => {

    if (store) {
        // console.log(store);
        if (store.getters['orders/hasDealer']) {
            next()
            return
        }
        else
            next('/')
    }
}

const hasAddress = (to, from, next) => {

    if (store) {
        // console.log(store);
        if (store.getters['orders/hasAddress']) {
            next()
            return
        }
        else
            next('/')
    }
}



export const routes = [

    { path: '/', component: Home, name: 'home' }, //buttons here
    { path: '/Customers', component: CustomerDetails, name: 'Customers', beforeEnter: isLoggedIn },
    { path: '/create-company', component: CreateCompany, name: 'CreateCompany', beforeEnter: isLoggedIn },
    { path: '/create-customer', component: CreateCustomer, name: 'CreateCustomer', beforeEnter: isLoggedIn },
    { path: '/customer-orders', component: CustomerOrders, name: 'CustomerOrders', beforeEnter: isLoggedIn },
    { path: '/create-pstn', component: CreatePstn, name: 'CreatePstn', beforeEnter: hasAddress },
    { path: '/create-ipvoice', component: CreateIPVoice, name: 'CreateIPVoice', beforeEnter: hasAddress },
    { path: '/create-internet', component: CreateInternet, name: 'CreateInternet', beforeEnter: isLoggedIn },
    { path: '/create-mobile', component: CreateMobile, name: 'CreateMobile', beforeEnter: isLoggedIn && false },
    { path: '/create-inbound', component: CreateInbound, name: 'CreateInbound', beforeEnter: isLoggedIn && false },
    { path: '/create-porting', component: CreatePorting, name: 'CreatePorting', beforeEnter: isLoggedIn && false },
    { path: '/create-hardware', component: CreateHardware, name: 'CreateHardware', beforeEnter: isLoggedIn },
    { path: '/app-status', component: AppStatus, name: 'AppStatus', beforeEnter: isLoggedIn },
    { path: '/edit-dealers', component: EditDealers, name: 'EditDealers', beforeEnter: isLoggedIn },
    { path: '/signature', component: Signature, name: 'Signature', style: 'glyphicon glyphicon-education', beforeEnter: isLoggedIn },
    { path: '/order-summary', component: OrderSummary, name: 'Summary', style: 'glyphicon glyphicon-education', beforeEnter: isLoggedIn },
    { path: '/approve/:guid', component: OrderApprove, name: 'OrderApprove', style: 'glyphicon glyphicon-education' },
    {
        path: '/callback',
        name: 'callback',
        component: Callback
    },
    {
        path: '/login',
        name: 'Login',
        component: Login,
        //. beforeEnter: ifNotAuthenticated,
    },
    {
        path: '/maintain-handsets', component: MaintainHandsets, name: 'Handsets', style: 'glyphicon glyphicon-education', beforeEnter: isLoggedIn
    },
    {
        path: '/maintain-rateplans', component: MaintainRatePlans, name: 'RatePlans', style: 'glyphicon glyphicon-education', beforeEnter: isLoggedIn
    },
]
