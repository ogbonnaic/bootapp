<template>

  <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h3 class="page-title"><font-awesome-icon icon="user" /> Users</h3>
        <div class="btn-toolbar mb-2 mb-md-0">

            <button type="button" class="btn btn-sm btn-success" data-bs-toggle="modal" data-bs-target="#new_user_modal">
               <font-awesome-icon icon="plus" /> New User
            </button>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">All Users</h5>

                            <table class="rwd-table">
            <thead>
                <tr>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Phone </th>
                    <th>Active</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users" :key="user.user_name">
<td>
    {{user.user_name}}
</td>
<td>
    {{user.email}}
</td>
<td>
    {{user.phone_number}}
</td>
<td>
    <div class="form-check form-switch">
  <input class="form-check-input" type="checkbox" :id="'fl_switch_create_'+user.id" v-model="user.active" true-value="true" v-on:change="update_user(user)"
  false-value="false" >
  <label class="form-check-label" :for="'fl_switch_create_'+user.id" ></label>
</div>
</td>
<td><button class="btn btn-sm btn-primary" v-on:click="select_user(user)">Edit</button></td>
                </tr>
            </tbody>
        </table>
                </div>
            </div>
        </div>

        <div class="col-md-4">
          <div class="card" v-if="Object.keys(user).length > 0">
            <div class="card-body">
              <h5 class="card-title">Edit User</h5>
                     <form  id="form_auth">
                <div class="form-data">
                    <div class="forms-inputs mb-4"> <span>Email</span> <input autocomplete="off" type="text" v-model="user.email" v-bind:class="{'form-control form-control-lg':true}" required>
                        <div class="invalid-feedback">A valid email is required!</div>
                    </div>

                     <div class="forms-inputs mb-4"> <span>Username</span> <input autocomplete="off" type="text" v-model="user.user_name" v-bind:class="{'form-control form-control-lg':true}" required>
                        <div class="invalid-feedback">A valid Username is required!</div>
                    </div>

                    <div class="forms-inputs mb-4"> <span>Phone number</span> <input autocomplete="off" type="text" v-model="user.phone_number" v-bind:class="{'form-control form-control-lg':true}" required>
                        <div class="invalid-feedback"></div>
                    </div>
                    <div class="mb-3"> <button type="button" v-on:click.stop.prevent="update_user" class="btn btn-dark w-100">Edit</button> </div>
                </div>
                </form>
            </div>
          </div>
        </div>

    </div>

<div class="modal fade" id="new_user_modal" tabindex="-1" aria-labelledby="newUserModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Add a New User</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
<form ref="form" id="form_user">

                <div class="form-data">
                    <div class="forms-inputs mb-4"> <span>Email or username</span> <input autocomplete="off" type="text" v-model="new_user.Email" v-bind:class="{'form-control form-control-lg':true}" required>
                        <div class="invalid-feedback">A valid email is required!</div>
                    </div>
                         <div class="forms-inputs mb-4"> <span>Phone number</span> <input autocomplete="off" type="text" v-model="new_user.PhoneNumber" v-bind:class="{'form-control form-control-lg':true}" required>
                        <div class="invalid-feedback"></div>
                    </div>
                    <div class="forms-inputs mb-4"> <span>Password</span> <input autocomplete="off" type="password" v-model="new_user.Password" v-bind:class="{'form-control form-control-lg':true}" required>
                        <div class="invalid-feedback">Password must be 8 character!</div>
                    </div>
                </div>
</form>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="button" v-on:click.stop.prevent="handleRegister" class="btn btn-primary">Register</button>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import UserService from '../../services/UserService'
export default ({
  name: 'Users',
  data () {
    return {
      users: [],
      user: {},
      loading: false,
      message: '',
      new_user: {}
    }
  },
  methods: {
    getUsers () {
      UserService.getUsers()
        .then((result) => {
          this.users = result.data
        }).catch((err) => {
          console.log(err)
        })
    },
    select_user (user) {
      this.user = user
    },
    update_user () {
      UserService.edit(this.user)
        .then((result) => {

        }).catch((err) => {
          console.log(err)
        })
    },
    handleRegister () {
      var self = this
      this.message = ''
      this.successful = false
      this.loading = true
      this.$store.dispatch('auth/register', self.new_user).then(
        (data) => {
          // this.$router.push('/login')
          this.message = data.message
          this.successful = true
          this.loading = false
          this.getUsers()
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
    }
  },
  created () {
    this.getUsers()
  }
})
</script>
