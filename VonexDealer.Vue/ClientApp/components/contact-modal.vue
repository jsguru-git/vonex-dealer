<template>
    <div>
        <v-title>
            Add Contact
        </v-title>

        <v-card>
            <v-card-text>
                <v-container grid-list-md>
                    <v-layout wrap>
                        <v-flex sm4 md4 lg4>
                            <v-text-field v-model="contact.contactName" label="Contact Name" v-validate="'required'" name="contactName" />
                            <span v-show="errors.has('contactName')" class="text-danger">{{ errors.first('contactName') }}</span>
                        </v-flex>
                        <v-flex sm4 md4 lg4>
                            <v-text-field v-model="contact.contactMobile" label="Mobile" v-validate="'required|mobilenumber'" name="contactMobile" />
                            <span v-show="errors.has('contactMobile')" class="text-danger">{{ errors.first('contactMobile') }}</span>
                        </v-flex>
                        <v-flex sm4 md4 lg4>
                            <v-text-field v-model="contact.contactEmail" label="Email" v-validate="'required|email'" name="contactEmail" />
                            <span v-show="errors.has('contactEmail')" class="text-danger">{{ errors.first('contactEmail') }}</span>
                        </v-flex>

                        <v-layout wrap>
                            <v-flex sm6 md6 lg6>
                                <v-btn color="primary" v-on:click="cancel">
                                    Cancel
                                </v-btn>
                            </v-flex>
                            <v-flex right sm6 md6 lg6>
                                <v-btn color="primary" v-on:click="closeContact">
                                    Save & Close
                                </v-btn>


                            </v-flex>
                        </v-layout>
                    </v-layout>
                </v-container>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>
    //@ts-check
    export default {
        data() {
            return {
                contact: {
                    contactName: null,
                    contactEmail: null,
                    contactMobile: null,
                }
            }
        },
        methods: {
            cancel() {
                this.$emit('closeContact');
            },
            closeContact: function () {
                var vm = this;

                vm.$validator.validateAll().then((result) => {
                    if (result) {
                        this.$emit('closeContact', this.contact);

                    }
                });
            },

        }
    }
</script>
