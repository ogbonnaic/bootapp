<template>
    <!--account name, code, username, email, password-->
<div class="body-bg">
    <div class="container mt-5">
        <div class="row  d-flex justify-content-center">
            <div class="col-md-6">
                <div class="card">
                     <img class="rounded-circle mx-auto mt-5 mb-3" src="data:image/gif;base64,R0lGODlhAQABAIAAAHd3dwAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" alt="Generic placeholder image" width="180" height="180">
                    <div class="card-body">
<h5 class="card-title text-center">Setup your new Account</h5>

 <form id="regForm">

     <div style="text-align:center;margin-top:40px;">

  </div>
  <!-- One "tab" for each step in the form: -->
  <div >
    <fieldset>
      <legend>Account Information</legend>
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
    </fieldset>

    <fieldset class="mt-3">
      <legend>Admin User Setup</legend>
               <div class="forms-inputs mb-2"> <span>Email</span> <input autocomplete="off" type="text" v-model="user.email" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">A valid email is required!</div>
                    </div>

                     <div class="forms-inputs mb-2"> <span>Username</span> <input autocomplete="off" type="text" v-model="user.user_name" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">A valid Username is required!</div>
                    </div>

                    <div class="forms-inputs mb-2"> <span>Phone number</span> <input autocomplete="off" type="text" v-model="user.phone_number" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback"></div>
                    </div>
    </fieldset>

  </div>

  <div style="overflow:auto;" class="mt-2 ">
    <div style="float:right;">
      <!-- <button type="button" class="btn btn-warning " id="prevBtn" @click="nextPrev(-1)">Previous</button> -->
      <button type="button" id="nextBtn" class="btn btn-primary mx-2" >Get Started</button>
    </div>
  </div>
  <!-- Circles which indicates the steps of the form: -->

</form>

</div>
                </div>

            </div>
        </div>

    </div>
</div>
    <!-- MultiStep Form -->

</template>

<script>
// import wizard from '../../utils/wizard-util'
import AccountService from '../../services/AccountService'
import Validator from '../../utils/validator'

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
  methods: {

    submit () {
      var self = this
      self.loading = true
      self.disabled = true
      // console.log(this.$refs.form)
      // self.isValid = Validator.validateEmail(self.email)
      if (Validator.validate('form_account')) {
        AccountService.create({ reg: this.user, config: this.connection })
          .then((response) => {
            self.loading = false
            self.disabled = false

            this.$swal('Account Setup', 'Your account has been successfully setup.', 'success')
            this.$router.push('/login')
          }).catch(function (error) {
            console.log(error)
            this.$swal('Account Setup', 'There was an error setting up your. Review your entry and try again. If the issue persists contact administrator', 'error')
          })
      }
    }
  },
  created () {
    if (this.loggedIn) {
      this.$router.push('/home')
    }
  },
  mounted () {

  },
  computed: {
    loggedIn () {
      return this.$store.state.auth.status.loggedIn
    }
  }

}

</script>
<style scoped>

/* Mark input boxes that gets an error on validation: */
input.invalid {
  background-color: #ffdddd;
}

/* Hide all steps by default: */
.tab {
  display: none;
}

/* Make circles that indicate the steps of the form: */
.step {
  height: 15px;
  width: 15px;
  margin: 0 2px;
  background-color: #bbbbbb;
  border: none;
  border-radius: 50%;
  display: inline-block;
  opacity: 0.5;
}

.step.active {
  opacity: 1;
}

/* Mark the steps that are finished and valid: */
.step.finish {
  background-color: #04AA6D;
}
</style>
