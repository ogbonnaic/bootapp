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
    <a class="nav-link active" id="home-tab" data-bs-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Email Settings</a>
  </li>
  <li class="nav-item" role="presentation">
    <a class="nav-link" id="domain-tab" data-bs-toggle="tab" href="#domain" role="tab" aria-controls="domain" aria-selected="false">Domains</a>
  </li>
  <li class="nav-item" role="presentation">
    <a class="nav-link" id="contact-tab" data-bs-toggle="tab" href="#contact" role="tab" aria-controls="contact" aria-selected="false">Contact</a>
  </li>
</ul>
<div class="tab-content" id="myTabContent">
  <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">...</div>
  <div class="tab-pane fade" id="domain" role="tabpanel" aria-labelledby="domain-tab">
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
   <h5>Domains</h5>
  <table class="table table-bordered">
    <thead>
      <tr>
        <th>Name</th>
        <th>Url</th>
        <th><button type="button" class="btn btn-sm btn-primary pull-right" data-bs-toggle="modal" data-bs-target="#new_domain_modal">
               <font-awesome-icon icon="plus" /> New Domain
            </button></th>
      </tr>
    </thead>
  </table>
</div>
     </div>
   </div>
  </div>
  <div class="tab-pane fade" id="contact" role="tabpanel" aria-labelledby="contact-tab">...</div>
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
<form ref="form" id="form_user">

                <div class="form-data">
  <div class="form-group">
    <label for="domain_name">Name</label>
    <input type="text" class="form-control" required id="domain_name">
  </div>
  <div class="form-group mt-2">
    <label for="domain_url">URL</label>
    <input type="text" class="form-control" required id="domain_url">
  </div>

                </div>
</form>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="button"  class="btn btn-success">Submit</button>
      </div>
    </div>
  </div>
</div>
</template>
<script>
export default ({
  name: 'Settings',
  data () {
    return {
      settings: []
    }
  },
  methods: {

  }

})
</script>

<style scoped>
label{
  padding-bottom: 5px;
  font-weight: bold;
}
</style>
