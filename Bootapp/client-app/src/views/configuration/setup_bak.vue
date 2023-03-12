<template>

<div class="body-bg">
    <div class="container mt-5">
        <div class="row  d-flex justify-content-center">
            <div class="col-md-6">
                <div class="card">
                     <img class="rounded-circle mx-auto mt-5 mb-3" src="data:image/gif;base64,R0lGODlhAQABAIAAAHd3dwAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" alt="Generic placeholder image" width="180" height="180">
                    <div class="card-body">
<h5 class="card-title text-center">Setup your new Account</h5>
                    </div>
                    <div class="accordion" id="accordionPanelsStayOpenExample">
  <div class="accordion-item">
    <h2 class="accordion-header" id="panelsStayOpen-headingOne">
      <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
        Account Information
      </button>
    </h2>
    <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
      <div class="accordion-body">
        <div class="form-group">
        <label for="name">Account Name</label>
        <input type="text" required v-model="account.name" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Name of the Account is required</div>
      </div>

   <div class="form-group mt-2">
        <label for="name">Code</label>
        <input type="text" required v-model="account.code" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Account code is required</div>
      </div>
      </div>
    </div>
  </div>
  <div class="accordion-item">
    <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
      <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
        Database Configuration
      </button>
    </h2>
    <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-headingTwo">
      <div class="accordion-body">

  <div class="form-group">
        <label for="name">Server</label>
        <input type="text" required v-model="config.server" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Name of the Account is required</div>
      </div>

   <div class="form-group mt-2">
        <label for="name">Database</label>
        <input type="text" required v-model="config.databas" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Account code is required</div>
      </div>

      <div class="form-group mt-2">
        <label for="name">Username</label>
        <input type="text" required v-model="config.databas" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Account code is required</div>
      </div>

      <div class="form-group mt-2">
        <label for="name">Password</label>
        <input type="text" required v-model="config.databas" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Account code is required</div>
      </div>

      <div class="form-group mt-2">
        <label for="name">Port</label>
        <input type="text" required v-model="config.databas" v-bind:class="{'form-control':true}">
        <div class="invalid-feedback">Account code is required</div>
      </div>

      </div>
    </div>
  </div>
  <div class="accordion-item">
    <h2 class="accordion-header" id="panelsStayOpen-headingThree">
      <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseThree" aria-expanded="false" aria-controls="panelsStayOpen-collapseThree">
        Admin User Information
      </button>
    </h2>
    <div id="panelsStayOpen-collapseThree" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-headingThree">
      <div class="accordion-body">
          <div class="form-data">
                    <div class="forms-inputs mb-4"> <span>Email</span> <input autocomplete="off" type="text" v-model="user.email" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">A valid email is required!</div>
                    </div>

                     <div class="forms-inputs mb-4"> <span>Username</span> <input autocomplete="off" type="text" v-model="user.user_name" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">A valid Username is required!</div>
                    </div>

                    <div class="forms-inputs mb-4"> <span>Phone number</span> <input autocomplete="off" type="text" v-model="user.phone_number" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback"></div>
                    </div>

                </div>
      </div>
    </div>
  </div>
</div>
<div class="card-body">
     <button type="button"  class="btn btn-primary btn-lg w-100" :disabled="disabled">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-if="loading"></span>
                            Setup your Account
                            </button>
</div>
                </div>

            </div>
        </div>

    </div>
</div>
    <!-- MultiStep Form -->

</template>

<script>
export default {
  name: 'Setup',
  data () {
    return {
      account: {},
      config: {},
      user: {},
      loading: false,
      disabled: false
    }
  },
  created () {
    // alert(jQuery('#app').val())
  }

}

</script>
