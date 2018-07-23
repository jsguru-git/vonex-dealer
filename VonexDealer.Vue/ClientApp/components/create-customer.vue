<template>
    <div>
        <customer-detail>
            <span v-if="isResidential">Account Holder</span>
            <span v-if="!isResidential">Authorised Representative</span>
        </customer-detail>
        <customer-address v-if="customerID>0"></customer-address>

    </div>
</template>
<script>
    import Vue from 'vue'
    import CustomerDetail from '../components/customer-detail'
    Vue.component('customer-detail', CustomerDetail);
    import { createNamespacedHelpers, mapGetters } from 'vuex';

    import {
        mapCustomerFields,
    } from '../store/modules/orders';
    const { mapState: mapOrderState, mapGetters: mapOrderGetters } = createNamespacedHelpers(`orders`);


    export default {

        data() {
            return {
                isDataLoading: true
            }
        },
        computed: {
            ...mapCustomerFields(['customer.customerID'], ),
            ...mapOrderState(['auth', 'message', 'isResidential', 'rows', 'address', 'company', 'customer', 'dealer', 'landline', 'newChurn']),

        }



    }
</script>
