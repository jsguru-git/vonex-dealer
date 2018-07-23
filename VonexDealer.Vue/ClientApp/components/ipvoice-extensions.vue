<template>
    <v-layout wrap>
        <v-flex sm6 md6 lg6>
            <v-radio-group v-model="iporder.isCreatedByAMPT" column value="false">
                <v-radio name="ampt" :value="false" label="Standard PBX">
                </v-radio>
                <v-radio name="ampt" :value="true" label="Created by AMPT">
                </v-radio>
            </v-radio-group>
        </v-flex>
        <!--<input type="radio" id="one" value=false checked="checked"  @click="iporder.amptDomainName = null">
            <label for="one">Standard PBX</label>
            <br>
            <input type="radio" id="two" value=true v-model="iporder.isCreatedByAMPT">
            <label for="two">Created By AMPT</label>
        </div>-->

        <v-flex sm6 md6 lg6 v-show="iporder.isCreatedByAMPT">
            <v-text-field label="AMPT Domain Name" name="amptDomainName" v-validate="iporder.isCreatedByAMPT ? 'required|ampt' : ''" v-model="iporder.amptDomainName" />
            <span v-show="errors.has('amptDomainName')" class="text-danger">Domain Name is invalid</span>
        </v-flex>
        <v-container sm12 md12 lg12>
            <v-layout wrap row>
                <v-flex sm3 md3 lg3>
                    <v-text-field label="Number of Extensions" type="number" name="services" v-model="NumberOfIPVoices" min="0" @keyup="changeRows" />
                </v-flex>
                <v-flex sm6 md6 lg6 color="secondary">
                    <v-card-text>
                        <v-checkbox name="dealerSupplied" v-model="iporder.dealerSupplied" @change="changeDealerSupplied" label="Dealer Supplied Hardware" />
                        <v-checkbox name="minMonthly" v-model="iporder.minMonthly" label="Minimum Monthly Plans" />
                    </v-card-text>
                </v-flex>
            </v-layout>
        </v-container>

        <table class="table table table-striped table-bordered mobile-table" v-show="NumberOfIPVoices > 0">
            <thead>
                <tr>
                    <th scope="col">ID</th>
                    <th scope="col">Ext.</th>
                    <th scope="col">Ext Name</th>
                    <th scope="col">
                        <div style="float:left;">Handset</div>
                        <div style="float:right;"> <button @click="copyDown('handsetID')"><i class="fas fa-paste" title="copy down"></i></button></div>

                    </th>
                    <th scope="col">Contract Length</th>
                    <th scope="col">
                        <div style="float:left;"> <span v-if="iporder.minMonthly">Plan</span><span v-if=!iporder.minMonthly>Mix n Match Plans</span></div>
                        <div style="float:right;"> <button @click="copyDown('ratePlanID')"><i class="fas fa-paste" title="copy down"></i></button></div>
                    </th>
                    <th scope="col">
                        <div style="float:left;">ANI</div>
                        <div style="float:right;"> <button @click="copyDown('outboundANI')"><i class="fas fa-paste" title="copy down"></i></button></div>
                    </th>
                    <th scope="col">
                        <div style="float:left;">Email</div>
                        <div style="float:right;"> <button @click="copyDown('voicemailEmail')"><i class="fas fa-paste" title="copy down"></i></button></div>
                    </th>
                    <th scope="col">
                        <div style="float:left;">Mobile</div>
                        <div style="float:right;"> <button @click="copyDown('mobileTwinning')"><i class="fas fa-paste" title="copy down"></i></button></div>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(extension, index) in rows">
                    <td align="center"><div v-show="extension.ipExtensionID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                    <td :class="{'': true, 'danger': errors.has('extensionNo'+index) }">
                        <input v-model="extension.extensionNo" class="form-control" v-validate="'required'" :name="'extensionNo'+index" />
                        <input type="hidden" v-model="extension.dealerSupplied" />
                    </td>
                    <td :class="{'': true, 'danger': errors.has('extensionName'+index) }"><input v-model="extension.extensionName" class="form-control" :name="'extensionName'+index" /></td>
                    <td :class="{'': true, 'danger': errors.has('handsetID'+index) }">
                        <select v-model="extension.handsetID" class="form-control" :name="'handsetID'+index" v-validate="'required'">
                            <option></option>
                            <option v-for="handset in ipVoice.handsets.filter(x => x.hardwareCategory.category == 'QA Handset')" :value="handset.handsetID">{{handset.manufacturer.description}} {{handset.model}}</option>
                        </select>
                    </td>
                    <td>
                        <select class="form-control" v-model="extension.contractLengthMonths" v-validate="'required'" :name="'contractLengthMonths'+index" :disabled="index>0" @change="copyDown('contractLengthMonths')">
                            <option value="0">0 months</option>
                            <option value="24">24 months</option>
                        </select>
                    </td>
                    <td style="min-width:80px;" :class="{'': true, 'danger': errors.has('ratePlanID'+index) }">
                        <select v-model="extension.ratePlanID" class="form-control" v-validate="'required'" :name="'ratePlanID'+index">
                            <option></option>
                            <option v-for="rateplan in validRatePlans(extension.contractLengthMonths)" :value="rateplan.ratePlanID">{{rateplan.ratePlanDescription}}</option>
                        </select>
                    </td>
                    <td :class="{'': true, 'danger': errors.has('outboundANI'+index) }"><input v-model="extension.outboundANI" class="form-control" :name="'outboundANI'+index" v-validate="extension.outboundANI ? 'phonenumber' : ''" /></td>
                    <td :class="{'': true, 'danger': errors.has('voicemailEmail'+index) }"><input v-model="extension.voicemailEmail" :name="'voicemailEmail'+index" v-validate="extension.voicemailEmail !== null ? 'email' : ''" class="form-control" /></td>
                    <td :class="{'': true, 'danger': errors.has('mobileTwinning'+index) }"><input v-model="extension.mobileTwinning" class="form-control" v-validate="extension.mobileTwinning !== null ? 'mobilenumber' : ''" :name="'mobileTwinning'+index" /></td>

                </tr>
            </tbody>
        </table>
        <v-flex sm12 md12 lg12>
            <v-btn color="primary" name="sbmt" @click="save">Save IP Order</v-btn>
            <message-alert :component="ipVoice.message"></message-alert>
        </v-flex>
    </v-layout>
