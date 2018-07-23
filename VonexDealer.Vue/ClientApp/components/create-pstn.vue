<template>
    <div>
        <v-dialog v-model="showModal">
            <customer-address add="true" @close="showModal = false"></customer-address>
        </v-dialog>
        <v-btn name="churn" color="primary" @click="ChurnServices" v-show="!isChurn">Add Churn</v-btn>
        <v-title>PSTN Churn</v-title>
        <v-card>
            <v-card-text v-show="isChurn">
                <v-container grid-list-md>
                    <v-layout row wrap>
                        <v-flex xs2 sm2 md2>

                            <v-text-field label="Number of Services" name="services" min="1" v-model="NumberOfServiceChurn" @keyup="changeRows"></v-text-field>
                        </v-flex>
                        <v-flex sm12 md12 lg12>
                            <table class="table table-striped table-bordered mobile-table" v-show="rows.length > 0">
                                <thead>
                                    <tr>
                                        <th>  </th>
                                        <th> Service Number </th>
                                        <th scope="col">
                                            <div style="float:left;">Contract Length</div>
                                            <div style="float:right;"> <button @click="copyDown('contractPeriod')"><i class="fas fa-paste" title="copy down"></i></button></div>
                                        </th>
                                        <th scope="col">
                                            <div style="float:left;"> Rate Plan</div>
                                            <div style="float:right;"> <button color="primary" @click="copyDown('ratePlanID')"><i class="fas fa-paste" title="copy down"></i></button></div>
                                        </th>
                                        <th> Address <v-btn color="primary" v-on:click="AddAddress()">Add Address</v-btn></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(item,index) in rows">
                                        <td align="center"><div v-show="item.churnID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                                        <td :class="{'': true, 'danger': errors.has('serviceNumber'+index) }">
                                            <input type="text" v-model="item.serviceNumber" class="form-control"
                                                   v-validate="'required|phonenumber'" :name="'serviceNumber'+index" />
                                        </td>
                                        <td>
                                            <select v-model="item.contractPeriod" :class="{'form-control': true, 'error': errors.has('contractPeriod'+index) }" v-validate="'required'" :name="'contractPeriod'+index">
                                                <option :value="0">No Contract</option>
                                                <option :value="12">12 months</option>
                                                <option :value="24">24 months</option>
                                            </select>
                                        </td>
                                        <td :class="{'': true, 'danger': errors.has('ratePlanID'+index) }">
                                            <select v-model="item.ratePlanID" class="form-control" v-validate="'required'" :name="'ratePlanID'+index">
                                                <option></option>
                                                <option v-for="rateplan in validRatePlans(item.contractPeriod)" :value="rateplan.ratePlanID">{{rateplan.ratePlanDescription}}</option>
                                            </select>

                                        </td>
                                        <td>
                                            <select class="form-control" name="StreetAddress" v-model="item.addressID">
                                                <option v-for="address in address.addresses" :value="address.addressID">{{address.formattedAddress}}</option>
                                            </select>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>



                            <v-btn :disabled="errors.any()" v-on:click="submit" color="primary">Save Churns</v-btn>

                            <message-alert :component="message"></message-alert>

                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card-text>
        </v-card>

        <new-pstn @saved="isPSTNSaved = true"></new-pstn>

        <isdn-churn v-if="!isResidential" @saved="isISDNSaved = true"></isdn-churn>

        <v-btn @click="saveNext" color="primary" v-show="isAllSaved">Next</v-btn>

        <!--   <v-btn @click="saveNext" color="primary" v-show="!isAllSaved">Skip to IP Voice</v-btn>-->

    </div>
