<template>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                Dealers
            </h3>

        </div>
        <div class="panel-body">
           
         
            <table class="table table table-striped table-bordered mobile-table" >
                <thead>
                    <tr>
                        <th scope="col">Dealer ID</th>
                        <th scope="col">Dealer FullName</th>
                        <th scope="col">Dealer Email</th>
                       <th>UB DealerID</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(dealer, index) in dealers">
                        <td align="center"><div v-show="dealer.dealerID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                        <td :class="{'': true, 'danger': errors.has('dealerFullName'+index) }"><input v-model="dealer.dealerFullName" class="form-control" v-validate="'required'" :name="'dealerFullName'+index" /></td>
                        <td :class="{'': true, 'danger': errors.has('dealerEmail'+index) }"><input v-model="dealer.dealerEmail" class="form-control" v-validate="'required'" :name="'dealerEmail'+index" /></td>
                        <td :class="{'': true, 'danger': errors.has('uBDealerID'+index) }"><input v-model="dealer.uBDealerID" class="form-control" v-validate="'required'" :name="'uBDealerID'+index"  /></td>
                    </tr>
                </tbody>
            </table>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-xs-12">
                    <button name="sbmt" class="btn" @click="save">Save Dealers</button>
                </div>
            </div>
            <message-alert :component="message"></message-alert>
        </div>
    </div>
</template>
<script>
    import { mapActions, createNamespacedHelpers } from 'vuex';
    import { mapMultiRowFields } from 'vuex-map-fields';
    import { mapDealerMultiRowFields } from '../store/modules/orders';
    const { mapActions: mapDealerActions} = createNamespacedHelpers(`orders/dealer`);
    import { httpError } from '../models/httpError'
import { mapDealerFields } from '../store/modules/orders';
    export default {
        data() {
            return {
              
            }
        },

        methods: {
            ...mapDealerActions(['listDealers','setMessage']),
           
            async save() {
                var vm = this;
                vm.$validator.validateAll().then(async (result) => {
                    if (result) {
                        try {
                            await vm.saveDealers();
                           

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
           
           
        },
        computed: {
            ...mapDealerMultiRowFields(['dealers']),
            ...mapDealerFields(['message']),

        },
        async mounted() {
            var vm = this;
            this.$nextTick(async () => {
                vm.setMessage(new httpError());
                await vm.listDealers();
            })
        }
    }
</script>
<style>
    .mobile-table tbody .form-control, .mobile-table tbody .form-control:focus {
        border: none;
        -moz-box-shadow: none;
        -webkit-box-shadow: none;
        box-shadow: none;
        background: transparent;
    }

        .mobile-table tbody .form-control:focus {
            border: 1px solid #a31a81;
            border-radius: 0px;
            border-top: none;
        }

    .mobile-table tbody td {
        vertical-align: baseline;
        padding: 0px !important;
    }
</style>
