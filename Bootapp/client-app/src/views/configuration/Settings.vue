<template>
   <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h3 class="page-title"><font-awesome-icon icon="cog" /> Settings
            <!--  <span class="badge bg-warning rounded-pill ">{{project.datasource.name}}</span> -->
        </h3>
        <div class="btn-toolbar mb-2 mb-md-0">

       <!--      <button type="button" class="btn btn-sm btn-primary">
                Back
            </button> -->
        </div>
    </div>

<div class="row">
  <div class="col-md-12">

<ul class="nav nav-tabs" id="myTab" role="tablist">

  <li class="nav-item" role="presentation">
    <a class="nav-link active" id="domain-tab" data-bs-toggle="tab" href="#domain" role="tab" aria-controls="domain" aria-selected="false">Domains</a>
  </li>

</ul>
<div class="tab-content" id="myTabContent">

  <div class="tab-pane fade show active" id="domain" role="tabpanel" aria-labelledby="domain-tab">
   <div class="row">
     <div class="col-md-12">
       <div class="mt-3">
 <!--       <form class="form-inline" action="">
         <h5>Add a new Domain</h5>
  <div class="form-group">
    <label for="domain_name">Name</label>
    <input type="text" class="form-control" id="domain_name">
  </div>
  <div class="form-group mt-2">
    <label for="domain_url">Url</label>
    <input type="text" class="form-control" id="domain_url">
  </div>

  <button type="button" class="btn btn-primary mt-2">Add Domain</button>
</form> -->
</div>

<div class="col-md-12 mt-3">
   <h5>Domains <button type="button" class="btn btn-sm btn-primary rounded-circle" data-bs-toggle="modal" data-bs-target="#new_domain_modal">
               <font-awesome-icon icon="plus" />
            </button></h5>
  <table class="rwd-table">
    <thead>
      <tr>
        <th>Name</th>
        <th>URL</th>
        <th></th>
      </tr>
    </thead>

               <tbody>
                <tr v-for="(domain, index) in domains" :key="domain.id">
<td>
    {{domain.name}}
</td>
<td>
    {{domain.domain}}
</td>
<td>
<button class="btn btn-sm btn-danger rounded" v-on:click="deleteDomain(domain.id, index)" type="button"><font-awesome-icon icon="trash" /> Delete</button>
</td>

                </tr>
            </tbody>
  </table>
</div>
     </div>
   </div>
  </div>

</div>
  </div>
</div>

<div class="modal fade" id="new_domain_modal" tabindex="-1" aria-labelledby="newDomainModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Add a New Domain</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
<form ref="form" id="form_domain">

                <div class="form-data">
  <div class="form-group">
    <label for="domain_name">Name</label>
    <input type="text" class="form-control" required id="domain_name" v-model="domain.name">
  </div>
  <div class="form-group mt-2">
    <label for="domain_url">URL</label>
    <input type="url" class="form-control" required id="domain_url" v-model="domain.domain">
  </div>

                </div>
</form>

      </div>
      <div class="modal-footer">
        <button type="button" ref="Close" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="button"  v-on:click="submit" class="btn btn-success">Submit</button>
      </div>
    </div>
  </div>
</div>
</template>
<script>
import DomainService from '../../services/DomainService'
import Validator from '../../utils/validator'
export default ({
  name: 'Settings',
  data () {
    return {
      settings: [],
      domain: {},
      domains: [],
      message: '',
      loading: false,
      disabled: false
    }
  },
  methods: {
    get_domains () {
      var self = this
      self.loading = true
      self.message = 'Loading projects. Please wait...'
      DomainService.getAll()
        .then((response) => {
          self.domains = response.data
          self.loading = false
        })
        .catch((error) => {
          console.log(error)
        })
    },
    submit () {
      var self = this
      self.loading = true
      self.disabled = true

      if (Validator.validate('form_domain')) {
        DomainService.create(self.domain)
          .then((response) => {
            self.domains.push(response.data)
            self.loading = false
            self.disabled = false
            this.$refs.Close.click()
          })
      }
    },
    deleteDomain (id, index) {
      var self = this
      self.loading = true
      // self.message = 'Loading projects. Please wait...'
      DomainService.delete(id)
        .then((response) => {
          self.domains.splice(index, 1)
          self.loading = false
        })
        .catch((error) => {
          console.log(error)
        })
    }
  },
  mounted () {
    this.get_domains()
  }

})
</script>

<style scoped>
label{
  padding-bottom: 5px;
  font-weight: bold;
}
</style>
