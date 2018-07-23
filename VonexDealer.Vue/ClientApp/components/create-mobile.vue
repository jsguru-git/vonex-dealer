<template>
    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Add Mobile
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>Order Type: </label>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Port &nbsp;</label>
                                    <input type="checkbox" name="Port" value="Port" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>New Service &nbsp;</label>
                                <input type="checkbox" name="newservice" value="newservice" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-default Custom_form">
                <div class="panel-body" style="padding:0">
                    <input type="button" name="addmobileservice" value="Add New Service" class="btn btn-lg btn-default" @click="NewServices" />
                </div>
            </div>

            <!--Table-->
            <vonex-mobile v-show="isNewService"></vonex-mobile>
            <div>
                <customer-address customerID="1001"></customer-address>
                <div class="panel panel-default Custom_form">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Contact Name:</label>
                                        <input type="text" name="contactname" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Contact Number:</label>
                                    <input type="text" name="contactnumber" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-default Custom_form">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                MOBILE DATA UNDERSTANDING
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <input type="checkbox" name="name" value="1" />
                                        <span>
                                            I understand it is not recommended for tethering to
                                            a computer or as a Wi-Fi hotspot. This can easily
                                            result in high data usage and excess data charges.
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <input type="checkbox" name="name" value="1" />
                                        <span>
                                            I understand Vonex Telecom will provide data
                                            notifications via SMS at 50%, 85% and 95% of my
                                            data usage. However these may be delayed by up
                                            to 48 hours after the day you have breached this
                                            level due to carrier delays. <br />
                                            Depending on the plan chosen excess data charges
                                            may be as much as 20c per Mb ($200 per Gb).
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-default Custom_form">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                CONTRACT TERM
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <input type="radio" name="contract" value="0" /><label>&nbsp;No Contract&nbsp;</label>
                                        <input type="radio" name="contract" value="12" /><label>&nbsp;12 Month Contract&nbsp;</label>
                                        <input type="radio" name="contract" value="24" /><label>&nbsp;24 Month Contract</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>CARRIER</label>
                                        <select class="form-control">
                                            <option>Optus</option>
                                            <option>Telstra</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        MOBILE SERVICE UNDERSTANDING
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <div class="form-group">
                            <input type="checkbox" name="name" value="1" />
                            <span>
                                By signing this form I acknowledge that I have read and understood the terms and conditions as shown on the Vonex website. Further to the Authority I have given on the Customer Details page of this
                                Application, I acknowledge that I have been advised by Vonex that: although I may have the right to Port the Mobile Service Number, there may be costs and obligations associated with the existing mobile
                                service and Porting the Mobile Service Number; I may or may not be in an existing contract with the current Service Provider; and the contract may or may not include an obligation to make early termination
                                payments to the current Service Provider. Note: I understand that early termination fees may apply as outlined in the Critical Information Summary. I understand that I must also pay all charges incurred on
                                my account (including the pro-rated Minimum Monthly Spend and all usage not included in the Monthly Included Value) up to the date of termination or transfer. Vonex’s Fair Use Policy and Acceptable Use
                                Policy applies. See Vonex's standard form of agreement for details at www.vonex.com.au. I authorise the transfer of the Existing Services and Existing Service Numbers from my current provider listed on the
                                service agreement and its attachments (‘Current Provider’) to Vonex; and I have the authority as lessee of the Existing Services and Existing Service Numbers, or as authorised agent for the lessee, to request
                                the transfer of the Existing services and Service Numbers to Vonex. I authorise and I have the authority to request the new service connections as listed on this service agreement and its attachments.
                            </span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="button" name="btn" value="Submit" class="btn SubmiTT" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import { createNamespacedHelpers } from "vuex";
    import { mapVonexMobileFields, mapVonexMobileMultiRowFields } from '../store/modules/forms/VonexMobile';

    export default {
        data() {
            return {
                isDataLoading: false
            }
        },
        computed: {
            ...mapVonexMobileFields(['VonexMobileService.isNewService'])
        },
        methods: {
            NewServices() {
                this.isNewService = !this.isNewService;
            }
        },
    }
</script>
