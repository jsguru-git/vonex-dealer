<template>
    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">

            <address-modal v-if="showModal" v-on:closeModal="showModal = false"></address-modal>

            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        FIXED LINE (PHONE)
                    </h3>
                </div>
                <div class="panel-body" style="padding:0;padding-top:20px">
                    <v-btn  name="churn" color="primary" @click="ChurnServices" v-show="!isChurn" >Add Churn</v-btn>
                </div>
            </div>
            <div class="panel panel-default Custom_form" id="churn" v-show="isChurn">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Churn
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Number of services</label>
                                        <input type="number" name="services" min="0" class="form-control" v-model="NumberOfServiceChurn" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <table class="table table table-striped table-bordered mobile-table" v-show="rows.length > 0">
                        <thead>
                            <tr>
                                <th> ID </th>
                                <th> Contract Period </th>
                                <th> Service Number </th>
                                <th> Rate Plan </th>
                                <th> Address <button v-on:click="AddAddress()" class="btn btn-default btn-sm">Add Address</button></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item,index) in rows" :key="index">
                                <td><span>{{item.churnID}}</span></td>
                                <td>
                                    <select requried v-model="item.contractPeriod" :class="{'form-control': true, 'error': errors.has('contractPeriod'+index) }" v-validate="'required'" :name="'contractPeriod'+index">
                                        <option :value="0">No Contract</option>
                                        <option :value="12">12 months</option>
                                        <option :value="24">24 months</option>
                                    </select>
                                </td>
                                <td>
                                    <input type="text" v-model="item.serviceNumber"
                                           :class="{'form-control': true, 'error': errors.has('serviceNumber'+index) }" v-validate="'required|phonenumber'" :name="'serviceNumber'+index" />
                                </td>
                                <td>
                                    <select v-model="item.ratePlan" class="form-control">
                                        <option></option>
                                        <option v-for="i in RatePlans" :value="i">{{i}}</option>
                                    </select>
                                </td>
                                <td>
                                    <select class="form-control" name="StreetAddress" v-model="item.addressID">
                                        <option value="-1">--Select Address--</option>

                                        <option v-for="address in addresses" :value="address.addressID">{{address.streetAddress}}</option>
                                    </select>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <v-btn :disabled="errors.any()" v-on:click="submit" color="primary">Save Churns</v-btn>

                            </div>

                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row" style="margin-top:20px" v-show="message.message.length > 0">
                        <div class="col-md-12">
                            <div :class="submitted">
                                {{message.message}}
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
</template>
<script>
    import Vue from 'vue'
    import VuejsDialog from "vuejs-dialog"
    import { createNamespacedHelpers } from 'vuex';
    import {
        mapOrderFields, mapAuthFields, mapCustomerFields, mapCompanyFields, mapNewChurnFields, mapNewChurnMultiRowFields, mapLandlineFields, mapLandlineMultiRowFields, mapAddressMultiRowFields

    } from '../store/modules/orders';


    import autocomplete from 'vue2-autocomplete-js';
    var PhoneNumber = require('awesome-phonenumber');

    const { mapActions: mapAddressActions } = createNamespacedHelpers(`orders/address`);
    const { mapState: mapCustomerState } = createNamespacedHelpers(`orders/customer`);
    const { mapActions: mapLandlineActions, mapState: mapLandlineState } = createNamespacedHelpers(`orders/landline`);
    const { mapActions: mapNewChurnActions, mapMutations: mapNewChurnMutations } = createNamespacedHelpers(`orders/newChurn`);

    Vue.use(VuejsDialog);
    export default {
        components: { autocomplete },
        data: function () {
            return {
                isValid: false,
                showModal: false,
                NumberOfServiceChurn: 0,
                isChurn: false,
            }
        },
        watch: {

            NumberOfServiceChurn: {
                handler: function (newVal, oldVal) {

                    oldVal = oldVal || 0
                    newVal = newVal || 0
                    var vm = this;
                    if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                        this.$dialog.confirm('You are about to delete rows, are you sure?')
                            .then(function () {
                                for (var i = newVal; i < oldVal; i++) {

                                    vm.removeNewChurn(vm.rows[(vm.rows.length - 1)].churnID).then(function () {
                                        this.setProgress({ message: 'row removed' });
                                        vm.NumberOfServiceChurn = vm.rows.length;

                                    }).catch(function (err) {
                                        this.setProgress({ message: err });
                                    });
                                }

                            })
                            .catch(function () {
                                //Clicked on cancel
                                vm.NumberOfServiceChurn = oldVal;
                                return;
                            });

                    }
                    else {
                        if (newVal > this.rows.length)
                            vm.addRow();

                    }
                },
                deep: true
            },

        },
        computed: {
            ...mapCustomerFields(['customer.customerID']),
            ...mapLandlineFields(['Landline.landlineID', 'Landline.orderID']),
            ...mapAddressMultiRowFields(['addresses']),
            ...mapNewChurnMultiRowFields(['rows']),
            ...mapNewChurnFields(['message']),
            ...mapOrderFields(['rows[0].orderID']),
            submitted: function () {
                if (!this.message.httpStatus || this.message.httpStatus == 200) {
                    return 'alert alert-success';
                } else
                    return 'alert alert-danger';
            },
        },
        mounted() {
            this.$nextTick(() => {
                var vm = this;
                if (this.customerID > 0) {
                    this.getAllAddresses(this.customerID);
                    this.getLandline();
                    this.getNewChurn().then(function (data) {
                        vm.NumberOfServiceChurn = vm.rows.length;
                        vm.isChurn = vm.rows.length > 0;
                    });
                }


            })
        },
        methods: {
            ...mapAddressActions(['resetAddress', 'getAddress', 'addAddress', 'updateAddress', 'getAllAddresses']),
            ...mapNewChurnActions(['addNewChurn', 'getNewChurn', 'resetNewChurn', 'removeNewChurn', 'updateNewChurn', 'submit']),
            ...mapNewChurnMutations(['addRow', 'setProgress']),

            ...mapLandlineActions(['addLandline', 'getLandline', 'updateLandline', 'removeLandline']),
            saveChurn: function () {
                if (this.landlineID) {

                    this.addNewChurn(this.landlineID);
                }
                else {
                    this.addLandline().then(() => {
                        this.addNewChurn(this.landlineID);
                    });
                }
            },
            submit: function () {
                var vm = this;
                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        
                        this.addNewChurn().then(function (data) {
                            vm.isValid = true;
                        }).catch(function (err) {
                            vm.isValid = false;
                            });
                    }
                    else {
                        this.isValid = false;
                        this.setProgress({ message: 'busted' })
                    }
                })
            },
            AddAddress: function () {
                this.showModal = true;
            },
          
            ChurnServices: function () {
                var vm = this;

                if (this.landlineID == 0)
                    this.addLandline().then(function (data) {
                        vm.isChurn = !vm.isChurn
                    });


            },


        },
        
    }
</script>
