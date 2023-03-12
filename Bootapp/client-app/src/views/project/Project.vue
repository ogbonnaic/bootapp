<template>
  <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h3 class="page-title">
        <router-link to="/projects"> <font-awesome-icon icon="arrow-left" /> </router-link>
      {{project.name}}
      <!--  <span class="badge bg-warning rounded-pill ">{{project.datasource.name}}</span> -->
    </h3>
    <div class="btn-toolbar mb-2 mb-md-0">
      <button class="btn btn-info btn-sm rounded" style="margin-top:-5px; margin-right:10px" type="button"
        data-bs-toggle="modal" data-bs-target="#keyModal"> Access Keys
        <font-awesome-icon icon="key" />
      </button>
       <button class="btn btn-warning btn-sm rounded" style="margin-top:-5px; margin-right:10px" type="button"  data-bs-toggle="modal" data-bs-target="#edit_project_modal"> Edit
        <font-awesome-icon icon="pencil" />
      </button>
    <!--   <button class="btn btn-secondary btn-sm" style="margin-top:-5px; margin-right:10px" type="button"
        @click="showAlert">
        <font-awesome-icon icon="user-secret" />
      </button> -->
    <div class="form-check form-switch" style="margin-right:10px">
        <input class="form-check-input" type="checkbox" id="flexSwitchCheckAuth" value="1" v-model="project.enable_auth"
          true-value="true" false-value="false" v-on:change="update_project" data-toggle="tooltip" data-placement="top"
          title="Enable authentication with this project">
        <label class="form-check-label" for="flexSwitchCheckAuth">Authentication</label></div>
      <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" id="flexSwitchCheckChecked" value="1" v-model="project.active"
          true-value="1" false-value="0" v-on:change="update_project">
        <label class="form-check-label" for="flexSwitchCheckChecked">Active</label>
      </div>
      <!--      <button type="button" class="btn btn-sm btn-primary">
                Back
            </button> -->
    </div>
  </div>
  <!-- <span>Test</span>
    <span>{{project.name}}</span> -->
  <Spinner msg="loading" v-if="loading" />
  <div class="row">
    <div class="col-md-7">
      <div class="form-group mb-3">
        <input type="text" class="form-control outline-secondary" placeholder="search table" v-model="search">
        <!--   <button class="btn btn-lg btn-outline-secondary" type="button" v-on:click="search_tables" id="button-addon2">Search</button> -->
      </div>
      <!--   <input type="text" placeholder="search table" class="form-control mb-3"> -->
      <div class="table-responsive">
        <table class="rwd-table">
          <thead>
            <tr>
              <!-- <th></th> -->
              <th>Table</th>
              <th>Schema</th>
              <!-- <th>Alias</th> -->
              <th>Rows</th>
              <th>Create</th>
              <th>Read</th>
              <th>Update</th>
              <th>Delete</th>
            </tr>
          </thead>
          <tbody v-for="table in all_tables" :key="table.table_name">

            <tr>

              <td>
                <a href="#" class="title_link" v-on:click="get_columns(project.id, table)">{{table.table_name}}</a>
              </td>
              <td>{{table.table_schema}}</td>
              <!-- <td></td> -->
              <td>{{table.rows}}</td>
              <td>
                <div class="form-check form-switch">
                  <input class="form-check-input" type="checkbox" :id="'fl_switch_create_'+table.table_name"
                    v-model="table.create" true-value="true" v-on:change="update_setting(table)" false-value="false">
                  <label class="form-check-label" :for="'fl_switch_create_'+table.table_name"></label>
                </div>
              </td>
              <td>
                <div class="form-check form-switch">
                  <input class="form-check-input" type="checkbox" :id="'fl_switch_read_'+table.table_name"
                    v-model="table.read" true-value="true" v-on:change="update_setting(table)" false-value="false">
                  <label class="form-check-label" :for="'fl_switch_read_'+table.table_name"></label>
                </div>
              </td>
              <td>
                <div class="form-check form-switch">
                  <input class="form-check-input" type="checkbox" :id="'fl_switch_updated_'+table.table_name"
                    v-model="table.update" true-value="true" v-on:change="update_setting(table)" false-value="false">
                  <label class="form-check-label" :for="'fl_switch_updated_'+table.table_name"></label>
                </div>
              </td>
              <td>
                <div class="form-check form-switch">
                  <input class="form-check-input" type="checkbox" :id="'fl_switch_delete_'+table.table_name"
                    v-model="table.delete" true-value="true" v-on:change="update_setting(table)" false-value="false">
                  <label class="form-check-label" :for="'fl_switch_delete_'+table.table_name"></label>
                </div>
              </td>
            </tr>

          </tbody>

        </table>
        <div class="d-flex justify-content-center mt-2">
          <pagination v-model="currentPage" :records="total" :per-page="perPage" @paginate="updateHandler" />
        </div>

      </div>

    </div>

    <div class="col-md-5">
      <div v-if="Object.keys( table ).length > 0">
        <div class="text-end">
          <h3>  {{table.table_name}} </h3>
        </div>
        <div>
          <div class="alert alert-primary" role="alert">
            <strong>POST Endpoint:</strong> <span>/api/v1/data/{{table.table_name}}</span>
          </div>
          <div class="alert alert-secondary" role="alert">
            <strong>UPDATE Endpoint:</strong> <span>/api/v1/data/{{table.table_name}}</span>
          </div>
          <div class="alert alert-success" role="alert">
            <strong>GET Endpoint:</strong> <span>/api/v1/data/{{table.table_name}}</span>
          </div>
          <div class="alert alert-danger" role="alert">
            <strong>DELETE Endpoint:</strong> <span>/api/v1/data/{{table.table_name}}</span>
          </div>
        </div>
        <div class="table-responsive">
          <table class="rwd-table">
            <thead>
              <tr>
                <th>Column Name</th>
                <th>Data Type</th>
                <th>Nullable</th>
                <!-- <th>Contraint</th> -->
              </tr>
            </thead>
            <tbody>
              <tr v-for="column in columns" :key="column.column_name">
                <td>{{column.column_name}}</td>
                <td><span class="data-type">{{column.data_type}}</span></td>
                <td>{{column.is_nullable}}</td>
                <!-- <td>{{column.constraint_type}}</td> -->
              </tr>
            </tbody>
          </table>

        </div>
      </div>
    </div>
  </div>
  <div>

  </div>

  <div class="modal fade" id="keyModal" tabindex="-1" aria-labelledby="keyModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Security</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
       <div class="input-group mb-3">
  <span class="input-group-text" id="basic-addon1">API_KEY</span>
  <input type="text" class="form-control" placeholder="API_KEY" v-model="project.api_key">
