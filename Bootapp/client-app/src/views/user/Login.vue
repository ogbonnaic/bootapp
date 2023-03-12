<template>
 <div class="body-bg">
    <div class="row d-flex justify-content-center">
        <div class="col-md-4 d-flex justify-content-center">
            <div class="card mt-5 shadow p-3 mb-5 bg-white rounded" id="form1" style="max-width: 450px; margin-top: 150px !important; width: 450px;">
              <!-- <img class="rounded-circle mx-auto mt-5 mb-3" v-bind:src="require('../../assets/logo_new.png')" alt="Generic placeholder image"  height="300"> -->
                <img class="mx-auto mt-5 mb-3" v-bind:src="require('../../assets/logo_new.png')"
                  alt="Generic placeholder image" height="180">
              <div class="card-body">
                <div class="row" v-if="!setup && db_connected">
 <h5 class="card-title text-center">Welcome Back</h5>
 <h6 class="text-center">Enter your credentials to log into your account</h6>
                <form  id="form_auth">
                <div class="form-data mt-3">
                    <div class="forms-inputs mb-4"> <span>Email</span> <input autocomplete="off" type="text" v-model="user.Email" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">A valid email is required!</div>
                    </div>
                    <div class="forms-inputs mb-4"> <span>Password</span> <input autocomplete="off" type="password" v-model="user.Password" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">Password must be 8 character!</div>
                    </div>
                    <div class="alert alert-danger" role="alert" v-if="message">
  There was an error logging you in. Please confirm your username and password then try again
</div>
                    <div class="mb-3" style="float:right;">
                          <!--<button type="button" v-on:click.stop.prevent="handleLogin" class="btn btn-dark w-100" :disabled="disabled">
                          <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-if="loading"></span>
                            Login
                            </button> -->

                            <!-- <button type="button" class="btn btn-warning " id="prevBtn" v-on:click="setup=true">Setup Account</button> -->
                            <button type="button" id="nextBtn" class="btn btn-primary mx-2" v-on:click.stop.prevent="handleLogin"  :disabled="disabled"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-if="loading"></span>
                            Login</button>
                        </div>
                </div>
                </form>
</div>

                  <div class="row" v-if="setup && db_connected">
                    <h5 class="card-title text-center">Setup your new Account</h5>
                    <form id="regForm">

                      <div style="text-align:center;margin-top:40px;">

                      </div>
                      <!-- One "tab" for each step in the form: -->
                      <div>
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
                          <div class="forms-inputs mb-2"> <span>Email</span> <input autocomplete="off" type="text" v-model="user.email"
                              v-bind:class="{'form-control':true}" required>
                            <div class="invalid-feedback">A valid email is required!</div>
                          </div>

                          <!-- <div class="forms-inputs mb-2"> <span>Username</span> <input autocomplete="off" type="text"
                              v-model="user.user_name" v-bind:class="{'form-control':true}" required>
                            <div class="invalid-feedback">A valid Username is required!</div>
                          </div> -->

                          <div class="forms-inputs mb-2"> <span>Phone number</span> <input autocomplete="off" type="text"
                              v-model="user.phone_number" v-bind:class="{'form-control':true}" required>
                            <div class="invalid-feedback"></div>
                          </div>

                            <div class="forms-inputs mb-2"> <span>Password</span> <input autocomplete="off" type="password" v-model="user.Password"
                                v-bind:class="{'form-control':true}" required>
                              <div class="invalid-feedback">Password must be 8 character!</div>
                            </div>
                        </fieldset>

                      </div>

                      <div style="overflow:auto;" class="mt-2 ">
                        <div style="float:right;">
                          <!-- <button type="button" class="btn btn-warning " id="prevBtn" @click="nextPrev(-1)">Previous</button> -->
                          <button type="button" id="nextBtn" class="btn btn-primary mx-2" v-on:click="createAccount">Get Started</button>
                        </div>
                      </div>
                      <!-- Circles which indicates the steps of the form: -->

                    </form>
                  </div>

<div class="row" v-if="!db_connected">
  <div class="col-md-12">
<h5 class="card-title text-center">Database error</h5>
<p>
  There was an error connecting to the the database. Can you check the connection string to ensure everything is ok or
  contact the admin
</p>

<div class="mb-3" style="float:right;">

  <button type="button" class="btn btn-warning " id="prevBtn" v-on:click="this.$router.go(0)">Reload</button>

</div>
  </div>

</div>
                  <div>

                  </div>
              </div>
            </div>
        </div>
    </div>

</div>
</template>
<style>
  .body-bg {
    /* background: linear-gradient(-45deg, #23a6d5, #23d5ab); */
    background-size: 100% 100%;
    animation: gradient 15s ease infinite;
    height: 100vh;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    background-position: top;
    /* background-color: #31525B; */
    background-color: #E8ECEB;
    overflow-x: hidden;
  }

  @keyframes gradient {
    0% {
      background-position: 0% 50%;
    }
    50% {
      background-position: 100% 50%;
    }
    100% {
      background-position: 0% 50%;
    }
  }

</style>
<script>
import Validator from '../../utils/validator'
import AccountService from '../../services/AccountService'

export default {
  name: 'Login',
  components: {},
  data () {
    return {
      user: {},
      loading: false,
      message: '',
      disabled: false,
      error: false,
      account: {},
      config: {},
      setup: false,
      db_connected: false

    }
  },
  methods: {
    // method for creating a new account
    createAccount () {
      var self = this
      self.loading = true
      self.disabled = true
      if (Validator.validate('regForm')) {
        AccountService.create({ reg: this.user, account: this.account })
          .then((response) => {
            self.loading = false
            self.disabled = false

            this.$swal('Account Setup', 'Your account has been successfully setup.', 'success')
            this.setup = false
            this.user = {}
            this.account = {}
          }).catch(function (error) {
            console.log(error)
            this.$swal('Account Setup', 'There was an error setting up your. Review your entry and try again. If the issue persists contact administrator', 'error')
          })
      }
    },
    // this method logs the user into the application
    handleLogin () {
      this.loading = true
      this.disabled = true
      this.error = false
      if (Validator.validate('form_auth')) {
        this.$store.dispatch('auth/login', this.user).then(
          () => {
            this.$router.push('/')
          },
          (error) => {
            this.loading = false
            this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString()
            this.error = true
            this.loading = false
            this.disabled = false
          }
        )
      } else {
        this.loading = false
        this.disabled = false
      }
    },
    getAccounts () {
      AccountService.getAll()
        .then((result) => {
          if (result.data.length === 0 || result.data == null) {
            this.setup = true
          }
        }).catch((err) => {
          console.log(err)
        })
    },
    checkDbConnection () {
      AccountService.checkDbConnection()
        .then((result) => {
          this.db_connected = result.data
        }).catch((err) => {
          console.log(err)
        })
    },

    checkAppSetup () {
      AccountService.IsAppSetup()
        .then((result) => {
          this.setup = result.data
        }).catch((err) => {
          console.log(err)
        })
    }
  },
  computed: {
    loggedIn () {
      return this.$store.state.auth.status.loggedIn
    }
  },
  created () {
    this.getAccounts()
    this.checkDbConnection()
    if (this.loggedIn) {
      this.$router.push('/')
    }
  }
}
</script>
