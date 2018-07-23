<template>
  <v-container fluid>
     <v-dialog v-model="dialog" max-width="500px">
        <v-btn slot="activator" color="primary" dark class="mb-2">New Item</v-btn>
        <v-card>
          <v-card-title>
            <span class="headline">Edit RatePlan</span>
          </v-card-title>
          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12 sm12 md12>
                  <v-text-field v-model="editedItem.ratePlanDescription" label="Rate Plan"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.usageType" label="UsageType"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.handset" label="Handset"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.monthlyRetail" label="Monthly Retail"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.contractMonths" label="Contract Months"></v-text-field>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
            <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
        <v-data-table
      :total-items="totalItems"
      :pagination.sync="pagination"
      :items="items"
      :headers="headers"
      :loading="loading"
                      
      >
     

    <template slot="items" slot-scope="props">
          <td>{{props.item.ratePlanID}}</td>
          <td>{{props.item.ratePlanDescription}}</td>
          <td>{{props.item.usageType}}</td>
          <td>{{props.item.contractMonths}}</td>
          <td><v-checkbox v-model="props.item.dealerSupplied" :disabled=true></v-checkbox></td>
          <td><v-checkbox v-model="props.item.mixnMatch" :disabled=true></v-checkbox></td>
          <td><v-checkbox v-model="props.item.additionalServices" :disabled=true></v-checkbox></td>
          <td><v-checkbox v-model="props.item.residential" :disabled=true></v-checkbox></td>

        <td class="justify-center layout px-0">
            <v-btn icon class="mx-0" @click="editItem(props.item)">
              <v-icon color="teal">edit</v-icon>
            </v-btn>
            <v-btn icon class="mx-0" @click="deleteItem(props.item)">
              <v-icon color="pink">delete</v-icon>
            </v-btn>
          </td>
      </template>
      
  </v-data-table>
   
  </v-container>
</template>
<script>
import { mapActions, createNamespacedHelpers } from "vuex";
import { mapMultiRowFields } from "vuex-map-fields";
import {
  mapRatePlanFields,
  mapRatePlanMultiRowFields
} from "../store/modules/orders";
const {
  mapActions: mapRatePlanActions,
  mapMutations: mapRatePlanMutations
} = createNamespacedHelpers(`orders/ratePlan`);
import { httpError } from "../models/httpError";
import { ratePlan } from "../models/ratePlan";

export default {
  data: () => ({
    dialog: false,
    editedItem: new ratePlan(),
    editedIndex: 0,
    items: [],
    totalItems: 0,
    pagination: {
      page: 1,
      rowsPerPage: 10,
      totalItems: 0
    },
    loading: false,
    headers: [
      {
        text: "RatePlanID",
        align: "left",
        sortable: false,
        value: "rateplanID"
      },
      {
        text: "Rate Plan",
        align: "left",
        sortable: true,
        value: "rateplanDescription"
      },
      {
        text: "Usage Type",
        align: "left",
        sortable: true,
        value: "usageType"
      },
      {
        text: "Contract Months",
        align: "right",
        sortable: true,
        value: "contractMonths"
      },
      {
        text: "Dealer Supplied",
        align: "right",
        sortable: true,
        value: "dealerSupplied"
      },
      {
        text: "Mix n Match",
        align: "left",
        sortable: true,
        value: "mixnmatch"
      },
      {
        text: "Additional",
        align: "left",
        sortable: true,
        value: "aditionalServices"
      },
      {
        text: "Residential",
        align: "left",
        sortable: true,
        value: "residential"
      },
      {
        text: "",
        align: "right",
        sortable: false,
        value: ""
      }

    ]
  }),

  computed: {
    ...mapRatePlanFields(["message"])
  },

  methods: {
    ...mapRatePlanActions(["getRatePlans", "saveRatePlans", "removeRatePlan"]),
    ...mapRatePlanMutations(["addRow"]),
    getDataFromApi() {
      var vm = this;
      this.loading = true;
      return new Promise((resolve, reject) => {
        const { sortBy, descending, page, rowsPerPage } = vm.pagination;
        this.getRatePlans(vm.pagination).then(data => {
          let items = data.data;
          const total = data.count;
          vm.loading = false;
          resolve({
            items,
            total
          });
        });
      });
    },

    editItem(item) {
      this.editedIndex = this.items.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.items.indexOf(item);
      let answer = confirm("Are you sure you want to delete this item?");
      if (answer) {
        this.removeRatePlan(item.ratePlanID).then(data => {
          this.items.splice(index, 1);
        });
      }
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.items[this.editedIndex], this.editedItem);
        this.saveRatePlans(this.editedItem);
      } else {
        this.items.push(this.editedItem);
        this.saveRatePlans(this.editedItem);
      }
      this.close();
    }
  },

  watch: {
    pagination: {
      handler() {
        this.getDataFromApi().then(data => {
          this.items = data.items;
          this.totalItems = data.total;
        });
      },
      deep: true
    }
  }
};
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