</div>
<div class="input-group mb-3">
  <span class="input-group-text" id="basic-addon1">API_SECRET</span>
  <input type="text" class="form-control" placeholder="API_SECRET" v-model="project.api_secret">
</div>
      </div>
      <div class="modal-footer">
               <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<div class="modal fade" id="edit_project_modal" tabindex="-1" aria-labelledby="editProjectModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Update Project</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
<form ref="form" id="form_project">
   <div class="form-group">
        <label for="name">Name</label>
        <input
        type="text"
        required
           v-model="project.name"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">Name of the project is required</div>
      </div>

      <div class="form-group">
        <label for="code">Code</label>
        <input
        type="text"
        required
           v-model="project.code"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">Project code is required</div>
      </div>

</form>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" id="close" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" v-on:click.stop.prevent="update_project" :disabled="disabled">
           <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-if="loading"></span>
                            Update
          </button>
      </div>
    </div>
  </div>
</div>

</template>

<script>
// Datatable Modules
// import 'datatables.net-dt/js/dataTables.dataTables'
// import 'datatables.net-dt/css/jquery.dataTables.min.css'
// import $ from 'jquery'
// import VuePaginationTw from 'vue-pagination-tw'
import ProjectService from '../../services/ProjectService'
import TableSettingService from '../../services/TableSettingService'
import Spinner from '../../components/Spinner.vue'
import Pagination from 'v-pagination-3'
import { Popover } from 'bootstrap/dist/js/bootstrap.esm.min.js'

