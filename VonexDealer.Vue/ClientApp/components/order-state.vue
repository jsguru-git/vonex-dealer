<template>
    <div class="bc clearfix" v-if="isLoggedIn">
        <a><h4><span>Order Progress:</span></h4></a>
        <a v-show="dealer.dealer.dealerID">
            <span>
                Dealer:
                {{dealer.dealer.dealerID}}
            </span>
        </a>
        <a v-show="rows[0].orderID"><span>Order: {{rows[0].orderID}}</span></a>
        <a v-if="company.company.abn">
            <span>
                Company:{{company.company.companyID}}
            </span>
        </a>

        <a v-show="customer.customer.customerID">
            <span>
                Customer:
                {{customer.customer.customerID}}
            </span>
        </a>
        <a v-show="rows[0].landlineID>0">
            <span>
                Fixed Line:
                {{rows[0].landlineID}}
            </span>
        </a>

    </div>
</template>
<script>

    import { createNamespacedHelpers } from 'vuex';
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);

    const { mapGetters: mapAuthGetters } = createNamespacedHelpers(`orders/auth`);

    export default {

        computed: {
            ...mapOrderState(['auth', 'rows', 'address', 'company', 'customer', 'dealer', 'landline', 'newChurn']),
            ...mapAuthGetters(['isLoggedIn'])

        },
        mounted() {
            this.$nextTick(() => {


            })
        },
        methods: {

            logout() {
                auth.logout()
                this.$router.go('/login')
            },

            login() {
                auth.login()
            }
        },
    };

</script>
<style>

    .user-dashboard {
        background: #e1e1d6;
    }

    h4 {
        margin: 1px 0 0 0;
    }

    h1.bc, .bc {
        font: bold 11px "Lucida Grande","Lucida","Arial",Sans-serif;
        margin: 10px 26px 0;
        padding: 0 0 2px 0;
        border-bottom: 1px solid #EBEBEB
    }

        h1.bc a, h1.bc a:visited, .bc a, .bc a:visited {
            background: #e1e1d6;
            display: block;
            color: #2f5578;
            float: left;
            height: 23px;
            padding-left: 5px;
            text-decoration: none
        }

            h1.bc a:hover, .bc a:hover {
                color: #2f5578;
            }

            h1.bc a span, .bc a span {
                background: transparent url(/images/bc_segment_right.png) no-repeat scroll top right;
                display: block;
                line-height: 13px;
                padding: 5px 17px 5px 6px
            }

            h1.bc a.root, h1.bc a.root:visited, h1.bc a.root-single, h1.bc a.root-single:visited, .bc a.root, .bc a.root:visited, .bc a.root-single, .bc a.root-single:visited {
                background: transparent url(/images/bc_root_left.gif) no-repeat scroll top left
            }

                h1.bc a.root-single span, .bc a.root-single span {
                    background: transparent url(/images/bc_segment_right_end.png) no-repeat top right;
                    padding-right: 14px
                }

                h1.bc a.root span, .bc a.root span {
                    background: transparent url(/images/bc_segment_right.png) no-repeat top right
                }

            h1.bc a.parent span, .bc a.parent span {
                background: transparent url(/images/bc_segment_right_end.png) no-repeat top right
            }

            h1.bc a.end, h1.bc a.end:visited, .bc a.end, .bc a.end:visited {
                background: none
            }

                h1.bc a.end:hover, .bc a.end:hover {
                    color: black;
                    cursor: default
                }

                h1.bc a.end span, .bc a.end span {
                    background: none;
                    color: black
                }
</style>
