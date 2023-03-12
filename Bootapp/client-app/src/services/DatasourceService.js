import http from '../http-common'
import authHeader from '../utils/auth-header'
class DatasourceService {
  getAll () {
    return http.get('/datasource/getall', { headers: authHeader() })
  }

  getActive () {
    return http.get('/datasource/getactive', { headers: authHeader() })
  }

  get (id) {
    return http.get(`/tutorials/${id}`)
  }

  create (data) {
    return http.post('/tutorials', data)
  }

  update (id, data) {
    return http.put(`/tutorials/${id}`, data)
  }

  delete (id) {
    return http.delete(`/tutorials/${id}`)
  }

  findByTitle (title) {
    return http.get(`/tutorials?title=${title}`)
  }
}
export default new DatasourceService()
