<template>
    <div class="isLoading"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> <p>Authorising...</p></div>

</template>
<script>
    import { HTTP } from '../http-common';
    import { createNamespacedHelpers } from 'vuex';
    import {
        mapAuthFields,
    } from '../store/modules/orders';
    import { httpError } from '../models/httpError';

    const { mapActions: mapAuthActions, mapMutations: mapAuthMutations }
        = createNamespacedHelpers(`orders/auth`);
    const { mapActions: mapDealerActions } = createNamespacedHelpers(`orders`);



    export default {
        name: 'callback',
        mounted() {
            var vm = this;
            this.$auth.handleAuthentication().then((data) => {
                vm.setSession(data)
                    .then(function () {
                        vm.getDealerFromEmail().then(function (data) {
                            vm.$router.push("/");
                            return Promise.resolve();
                        }).catch(function (data) {
                            vm.setMessage(new httpError({ message: 'unable to login' + data, httpStatus: 500 }));
                            vm.$router.push("/");
                        })

                    })
                    .catch(function (err) {
                        console.log(err);
                        vm.$router.push("/");

                    }).catch(function (err) {
                        console.log(err);
                        vm.$router.push("/");
                    })
            }).catch(function (err) {
                console.log(err.errorDescription);
                vm.$router.push("/");
            })
        },
        methods: {
            ...mapDealerActions(['getDealerFromEmail']),
            ...mapAuthMutations(['setAuth']),
            ...mapAuthActions(['setSession', 'checkLocal', 'login', 'clearSession', 'handleAuthentication', 'setMessage'])
        },
        computed: {
            ...mapAuthFields(['auth.name', 'auth.nickname', 'auth.idToken', 'auth.pictureURL', 'auth.isLoggedIn']),

        }
    }
</script>
<style>

    .callback {
        width: 40px;
        height: 40px;
        background-color: #333;
        margin: 100px auto;
        -webkit-animation: sk-rotateplane 1.2s infinite ease-in-out;
        animation: sk-rotateplane 1.2s infinite ease-in-out;
    }

    @-webkit-keyframes sk-rotateplane {
        0% {
            -webkit-transform: perspective(120px)
        }

        50% {
            -webkit-transform: perspective(120px) rotateY(180deg)
        }

        100% {
            -webkit-transform: perspective(120px) rotateY(180deg) rotateX(180deg)
        }
    }

    @keyframes sk-rotateplane {
        0% {
            transform: perspective(120px) rotateX(0deg) rotateY(0deg);
            -webkit-transform: perspective(120px) rotateX(0deg) rotateY(0deg)
        }

        50% {
            transform: perspective(120px) rotateX(-180.1deg) rotateY(0deg);
            -webkit-transform: perspective(120px) rotateX(-180.1deg) rotateY(0deg)
        }

        100% {
            transform: perspective(120px) rotateX(-180deg) rotateY(-179.9deg);
            -webkit-transform: perspective(120px) rotateX(-180deg) rotateY(-179.9deg);
        }
    }
</style>
