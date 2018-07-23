<template>
    <div>
        <v-title>New Orders</v-title>
        <v-card>
            <v-card-text>
                <v-layout fluid>
                    <v-flex xs12 md12 lg12>
                        <v-btn color="primary" @click="NewOrder('Business')">Business</v-btn>
                        <v-btn color="secondary" @click="NewOrder('Residential')">Residential</v-btn>
                    </v-flex>

                </v-layout>
            </v-card-text>

        </v-card>
        <v-title>Retrieve Orders</v-title>
        <v-card>
            <v-card-text>
                <v-layout fluid>
                    <v-flex xs12 md12 lg12>
                        <label>Choose order from list below and select "Import"</label>
                        <v-select v-model="orderID"
                                  :items="dealerOrders"
                                  item-text="`${data.item}`"
                                  item-value="orderID">
                            <template slot="selection" slot-scope="data">
                                {{orderString(data.item)}}
                            </template>
                            <template slot="item" slot-scope="data">
                                <v-list-tile-content>
                                    <v-list-tile-title v-html="`${orderString(data.item)}`">
                                    </v-list-tile-title>
                                    <v-list-tile-sub-title v-html="data.item.group"></v-list-tile-sub-title>
                                </v-list-tile-content>
                            </template>
                        </v-select>

                        <v-btn :disabled="isImport ? false : true" name="ExistingOrder" color="primary" @click="existingOrder">Import</v-btn>
                    </v-flex>
                </v-layout>
            </v-card-text>
        </v-card>
        <message-alert :component="message"></message-alert>
        <v-layout row justify-center>
            <v-dialog v-model="dialog" persistent max-width="290">

                <v-card>
                    <v-card-title class="headline">Unfinished order detected!<br /> Start New, or continue?</v-card-title>
                    <v-card-text>From this point you may continue any previous order (not yet signed and submitted)</v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="green darken-1" flat @click.native="dialog = false">Start New</v-btn>
                        <v-btn color="green darken-1" flat @click.native="closeRetrieve(dialog)">Continue</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-layout>
        <v-layout row justify-center>
            <v-dialog v-model="retrieved" persistent max-width="290">

                <v-card>
                    <v-card-title class="headline">Order Retrieved</v-card-title>
                    <v-card-text>Please navigate to the relevant section of the order using the navigation bar to the left.</v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="green darken-1" flat @click.native="closeRetrieve(retrieved)">OK</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-layout>
    </div>
</template>

<script>
    import { createNamespacedHelpers } from "vuex";

    import {
        mapOrderFields,
        mapOrderMultiRowFields,
        mapAuthFields,
        mapCustomerFields
    } from "../store/modules/orders";

    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    const {
        mapActions: mapOrderActions,
        mapMutations: mapOrderMutations,
        mapGetters: mapOrderGetters
    } = createNamespacedHelpers(`orders`);

    const { mapActions: mapCustomerActions } = createNamespacedHelpers(
        `orders/customer`
    );

    export default {
        name: "order-create",
        data() {
            return {
                custID: null,
                orderID: null,
                dealerOrders: [],
                dialog: false,
                retrieved: false,
            };
        },
        computed: {
            ...mapCustomerFields(["customer.customerID"]),
            ...mapOrderMultiRowFields({ orders: "rows" }),
            ...mapOrderState(["message", "company", "customer", "dealer", "isResidential"]),
            ...mapOrderGetters(["hasDealer", "hasOrder"]),
            isImport: function () {
                if (this.orderID > 0) {
                    return true;
                }
                return false;
            },
            submitted: function () {
                if (this.message.httpStatus === 200) {
                    return "alert alert-success";
                } else return "alert alert-danger";
            }
        },
        mounted() {
            this.$nextTick(async () => {
                var vm = this;

                if (vm.$route.name == "home") {

                    if (vm.hasOrder) {
                        this.dialog = true;
                    }
                    var data = await this.listOrders(0);
                    if (data[0] !== null) {
                        data[0].forEach(function (el) {
                            vm.dealerOrders.push(el);
                        });
                    }
                    var customers = await this.listCustomers(vm.dealer.dealer.dealerID);
                }
            });
        },
        methods: {
            ...mapCustomerActions(["getCustomer", "resetCustomer", "listCustomers"]),
            ...mapOrderActions(["addOrder", "getOrder", "resetOrder", "listOrders"]),
            ...mapOrderMutations(["setResidential", "setProgress"]),
            CreateNewCustomer: function () {
                var vm = this;
                vm.dialog = false;
                this.resetCustomer().then(data => {
                    vm.$router.push({ path: `/create-customer` }); // /${customerId}` })
                });
            },
            orderString: function (ord) {
                if (ord.customerDetails == null) {
                    return `${ord.orderID} - No customer`;
                }
                if (ord.customerDetails.companyID !== null) {
                    return `${ord.orderID} - ${
                        ord.customerDetails.companyDetails.companyName
                        } - Status(${ord.orderStatus})`;
                } else {
                    return `${ord.orderID} - ${ord.customerDetails.surname}- Status(${
                        ord.orderStatus
                        })`;
                }
            },
            existingCustomer() {
                this.getCustomer(this.custID);
            },
            NewOrder: function (orderType) {
                var vm = this;
                vm.dialog = false;
                vm.resetOrder()
                    .then(function (data) {
                        vm.setResidential(orderType !== "Business");
                        vm.$router.push({
                            path:
                                orderType === "Business" ? `/create-company` : `/create-customer`
                        });
                    })
                    .catch(function (err) {
                        console.log(err.message);
                    });
            },
            async existingOrder() {
                var vm = this;
                vm.dialog = false;
                vm.setResidential(true);
                try {
                    var order = await this.getOrder(this.orderID);

                    if (order) {
                        if (vm.message.httpStatus === 200) {
                            vm.setResidential(typeof vm.company.companyID === "undefined");
                            if (order.orderStatus > 0) {
                                vm.$router.push({ path: "/order-summary" });
                                return;
                            }

                            vm.retrieved = true;
                        }
                    }
                } catch (e) {
                    alert(e);
                }
            },
            closeRetrieve(el) {
                var vm = this;
                vm[el] = false;
                vm.$router.push({
                    path: `/create-customer`
                });
            }

        }
    };
</script>
<style></style>
