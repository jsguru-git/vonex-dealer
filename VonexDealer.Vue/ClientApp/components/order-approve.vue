<template>
    <div>
        <v-title>Detailed Order Summary</v-title>
        <v-card>
            <v-card-text>
                <h2 v-show="isDataLoading"> <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>Please wait - order being generated.</h2>

                <v-flex sm12 md12 lg12>
                    <v-alert style="margin-top:20px" v-show="!isValid">
                        Invalid Order Number
                    </v-alert>
                </v-flex>

                <div v-if="isValid">
                    <div id="HtmlToEmail" ref="summary">
                    </div>
                    <div class="row" v-show="rows[0].orderStatus===1 && !isDataLoading">
                        <v-flex sm12 md12 lg12 v-show="!isAgreed">
                            <label class="headline">I agree with all the terms and conditions and understand that once I send this order it can no longer be modified.</label>
                            <v-btn color="primary" @click="isAgreed = true">Click to Agree</v-btn>
                        </v-flex>
                        <signature-pad v-if="isAgreed" @closeModal="closeModal" @signFailed="signFailed" :edit="isAgreed"></signature-pad>
                    </div>
                    <v-alert type="success" v-show="isSending"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> Sending Email...</v-alert>
                        <v-flex sm12 md12 lg12>

                            <v-alert type="success" v-show="rows[0].orderStatus == 2">You have approved this order. Vonex has been notified.</v-alert>
                        </v-flex>
                    
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
                isValid: false,
                isSending: false,

            }
        },
        mounted() {
            var vm = this;
            if (typeof this.$route.params.guid === 'undefined')
                this.isValid = false;
            else
                this.isValid = true;

            vm.setMessage(new httpError());
            vm.refreshSummary(this.$route.params.guid)
                .then(data => {
                    this.isValid = true;
                })
                .catch(err => {
                    this.isValid = false;
                })
        },
        computed: {
            ...mapOrderState(['summary', 'rows']),
            correctOrder() {
                if (this.$route.params.length > 0)
                    return true;
                else
                    return false;

            }
        },
        methods: {
            ...mapSummaryActions(['sendEmail', 'downloadPDF', 'populateSummary', 'setMessage']),
            refreshSummary(guid) {
                return new Promise((resolve, reject) => {
                    var vm = this;
                    var data = this.HTML(guid).then(result => {
                        document.getElementById('HtmlToEmail').innerHTML = result;
                        document.getElementById('signature').setAttribute('hidden', true);
                        document.getElementById('custMessage').setAttribute('hidden', true);
                        vm.isDataLoading = false;
                        resolve(data);
                    }).catch(err => {
                        this.isValid = false;
                        this.isDataLoading = false;
                        reject(err);
                    })
                })
            },
            closeModal() {
                this.isAgreed = true;
                this.setMessage({ message: 'Order signed and submitted', httpStatus: 200 });
            },
            signFailed() {
                this.setMessage({ message: 'Signature not saved', httpStatus: 500 });
            },
            HTML: function (guid) {
                var vm = this;
                return new Promise((resolve, reject) => {

                    this.populateSummary(guid)
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
                                return;

                            }
                        })
                        .catch(err => {
                            if (err === 401)
                                vm.$router.push('/');

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
                // orderItems.to = this.dealer.dealer.dealerEmail; //dealer.email
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

