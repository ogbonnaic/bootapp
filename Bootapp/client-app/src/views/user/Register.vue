<template>
  <div class="body-bg">
 <div class="row d-flex justify-content-center">
        <div class="col-md-4 d-flex justify-content-center">
            <div class="card mt-5 shadow p-3 mb-5 bg-white rounded" id="form1" style="max-width: 500px; margin-top: 50px !important; width: 500px;">
              <img class="rounded-circle mx-auto mt-5 mb-3" src="https://images.unsplash.com/photo-1546776310-eef45dd6d63c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=3368&q=80" alt="Generic placeholder image" width="180" height="180">
              <div class="card-body">
 <h5 class="card-title text-center">Welcome</h5>
 <h6 class="text-center">Complete the following steps to get started</h6>
                <form  id="form_account">
                <div class="form-data mt-3">
                    <div class="forms-inputs mb-2"> <span>Email or username</span> <input autocomplete="off" type="text" v-model="user.Email" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">A valid email is required!</div>
                    </div>
                    <div class="forms-inputs mb-2"> <span>Password</span> <input autocomplete="off" type="password" v-model="user.Password" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">Password must be 8 character!</div>
                    </div>

                      <div class="forms-inputs mb-2"> <span>Confirm Password</span> <input autocomplete="off" type="password" v-model="user.CPassword" v-bind:class="{'form-control':true}" required>
                        <div class="invalid-feedback">Password must be 8 character!</div>
                    </div>
<strong>Database setup</strong>

  <div class="form-group">
        <label>Host</label>
        <input
        type="text"
        required
           v-model="connection.host"
           v-bind:class="{'form-control  mb-2':true}"
          >
        <div class="invalid-feedback">Host is required</div>
      </div>

      <div class="form-group">
        <label>Port</label>
        <input
        type="text"
        required
           v-model="connection.port"
           v-bind:class="{'form-control  mb-2':true}"
          >
        <div class="invalid-feedback">Port is required</div>
      </div>
          <div class="form-group">
        <label>User ID</label>
        <input
        type="text"
        required
           v-model="connection.user_id"
           v-bind:class="{'form-control  mb-2':true}"
          >
        <div class="invalid-feedback">User ID is required</div>
      </div>

      <div class="form-group">
        <label>Password</label>
        <input
        type="password"
        required
           v-model="connection.password"
           v-bind:class="{'form-control  mb-2':true}"
          >
        <div class="invalid-feedback">Password is required</div>
      </div>

          <div class="form-group">
        <label>Database</label>
        <input
        type="text"
        required
           v-model="connection.database"
           v-bind:class="{'form-control  mb-4':true}"
          >
        <div class="invalid-feedback">Database is required</div>
      </div>

                    <div class="alert alert-danger" role="alert" v-if="message">
  There was an error logging you in. Please confirm your username and password then try again
</div>
                    <div class="mb-3">
                        <button type="button" v-on:click.stop.prevent="handleLogin" class="btn btn-dark w-100" :disabled="disabled">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-if="loading"></span>
                            Setup Your Account
                            </button>
                        </div>
                </div>
                </form>
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
import AccountService from '../../services/AccountService'
import Validator from '../../utils/validator'

export default {
  name: 'Register',
  components: {},
  data () {
    return {
      user: {},
      loading: false,
      message: '',
      connection: {},
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
          }).catch(function (error) {
            console.log(error)
            this.$swal('Account Setup', 'There was an error setting up your. Review your entry and try again. If the issue persists contact administrator', 'error')
          })
      }
    },
    handleRegister () {
      var self = this
      this.message = ''
      this.successful = false
      this.loading = true
      this.$store.dispatch('auth/register', self.user).then(
        (data) => {
          this.$router.push('/login')
          this.message = data.message
          this.successful = true
          this.loading = false
        },
        (error) => {
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString()
          this.successful = false
          this.loading = false
        }
      )
    },
    handleLogin () {
      this.loading = true
      if (Validator.validate('form_auth')) {
        this.$store.dispatch('auth/register', this.user).then(
          () => {
            this.$router.push('/profile')
          },
          (error) => {
            this.loading = false
            this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString()
          }
        )
      }
    }
  },
  computed: {
    loggedIn () {
      return this.$store.state.auth.status.loggedIn
    }
  },
  created () {
    if (this.loggedIn) {
      // this.$router.push('/home')
    }
  }
}
</script>
