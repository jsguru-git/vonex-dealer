<template>
    <div>
        <v-btn color="primary" name="isdnChurn" @click="isdnChurn" v-show="!isIsdnChurn">Add ISDN Churn</v-btn>
        <div v-show="isIsdnChurn">
            <v-title>ISDN Churn</v-title>
            <v-card>
                <v-card-text>
                    <v-container grid-list-md>
                        <v-flex sm12 md12 lg12>
                        </v-flex>
                        <v-flex sm2 md2 lg2>
                            <v-text-field type="number" min="0" name="services" v-model="NumberOfISDN" @keyup="changeRows" label="Number of Services" />
                        </v-flex>

                        <table class="table table table-striped table-bordered mobile-table" v-show="NumberOfISDN > 0">
                            <thead>
                                <tr>

                                    <th></th>
                                    <th> Contract Length </th>
                                    <th> Rate Plan </th>
                                    <th> Range </th>
                                    <th> Start of Range</th>
                                    <th> GDN </th>
                                    <th> Prime1 </th>
                                    <th> Prime2 </th>
                                    <th> Address <v-btn small color="primary" v-on:click="AddAddress()" >Add Address</v-btn> </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item, index) in rows" :key="index">

                                    <td align="center">
                                        <div v-show="item.isdnid">
                                            <i class="fas fa-check-circle" style="color:green"></i>
                                        </div>
                                    </td>

                                    <td :class="{'': true, 'danger': errors.has('contractLengthMonths'+index) }">
                                        <select class="form-control" v-model="item.contractLengthMonths">
                                            <option value="0">0 months</option>
                                            <option value="12">12 months</option>
                                            <option value="24">24 months</option>
                                        </select>
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('ratePlanID'+index) }">

                                        <select v-model="item.ratePlanID" class="form-control" :name="'ratePlanID'+index">
                                            <option></option>
                                            <option v-for="rateplan in validRatePlans(item.contractLengthMonths)" :value="rateplan.ratePlanID">{{rateplan.ratePlanDescription}}</option>
                                        </select>
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('rangeSize'+index) }">
                                        <select v-model="item.rangeSize" class="form-control" :name="'rangeSize'+index" @change="changeRange(item)" v-validate="'required'">
                                            <option></option>
                                            <option value="0">No Range </option>
                                            <option value="100">100 Range</option>
                                        </select>
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('numbers[0]'+index) }">

                                        <input :disabled="item.rangeSize == 0 ? true : false" type="text" class="form-control" v-model="item.numbers[0]"
                                               v-validate="item.rangeSize > 0 ? 'required|phonenumber' : ''" :name="'numbers[0]'+index" />
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('numbers[1]'+index) }">
                                        <input :disabled="item.rangeSize > 0 ? true : false" type="text" class="form-control" v-model="item.numbers[1]"
                                               :name="'numbers[1]'+index" />
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('numbers[2]'+index) }">
                                        <input :disabled="item.rangeSize > 0 ? true : false" type="text" class="form-control" v-model="item.numbers[2]"
                                               v-validate="item.rangeSize == 0 ? 'required|phonenumber' : ''" :name="'numbers[2]'+index" />
                                    </td>
                                    <td :class="{'': true, 'danger': errors.has('numbers[3]'+index) }">
                                        <input :disabled="item.rangeSize > 0 ? true : false" type="text" class="form-control" v-model="item.numbers[3]"
                                               :name="'numbers[3]'+index" />
                                    </td>

                                    <td :class="{'': true, 'danger': errors.has('addressID'+index) }">
                                        <select class="form-control" name="StreetAddress" v-model="item.addressID" v-validate="'required'" :name="'addressID'+index">
                                            <option v-for="address in address.addresses" :value="address.addressID">{{address.formattedAddress}}</option>
                                        </select>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                        <v-flex sm12 md12 lg12>
                            <v-btn color="primary" :disabled="errors.any()" v-on:click="submit" >Save ISDNs</v-btn>
                        </v-flex>
                        <message-alert :component="message"></message-alert>
                    </v-container>
                </v-card-text>
            </v-card>
        </div>
    </div>