</template>
<script>
    import Vue from "vue";
    import isdnChurn from "../components/isdn-churn.vue";
    import newPstn from "../components/new-pstn.vue";
    import VueDialog from 'vuejs-dialog';
    Vue.use(VueDialog);
    import VeeValidate from "vee-validate";
    Vue.use(VeeValidate);
    import { createNamespacedHelpers } from "vuex";
    import {
        mapOrderFields,
        mapAuthFields,
        mapCustomerFields,
        mapCompanyFields,
        mapNewChurnFields,
        mapNewChurnMultiRowFields,
        mapLandlineFields,
        mapLandlineMultiRowFields,
        mapAddressMultiRowFields
    } from "../store/modules/orders";
    import { httpError } from "../models/httpError";
    import ratePlan from "../store/modules/forms/ratePlan";

    const { mapActions: mapAddressActions } = createNamespacedHelpers(
        `orders/address`
    );
    const { mapState: mapCustomerState } = createNamespacedHelpers(
        `orders/customer`
    );
    const {
        mapActions: mapLandlineActions,
        mapState: mapLandlineState
    } = createNamespacedHelpers(`orders/landline`);
    const {
        mapActions: mapNewChurnActions,
        mapMutations: mapNewChurnMutations
    } = createNamespacedHelpers(`orders/newChurn`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);

    export default {
        components: {
            isdnChurn,
            newPstn
        },
        data: function () {
            return {
                isLoading: true,
                isValid: false,
                showModal: false,
                NumberOfServiceChurn: 0,
                isChurn: false,
                isChurnSaved: false,
                isPSTNSaved: false,
                isISDNSaved: false
            };
        },

        computed: {
            ...mapCustomerFields(["customer.customerID"]),
            ...mapLandlineFields(["Landline.landlineID", "Landline.orderID"]),
            ...mapNewChurnMultiRowFields(["rows"]),
            ...mapNewChurnFields(["message"]),
            ...mapOrderFields(["rows[0].orderID"]),
            ...mapOrderState(["ratePlan", "isdn", "address", "pstn", "isResidential"]),
            isAllSaved() {
                return this.isChurnSaved && this.isPSTNSaved && this.isISDNSaved;
            }
        },
        mounted() {
            this.$nextTick(async () => {
                var vm = this;
                vm.setMessage(new httpError());
                if (this.customerID > 0) {
                    var addresses = await this.getAllAddresses(this.customerID);
                    var landline = await vm.getLandline();
                    var churns = await vm.getNewChurn();
                    await Promise.all([addresses, landline, churns]);
                    vm.isChurn = false;
                    if (typeof churns === "undefined") {
                        if (vm.rows.length > 0) {
                        }
                        else {
                            vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24});

                        }
                        vm.NumberOfServiceChurn = vm.rows.length;
                        vm.isChurn = true;
                    } else {
                        vm.NumberOfServiceChurn = vm.rows.length;
                        vm.isChurn = vm.rows.length > 0;
                        vm.isChurnSaved = true;
                    }
                }
            });
        },
        methods: {
            ...mapAddressActions([
                "resetAddress",
                "getAddress",
                "addAddress",
                "updateAddress",
                "getAllAddresses"
            ]),
            ...mapNewChurnActions([
                "checkDuplicates",
                "addNewChurn",
                "getNewChurn",
                "resetNewChurn",
                "removeNewChurn",
                "updateNewChurn",
                "submit",
                "removeRows",
                "setMessage"
            ]),
            ...mapNewChurnMutations(["addRow"]),
            ...mapLandlineActions([
                "addLandline",
                "getLandline",
                "updateLandline",
                "removeLandline"
            ]),
            copyDown: function (column) {
                console.log(column);
                var vm = this;
                for (var i = 1; i < vm.rows.length; i++) {
                    vm.rows[i][column] = vm.rows[0][column];
                }
                return;
            },
            saveChurn: function () {
                if (this.landlineID) {
                    this.addNewChurn(this.landlineID);
                } else {
                    this.addLandline().then(() => {
                        this.addNewChurn(this.landlineID);
                    });
                }
            },
            checkPSTN: function (ratePlan) {
                if (this.isResidential) {
                    return (
                        ratePlan.ratePlanDescription.startsWith("PSTN") &&
                        ratePlan.residential == true &&
                        !ratePlan.ratePlanDescription.includes("ISDN")
                    );
                } else
                    return (
                        ratePlan.ratePlanDescription.startsWith("PN") &&
                        !ratePlan.ratePlanDescription.includes("ISDN") &&
                        !ratePlan.ratePlanDescription.includes("Logical") &&
                        ratePlan.residential == false
                    );
            },
            validRatePlans: function (months) {
                if (this.ratePlan.rows && this.ratePlan.rows.length > 0)
                    return this.ratePlan.rows
                        .filter(e => e.ratePlanDescription !== null)
                        .filter(this.checkPSTN)
                        .filter(e => e.contractMonths == months);
                else return null;
            },
            submit: function () {
                var vm = this;
                vm
                    .checkDuplicates()
                    .then(function () {
                        vm.$validator.validateAll().then(result => {
                            if (result) {
                                vm
                                    .addNewChurn()
                                    .then(function (data) {
                                        vm.isChurnSaved = true;
                                    })
                                    .catch(function (err) {
                                        vm.isValid = false;
                                    });
                            } else {
                                vm.isValid = false;
                                vm.setMessage({ message: "unable to save", httpStatus: 500 });
                            }
                        });
                    })
                    .catch(function (err) {
                        vm.setMessage(
                            new httpError({
                                message: "Duplicate numbers in list",
                                httpStatus: 500
                            })
                        );
                        return Promise.reject(err);
                    });
            },
            AddAddress: function () {
                this.showModal = true;
            },
            async modalClosed() {
                this.showModal = false;
                await await this.getAllAddresses(this.customerID);
            },

            ChurnServices: function () {
                var vm = this;

                vm.isChurn = !vm.isChurn;
                vm.addRow(vm.address.addresses[0].addressID);
                vm.NumberOfServiceChurn = 1;
                vm.isSaved = false;
            },
            changeRows: function () {
                var vm = this;
                var oldVal = vm.rows.length;
                var newVal = vm.NumberOfServiceChurn;

                if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                    this.$dialog
                        .confirm("You are about to delete rows, are you sure?")
                        .then(function () {
                            vm
                                .removeRows(newVal)
                                .then(function () {
                                    vm.NumberOfServiceChurn = vm.rows.length;
                                })
                                .catch(function (err) {
                                    vm.NumberOfServiceChurn = vm.rows.length;
                                });
                        })
                        .catch(function () {
                            //Clicked on cancel
                            vm.NumberOfServiceChurn = oldVal;
                            return;
                        });
                } else {
                    if (newVal > this.rows.length)
                        for (var i = oldVal; i < newVal; i++) {
                            vm.addRow({ addressID: vm.address.addresses[0].addressID, contractPeriod: 24 });
                        }
                }
            },
            saveNext: function () {
                this.$router.push("create-ipvoice");
            }
        }
    };
</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