export default ({
  name: 'Project',
  components: { Spinner, Pagination },
  data () {
    return {
      currentPage: 1,
      perPage: 25,
      total: 20,
      project: {},
      id: '',
      tables: [],
      table_lg: [],
      search: '',
      loading: false,
      columns: [],
      table: {},
      table_settings: [],
      disabled: false

    }
  },
  methods: {
    get_project (id) {
      ProjectService.get(id)
        .then(response => {
          this.project = response.data
          this.get_table_settings()
          // $('#dataTable').DataTable()
        }).catch(e => {
          console.log(e)
        })
    },
    get_tables (id) {
      ProjectService.getProjectTablesWithSettings(id)
        .then(response => {
          this.tables = response.data
          this.table_lg = response.data
          this.total = this.table_lg.length
        }).catch(e => {
          console.log(e)
        })
    },
    get_columns (id, table) {
      this.table = table
      ProjectService.getTableColumns(id, table.table_schema, table.table_name)
        .then(response => {
          this.columns = response.data
        }).catch(e => {
          console.log(e)
        })
    },
    search_tables () {
      this.tables = this.table_lg.filter(tb => tb.table_name.includes(this.search))
    },
    get_table_settings () {
      TableSettingService.getProjectTableSettings(this.project.id)
        .then(response => {
          this.table_settings = response.data
        }).catch(e => {
          console.log(e)
        })
    },
    update_setting (data) {
      data.project_id = this.project.id
      TableSettingService.update(data)
        .then(response => {
          this.loading = false
        }).catch(e => {
          console.log(e)
        })
    },
    update_project () {
      var item = this.project
      item.datasourceid = item.datasource.id
      this.loading = true
      this.disabled = true
      // alert(item.datasourceid)
      ProjectService.update(item)
        .then(response => {
          this.loading = false
          this.disabled = false
          document.getElementById('close').click()
          this.$swal('Update', 'Project was updated successfully.', 'success')
        }).catch(function (error) {
          this.loading = false
          this.disabled = false
          console.log(error)
          this.$swal('Update', 'There was an error updating the project.', 'error')
        })
    }

  },
  computed: {
    totalPages () {
      if (this.tables.length === 0) {
        return 1
      } else {
        return Math.ceil(this.tables.length / this.perPage)
      }
    },
    all_tables () {
      if (!this.tables) {
        return
      }
      // this.total = this.table_lg.length
      if (this.currentPage >= this.total) {
        // this.currentPage = this.total
      }
      var index = this.currentPage * this.perPage - this.perPage
      return this.tables.filter(tb => tb.table_name.includes(this.search)).slice(index, index + this.perPage)
    }
  },
  mounted () {
    this.id = this.$route.params.id
    this.get_project(this.$route.params.id)
    this.get_tables(this.id)

    Array.from(document.querySelectorAll('button[data-bs-toggle="popover"]'))
      .forEach(popoverNode => new Popover(popoverNode))
  }

})
</script>
<style scoped>
table>:not(:first-child) {
    border-top: 0;
}
.title_link {
  text-decoration: none;
  color: #31525B;
  font-weight: 500;
}
.data-type{
  font-weight: 800;
  font-size: 1rem;
}
.form-check-input:checked {
    background-color: #31525B;
    border-color: #31525B;
}
.page-item.active .page-link {
 background-color: #8CBDB9 !important;
 border-color: #8CBDB9 !important;
}

.form-group{
  margin-bottom: 10px;
}

.form-group label{
  font-weight: bold;
}
.page-link.active{
  background-color: #31525B !important;
}
</style>
