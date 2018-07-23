<template>
    <div>
        <v-title>Hardware</v-title>
        <v-card>
            <v-card-text>
                <handset-list></handset-list>
            </v-card-text>
        </v-card>

    </div>
</template>
<script>
    import { HTTP } from '../http-common';
    import HandsetList from './handset-list.vue';


    export default {
        components: {
            'handset-list': HandsetList
        },
        data: function () {
            return {
            }
        },
        computed: {

        },
        methods: {

        }
    }
</script>
<style scoped>
</style>
