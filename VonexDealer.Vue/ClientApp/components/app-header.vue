<template>
    <v-card color="primary" class="d-inline ma-0 pa-0">

        <v-card-text class="text-lg-left ma-0 pa-0">
            <v-btn v-if="!isLoggedIn" small @click="login">Login</v-btn>
            <div v-if="isLoggedIn">
                <img :src="pictureURL" width="30" height="30">
                <span class="text-muted font-weight-light px-2" style="color:#fff !important">{{name}}</span>
                <v-btn small color="danger" v-on:click="logout">Logout</v-btn>
            </div>
        </v-card-text>
    </v-card>
</template>
<script>
    import { createNamespacedHelpers } from 'vuex';
   // import store from '../store'
    import {
     //   orders,
        mapAuthFields,
    } from '../store/modules/orders';
    const { mapGetters: mapAuthGetters, mapActions: mapAuthActions } = createNamespacedHelpers(`orders/auth`);

    //if (!store.state.orders) { store.registerModule(`orders`, orders, { preserveState: true }); }


    export default {
        methods: {
            ...mapAuthActions(['clearSession','checkToken', ]),
            logout() {
                var vm = this;
                this.$auth.logout().then((data) => {
                    vm.clearSession().then(
                        window.location.pathname = "/")
                })

            },

            login() {
                this.$auth.login()
            }
        },

        computed: {
            ...mapAuthFields(['auth.name', 'auth.nickname', 'auth.idToken', 'auth.pictureURL']),
            ...mapAuthGetters(['isLoggedIn'])

        },

        mounted() {
            
            this.$nextTick(() => {
                this.checkToken();
                if (this.isLoggedIn) {
                    this.name = this.name;
                    this.pictureURL = this.pictureURL;
                }
                else {
                    this.name = "Guest";
                    this.pictureURL = "/images/nouser.jpg";
                }
            })
        }
    }
</script>
