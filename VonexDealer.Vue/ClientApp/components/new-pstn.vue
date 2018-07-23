<template>
    <div>
        <v-btn name="newService" color="primary" @click="newServices" v-show="!isnewServices">Add New Services</v-btn>
        <div v-show="isnewServices">
            <v-title>
                New PSTN Service
            </v-title>
            <v-card>
                <v-card-text>
                    <v-container>
                        <v-flex sm3 md3 lg3>
                            <v-text-field type="number" min="0" name="services" label="Number of Services" v-model="NumberOfService" @keyup="changeRows" />
                        </v-flex>
                        <v-flex sm12 md12 lg12>
                            <table class="table table table-striped table-bordered mobile-table" v-if="rows.length > 0">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th> Terminate At</th>
                                        <th scope="col">
                                            <div style="float:left;">Contract Length</div>
                                            <div style="float:right;"> <button @click="copyDown('contractPeriod')"><i class="fas fa-paste" title="copy down"></i></button></div>
                                        </th>
                                        <th scope="col">
                                            <div style="float:left;"> Rate Plan</div>
                                            <div style="float:right;"> <button @click="copyDown('ratePlanID')"><i class="fas fa-paste" title="copy down"></i></button></div>
                                        </th>
                                        <th> Address <button v-on:click="AddAddress()" class="btn btn-default btn-sm">Add Address</button> </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(item, index) in rows">
                                        <td class="text-center">
                                            <div v-show="item.newPSTNID"><i class="fas fa-check-circle" style="color:green"></i></div>
                                        </td>
                                        <td :class="{'': true, 'danger': errors.has('termination'+index) }">
                                            <select class="form-control" v-model="item.termination" v-validate="'required'" :name="'termination'+index">
                                                <option value="1">Socket</option>
                                                <option value="2">MDF</option>
                                            </select>
                                        </td>
                                        <td :class="{'': true, 'danger': errors.has('contractPeriod'+index) }">
                                            <select class="form-control" v-model="item.contractPeriod" v-validate="'required'" :name="'contractPeriod'+index">
                                                <option :value="0">No Contract</option>
                                                <option :value="12">12 months</option>
                                                <option :value="24">24 months</option>
                                            </select>
                                        </td>
                                        <td :class="{'': true, 'danger': errors.has('ratePlanID'+index) }">
                                            <select v-model="item.ratePlanID" class="form-control" :name="'ratePlanID'+index">
                                                <option></option>
                                                <option v-for="rateplan in validRatePlans(item.contractPeriod)" :value="rateplan.ratePlanID">{{rateplan.ratePlanDescription}}</option>
                                            </select>
                                        </td>
                                        <td :class="{'': true, 'danger': errors.has('addressID'+index) }">
                                            <select class="form-control" name="StreetAddress" v-model="item.addressID" v-validate="'required'" :name="'addressID'+index">
                                                <option v-for="address in address.addresses" :value="address.addressID">{{address.formattedAddress}}</option>
                                            </select>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </v-flex>
                        <v-flex sm12 md12 lg12>
                            <p class="mb-0" v-show="isResidential">
                                Fees: If termination point is the 1st socket, and if there is an inactive inplace available the fee will only be $60.48,
                                if the technician needs to do any work the customer will be charged up to $306.48

                            </p>
                            <p class="mb-0" v-show="!isResidential">
                                Fees:
                                <ul>
                                    <li>
                                        MDF: if there is an inactive inplace available the fee will only be $60.48,
                                        if a technician is required the fee will be up to $306.48
                                    </li>
                                    <li>
                                        SOCKET: A base fee of $306.48 with Fee For Service charges on top which can be up to $135.00 for 30 minutes and vary depending on how much work is involved
                                    </li>
                                </ul>
                            </p>
                        </v-flex>

                        <v-flex sm12 md12 lg12>
                            <v-btn :disabled="errors.any()" @click="submit" color="primary">Save PSTN</v-btn>

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
    import { mapActions, createNamespacedHelpers } from 'vuex';
    import { mapMultiRowFields } from 'vuex-map-fields';
    import { mapNewPSTNFields, mapNewPSTNMultiRowFields, mapLandlineFields, mapCustomerFields, mapOrderFields } from '../store/modules/orders'
    const { mapActions: mapAddressActions } = createNamespacedHelpers(`orders/address`);
    const { mapActions: mapNewPSTNActions, mapMutations: mapNewPSTNMutations } = createNamespacedHelpers(`orders/pstn`);
    const { mapActions: mapLandlineActions } = createNamespacedHelpers(`orders/landline`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);

    Vue.use(VeeValidate);

    export default {
        data() {
            return {
                isValid: false,
                isnewServices: false,
                NumberOfService: 0,
            }
        },
        computed: {
            ...mapNewPSTNMultiRowFields(['rows']),
            ...mapNewPSTNFields(['message']),
            ...mapCustomerFields(['customer.customerID']),
            ...mapOrderFields(['rows[0].orderID']),
            ...mapOrderState(['ratePlan', 'isdn', 'address', 'pstn', 'isResidential']),

        },


        async mounted() {

            this.$nextTick(async () => {
                var vm = this;
                vm.NumberOfService = 0;
                vm.isnewServices = false;
                if (this.customerID > 0) {
                    var addresses = await vm.getAllAddresses(this.customerID);
                    var landline = await vm.getLandline();
                    var newServices = await vm.getNewPSTN();

                    await Promise.all([addresses, landline, newServices]);
                    vm.isnewServices = false;
                    if (typeof newServices === 'undefined') {
                        if (vm.rows.length > 0) {
                            vm.NumberOfService = vm.rows.length;
                            vm.isnewServices = true;
                        }
                    } else {
                        vm.NumberOfService = vm.rows.length;
                        vm.isnewServices = vm.rows.length > 0;
                        vm.$emit('saved', true);
                    }

                }
            })
        },
        methods: {
            ...mapAddressActions(['resetAddress', 'getAddress', 'addAddress', 'updateAddress', 'getAllAddresses']),
            ...mapLandlineActions(['addLandline', 'getLandline', 'updateLandline', 'removeLandline']),
            ...mapNewPSTNActions(['getNewPSTN', 'addNewPSTN', 'updateNewPSTN', 'removeNewPSTN', 'removeRows', 'setMessage']),
            ...mapNewPSTNMutations(['addRow',]),
            copyDown: function (column) {
                var vm = this;
                for (var i = 1; i < vm.rows.length; i++) {
                    vm.rows[i][column] = vm.rows[0][column];
                }
                return;
            },
            checkPSTN: function (ratePlan) {
                return ratePlan.ratePlanDescription.startsWith('PN') && !ratePlan.ratePlanDescription.includes('ISDN') && !ratePlan.ratePlanDescription.includes('Logical');
            },
            validRatePlans: function (months) {
                return this.ratePlan.rows.filter(e => e.ratePlanDescription !== null).filter(this.checkPSTN).filter(e => e.contractMonths == months);
            },
            submit: function () {
                var vm = this;
                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        vm.addNewPSTN().then(function (data) {
                            vm.isValid = true;
                            vm.$emit('saved', true);
                        }).catch(function (err) {
                            vm.isValid = false;
                            vm.setProgress({ message: err.message, httpStatus: err.status })
                        });
                    }
                    else {
                        vm.isValid = false;
                        vm.setProgress({ message: 'Not all entries are valid', httpStatus: 500 })
                    }
                })
            },
            newServices: function () {
                var vm = this;
                if (this.landlineID == 0)
                    this.addLandline().then(function (data) {
                        vm.isChurn = !vm.isChurn
                        vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });
                        vm.NumberOfService = 1;
                    });
                else {
                    vm.isChurn = true;
                    vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });
                    vm.NumberOfService = 1;
                }
                vm.isnewServices = true;
            },
            changeRows: function () {
                var vm = this;

                var oldVal = vm.rows.length;
                var newVal = vm.NumberOfService;

                if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                    this.$dialog.confirm('You are about to delete rows, are you sure?')
                        .then(function () {
                            vm.removeRows(newVal).then(function () {
                                vm.NumberOfService = vm.rows.length;
                            })
                                .catch(function (err) {
                                    vm.setMessage({ message: err, httpStatus: 500 });
                                });
                        })
                        .catch(function () {
                            //Clicked on cancel
                            vm.NumberOfService = oldVal;
                            return;
                        });

                }
                else {
                    if (newVal > this.rows.length) {

                        for (var i = oldVal; i < newVal; i++) {
                            vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });

                        }
                    }

                }
            }

        }
    }
</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
