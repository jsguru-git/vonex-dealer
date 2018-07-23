<template>
    <div>
        <v-title>Detailed Order Summary</v-title>
        <v-card>
            <v-card-text>


                <div id="HtmlToEmail" ref="summary">
                    <h2 v-show="isDataLoading"> <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>Please wait - order being generated.</h2>

                </div>
                <v-flex sm12 md12 lg12>
                    <v-alert type="success" v-show="isSending"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> Sending Email...</v-alert>
                    <v-alert type="success" v-show="rows[0].orderStatus == 1">Email has been sent, awaiting approval.</v-alert>
                </v-flex>
                <v-layout row wrap v-show="rows[0].orderStatus===0 && !isDataLoading">
                    <v-flex sm10 md10 lg10>
                        <label class="subheading">Send this order to Authorised Rep: <strong>({{formattedName}})</strong> to be approved;<br /> Once sent and approved by customer it can no longer be modified.</label>
                    </v-flex>
                    <v-flex sm2 md2 lg2>
                        <v-btn color="primary" @click="EmailOrderSummary" name="SendAsEmail">Send as Email</v-btn>
                        <v-btn color="primary" @click="createPDF">Download PDF</v-btn>
                    </v-flex>
                </v-layout>
                <div class="row" style="margin-top:20px" v-show="rows[0].orderStatus==2">
                    <div class="col-md-12">
                        <v-btn color="primary" @click="EmailOrderSummary" name="SendAsEmail">Send as Email</v-btn>
                        <v-btn color="primary" @click="createPDF">Download PDF</v-btn>
                    </div>
                </div>



                <message-alert :component="summary.message"></message-alert>

            </v-card-text>
        </v-card>
    </div>
</template>
<script>
    import { createNamespacedHelpers } from 'vuex';
    const { mapActions: mapSummaryActions, } = createNamespacedHelpers(`orders/summary`);
    import { Summary } from '../models/summary';
    import Signature from './signature-pad'
    import { httpError } from '../models/httpError';
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    export default {
        components: { 'signature-pad': Signature },
        data: function () {
            return {
                isAgreed: false,
                isDataLoading: true,
                isSending: false,


            }
        },
        mounted() {
            var vm = this;
            vm.setMessage(new httpError());
            vm.refreshSummary();
        },
        beforeRouteUpdate: (to, from, next) => {

        },
        computed: {
            ...mapOrderState(['summary', 'rows', 'customer']),
            formattedName() {
                return `${this.customer.customer.salutation} ${this.customer.customer.firstName} ${this.customer.customer.surname}, Email: ${this.customer.customer.email}`
            },

        },
        methods: {
            ...mapSummaryActions(['sendEmail', 'downloadPDF', 'populateSummary', 'setMessage']),
            refreshSummary() {
                var vm = this;
                var data = this.HTML().then(result => {
                    document.getElementById('HtmlToEmail').innerHTML = result;
                    document.getElementById('signature').setAttribute('hidden', true);
                    document.getElementById('custMessage').setAttribute('hidden', true);
                    vm.isDataLoading = false;
                    return Promise.resolve();
                }).catch(err => {
                    return Promise.reject(err);
                })
            },
            closeModal() {
                this.isAgreed = true;
                this.refreshSummary();
            },
            HTML: function () {
                var vm = this;
                return new Promise((resolve, reject) => {

                    this.populateSummary()
                        .then(data => {
                            if (data) {
                                //hide signature

                                resolve(
                                    `<html>
                                <head>
                                <title>
                                    Order Summary
                                </title>
  <link rel="stylesheet" href="https://__URL__/css/site.css" />

                                </head>
                            <body>`+ data + `

                            </body>
                            </html>
                            `);
                                resolve(data);
                                return;
                            }
                        })
                        .catch(err => {
                            if (err === 401)
                                vm.$router.push('/');
                            vm.setMessage(new httpError({ message: err.message, httpStatus: 500 }));
                            reject(err);
                            return;
                        })
                })
            },
            createPDF() {
                this.downloadPDF(document.getElementById('HtmlToEmail').innerHTML);
            },
            EmailOrderSummary() {
                var vm = this;
                vm.isSending = true;

                var orderItems = new Summary();
                orderItems.to = this.customer.customer.email;
                orderItems.subject = "new order";
                orderItems.emailBody = document.getElementById('HtmlToEmail').innerHTML;

                vm.sendEmail(orderItems).then(function (data) {
                    vm.isSending = false;
                }).catch(function (err) {
                    vm.isSending = false;
                })

            }
        }
    }
</script>

