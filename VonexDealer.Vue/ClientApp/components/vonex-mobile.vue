<template>
    <div class="panel panel-default Custom_form">
        <div class="panel-heading">
            <h3 class="panel-title">
                New Service
            </h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Number of services</label>
                                <input type="number" name="services" class="form-control" min="1" v-model="NumberOfService" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table table-striped table-bordered mobile-table" v-show="NumberOfService > 0">
                <thead>
                    <tr>
                        <th>Mob. No.</th>
                        <th scope="col">Plan Name</th>
                        <th scope="col">Plan No.</th>
                        <th scope="col">Value</th>
                        <th scope="col">SIM Size</th>
                        <th scope="col">SIM Card no. (Office use only)</th>
                        <th scope="col">State</th>
                        <th scope="col">Current carrier</th>
                        <th scope="col">A/C No. <br /> D.O.B(Prepaid only)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="extension in extensionData">
                        <td scope="row"><input v-model="extension.mobno" class="form-control" /></td>
                        <td><input v-model="extension.planname" class="form-control" /></td>
                        <td><input v-model="extension.planno" class="form-control" /></td>
                        <td><input v-model="extension.value" class="form-control" /></td>
                        <td style="min-width:105px;">
                            <select v-model="extension.simsize" class="form-control">
                                <option value="Standard">Standard</option>
                                <option value="Micro">Micro</option>
                                <option value="Nano">Nano</option>
                            </select>
                        </td>
                        <td><input v-model="extension.simcardno" class="form-control" /></td>
                        <td style="min-width:80px;">
                            <select v-model="extension.state" class="form-control">
                                <option value="QLD">QLD</option>
                                <option value="NSW">NSW</option>
                                <option value="VIC">VIC</option>
                                <option value="TAS">TAS</option>
                                <option value="SA">SA</option>
                                <option value="WA">WA</option>
                                <option value="NT">NT</option>
                                <option value="ACT">ACT</option>
                            </select>
                        </td>
                        <td><input v-model="extension.currentcareer" class="form-control" /></td>
                        <td><input v-model="extension.acno" class="form-control" /></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr><td colspan="9"><label>Special instruction (i.e. porting instructions etc.)</label></td></tr>
                </tfoot>
            </table>
        </div>
    </div>
</template>
<script>
    import { HTTP } from '../http-common';
    import { mapMultiRowFields } from 'vuex-map-fields';
    import { mapActions, createNamespacedHelpers } from 'vuex';
    const { mapActions: mapVonexMobileActions } = createNamespacedHelpers(`orders/vonexmobile`);
    import { mapVonexMobileFields, mapVonexMobileMultiRowFields } from '../store/modules/forms/VonexMobile'

    export default {
        data() {
            return {

            }
        },
        watch: {
            NumberOfService: {
                handler: function (newVal, oldVal) {
                    oldVal = oldVal || 0
                    newVal = newVal || 0
                    var vm = this;
                    if (parseInt(oldVal, 10) > parseInt(newVal, 10)) {
                        this.$dialog.confirm('You are about to delete rows, are you sure?')
                            .then(function () {
                                vm.AddServicesToVuex();
                            })
                            .catch(function () {
                                //Clicked on cancel
                                vm.NumberOfService = oldVal;
                                return;
                            });
                    }
                    else {
                        vm.AddServicesToVuex();
                    }
                },
                deep: true
            },
        },
        computed: {
            ...mapVonexMobileFields(['VonexMobileService.NumberOfService']),
            ...mapVonexMobileMultiRowFields(['VonexMobileService.extensionData'])
        },
        methods: {
            ...mapVonexMobileActions(['addVonexMobileService', 'updateVonexMobileService', 'resetVonexMobileService']),
            AddServicesToVuex() {
                this.addVonexMobileService();
            },
        },
        mounted() {
            this.$nextTick(() => {
                var vm = this;
                vm.AddServicesToVuex();
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
