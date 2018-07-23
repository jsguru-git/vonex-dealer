<template>
    <v-menu ref="menu1"
            :close-on-content-click="false"
            v-model="menu1"
            :nudge-right="40"
            lazy
            transition="scale-transition"
            offset-y
            full-width
            max-width="290px"
            min-width="290px">
        <v-text-field slot="activator"
                      v-model="dateFormatted"
                      :label="label"
                      hint="DD/MM/YYYY format"
                      persistent-hint
                      prepend-icon="event"
                      @blur="bubble"
                      >
                      </v-text-field>
        <v-date-picker v-model="date" no-title @input="parseDate" locale="en-au"></v-date-picker>
    </v-menu>
</template>
<script>
    import Vue from 'vue'

    export default {
        name: 'DatePicker',
        props: ['value','label'],
        data: () => ({
            date: null,
            dateFormatted: null,
            menu1: false,
            menu2: false
        }),
        created() {
            if (Object.prototype.toString.call(this.value) === "[object Date]") {
                // it is a date
                if (isNaN(d.getTime())) {  // d.valueOf() could also work
                    // date is not valid
                }
                else {
                    this.date = this.value;
                    this.dateFormatted = this.formatDate(this.value);
                }
            }
            else {
                // not a date
            }
            
        },
      
        methods: {
            bubble() {
                this.$emit('input', this.dateFormatted);
            },
            formatDate(date) {
                if (!date) return null

                const [year, month, day] = date.substring(0, 10).split('-');
                return `${day}/${month}/${year}`;
            },
            parseDate() {
                if (!this.date) return null;
                this.menu1 = false;
                this.dateFormatted = this.formatDate(this.date);
                this.bubble();
            }
        }
    }
    </script>
