<template>
    <div v-if="order[0].orderID > 0 && isLoggedIn">
    <v-title  >Notes</v-title>
        <v-card >
            <v-card-title>
                <v-btn color="primary" @click="addRow">
                    <v-icon> {{isDirty ? 'save' : 'add'}}</v-icon>
                {{isDirty ? 'Save' : 'Add'}}</v-btn>
            </v-card-title>
        <v-card-text  v-if="Array.isArray(rows)">
            <v-layout  v-for="(note, index) in rows">
                   
                    <v-flex lg11>
                        <textarea class="form-control" id="exampleInputAmount" placeholder="Note" v-model="note.noteText" @keyup="isDirty = true" />
                    </v-flex>
                    <v-flex>
                        <v-flex v-show="note.noteID > 0"><v-icon color="green">check_circle</v-icon></v-flex>
                        <v-icon @click="deleteNote(note.noteID)">delete</v-icon>
                    </v-flex>

            </v-layout>
        </v-card-text>
        </v-card>
        </div>
</template>
<script>
import Vue from "vue";
import { mapActions, createNamespacedHelpers } from "vuex";
import { mapMultiRowFields } from "vuex-map-fields";
import { mapNoteFields, mapNoteMultiRowFields } from "../store/modules/orders";
const { mapActions: mapNoteActions } = createNamespacedHelpers(`orders/notes`);
const { mapState: mapOrderState } = createNamespacedHelpers(`orders`);
const { mapGetters: mapAuthGetters } = createNamespacedHelpers(`orders/auth`);

export default {
  name: "AppNote",
  data() {
    return {
      isDirty: false
    };
  },
  computed: {
    ...mapNoteFields(["message"]),
    ...mapNoteMultiRowFields(["rows"]),
    ...mapOrderState({ order: "rows" }),
    ...mapAuthGetters(["isLoggedIn"])
  },
  methods: {
    ...mapNoteActions(["addNote", "saveNote", "delNote", "getNotes"]),
    addRow: function() {
      var vm = this;
      if (vm.isDirty) {
        vm.saveNote().then(data => {
          vm.isDirty = false;
        });
      } else {
        vm.addNote(vm.$route.name).then(data => {
          vm.isDirty = true;
        });
      }
    },
    deleteNote: function(note) {
      this.delNote(note);
    }
  },

  watch: {
      $route(to, from) {
          
      var vm = this;
      if (vm.isDirty) vm.saveNote();
      this.getNotes(to.name).then(data => {
        vm.isDirty = false;
      });
    }
  }
};
</script>
<style scoped>
.form-group {
  margin-bottom: 2px;
}
</style>
