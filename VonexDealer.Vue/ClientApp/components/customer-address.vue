<template>
    <div>
        <v-title>Address Details</v-title>
        <v-card>
            <v-card-text>
                <v-form method="post" @submit.prevent="addCustomerAddress">
                    <v-container grid-list-md>
                        <v-layout wrap>
                            <v-flex sm6 md6 lg6>
                                <v-select :items="results"
                                          label="Street Address"
                                          autocomplete
                                          :search-input.sync="search"
                                          v-model="streetAddress"
                                          item-text="formatted_address"
                                          item-value="formatted_address"
                                          @change="completeAddress"
                                          :hint="`${streetAddress}`"
                                          persistent-hint
                                          >
                                    <template slot="nodata" slot-scope="data">

                                    </template>
                                </v-select>
                                
                                <span v-show="errors.has('streetAddress')" class="text-danger">{{ errors.first('streetAddress') }}</span>
                            </v-flex>
                            <v-flex sm6 md6 lg6>
                                <v-text-field label="Suburb" type="text" name="suburb"  v-validate="'required'" v-model="suburb"></v-text-field>
                                <span v-show="errors.has('suburb')" class="red--text">{{ errors.first('suburb') }}</span>
                            </v-flex>
                            <v-flex sm3 md3 lg3>
                                <v-text-field label="State" name="state" v-model="state" v-validate="'required'"></v-text-field>

                                <span v-show="errors.has('state')" class="red--text">{{ errors.first('state') }}</span>
                            </v-flex>
                            <v-flex sm3 md3 lg3>

                                <v-text-field label="Postcode" type="text" name="postCode" v-validate="'required|numeric'"  v-model="postCode"></v-text-field>
                                <span v-show="errors.has('postCode')" class="red--text">{{ errors.first('postCode') }}</span>
                            </v-flex>
                            <v-flex sm12 md12 lg12>
                                <v-btn type="submit" color="primary">Save</v-btn>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-form>

                <message-alert :component="address.message"></message-alert>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>
    import { mapActions } from "vuex";
    import { createNamespacedHelpers } from "vuex";
    import { HTTP } from "../http-common";
    import {
        mapAddressFields,
        mapCustomerFields
    } from "../store/modules/orders";
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    const { mapActions: mapAddressActions, mapMutations: mapAddressMutations } = createNamespacedHelpers(
        `orders/address`
    );
    const {
        mapActions: mapCustomerActions,
    } = createNamespacedHelpers(`orders/customer`);

    export default {
        props: ["add"],
        computed: {
            ...mapAddressFields([
                "address.addressID",
                "address.postCode",
                "address.state",
                "address.streetAddress",
                "address.suburb",
                "address.customerID",
                "results"
            ]),
            ...mapOrderState(["company", "customer", "address"]),
            ...mapCustomerFields({
                custid: "customer.customerID",
                addrID: "customer.addressID"
            })
        },
        data() {
            return {
                isDataLoading: false,
                isSaved: false,

                isSubmittedSuccessfully: false,
                isLoaded: false,
                search: null
            };
        },

        mounted() {
            this.$nextTick(() => {
                    this.setResults();
                if (this.add == "true") {
                    this.customerID = this.custid;
                } else {
                    if (this.addrID) {
                        this.getAddress(this.addrID);
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
                "addAddressToCust",
                "updateAddressToCust",
                "searchAddress",
                "completeAddress",
            ]),
            ...mapCustomerActions(['addCustomerToUB']),
            ...mapAddressMutations(["setResults"]),
            addCustomerAddress: function (event) {
                var vm = this;
                vm.isSubmittedSuccessfully = false;
                vm.$validator.validateAll().then(result => {
                    vm.customerID = vm.custid;
                    if (result) {
                        if (vm.add == "true") {
                            if (vm.addressID > 0) {
                                vm.updateAddress(vm.custid).then(function () {
                                    vm.$emit("close", vm.addressID);
                                    return;
                                });
                            } else {
                                vm.addAddress(vm.custid).then(function () {
                                    vm.$emit("close", vm.addressID);
                                    return;
                                });
                            }
                        } else {
                            if (vm.addressID > 0)
                                vm.updateAddressToCust().then(async function () {
                                    if (vm.customer.customer.ubAccountNo === 0)
                                       // await vm.addCustomerToUB();
                                    vm.$router.push("app-status");
                                });
                            else
                                vm.addAddressToCust().then(async function () {
                                    if (vm.customer.customer.ubAccountNo === 0)
                                        //await vm.addCustomerToUB();
                                   vm.$router.push("app-status");
                                });

                            return;
                        }
                    }
                });
            },

            clearDropdown: function (e) {
                var vm = this;
                vm.results = [];
            }
        },
        watch: {
            search(val) {
                val && this.searchAddress(val);
            }
        }
    };
</script>
