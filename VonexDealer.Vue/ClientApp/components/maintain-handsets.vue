<template>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                Hardware
            </h3>

        </div>
        <div class="panel-body">
            <v-btn color="primary" @click="addRow">Add Row</v-btn>

            <table class="table table table-striped table-bordered mobile-table">
                <thead>
                    <tr>
                        <th scope="col"></th>
                        <th class="col-lg-1">Model</th>
                        <th scope="col">Description</th>
                        <th>Image Path</th>
                        <th class="col-lg-1">Dealer Ex</th>
                        <th align="right">RRP Ex</th>
                        <th>Category</th>
                        <th>Manufacturer</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(extension, index) in rows">
                        <td align="center"><div v-show="extension.handsetID"><i class="fas fa-check-circle" style="color:green"></i></div></td>
                        <td :class="{'text-right': true, 'text-right danger': errors.has('model'+index) }"><input v-model="extension.model" class="text-right form-control" v-validate="'required'" :name="'model'+index" /></td>
                        <td :class="{'text-right': true, 'text-right danger': errors.has('description'+index) }"><input v-model="extension.description" class="text-right form-control" v-validate="'required'" :name="'description'+index" /></td>

                        <td :class="{'text-right': true, 'text-right danger': errors.has('imagePath'+index) }"><input v-model="extension.imagePath" class="text-right form-control" v-validate="''" :name="'imagePath'+index" /></td>
                        <td :class="{'text-right': true, 'text-right danger': errors.has('dealerEx'+index) }"><input v-model="extension.dealerEx" class="text-right form-control" v-validate="'required'" :name="'dealerEx'+index" /></td>
                        <td :class="{'text-right': true, 'text-right danger': errors.has('rrpEx'+index) }"><input v-model="extension.rrpEx" class="text-right form-control" v-validate="'required'" :name="'rrpEx'+index" /></td>
                        <td :class="{'': true, 'danger': errors.has('hardwareCategoryID'+index) }">
                            <select v-model="extension.hardwareCategoryID" class="form-control" :name="'hardwareCategoryID'+index" v-validate="'required'" @change="onChange(index,extension)">
                                <option></option>
                                <option v-for="cat in categories" :value="cat.hardwareCategoryID">{{cat.category}}</option>
                            </select>
                        </td>
                        <td :class="{'': true, 'danger': errors.has('nanufacturerDetailsID'+index) }">
                            <select v-model="extension.manufacturerDetailsID" class="form-control" :name="'manufacturerDetailsID'+index" v-validate="'required'" @change="onChange(index,extension)">
                                <option></option>
                                <option v-for="item in manufacturers" :value="item.manufacturerID">{{item.description}}</option>
                            </select>
                        </td>
                        <td align="center">
                            <button @click="removeHandset(rows[index])"><i class="fas fa-minus-circle" style="color:red"></i></button>
                            <button @click="addRow" v-show="index == rows.length-1"><i class="fas fa-plus-circle" style="color:green"></i></button>
                        </td>
                    </tr>

                </tbody>
            </table>


            <div class="row">
                <div class="col-lg-12 col-md-12 col-xs-12">
                    <button name="sbmt" class="btn pull-left" @click="save">Save Hardware</button>
                </div>
            </div>
            <message-alert :component="message"></message-alert>
        </div>
    </div>
</template>
<script>
    import { mapActions, createNamespacedHelpers } from 'vuex';
    import { mapMultiRowFields } from 'vuex-map-fields';
    import { mapHandsetFields, mapHandsetMultiRowFields } from '../store/modules/orders';
    const { mapActions: mapHandsetActions, mapMutations: mapHandsetMutations } = createNamespacedHelpers(`orders/handset`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    import { httpError } from '../models/httpError'
    export default {
        data() {
            return {

            }
        },
        methods: {
            ...mapHandsetActions(['getHandsets', 'saveHandsets', 'removeHandset']),
            ...mapHandsetMutations(['addRow']),
            save: function () {
                var vm = this;
                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        vm.saveHandsets();
                    }
                })
            }

        },
        computed: {
            ...mapHandsetMultiRowFields(['rows', 'manufacturers', 'categories']),
            ...mapHandsetFields(['message']),

        },
        async mounted() {
            var vm = this;
            this.$nextTick(async () => {
                await vm.getHandsets();
            })
        }
    }
</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
