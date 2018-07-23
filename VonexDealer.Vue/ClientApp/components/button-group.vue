<template>
    <div>

        <div class="btn-group" role="group">
            <button type="button" class="btn btn-secondary" v-for="(key, val) in values" @click="changeSelectVal(key)">{{ val }}</button>
        </div>

        <div class="radio">
            <label>
                <input type="radio" name="optionsRadios" id="optionsRadios1" value="option1" checked>
                Option one is this and that&mdash;be sure to include why it's great
            </label>
        </div>
        <div class="radio">
            <label>
                <input type="radio" name="optionsRadios" id="optionsRadios2" value="option2">
                Option two can be something else and selecting it will deselect option one
            </label>
        </div>
        <div class="radio disabled">
            <label>
                <input type="radio" name="optionsRadios" id="optionsRadios3" value="option3" disabled>
                Option three is disabled
            </label>
        </div>
    </div>
</template>
<script>
    export default {
        props: ['values', 'default'],
        data() {
            return {
                selection: this.default
            }
        },
        ready: function () {
            this.selection = this.default;
        },
        methods: {
            changeSelectVal: function (val) {
                this.selection = val;
                this.$emit('clicked', val);
                
            }
        }
    };

</script>
<style scoped>
    .btn-group > .btn.active, .btn-group > .btn:active, .btn-group > .btn:focus, .btn-group > .btn:hover {
        z-index: 2;
        background-color: Highlight;
    }
</style>
