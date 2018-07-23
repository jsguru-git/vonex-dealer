<template>
    <div>
        <v-title>Customer</v-title>
        <v-card>

            <v-card-text>
                <v-container grid-list-md>
                    <v-layout wrap>
                        <v-flex xs2 sm2 md2>
                            <v-select data-vv-as="Salutation" name="salutation" v-validate="'required'" v-model="salutation" label="Title"
                                      :items="sals">

                            </v-select>
                            <span v-show="errors.has('salutation')" class="text-danger">{{ errors.first('salutation') }}</span>
                        </v-flex>
                        <v-flex xs6 sm6 lg6 right>
                            <label>Date Added</label>
                            <date-picker lang="en-au" name="dob" format="dd/MM/yyyy" v-model="dateAdded" disabled />
                        </v-flex>
                        <v-flex xs6 sm6 lg6>
                            <v-text-field label="First Name" name="firstName" v-validate="'required'" v-model="firstName" ></v-text-field>
                            <span v-show="errors.has('firstName')" class="text-danger">{{ errors.first('firstName') }}</span>
                        </v-flex>

                        <v-flex xs6 sm6 lg6>
                            <v-text-field label="Surname" name="surname" v-model="surname" v-validate="'required'" ></v-text-field>

                            <span v-show="errors.has('surname')" class="text-danger">{{ errors.first('surname') }}</span>
                        </v-flex>

                        <v-flex xs6 sm6 lg6>
                            <v-text-field label="Email" name="email" v-validate="'required|email'" v-model="email" ></v-text-field>
                            <span v-show="errors.has('email')" class="text-danger">{{ errors.first('email') }}</span>
                        </v-flex>
                        <v-flex xs6 sm6 lg6>
                            <label>Date of Birth</label>
                            <date-picker lang="en-au" name="dob" format="dd/MM/yyyy" placeholder="dd/MM/yyyy" v-model="dob" v-validate="'required|dateofbirth'" editable />
                            <span v-show="errors.has('dob')" class="text-danger">{{ errors.first('dob') }}</span>

                        </v-flex>
                        <v-flex xs6 sm6 lg6>
                            <v-text-field label="Phone"
                                          v-validate="(!mobile || phone) ? 'required|phonenumber' : ''" name="phone" v-model="phone" ></v-text-field>
                            <span v-show="errors.has('phone')" class="text-danger">{{ errors.first('phone') }}</span>
                        </v-flex>
                        <v-flex xs6 sm6 lg6>
                            <v-text-field label="Mobile" name="mobile"
                                          v-validate="(!phone || mobile) ? 'required|mobilenumber' : ''" v-model="mobile" ></v-text-field>
                            <span v-show="errors.has('mobile')" class="text-danger">{{ errors.first('mobile') }}</span>
                        </v-flex>

                        <v-flex sm12 lg12 v-show="!readOnly">
                            <v-btn color="primary" @click="createCustomer" v-if="!isSaved" :disabed="errors.any()">Save</v-btn>
                            <v-btn color="primary" @click="isSaved = false" v-if="isSaved" :disabled="errors.any()">Edit</v-btn>
                        </v-flex>
                        <v-flex v-show="IsAuthorisedRepresentative == 'true'">
                            <v-btn color="primary">Add Primary Contact</v-btn>
                        </v-flex>
                        <v-flex>
                            <message-alert :component="customer.message"></message-alert>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>

    import { createNamespacedHelpers } from 'vuex';
    import { mapAuthFields, mapCustomerFields, mapDealerFields, } from '../store/modules/orders';
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);

    //import VeeValidate from 'vee-validate';
    // import DatePicker from '../components/date-picker';

    const {
        mapActions: mapCustomerActions,
    } = createNamespacedHelpers(`orders/customer`);


    const { mapActions: mapOrderActions, }
        = createNamespacedHelpers(`orders`);
    export default {

        props: ['readOnly', 'title', 'IsAuthorisedRepresentative'],
        //  components: { DatePicker },
        data() {
            return {
                isDataLoading: false,
                isSaved: false,
                results: [],
                isCompleted: false,
                isLoaded: false,
                sals: [
                    'Dr', 'Miss', 'Mr', 'Mrs', 'Ms', 'Mstr']

            }
        },
        computed: {
            ...mapOrderState(['company', 'customer']),
            ...mapDealerFields({ dealer: 'dealer.dealerID' }),
            ...mapCustomerFields(['customer.customerID', 'customer.salutation', 'customer.firstName', 'customer.surname',
                'customer.dateAdded', 'customer.contactNo',
                'customer.position', 'customer.phone',
                'customer.fax', 'customer.mobile',
                'customer.email', 'customer.dealerID', 'customer.companyID', 'customer.dob',
            ]),

            submitted: function () {
                if (this.customer.message.httpStatus === 200) {
                    return 'alert alert-success';
                } else
                    return 'alert alert-danger';
            }
        },
        mounted() {
            this.$nextTick(() => {
                //set customer record to have new dealer num
                this.dealerID = this.dealer;
            })
        },

        methods: {
            ...mapOrderActions(['addOrder']),
            ...mapCustomerActions(['getCustomer', 'addCustomer', 'updateCustomer', 'resetCustomer']),
            createCustomer: function (event) {
                var vm = this;
                vm.$validator.validateAll().then((result) => {
                    if (result) {

                        vm.className = 'alert alert-success';
                        if (vm.customerID) {
                            vm.companyID = vm.company.company.companyID;
                            this.updateCustomer(vm.customerID).then(function (data) {
                                vm.isSaved = true;
                                return;
                            }).catch(function (e) {
                                console.log(e);
                            })

                        }
                        else {
                            vm.companyID = vm.company.company.companyID;
                            this.addCustomer(vm.dealerID).then(function (data) {
                                vm.isSaved = true;
                                return;
                            }).catch(function (err) {
                                console.log(err);
                            });
                        }
                        vm.isCompleted = true;
                    }
                });
                vm.isDataLoading = false;
                return;
            },
            resetForm: function (event) {
                this.resetCustomer();
            }
        }
    }
</script>
