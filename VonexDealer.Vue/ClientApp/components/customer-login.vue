<template>
    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Customer Login
                    </h3>
                </div>
                <div class="panel">
                    <form @submit.prevent="Login">
                        <div class="isLoading" v-show="isDataLoading"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> <p>Loding...</p></div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <input type="text" name="email" placeholder="Email" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <input type="password" name="password" placeholder="Password" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <input type="submit" name="login" class="btn SubmiTT" />
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
    export default {
       
        data() {
            return {
                isDataLoading: false
            }
        },
        methods: {
            Login() {

            }
        },
    }
</script>
