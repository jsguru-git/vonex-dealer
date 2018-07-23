<template>
    <div><slot></slot><label>{{formatAddress}}</label></div>


</template>
<script>
    import { createNamespacedHelpers } from 'vuex';
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
    export default {
        props: ['addressID'],
        data: function () {
            return {}
        },
        computed: {
            ...mapOrderState(['address']),
            formatAddress() {
                var vm = this;
                var result = this.address.addresses.filter(function (address) {
                    return address.addressID == vm.addressID;
                });
                return `${result[0].streetAddress}, ${result[0].suburb}, ${result[0].state}, ${result[0].postCode}`;
            },
        }
    }
</script>
