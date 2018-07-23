<template>
    <v-flex>
        <v-title>Internet</v-title>
        <v-dialog v-model="showModal" max-width="90%">
            <customer-address add="true" @close="showModal = false"></customer-address>
            <!--<address-modal v-if="showModal" v-on:closeModal="modalClosed"></address-modal>-->
        </v-dialog>
        <v-dialog v-model="showContact" max-width="90%">
            <contact-modal v-if="showContact" v-on:closeContact="contactClosed"></contact-modal>
        </v-dialog>

        <v-card>
            <v-card-text>
                <v-layout row wrap>
                    <v-flex sm6 md6 lg6>
                        <v-text-field type="number" name="services" min="1" v-model="NumberOfServiceInternet" @keyup="changeRows" label="Number of Services" />
                    </v-flex>
                    <v-flex sm12 md12 lg12>
                        <table class="table table table-striped table-bordered mobile-table" v-if="rows">
                            <thead>
                                <tr>
                                    <th>  </th>
                                    <th>Type</th>
                                    <th> Service Number </th>
                                    <th scope="col">
                                        <div style="float:left;">Contract Length</div>
                                        <div style="float:right;"> </div>
                                    </th>
                                    <th scope="col">
                                        <div style="float:left;"> Rate Plan</div>
                                        <div style="float:right;"></div>
                                    </th>
                                    <th> Address <v-btn v-on:click="showModal = true" color="primary" small>Add Address</v-btn></th>
                                    <th>On Site Contact  <v-btn small color="primary" v-on:click="showContact = true">Add Contact</v-btn></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item,index) in rows">
                                    <td align="center"><div v-show="item.internetID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                                    <td>
                                        <select v-model="item.internetType" :class="{'form-control': true, 'error': errors.has('internetType'+index) }" v-validate="'required'" :name="'internetType'+index">
                                            <option value="adsl">ADSL</option>
                                            <option value="nbn">NBN</option>
                                            <option value="efm">EFM</option>
                                            <option value="mbe">MBE</option>
                                            <option value="fibre">Fibre</option>
                                        </select>
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('serviceNumber'+index) }">
                                        <input type="text" v-model="item.serviceNumber" class="form-control" :name="'serviceNumber'+index" />
                                    </td>
                                    <td>
                                        <select v-model="item.contractTerm" :class="{'form-control': true, 'error': errors.has('contractTerm'+index) }" v-validate="'required'" :name="'contractTerm'+index">
                                            <option :value="0">No Contract</option>
                                            <option :value="12">12 months</option>
                                            <option :value="24">24 months</option>
                                            <option :value="36">36 months</option>
                                        </select>
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('ratePlanID'+index) }">
                                        <select v-model="item.ratePlanID" class="form-control" v-validate="'required'" :name="'ratePlanID'+index">
                                            <option></option>
                                            <option v-for="rateplan in validRatePlans(item)" :value="rateplan.ratePlanID">{{rateplan.ratePlanDescription}}</option>
                                        </select>




                                    </td>
                                    <td>
                                        <select class="form-control" :name="'streetAddress' + index" v-model="item.addressID">
                                            <option v-for="address in address.addresses" :value="address.addressID">{{address.formattedAddress}}</option>
                                        </select>
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('contact'+index) }">
                                        <select class="form-control" :name="'contact' + index" v-model="item.contactID" v-validate="item.internetType == 'mbe' || item.internetType == 'nbn' ? 'required' : ''">
                                            <option v-for="contact in contacts" :value="contact.contactID">{{contact.contactName}}</option>
                                        </select>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </v-flex>
                    <v-flex sm12 md12 lg12>
                        <v-btn :disabled="errors.any()" v-on:click="submit" color="primary">Save Internet</v-btn>
                    </v-flex>
                    <message-alert :component="message"></message-alert>
                   
                </v-layout>
            </v-card-text>
        </v-card>
    </v-flex>