</template>
<script>
    import { mapActions, createNamespacedHelpers } from 'vuex';
    import { mapIPVoiceFields, mapIPVoiceMultiRowFields } from '../store/modules/orders';
    //  import handset from '../store/modules/forms/handset';
    const { mapActions: mapIPVoiceActions, mapMutations: mapIPVoiceMutations } = createNamespacedHelpers(`orders/ipVoice`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    import { httpError } from '../models/httpError'
    export default {
        data() {
            return {
                plans: null,
                NumberOfIPVoices: 0,
                dealerSupplied: false,
                minMonthly: false,
            }
        },

        methods: {
            ...mapIPVoiceActions(['addIPVoice', 'updateIPVoice', 'getIPVoice', 'getHandsets', 'getIPExtensions', 'setMessage', 'removeRows', 'saveIPOrder']),
            ...mapIPVoiceMutations(['addRow', 'deleteRow']),
            copyDown: function (column) {
                console.log(column);
                var vm = this;
                for (var i = 1; i < vm.rows.length; i++) {
                    vm.rows[i][column] = vm.rows[0][column];
                }
                return;
            },
            changeDealerSupplied(e) {
                this.rows.forEach(function (row) {
                    row.dealerSupplied = e;
                })
                return;
            },

            checkPlan: function (ratePlan) {
                return (ratePlan.usageType === 'DID') || (
                (ratePlan.dealerSupplied == this.iporder.dealerSupplied)
                    && (ratePlan.mixnMatch !== this.iporder.minMonthly)
                    && (!this.isResidential ? ratePlan.residential == false : 1 == 1) )
                    ;
            },
            validRatePlans: function (months) {
                return this.ratePlans.filter(this.checkPlan).filter(e => !this.minMonthly ? e.contractMonths == months : 1 == 1); // && e.dealerSupplied === this.dealerSupplied);
            },
            async save() {
                var vm = this;
                vm.$validator.validateAll().then(async (result) => {
                    if (result) {
                        try {
                            await vm.saveIPOrder();
                            await vm.addIPVoice();

                        } catch (err) {

                            vm.setMessage({ message: err, httpStatus: 500 });
                        }
                    }
                    else {
                        vm.setMessage({ message: 'Unable to save: Check the highlighted fields above', httpStatus: 500 });
                    }
                }).catch(function (err) {
                    vm.setMessage({ message: err, httpStatus: 500 });
                })
            },

            changeRows: function () {
                var vm = this;

                var oldVal = 0 || vm.rows.length;
                var newVal = vm.NumberOfIPVoices;
                if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                    this.$dialog.confirm('You are about to delete rows, are you sure?')
                        .then(function () {
                            vm.removeRows(newVal).then(function () {
                                vm.NumberOfIPVoices = vm.rows.length;
                            })

                        })
                        .catch(function () {
                            //Clicked on cancel
                            vm.NumberOfIPVoices = oldVal;
                            return;
                        });

                }
                else {

                    if (newVal > this.rows.length)
                        for (var i = oldVal; i < newVal; i++) {

                            vm.addRow(vm.rows[0] ? vm.rows[0].contractLengthMonths : 24);
                            vm.rows[i].extensionNo = 500 + i;
                        }

                }
            },
            createIPVoice: function () {
                var vm = this;
                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        vm.saveIPOrder();
                    }
                })
            }
        },
        computed: {
            ...mapIPVoiceMultiRowFields(['rows', 'ratePlans']),
            ...mapOrderState(['ipVoice', 'isResidential']),
            ...mapIPVoiceFields(['iporder']),

        },
        async mounted() {
            var vm = this;
            this.$nextTick(async () => {
                vm.setMessage(new httpError());
                vm.iporder.isCreatedByAMPT = false;
                await vm.getIPVoice();
                await vm.getIPExtensions();
                vm.NumberOfIPVoices = vm.rows.length;
                var handsets = await vm.getHandsets();
                if (vm.rows.length == 0) {
                    vm.addRow(24);
                    vm.rows[0].extensionNo = 500;
                }
                vm.NumberOfIPVoices = vm.rows.length;

            })
        }
    }
</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
