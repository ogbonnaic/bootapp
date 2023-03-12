<template>
<div id="app">

    <!--<div id="nav">
        <router-link to="/">Home</router-link> |
        <router-link to="/about">About</router-link>
    </div>-->
    <div v-if="currentRouteName == '/login' || currentRouteName == '/register' || currentRouteName == '/setup'">
          <router-view />
    </div>
<div v-else>
    <header class="navbar navbar-dark sticky-top bg-menu flex-md-nowrap p-0 shadow">
        <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" href="#">
        <img v-bind:src="require('./assets/logo-flat.png')" style="height:40px" />
        </a>
        <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <!--<input class="form-control form-control-dark w-100" type="text" placeholder="Search" aria-label="Search">-->
        <div class="navbar-nav">
            <div class="nav-item text-nowrap">
                <a class="nav-link px-3" style="color:white" href="#" @click.prevent="logOut"><font-awesome-icon icon="power-off" /> Sign out</a>
            </div>
        </div>
    </header>
    <div class="container-fluid">
        <div class="row">
            <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-sidemenu min-vh-100 sidebar collapse">
                <div class="position-sticky pt-3">
                    <ul class="nav flex-column">
                        <li class="nav-item">
                          <router-link to="/"> <font-awesome-icon icon="dashboard" /> Dashboard</router-link>
                        </li>
                       <!--  <li class="nav-item">
                            <router-link to="/">Home</router-link>
                        </li> -->

                        <li class="nav-item">
                            <router-link to="/projects"><font-awesome-icon icon="database" /> Projects</router-link>
                        </li>
                        <li class="nav-item">
                           <router-link to="/users"><font-awesome-icon icon="user" /> Users</router-link>
                        </li>
                        <li class="nav-item">
                           <router-link to="/settings"><font-awesome-icon icon="cog" /> Settings</router-link>
                        </li>
                             <li class="nav-item">
                            <router-link to="/about"> <font-awesome-icon icon="info" /> About</router-link>
                        </li>

                    </ul>

                </div>
            </nav>

            <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">

                        <router-view />

            </main>
        </div>
    </div>
</div>
</div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;

}
.card{
  border-radius: .6rem !important;
}
.bg-menu {
  background-color: #31525B;
}
.bg-sidemenu{
  /* background-color: #F6F4E8; */
  background-color: #E8ECEB;
}

.btn-primary {
  background-color: #31525B !important;
  border-color: #31525B !important;
}
.card-title{
  font-size: 1.1rem;
  padding-bottom: 10px;
}

.nav-item a {
    display: block;
    padding: 0.5rem 1rem;
    color: #31525B;
    text-decoration: none;
    transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out;
}
.navbar {
  height: 60px;
}
a.router-link-exact-active {
  color: #42b983;
  font-weight: 600;
}

.page-title {
    font-size: 1.2em;
    color: #31525B;
}

.rwd-table {
  margin: auto;
  min-width: 100%;
  max-width: 100%;
  border-collapse: collapse;
}

.rwd-table thead tr:first-child {
  border-top: none;
  /* background: #428bca; */
  color: #fff;
  background-color: #8CBDB9;
}

.rwd-table tr {
  border-top: 1px solid #ddd;
  border-bottom: 1px solid #ddd;
  background-color: #f5f9fc;
}

/* .rwd-table tr:nth-child(odd):not(:first-child) {
  background-color: #ebf3f9;
} */

.rwd-table th {
  display: none;
}

.rwd-table td {
  display: block;
}

.rwd-table td:first-child {
  margin-top: .5em;
}

.rwd-table td:last-child {
  margin-bottom: .5em;
}

.rwd-table td:before {
  content: attr(data-th) ": ";
  font-weight: bold;
  width: 120px;
  display: inline-block;
  color: #000;
}

.rwd-table th,
.rwd-table td {
  text-align: left;
}

.rwd-table {
  color: #333;
  border-radius: .4em;
  overflow: hidden;
}

.rwd-table tr {
  border-color: #bfbfbf;
}

.rwd-table tbody tr:last-of-type {
  border-bottom: 2px solid #8CBDB9;
}

.rwd-table th,
.rwd-table td {
  padding: .5em 1em;
}
@media screen and (max-width: 601px) {
  .rwd-table tr:nth-child(2) {
    border-top: none;
  }
}
@media screen and (min-width: 600px) {
  .rwd-table tr:hover {
    background-color: #d8e7f3;
  }
  .rwd-table td:before {
    display: none;
  }
  .rwd-table th,
  .rwd-table td {
    display: table-cell;
    padding: .25em .5em;
  }
  .rwd-table th:first-child,
  .rwd-table td:first-child {
    padding-left: 0;
  }
  .rwd-table th:last-child,
  .rwd-table td:last-child {
    padding-right: 0;
  }
  .rwd-table th,
  .rwd-table td {
    padding: .5em !important;
  }

  .popover-body, .tooltip{
     font-family: Avenir, Helvetica, Arial, sans-serif;
  }
}
</style>

<script>
import { useRoute } from 'vue-router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'

export default {
  computed: {
    loggedIn () {
      return this.$store.state.auth.status.loggedIn
    },
    user () {
      return this.$store.state.auth.user
    },
    currentRouteName () {
      const route = useRoute()
      return route.path
    }
  },
  methods: {
    logOut () {
      this.$store.dispatch('auth/logout')
      this.$router.push('/login')
    }
  },

  created () {
    if (!this.loggedIn) {
      // this.$router.push('/login')
    }
  },

  mounted () {
    // inti tooltip

  }
}
</script>
