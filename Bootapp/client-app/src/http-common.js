import axios from 'axios'
export default axios.create({
  baseURL: '/api',//'http://localhost:5000/api',
  headers: {
    'Content-type': 'application/json'
  }
})
