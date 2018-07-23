<template>
    <table class="table table-striped table-bordered">
        <thead>
            <tr>
                <th v-for="(item,key) in summary" :key="key">{{key}}</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td v-for="(item,key) in summary" :key="key">
                    {{item}}
                </td>
            </tr>
        </tbody>
    </table>
</template>
<script>
    export default {
        props: ['summary', 'hideCols', 'showCols'],
        data: function () {
            return {

            }
        },
        created: function () {
            for (var field in this.hideCols) {
                delete this.summary[this.hideCols[field]];
            }
        }
    }
</script>
