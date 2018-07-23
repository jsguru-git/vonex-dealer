<template>
    <v-app >
            <app-sidebar></app-sidebar>
       
        <v-content>
            <!--<order-state></order-state>-->
            <router-view />
           <!-- <app-notifications></app-notifications>-->
            <app-note v-show="$route.name !== 'Summary'">

            </app-note>

        </v-content>
       
        <v-footer :fixed="fixed" app>
            <span>&copy; 2018 Copyright: vonex.com</span>
        </v-footer>
        
    </v-app>
</template>
<script>

    import Vue from 'vue'
    import Header from './app-header'
    import SideBar from './app-sidebar'
    import Notification from './app-notification'
    import VeeValidate from 'vee-validate'
    import Vue2Filters from 'vue2-filters'
    Vue.use(Vue2Filters);
    var PhoneNumber = require('awesome-phonenumber');
    Vue.component('app-header', Header);
    Vue.component('app-sidebar', SideBar);
    Vue.component('app-notifications', Notification);

    import CustomerAddress from './customer-address'
    import Signature from './signature-pad'
    import MessageAlert from './MessageAlert'
    Vue.component('message-alert', MessageAlert);
    import DatePicker from 'vue2-datepicker'
    Vue.component('date-picker', DatePicker);
    import AppNote from './app-note'
    import Title from '../components/title.vue'
    Vue.component('v-title',Title)
    Vue.component('app-note', AppNote)

    Vue.component('customer-address', CustomerAddress)


    VeeValidate.Validator.extend('phonenumber', {
        // Custom validation message
        getMessage: (field) => `The ${field} is not a valid phone number.`,
        // Custom validation rule
        validate: (value) => new Promise(resolve => {

            var pn = new PhoneNumber(value, 'AU');

            resolve({
                valid: pn.isValid() && !pn.isMobile()
            });
        })
    });

    VeeValidate.Validator.extend('mobilenumber', {
        // Custom validation message
        getMessage: (field) => `The ${field} is not a valid mobile.`,
        // Custom validation rule
        validate: (value) => new Promise(resolve => {
            var pn = new PhoneNumber(value, 'AU');
            resolve({
                valid: pn.isMobile()
            });
        })
    });

    VeeValidate.Validator.extend('dateofbirth', {
        // Custom validation message
        getMessage: (field) => `The ${field} is not a valid birthdate.`,
        // Custom validation rule
        validate: (value) => new Promise(resolve => {
           // const [day, month, year] = value.substring(0, 10).split('/');
            var age = new Date(new Date - new Date(value)).getFullYear() - 1970;
            resolve({ valid: (age > 17 && age < 100) })
        })
    });
    VeeValidate.Validator.extend('ampt', {
        // Custom validation message
        getMessage: (field) => `The ${field} is not a valid AMPT domain.`,
        // Custom validation rule
        validate: (value) => new Promise(resolve => {

            resolve({ valid: (value.endsWith('vonex.biz') && value.length < 41) })
        })
    });
    export default {
        data() {
            return {
                clipped: true,
                fixed: true,
                title: 'Vonex',
                drawer: true,
            };
        },
        name: 'App',
      
    }


</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
