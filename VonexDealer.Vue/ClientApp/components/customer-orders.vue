<template>
    <div class="row centered-form">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="panel panel-default Custom_form">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        CUSTOMER ORDERS
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="isLoading" v-show="isDataLoading"><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i> <p>Loding...</p></div>
                    <table class="table table-striped table-bordered dataTable">
                        <thead>
                            <tr>
                                <th>Dealer Name</th>
                                <th>Customer Name</th>
                                <th>Created Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in orders">
                                <td>{{item.DealerDetails.DealerFullName}}</td>
                                <td>{{item.CustomerDetails.FirstName}} {{item.CustomerDetails.LastName}}</td>
                                <td>{{item.CreatedDate}}</td>
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
                orders: null,
                isDataLoading: false
            };
        },
        async created() {
            this.isDataLoading = true;
            var response = await this.$http.get('https://localhost:44324/api/Order/GetOrders/1', {
                responseType: 'json'
            });
            this.orders = response.data;
            this.$nextTick(() => {
                $('.dataTable').DataTable();
            });
            this.isDataLoading = false;
        }
    }
</script>
<style>

</style>
