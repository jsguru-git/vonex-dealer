<template>
    <v-flex sm12 md12 lg12>
        <v-alert style="margin-top:20px" v-show="component.httpStatus != 0" :type="submitted"> 
                    {{component.message}}
            </v-alert>
    </v-flex>
</template>
<script>

    export default {
        name:'MessageAlert',
        props: ['component'],
        computed: {
            submitted: function () {
                if (this.component.httpStatus === 200) {
                    return 'success';
                } else
                    return 'error';
            }
        }

    }
</script>
