import http from '../http-common'
import authHeader from '../utils/auth-header'
class DomainService {
  getAll () {
    return http.get('/domains/getall', { headers: authHeader() })
  }

  get (id) {
    return http.get(`/domains/get/${id}`, { headers: authHeader() })
  }

  create (data) {
    return http.post('/domains/post', data, { headers: authHeader() })
  }

  update (id, data) {
    return http.put('/domains/update', data, { headers: authHeader() })
  }

  delete (id) {
    return http.delete(`/domains/delete/${id}`)
  }
}
export default new DomainService()
