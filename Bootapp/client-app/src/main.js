import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import VueSweetalert2 from 'vue-sweetalert2'
import 'sweetalert2/dist/sweetalert2.min.css'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faPhone, faPlus, faPowerOff, faDashboard, faDatabase, faCog, faInfo, faUser, faCalendar, faEdit, faFolderOpen, faInfoCircle, faKey, faUserSecret, faTrash, faTable, faPencil, faArrowLeft } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(faPhone, faPlus, faPowerOff, faDashboard, faDatabase, faCog, faInfo, faUser, faCalendar, faEdit, faFolderOpen, faInfoCircle, faKey, faUserSecret, faTrash, faTable, faPencil, faArrowLeft)

const options = {
  confirmButtonColor: '#31525B'
  // cancelButtonColor: '#ff7674'
}

const app = createApp(App)
app.use(store)
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(router)
app.use(VueSweetalert2, options)
app.mount('#app')
