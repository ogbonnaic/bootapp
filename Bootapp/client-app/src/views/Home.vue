/* eslint-disable no-tabs */
<template>

    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h1 class="page-title"><font-awesome-icon icon="dashboard" /> Dashboard</h1>
       <!--  <div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group me-2">
                <button type="button" class="btn btn-sm btn-outline-secondary">Share</button>
                <button type="button" class="btn btn-sm btn-outline-secondary">Export</button>
            </div>
            <button type="button" class="btn btn-sm btn-outline-secondary dropdown-toggle">
                <span data-feather="calendar"></span>
                This week
            </button>
        </div> -->
    </div>

    <div class="row">
      <div class="col-md-12">
      <div class="card shadow">
        <div class="card-body">
          <h5 class="card-title"> Requests</h5>
          <div id="chart">
        <apexchart type="line" height="350" :options="chartOptions" :series="series" ></apexchart>
      </div>
        </div>
      </div>
      </div>

    </div>
    <div class="row">

        <div class="col-md-12">
            <div class="col-md-12">
    <div class="card mt-3 mb-3 shadow">
      <div class="card-body">
 <h5 class="card-title">Project Requests</h5>
  <table class="rwd-table">
    <thead>
      <tr>
        <th>Code</th>
        <th>Project</th>
        <th>Data Source</th>
        <th>Requests</th>
        <th>Added</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="project in projects" :key="project.id">
        <td>{{project.code}}</td>
        <td>{{project.name}}</td>
        <td><font-awesome-icon icon="database" /> {{project.datasource.name}}</td>
        <td>
          <span v-for="req in project_connections.filter(d=>d.project_id==project.id)" :key="req" >
{{req.requests}}
          </span>
        </td>
        <td>
          <font-awesome-icon icon="calendar" />
          <DateFromNow :date="project.created_at"/>
        </td>
      </tr>
    </tbody>

  </table>
      </div>

            </div>
        </div>
    </div>
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
import AnalyticsService from '../services/AnalyticsService'
import ProjectService from '../services/ProjectService'
import DateFromNow from '../components/DateFromNow.vue'
// import SmartTable from 'vuejs-smart-table'
import VueApexCharts from 'vue3-apexcharts'
export default {
  name: 'Home',
  components: {
    apexchart: VueApexCharts, DateFromNow
  },
  data () {
    return {
      projects: [],
      project_connections: [],
      series: [{
        name: 'Connection',
        type: 'line',
        data: []
      }],

      chartOptions: {
        labels: [],
        xaxis: {
          type: 'date'
        }
      }
    }
  },
  computed: {
    loggedIn () {
      return this.$store.state.auth.status.loggedIn
    }
  },
  methods: {
    get_connection_summary () {
      // var self = this
      AnalyticsService.getGetConnectionSummary()
        .then((result) => {
          this.project_connections = result.data.project_connections
          // this.categories = result.data.categories
          this.series = [{
            name: 'Connection',
            type: 'line',
            data: result.data.data
          }]

          this.chartOptions = {
            chart: {
              toolbar: {
                show: false
              },
              height: 350,
              type: 'line',
              fontFamily: 'Avenir, Helvetica, Arial, sans-serif'
            },
            stroke: {
              curve: 'smooth',
              width: 2
            },
            colors: ['#E59A59'],
            dataLabels: {
              enabled: true,
              enabledOnSeries: [0]
            },
            labels: result.data.categories,
            xaxis: {
              type: 'date'
            }
          }
        }).catch((err) => {
          console.log(err)
        })
    },
    get_projects () {
      ProjectService.getAll()
        .then((result) => {
          this.projects = result.data
        }).catch((err) => {
          console.log(err)
        })
    }

  },
  mounted () {
    this.get_connection_summary()
  },
  created () {
    if (!this.loggedIn) {
      this.$router.push('/login')
    }
    this.get_projects()
  }

}
</script>
<style scoped>

</style>
