<template>
    <v-card>
        <v-card-title>
            Signature of authorised customer
        </v-card-title>
        <v-card-media :src="signature" id='sign_prev' v-if="signature">
        </v-card-media>
        <v-card-text>
            <div v-if="edit">
                <VueSignaturePad height="400px" saveType="image/png"
                                 ref="signaturePad" v-validate="'required'"></VueSignaturePad>
            </div>
        </v-card-text>
        <v-card-actions>
            <v-btn @click="change" color="primary" v-show="!edit">Edit Signature</v-btn>
            <v-btn @click="save" color="primary" v-show="edit">Save Signature</v-btn>
            <v-btn @click="undo" color="primary" v-show="edit">Undo</v-btn>
        </v-card-actions>
        <message-alert :component="message"></message-alert>
    </v-card>
</template>
<script>
    import Vue from 'vue';
    import VueSignaturePad from 'vue-signature-pad';
    // Vue.use(VueSignaturePad);
    import { createNamespacedHelpers } from 'vuex';
    import { mapOrderFields } from '../store/modules/orders';
    const { mapActions: mapOrderActions } = createNamespacedHelpers(`orders`);
    const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);

    export default {
        name: 'signature-pad',
        props: ['edit'],
        components: { VueSignaturePad },
        methods: {
            ...mapOrderActions(['updateSignature']),
            undo() {
                this.$refs.signaturePad.undoSignature();
            },
            save() {
                var vm = this;
                const data = this.$refs.signaturePad.saveSignature();
                const orderGuid = this.$route.params.guid;
                
                if (data.isEmpty === false)
                var signature = { data: data.data, orderGUID: orderGuid };
                    this.updateSignature(signature).then(function () {

                        vm.$emit('closeModal');
                    }).catch(err => {
                        vm.$emit('signFailed');
                        })

            },
            change() {
                this.edit = true;
            }

        },
        computed: {
            ...mapOrderFields(['rows[0].signature']),
            ...mapOrderState(['message'])
        },
        //data() {
        //    return {
        //        edit: false,
        //    }
        //}
    }
</script>
<style>
    canvas {
        background-color: #a21a8014;
    }
</style>