</template>
<script>
    import { createNamespacedHelpers } from 'vuex';
    import { httpError } from '../models/httpError';
    import {
        mapOrderFields, mapAuthFields, mapCustomerFields, mapCompanyFields, mapInternetFields, mapAddressMultiRowFields, mapInternetMultiRowFields
    } from '../store/modules/orders';
    const { mapActions: mapAddressActions, } = createNamespacedHelpers(`orders/address`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    const { mapActions: mapInternetActions, mapMutations: mapInternetMutations } = createNamespacedHelpers(`orders/internet`);
    import ContactModal from './contact-modal.vue'


    export default {
        components: {
            ContactModal
        },
        data() {
            return {
                showModal: false,
                showContact: false,
                isDataLoading: false,
                NumberOfServiceInternet: 1,
                isSaved: false,
                dialog2: false,

            }
        },
        computed: {
            ...mapOrderState(['ratePlan', 'address', 'isResidential']),
            ...mapOrderState({ order: 'rows' }),
            ...mapInternetMultiRowFields(['rows', 'contacts']),
            ...mapInternetFields(['message']),
            ...mapCustomerFields(['customer.customerID']),

        },
        methods: {
            ...mapInternetActions(['setMessage', 'getInternet', 'addNewInternet', 'addContacts', 'getContacts', 'removeRows']),
            ...mapAddressActions(['resetAddress', 'getAddress', 'addAddress', 'updateAddress', 'getAllAddresses']),
            ...mapInternetMutations(['addRow']),


            validRatePlans: function (service) {
                var plans = this.ratePlan.rows.filter(e => e.contractMonths == service.contractTerm);
                switch (service.internetType) {
                    case 'adsl':
                        return plans.filter(e => e.usageType == 'DSL').filter(e => e.ratePlanDescription.startsWith('IDSL'));
                    case 'nbn':
                        return plans.filter(e => e.usageType == 'DSL').filter(e => e.ratePlanDescription.startsWith('NBN'));
                    case 'efm':
                        return plans.filter(e => e.usageType == 'Fibre').filter(e => e.ratePlanDescription.startsWith('EFM'));
                    case 'mbe':
                        return plans.filter(e => e.usageType == 'Fibre').filter(e => e.ratePlanDescription.startsWith('MBE'));
                    case "fibre":
                        return plans.filter(e => e.usageType == 'DSL').filter(e => e.ratePlanDescription.includes('FIB'));
                    default:
                        return null;
                }
            },
            changeRows: function () {
                var vm = this;
                var oldVal = vm.rows.length;
                var newVal = vm.NumberOfServiceInternet;

                if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                    this.$dialog.confirm('You are about to delete rows, are you sure?')
                        .then(function () {
                            vm.removeRows(newVal).then(function () {
                                vm.NumberOfServiceInternet = vm.rows.length;

                            }).catch(function (err) {
                                vm.NumberOfServiceInternet = vm.rows.length;
                            });


                        })
                        .catch(function () {
                            //Clicked on cancel
                            vm.NumberOfServiceInternet = oldVal;
                            return;
                        });

                }
                else {
                    if (newVal > this.rows.length)
                        for (var i = oldVal; i < newVal; i++) {
                            vm.addRow({ addressID: vm.address.addresses[0].addressID, months: 24 });

                        }

                }
            },
            submit: function () {
                var vm = this;

                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        vm.addNewInternet().then(function (data) {
                            vm.isSaved = true;
                        }).catch(function (err) {
                            vm.isValid = false;
                        });
                    }
                    else {
                        vm.isValid = false;
                        vm.setMessage({ message: 'unable to save', httpStatus: 500 })
                    }
                })

            },
            saveNext: function () {
                this.$router.push('create-hardware');
            },
            async modalClosed() {
                this.showModal = false;
                await await this.getAllAddresses(this.customerID);
            },
            async contactClosed(payload) {
                var vm = this;
                this.showContact = false;
                if (typeof payload !== 'undefined') {
                    vm.addContacts(payload);

                }
            },
        },

        async mounted() {

            this.$nextTick(async () => {
                var vm = this;
                vm.setMessage(new httpError());
                if (this.customerID > 0) {
                    var addresses = await vm.getAllAddresses(vm.customerID);
                    var contacts = await vm.getContacts(vm.order[0].orderID);
                    var internets = await vm.getInternet();
                    await Promise.all([addresses, internets, contacts]);
                    if (vm.rows.length > 0) {
                        vm.NumberOfServiceInternet = vm.rows.length;
                    }
                    else {
                        vm.NumberOfServiceInternet = 1;
                        vm.addRow({ addressID: addresses[0].addressID, months:24 });
                    }

                }
            })
        },

    }
</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
