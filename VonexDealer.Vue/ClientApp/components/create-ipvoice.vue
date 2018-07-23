<template>
    <v-flex>
        <v-title>
            Create IP Voice
        </v-title>
        <v-card>
            <v-card-text>

                <ipvoice-extensions></ipvoice-extensions>

                <!--<v-flex sm12 md12 lg12>
                    <v-btn @click="next" color="primary">Save & Next</v-btn>
                </v-flex>-->
            </v-card-text>
        </v-card>
    </v-flex>

</template>
<script>
    import IPVoice from './ipvoice-extensions'

    export default {
        components: { 'ipvoice-extensions': IPVoice },
        methods: {
            next: function () {
                this.$router.push('create-internet');
            }
        }
    }

</script>
<style scoped>
    table.table tbody td, table.table tbody th {
        height: 100%;
    }
</style>
