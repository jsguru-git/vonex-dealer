<template>

    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        CUSTOMER DETAILS
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="isLoading" v-show="isDataLoading"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> <p>Loding...</p></div>
                    <table class="table table-striped table-bordered dataTable">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Phone No</th>
                                <th>Position</th>
                                <th>Contract Term</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in customers" track-by="id">
                                <td>{{item.firstName}} {{item.lastName}}</td>
                                <td>{{item.email}}</td>
                                <td>{{item.phoneNumber}}</td>
                                <td>{{item.position}}</td>
                                <td>{{item.contract_Term}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
    data() {
        return {
            customers: null,
            isDataLoading: false
        };
    },

    methods: {
    },

    async created() {
      
        this.isDataLoading = true;
        var response = await this.$http.get('https://api.vonex.com.au/api/customer?dealerid=0', {
            responseType: 'json'
        });
        this.customers = response.data;
       
        localStorage.setItem('CustomerByDealer_Details', JSON.stringify(this.customers));
        console.log(JSON.parse(localStorage.getItem('CustomerByDealer_Details')));
        this.isDataLoading = false;
    }
}
</script>

<style>

</style>
