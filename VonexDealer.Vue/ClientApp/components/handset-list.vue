<template>
    <div>

        <table class="table table table-striped table-bordered mobile-table">
            <thead>
                <tr>
                    <td colspan="6">
                        <v-toolbar color="secondary" pa-0>
                            <v-toolbar-title class="white--text">
                                From IP Order
                            </v-toolbar-title>
                        </v-toolbar>
                    </td>
                </tr>
                <tr>
                    <th scope="col"></th>
                    <th class="col-lg-1">Quantity</th>
                    <th scope="col">Equipment</th>

                    <th>Cost</th>
                    <th scope="col">Comment</th>
                    <th align="right">Amount</th>
                </tr>
            </thead>
            <tbody>

                <tr v-for="(extension, index) in rows.filter(x => x.fromIPOrder === true)">
                    <td align="center"><div v-show="extension.hardwareID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                    <td><input v-model="extension.quantity" class="text-right form-control" v-validate="{ min_value:1}" :name="'quantity'+index" @change="updateRow(index)" /></td>
                    <td>
                        <select v-model="extension.handsetID" class="form-control" :name="'handsetID'+index" v-validate="'required'" @change="onChange(index,extension)">
                            <option></option>
                            <option v-for="handset in handsets" :value="handset.handsetID">{{handset.manufacturer.description}} {{handset.model}} ({{handset.rrpEx | currency}} )</option>
                        </select>
                        

                    </td>
                    

                    <!-- <td>{{extension.fromIPOrder}}</td>-->
                    <td align="right">{{extension.cost | currency}}</td>
                    <td><input v-model="extension.comment" class="form-control" :name="'comment'+index" /></td>
                    <td align="right">{{extension.lineTotal | currency}}</td>
                </tr>

            </tbody>
        </table>
        <table class="table table table-striped table-bordered mobile-table">
            <thead>
                <tr>
                    <td colspan="6">
                        <v-toolbar color="secondary" pa-0>
                            <v-toolbar-title class="white--text">
                                Additional Hardware <v-btn round small color="primary" name="addrow" @click="addRow">Add Row</v-btn>
                            </v-toolbar-title>
                        </v-toolbar>
                    </td>
                </tr>
                <tr>
                    <th scope="col"></th>
                    <th class="col-lg-1">Quantity</th>
                    <th scope="col">Equipment</th>

                    <th>Cost</th>
                    <th scope="col">Comment</th>
                    <th align="right">Amount</th>
                </tr>
            </thead>
            <tbody>

                <tr v-for="(extension, index) in rows" :style="extension.fromIPOrder  ? 'display:none' : '' ">
                    <td align="center"><div v-show="extension.hardwareID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                    <td :class="{'text-right': true, 'text-right danger': errors.has('quantity'+index) }"><input v-model="extension.quantity" class="text-right form-control" v-validate="{ min_value:1}" :name="'quantity'+index" @change="updateRow(index)" /></td>
                    <td :class="{'': true, 'danger': errors.has('handsetID'+index) }">
                        <select v-model="extension.handsetID" class="form-control" :name="'handsetID'+index" v-validate="'required'" @change="onChange(index,extension)">
                            <option></option>
                            <option v-for="handset in handsets" :value="handset.handsetID">{{handset.manufacturer.description}} {{handset.model}} ({{handset.rrpEx | currency}} )</option>
                        </select>
                    </td>
                    <td align="right">{{extension.cost | currency}}</td>
                    <td :class="{'': true, 'danger': errors.has('comment'+index) }"><input v-model="extension.comment" class="form-control" :name="'comment'+index" /></td>
                    <td align="right">{{extension.lineTotal | currency}}</td>
                </tr>
                <!--summary-->
                <tr style="border-top: double">
                    <td align="center">Total</td>
                    <td><input class="text-right form-control" v-model="sumQuantity" readonly /></td>
                    <td></td>
                    <td></td>

                    <td></td>
                    <td align="right">Freight: {{totalFreight | currency}}</td>
                </tr>
                <tr>
                    <td align="center"></td>
                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                    <td align="right"><strong>{{sumLineTotals + totalFreight | currency}}</strong></td>
                </tr>
            </tbody>
        </table>


        <v-flex sm12 md12 lg12>
            <v-btn name="sbmt" color="primary" @click="save">Save Hardware</v-btn>

        </v-flex>

        <message-alert :component="hardware.message"></message-alert>
    </div>
</template>
<script>
    import { mapActions, createNamespacedHelpers } from 'vuex';
    import { mapMultiRowFields } from 'vuex-map-fields';
    import { mapHardwareFields, mapHardwareMultiRowFields } from '../store/modules/orders';
    const { mapActions: mapHardwareActions, mapMutations: mapHardwareMutations } = createNamespacedHelpers(`orders/hardware`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    import { httpError } from '../models/httpError'
    export default {
        data() {
            return {

            }
        },

        methods: {
            ...mapHardwareActions(['addHandset', 'updateHandset', 'getHandsets', 'getHardware', 'setMessage', 'saveHardware', 'saveHandsets']),
            ...mapHardwareMutations(['addRow', 'deleteRow']),
            copyDown: function (column) {
                console.log(column);
                var vm = this;
                for (var i = 1; i < vm.rows.length; i++) {
                    vm.rows[i][column] = vm.rows[0][column];
                }
                return;
            },

            onChange: function (index, data) {
                this.rows[index].cost = this.handsets.filter(e => e.handsetID == data.handsetID)[0].rrpEx;
                this.updateRow(index);
            },
            updateRow: function (index) {
                this.rows[index].lineTotal = this.rows[index].quantity * this.rows[index].cost;
            },
            checkPlan: function (ratePlan) {
                return ratePlan.usageType.includes('VoIP')
                    && (ratePlan.dealerSupplied == this.dealerSupplied)
                    && (ratePlan.mixnMatch !== this.minMonthly)
                    && (!this.isResidential ? ratePlan.residential == false : 1 == 1)
                    ;
            },
            async save() {
                var vm = this;
                vm.$validator.validateAll().then(async (result) => {
                    if (result) {
                        try {
                            await vm.saveHardware();
                            await vm.saveHandsets();

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
            validRatePlans: function (months) {
                return this.ratePlans.filter(this.checkPlan).filter(e => !this.minMonthly ? e.contractMonths == months : 1 == 1); // && e.dealerSupplied === this.dealerSupplied);
            },


        },
        computed: {
            ...mapHardwareMultiRowFields(['rows', 'handsets']),
            ...mapOrderState(['hardware', 'isResidential']),
            ...mapHardwareFields(['iporder']),
            sumQuantity() {
                if (this.rows && this.rows.length > 0)
                    return this.rows.map(row => row.quantity).reduce((prev, next) => parseInt(prev) + parseInt(next));
                else
                    return 0;
            },
            totalFreight() {
                if (this.rows.length > 0)
                    return ((this.sumQuantity - 1) * 7.5) + 20.0;
                else
                    return 0.00;
            },
            sumLineTotals() {
                if (this.rows && this.rows.length > 0)
                    return this.rows.map(row => row.lineTotal).reduce((prev, next) => parseInt(prev) + parseInt(next));
                else
                    return 0;
            },
        },
        async mounted() {
            var vm = this;
            this.$nextTick(async () => {
                vm.setMessage(new httpError());
                await vm.getHandsets().then(data => {

                    vm.getHardware();
                })

            })
        }
    }
</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
