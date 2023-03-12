<template>
    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h3 class="page-title"><font-awesome-icon icon="database" /> Projects</h3>
        <div class="btn-toolbar mb-2 mb-md-0">

            <button type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#new_project_modal">
             <font-awesome-icon icon="plus" /> New Project
            </button>
        </div>
    </div>
    <div class="row">
      <Spinner :msg="message" v-if="loading"  />
        <div class="col-md-3" v-for="project in projects" :key="project.id">

            <div class="card shadow mb-3">

               <!--  <img class="rounded-circle mx-auto mt-2" src="data:image/gif;base64,R0lGODlhAQABAIAAAHd3dwAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" alt="Generic placeholder image" width="140" height="140"> -->
                <img class=" mx-auto mt-2" :src="'/api/datasource/getimage/'+project.datasource.id" :alt="project.name" width="140" height="140">

                <div class="card-body">
                    <p class="card-text text-center">
                        {{project.name}}
                    </p>
                 <!--    <span>
                        {{project.code}}
                    </span> -->

                    <div class="d-flex bd-highlight">
  <div class="bd-highlight" style="padding-right: 10px">
    <a :href="'/projects/' + project.id" class="btn btn-sm btn-primary text-white"><font-awesome-icon icon="folder-open" /> View</a>
    </div>
  <div class="bd-highlight">
   <!--  <button type="button" class="btn btn-sm btn-success text-white"><font-awesome-icon icon="edit" /> Edit</button> -->
    </div>
  <div class="ms-auto bd-highlight"><small class="text-muted ms-auto text-end"> <font-awesome-icon icon="calendar" />
                          <DateFromNow :date="project.created_at"/>
                          </small></div>
</div>
                </div>
            </div>
        </div>

    </div>
<!-- Modal -->
<div class="modal fade" id="new_project_modal" tabindex="-1" aria-labelledby="newProjectModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Add a New Project</h5>
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

      <div class="form-group">
        <label>Datasource</label>
        <select
        required
           v-model="project.datasourceid"
           class="form-control  form-select"
          >
          <option v-for="datasource in datasources" :value="datasource.id" :key="datasource.id">{{datasource.name}}</option>
        </select>
        <div class="invalid-feedback">You must select a datasource</div>
      </div>

<div v-if="project.datasourceid==1 || project.datasourceid == 2 || project.datasourceid == 3">
       <div class="form-group">
        <label>Host</label>
        <input
        type="text"
        required
           v-model="project.host"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">Host is required</div>
      </div>

      <div class="form-group">
        <label>Port</label>
        <input
        type="text"
        required
           v-model="project.port"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">Port is required</div>
      </div>
          <div class="form-group">
        <label>User ID</label>
        <input
        type="text"
        required
           v-model="project.user_id"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">User ID is required</div>
      </div>

      <div class="form-group">
        <label>Password</label>
        <input
        type="password"
        required
           v-model="project.password"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">Password is required</div>
      </div>

          <div class="form-group">
        <label>Database</label>
        <input
        type="text"
        required
           v-model="project.database"
           v-bind:class="{'form-control ':true}"
          >
        <div class="invalid-feedback">Database is required</div>
      </div>

      <div class="alert alert-success" role="alert" v-if="project_success">
Your project has been created successfully.
</div>
</div>
          <div class="form-check form-switch" style="margin-right:10px;">
  <input class="form-check-input" type="checkbox" id="flexSwitchCheckAuth" value="1" v-model="project.enable_auth" true-value="true"
  false-value="false" data-toggle="tooltip" data-placement="top" title="Enable authentication with this project">
  <label class="form-check-label" for="flexSwitchCheckAuth">Enable Authentication</label> <span data-bs-toggle="tooltip" data-bs-placement="right" title="Check this option if you want an authentication system to be setup for this database"><font-awesome-icon icon="info-circle" /> </span>
</div>
</form>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" id="close" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" v-on:click.stop.prevent="submit" :disabled="disabled">
           <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-if="loading"></span>
                            Create Project
          </button>
      </div>
    </div>
  </div>
</div>
</template>

<script>// @ is an alias to /src

import ProjectService from '../../services/ProjectService'
import DatasourceService from '../../services/DatasourceService'
import Validator from '../../utils/validator'
import Spinner from '../../components/Spinner.vue'
import DateFromNow from '../../components/DateFromNow.vue'
// import DateFormatter from '../../utils/DateFormatter'
import { Tooltip } from 'bootstrap/dist/js/bootstrap.esm.min.js'

export default {
  name: 'Projects',
  components: {
    Spinner, DateFromNow

  },
  data () {
    return {
      projects: [],
      project: {},
      email: '',
      isValid: false,
      datasources: [],
      message: '',
      loading: false,
      project_success: false,
      disabled: false,
      processing: false
    }
  },
  methods: {
    get_projects () {
      var self = this
      self.loading = true
      self.message = 'Loading projects. Please wait...'
      ProjectService.getAll()
        .then((response) => {
          self.projects = response.data
          self.loading = false
        })
        .catch((error) => {
          console.log(error)
        })
    },

    get_datasources () {
      var self = this
      DatasourceService.getActive()
        .then((response) => {
          self.datasources = response.data
        })
        .catch((error) => {
          console.log(error)
        })
    },
    submit () {
      var self = this
      self.processing = true
      self.disabled = true
      // console.log(this.$refs.form)
      // self.isValid = Validator.validateEmail(self.email)
      if (Validator.validate('form_project')) {
        ProjectService.create(self.project)
          .then((response) => {
            self.projects.push(response.data)
            self.processing = false
            self.project_success = true
            self.disabled = false

            document.getElementById('close').click()
            this.$swal('Create Project', 'Project was added successfully.', 'success')
          }).catch(function (error) {
            console.log(error)
            this.$swal('Create Project', 'There was an error creating the project. Review your entry and try again. If the issue persists contact administrator', 'error')
          })
      }
    }
  },
  mounted () {
    this.get_projects()
    this.get_datasources()

    Array.from(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
      .forEach(tooltipNode => new Tooltip(tooltipNode))
  }

}</script>

<style scoped>
.form-group{
  margin: 10px 0;
}

label{
  padding-bottom: 5px;
  font-weight: bold;
}
</style>
