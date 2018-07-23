<template>
    <div class="row centered-form">
        <v-title>Porting</v-title>
        <v-card>
            <v-card-text>
                <v-container grid-list-md>
                    <v-layout row wrap>
                        <v-flex sm12 md12 lg12>

                            <h2>IP Porting Request</h2>
                            <p>I wish to port the following:</p>
                           
                            <table class="table table-striped table-bordered mobile-table">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Category</th>
                                        <th>Service Number or Start of Range</th>
                                        <th>Current Account Number</th>
                                        <th>Current Service Provider</th>
                                        <th>White Pages Listing</th>
                                        <th>Current Bill Upload</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td></td>
                                        <td width="10%"><v-select :items="category"></v-select></td>
                                        <td><v-text-field class="form-control"></v-text-field></td>
                                        <td><v-text-field class="form-control"></v-text-field></td>
                                        <td><v-text-field class="form-control"></v-text-field></td>
                                        <td><v-select :items="yesNo"></v-select></td>
                                        <td><input type="file" name="files" /></td>
                                        <td>
                                            <div class="text-xs-center">
                                                <v-btn color="primary" title="Save" flat><v-icon dark>save</v-icon></v-btn><br />
                                                <v-btn color="warning" title="Add Row" flat><v-icon dark>add_circle</v-icon></v-btn><br />
                                                <v-btn color="red" title="Remove Row" flat><v-icon dark>block</v-icon></v-btn>
                                            </div>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                            <terms-conditions>The Porting Authority form is valid for 90 days from date of signing.</terms-conditions>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>
    // import termsConditions from './terms-conditions'
    export default {
        components: {
            // termsConditions,
        },
        methods: {
            AddPort() {
                this.Port = !this.Port
            },
        },
        data() {
            return {
                isDataLoading: false,
                WhitePages: null,
                PhoneAccount: null,
                Port: false,
                yesNo: ['Yes', 'No'],
                category: ['A - Single', 'C - Range']
            }
        },
        
    }
</script>
