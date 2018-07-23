<template>
    <v-container>

            <v-title v-show="!isLoggedIn">
                Welcome to the new Vonex Dealer Portal.
            </v-title>
            <v-card>
                <v-card-text>
                    <div v-if="isLoggedIn">
                        <h3>
                            Hello, {{nickname}}
                        </h3>
                        <h2 v-show="!hasDealer">
                            <span class="alert alert-danger">You have a valid login - but are not associated with a valid dealer account - contact Vonex</span>
                        </h2>
                    </div>
                    <v-flex sm12 md12 lg12>

                    <p>This is to be the home of all things "Dealer Related" for Vonex. </p>
                    <hr />
                    <v-btn color="primary" v-on:click="login" v-if="!isLoggedIn">Login</v-btn>
                    <!--<a class="btn btn-primary btn-lg" href="/getting-started" target="_blank" role="button">Getting Started</a>-->
                    <hr>
                    <order-create v-if="isLoggedIn && hasDealer"></order-create>
                    </v-flex>
                </v-card-text>
            </v-card>
    </v-container>
</template>
<script>
    import Vue from 'vue'
    import OrderCreate from '../components/order-create'
    import { createNamespacedHelpers } from 'vuex';
    const { mapState: mapOrderState, mapGetters: mapOrderGetters } = createNamespacedHelpers(`orders`);

    Vue.component('order-create', OrderCreate);

    import {
        mapAuthFields
    } from '../store/modules/orders';


    const { mapGetters: mapAuthGetters, mapActions: mapAuthActions }
        = createNamespacedHelpers(`orders/auth`);


    export default {
        name: "Home",
        data() {
            return {
            }
        },
        methods: {
            ...mapAuthActions['checkLocal', 'login'],

            login: function () {
                this.$auth.login();
            }
        },
        computed: {
            ...mapAuthFields(['auth.name', 'auth.nickname', 'auth.idToken', 'auth.pictureURL']),
            ...mapAuthGetters(['isLoggedIn']),
            ...mapOrderGetters(['hasDealer', 'hasOrder']),

        },
        mounted() {
            this.$nextTick(() => {

            })
        },

    }


</script>
<style scoped>


    .btn-primary {
        background: #468f65;
        border: 1px solid #468f65;
    }

    .card {
        text-decoration: none;
        color: #000;
    }
</style>
