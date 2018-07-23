<template>
    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Dealer Details
                    </h3>
                </div>
                <div class="panel">
                    <div class="isLoading" v-show="isDataLoading"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> <p>Loding...</p></div>
                    <form @submit.prevent="addSate">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>DealerID:</label>
                                        <input type="text" class="form-control" v-model="DealerID" placeholder="DealerID" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>DealerFullName:</label>
                                        <input type="text" class="form-control" v-model="DealerFullName" placeholder="DealerFullName" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>DealerEmail:</label>
                                        <input type="text" class="form-control" v-model="DealerEmail" placeholder="DealerEmail" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>UBDealerID:</label>
                                        <input type="text" class="form-control" v-model="UBDealerID" placeholder="UBDealerID" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <input type="submit" name="btn" value="Add State" class="btn SubmiTT" />  
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import { mapActions } from 'vuex'
    import { createNamespacedHelpers } from 'vuex';

    import { mapDealerFields } from '../store/modules/forms/dealer'

    export default {
        data() {
            return {
                isDataLoading: false
            }
        },
        methods: {
            ...mapActions([
                'getDealer', 'addDealer'
            ]),
            addSate() {
                this.addDealer()
            }
        },
        computed: {
            ...mapDealerFields(['dealer.DealerID', 'dealer.DealerFullName', 'dealer.DealerEmail','dealer.UBDealerID'
            ])
        },
        mounted() {
            
        }
    }
</script>
