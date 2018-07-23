<template>
    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Order Type:
                    </h3>
                </div>
                <div class="panel-body">
                    <form>
                        <div class="isLoading" v-show="isDataLoading"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> <p>Loding...</p></div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label class="chkboxcontainer">
                                             Port
                                            <input type="checkbox" name="Port" value="Port" />
                                            <span class="checkmark"></span>
                                        </label>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="chkboxcontainer">
                                            New Service
                                            <input type="checkbox" name="newservice" value="newservice" />
                                            <span class="checkmark"></span>
                                        </label>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <v-btn color="primary" value="Submit" >Save Inbound</v-btn>
                            </div>
                        </div>
                    </form>

                </div>
            </div>
        </div>
    </div>
    
</template>
<script>
    import { HTTP } from '../http-common';
 

    export default {
        methods: {

        },
        data() {
            return {
                isDataLoading: false
            }
        }
    }
</script>
