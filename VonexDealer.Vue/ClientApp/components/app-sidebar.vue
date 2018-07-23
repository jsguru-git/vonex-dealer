<template>
    <div>
        <v-navigation-drawer persistent
                             :mini-variant="miniVariant"
                             :clipped="clipped"
                             v-model="drawer"
                             enable-resize-watcher
                             fixed
                             app>

            <v-toolbar flat color="secondary">

                <v-list>
                    <v-list-tile @click="navigate('/')">
                        <v-list-tile-title class="title">
                            Start
                        </v-list-tile-title>
                        <v-list-tile-action>
                            <v-icon>home</v-icon>
                        </v-list-tile-action>
                    </v-list-tile>
                </v-list>
            </v-toolbar>
            <v-divider></v-divider>
            <v-list>
                <v-list-tile v-for="item in items.filter(x => status(x.title))" :key="item.title" @click="status(item.title) ? navigate(item.route) : ''">
                    <v-list-tile-action v-if="status(item.title)">
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content v-if="status(item.title)">
                        <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </v-list>
        </v-navigation-drawer>
        <v-toolbar app
                   :clipped-left="clipped"
                   color="primary">

            <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
            <!--<v-btn icon @click.stop="miniVariant = !miniVariant">
                <v-icon v-html="miniVariant ? 'chevron_right' : 'chevron_left'"></v-icon>
            </v-btn>
            <v-btn icon @click.stop="clipped = !clipped">
                <v-icon>web</v-icon>
            </v-btn>
            <v-btn icon @click.stop="fixed = !fixed">
                <v-icon>remove</v-icon>
            </v-btn>
            <v-toolbar-title v-text="title"></v-toolbar-title>-->
            <v-card width="150px">
                <v-card-media src="/images/vonexLogo.png" height="64px"></v-card-media>

            </v-card>

            <v-spacer></v-spacer>
            <app-header></app-header>

        </v-toolbar>
    </div>
</template>

<script>
    import { createNamespacedHelpers, mapGetters } from 'vuex';
    const { mapGetters: mapAuthGetters } = createNamespacedHelpers(`orders/auth`);
    const { mapState: mapOrderState, mapGetters: mapOrderGetters } = createNamespacedHelpers(`orders`);


    export default {
        name: 'sidebar',
        data() {
            return {
                clipped: true,
                fixed: false,
                drawer: true,
                miniVariant: false,
                right: true,
                rightDrawer: false,
                title: 'Vonex',
                enabledClass: 'enabled',
                disabledClass: 'disabled',
                items: [{
                    icon: 'bubble_chart',
                    title: 'Business',
                    route: 'create-company'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Auth Rep',
                    route: 'create-customer'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Residential',
                    route: 'create-customer'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Fixed Line',
                    route: 'create-pstn'
                },
                {
                    icon: 'bubble_chart',
                    title: 'IP Voice',
                    route: 'create-ipvoice'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Internet',
                    route: 'create-internet'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Mobile Plans',
                    route: 'create-mobile'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Inbound',
                    route: 'create-inbound'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Porting',
                    route: 'create-porting'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Hardware',
                    route: 'create-hardware'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Services',
                    route: 'sip'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Finance',
                    route: 'finance'
                },
                {
                    icon: 'bubble_chart',
                    title: 'Sign and Send',
                    route: 'order-summary'
                },
                ]
            }
        },
        watch: {
            '$route'(to, from) {
                // react to route changes...
                var vm = this;
                var route = vm.$route;
                //vm.IsService = vm.services.indexOf(route.name) > -1;
                //vm.IsExisting = vm.existing.indexOf(route.name) > -1;
            }
        },
        computed: {
            ...mapOrderState(['auth', 'message', 'isResidential', 'rows', 'address', 'company', 'customer', 'dealer', 'landline', 'newChurn']),
            ...mapOrderGetters(['hasDealer', 'hasOrder', 'isSigned', 'hasAddress']),
            ...mapAuthGetters(['isLoggedIn']),

            hasStartedOrder: function () {
                return this.$route.name !== 'home';
            },
        },
        mounted: function () {
            var vm = this;
            vm.$nextTick(function () {
                // Code that will run only after the
                // entire view has been rendered
                var route = vm.$route;
                //vm.IsService = vm.services.indexOf(route.name) > -1;
                //vm.IsExisting = vm.existing.indexOf(route.name) > -1;
            })
        },
        methods: {
            navigate: function (route) {
                this.$router.push(route);
            },
            status: function (el) {

                var result = true;

                switch (el) {
                    default:
                        result = false;
                        break;
                    case 'Business':
                        result = !this.isResidential && ((this.isLoggedIn || false) && this.hasDealer);
                        break;
                    case 'Auth Rep':
                        result = !this.isResidential && ((this.isLoggedIn || false) && this.hasDealer) && this.company.company.companyID > 0;
                        break;
                    case 'Residential':
                        result = this.isResidential && ((this.isLoggedIn || false) && this.hasDealer);
                        break;
                    case 'Fixed Line':
                    case 'Hardware':
                    case 'IP Voice':
                    case 'Internet':
                        result = ((this.isLoggedIn || false) && this.hasOrder && this.hasAddress);
                        break;
                    case 'Sign and Send':
                        result = (this.hasOrder) && this.hasAddress;
                        break;
                    case 'Hardware':
                        result = ((this.isLoggedIn || false) && this.hasOrder);

                }

                return result && !this.isSigned && this.hasStartedOrder;

            },
            collapseService: function () {
                this.IsService = !this.IsService;
            },
            collapseExisting: function () {
                this.IsExisting = !this.IsExisting;
            },

        }
    }
</script>
<style>
    .expand {
        height: auto !important;
        display: block !important;
    }

    .new-collapse {
        display: none !important;
    }
</style>
