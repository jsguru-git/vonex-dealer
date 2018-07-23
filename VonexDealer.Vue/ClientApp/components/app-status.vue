<template>
    <div>
        <v-title>
            Order Status
        </v-title>
        <v-card>
            <v-card-text>
                <v-layout wrap></v-layout>
                <v-flex sm6 md6 lg6>

                    <v-text-field label="Vonex account has now been created for new customer:" v-model="customer.customer.ubAccountNo" readonly v-if="customer.customer.ubAccountNo"></v-text-field>
                </v-flex>
                <v-flex sm12 md12 lg12>
                    <blockquote>
                        To add products to this order,
                        please select the appropriate links from the left menu
                    </blockquote>
                </v-flex>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>
    import { createNamespacedHelpers } from 'vuex'
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`)

    export default {
        computed: {
            ...mapOrderState(['auth', 'customer', 'dealer']),

        },
    };

</script>