</template>
<script>
    import Vue from 'vue'
    import VeeValidate from 'vee-validate'
    import { createNamespacedHelpers } from 'vuex';
    import {
        mapCustomerFields, mapNewISDNFields, mapNewISDNMultiRowFields

    } from '../store/modules/orders';
    import { mapAddressFields } from '../store/modules/forms/address';
    const { mapState: mapOrderState, } = createNamespacedHelpers(`orders`);
    const { mapActions: mapNewISDNActions, mapMutations: mapNewISDNMutations } = createNamespacedHelpers(`orders/isdn`);
    const { mapActions: mapLandlineActions } = createNamespacedHelpers(`orders/landline`);
    const { mapActions: mapAddressActions } = createNamespacedHelpers('orders/address');


    export default {
        data() {
            return {
                ISDNServices: false,
                NumberOfISDN: 0,
                isValid: false,
                isIsdnChurn: false,
            }
        },
        computed: {
            ...mapCustomerFields(['customer.customerID']),
            ...mapOrderState(['ratePlan', 'address']),
            ...mapNewISDNMultiRowFields(['rows']),
            ...mapNewISDNFields(['message']),

        },

        methods: {
            ...mapAddressActions(['resetAddress', 'getAddress', 'addAddress', 'updateAddress', 'getAllAddresses']),
            ...mapNewISDNActions(['addNewISDN', 'getISDN', 'resetNewISDN', 'removeNewISDN', 'updateNewISDN', 'submit', 'checkDuplicates', 'setMessage']),
            ...mapLandlineActions(['addLandline']),
            ...mapNewISDNMutations(['addRow', 'deleteRow']),
            isdnChurn: function () {
                var vm = this;

                if (vm.landlineID == 0)
                    vm.addLandline().then(function (data) {
                        vm.isIsdnChurn = true;
                        vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });
                        vm.NumberOfISDN = 1;
                    });
                else {
                    vm.isIsdnChurn = true;
                    vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });
                    vm.NumberOfISDN = 1;
                }
            },
            submit: async function () {
                var vm = this;
                var dups = await vm.checkDuplicates();

                await Promise.all([dups]);

                if (dups == false) {
                    vm.$validator.validateAll().then((result) => {
                        if (result) {
                            vm.addNewISDN().then(function (data) {
                                vm.isValid = true;
                                vm.$emit('saved', true);
                            }).catch(function (err) {
                                vm.isValid = false;
                                vm.$emit('saved', false);
                            });
                        }
                        else {
                            vm.isValid = false;
                            vm.setMessage({ message: 'Not all fields filled out', httpStatus: 500 })
                        }
                    })
                }
                else {
                    vm.setMessage({ message: "duplicates in numbers", httpStatus: 500 });
                }


            },
            checkISDN: function (ratePlan) {
                return ratePlan.ratePlanDescription.startsWith('PN') && ratePlan.ratePlanDescription.includes('ISDN');
            },
            validRatePlans: function (months) {
                return this.ratePlan.rows.filter(d => d.ratePlanDescription !== null).filter(this.checkISDN).filter(e => e.contractMonths == months);
            },
            changeRows: function () {

                var vm = this;

                var oldVal = 0 || vm.rows.length;
                var newVal = vm.NumberOfISDN;
                if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                    this.$dialog.confirm('You are about to delete rows, are you sure?')
                        .then(function () {
                            for (var i = newVal; i < oldVal; i++) {

                                vm.removeNewISDN(vm.rows[(vm.rows.length - 1)].isdnid).then(function () {
                                    vm.NumberOfServiceISDN = vm.rows.length;

                                }).catch(function (err) {
                                    vm.setMessage({ message: err, httpStatus: 500 });
                                });
                            }
                        })
                        .catch(function () {
                            //Clicked on cancel
                            vm.NumberOfServiceISDN = oldVal;
                            return;
                        });

                }
                else {

                    if (newVal > this.rows.length)
                        for (var i = oldVal; i < newVal; i++) {

                            vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });
                        }

                }
            },
            changeRange: function (row) {
                if (row.rangeSize > 0) {
                    for (var i = 1; i < 5; i++) {

                        row.numbers[i] = null;
                    }
                }
                else {
                    row.numbers[0] = null;
                }
            }
        },
        mounted() {
            this.$nextTick(() => {
                var vm = this;
                vm.NumberOfISDN = vm.rows.length;
                if (this.customerID > 0) {
                    this.getAllAddresses(this.customerID);
                    this.getISDN().then(function (data) {
                        vm.NumberOfISDN = vm.rows.length;
                        vm.isIsdnChurn = vm.rows.length > 0;
                        vm.$emit('saved', true);
                    });
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
