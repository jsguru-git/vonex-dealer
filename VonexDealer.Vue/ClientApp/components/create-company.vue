<template>
    <div>
        <v-title>Company</v-title>
        <v-card>
            <v-card-text>
                <v-container grid-list-md>
                    <v-flex xs7 offset-xs5 offset-md10 offset-lg10>
                        <v-btn @click="resetForm" color="warning">Reset Form</v-btn>
                    </v-flex>
                    <hr />
                    <v-layout wrap>
                        <v-flex sm12 md5 lg5>

                            <v-text-field type="number" name="acn" hint="ABN or ACN" v-validate="'required'" v-model="acn" label="Enter Valid ABN/ACN to retrieve business entity" />
                            <span v-show="errors.has('acn')" class="text-danger">ABN/ACN is invalid</span>
                            <v-text-field v-show="ACNinvalid" color="warning" label="ABN/ACN is invalid"></v-text-field>
                        </v-flex>
                        <v-flex sm12 md12 lg12 >

                        <v-btn offset-md10 name="abn" @click="GetABNorACNdetails" color="primary">Retrieve</v-btn>
                        </v-flex>

                        <v-flex sm12 md12 lg12 v-show="companyName != null">
                            <v-flex sm6 md6 lg6>
                                <v-text-field label="Company Name / Trading Name" name="companyname" v-model="companyName" readonly />
                                <span v-show="errors.has('companyname')" class="text-danger">{{ errors.first('companyname') }}</span>
                            </v-flex>
                        </v-flex>
                        <v-flex sm12 md12 lg12 id="2B" v-show="abn != null && (soletrader || otherOrganisation)">
                            <hr />

                            <v-flex sm12 md12 lg12 id="soleTrader" v-show="soletrader">
                                <v-flex sm6 md6 lg6>
                                    <h3>Extra Details Required</h3>
                                </v-flex>
                                <v-flex sm6 md6 lg6>

                                    <v-text-field label="Trading Name (if applicable)" name="tradingname" v-model="tradingName" />
                                </v-flex>

                                <v-flex sm6 md6 lg6>

                                    <v-text-field label="Director Given Name" name="directorgivenname" v-validate="isdirectorSurnameRequired ? 'required' : ''" v-model="directorGivenName" />
                                    <span v-show="errors.has('directorgivenname')" class="text-danger">Given name is required</span>
                                </v-flex>
                                <v-flex sm6 md6 lg6>
                                    <v-text-field label="surname" name="directorSurname" v-model="directorSurname" v-validate="isdirectorSurnameRequired ? 'required' : ''" />
                                    <span v-show="errors.has('directorSurname')" class="text-danger">Surname is required</span>
                                </v-flex>

                                <v-flex sm3 md3 lg3>
                                    <v-text-field name="driverlicence" label="Drivers License Number" v-model="driversLicenseNumber" v-validate="isdirectorSurnameRequired ? 'required' : ''" />
                                    <span v-show="errors.has('driverlicence')" class="text-danger">Drivers license is required</span>
                                </v-flex>
                                <v-flex sm3 md3 lg3>
                                    <label>Date of Birth</label>
                                    <date-picker class="form-group" lang="en-au" name="dateOfBirth" id="dob" v-model="dateOfBirth" v-validate="isdateOfBirthRequired ? 'required|dateofbirth' : ''" format="dd/MM/yyyy" placeholder="dd/MM/yyyy" editable />
                                    <span v-show="errors.has('dateOfBirth')" class="text-danger">Not a valid birth date</span>
                                </v-flex>
                            </v-flex>
                        </v-flex>

                        <div id="Other" class="col-md-6" v-show="otherOrganisation">

                            <v-flex v-show="1==2">
                                <label>Registered Business Entity Application (other Organisation) </label> <br />
                                <input type="checkbox" name="otherOrganisation" v-model="otherOrganisation" />
                            </v-flex>

                            <v-flex v-show="1==2">
                                <label>ABN (if applicable):</label>
                                <input type="text" class="form-control" name="abn" placeholder="ABN (if applicable)" v-model="abn" />
                            </v-flex>


                        </div>
                        <div id="2C" v-show="guaranteeAttached">
                            <v-flex sm12 md12 lg12>
                                <h4>
                                    SECTION 2C: TRUST PARTNERSHIP
                                </h4>
                            </v-flex>
                            <hr />

                            <v-flex sm12 md12 lg12>
                                <v-checkbox name="guaranteeAttached" v-model="guaranteeAttached" />
                                <v-text-field label="Personal Guarantee Attached:" />

                            </v-flex>
                            <v-flex sm6 md6 lg6>
                                <v-text-field> label="Date of Birth"</v-text-field>
                                <date-picker lang="en-au" name="dobconsumer" id="dobconsumer" class="form-control" format="dd/MM/yyyy'" v-model="dobConsumer" v-validate="guaranteeAttached ? 'required|dateofbirth' : ''" editable />
                                <span v-show="errors.has('dobconsumer')" class="text-danger">{{ errors.first('dobconsumer') }}</span>
                            </v-flex>
                        </div>
                        <v-flex sm12 md12 lg12>
                            <v-btn color="primary" @click="createCustomerCompany" :disabled="errors.any()">Save & Next</v-btn>
                        </v-flex>
                        <message-alert :component="company.message"></message-alert>
                    </v-layout>
                </v-container>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>


    import VeeValidate from 'vee-validate'
    import { createNamespacedHelpers } from 'vuex';
    import moment from 'moment';
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    import { mapAuthFields, mapCustomerFields, mapCompanyFields } from '../store/modules/orders';

    const { mapActions: mapCompanyActions, mapState: mapCompanyState, mapGetters: mapCompanyGetters, } = createNamespacedHelpers(`orders/company`);


    export default {

        data() {
            return {
                isDataLoading: false,
                isCompleted: false,
                ACNinvalid: false
            };
        },
        methods: {

            ...mapCompanyActions([
                'getCompany', 'addCompany', 'updateCompany', 'resetCompany', 'validateACN'
            ]),
            createCustomerCompany: function () {
                var vm = this;
                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        if (vm.companyID == 0) {
                            vm.isDataLoading = true;
                            vm.addCompany().then(function (data) {

                                vm.$router.push("create-customer")
                            });
                        }
                        else {
                            vm.isDataLoading = true;
                            vm.updateCompany().then(function (data) {

                                vm.$router.push("create-customer")
                            });
                        }
                    }
                })
            },
            GetACN: function () {
                var vm = this;

                vm.isDataLoading = true;
                vm.validateACN().then(function (response) {
                    var res = response.data;
                    if (res.result_count === 0) {
                        vm.ACNinvalid = true;
                        vm.isDataLoading = false;

                    }
                    else {
                        vm.ACNinvalid = false;
                        vm.isDataLoading = false;
                        vm.$validator.validateAll();

                    }
                }).catch((err) => {
                    vm.isDataLoading = false;
                    console.log(err);
                });


            },
            GetABNorACNdetails: function (e) {
                var vm = this;
                vm.GetACN();

            },
            resetForm: function () {
                var vm = this;
                vm.resetCompany();
            }
        },
        computed: {
            ...mapCompanyFields(['company.companyID', 'company.companyName', 'company.tradingName', 'company.entityType', 'company.directorSurname',
                'company.abn', 'company.acn', 'company.driversLicenseNumber', 'company.dateOfBirth', 'company.directorGivenName',
                'company.otherOrganisation', 'company.guaranteeAttached', 'company.dobConsumer', 'company.isdirectorSurnameRequired', 'company.isdateOfBirthRequired',

            ]),
            ...mapCompanyGetters(['soletrader']),
            ...mapOrderState(['company']),

        },


    }
</script>
