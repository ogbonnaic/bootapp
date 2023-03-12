import http from '../http-common'
import authHeader from '../utils/auth-header'
class ProjectService {
  getAll () {
    return http.get('/project/GetProjects', { headers: authHeader() })
  }

  get (id) {
    return http.get(`/project/get/${id}`, { headers: authHeader() })
  }

  getProjectTablesWithSettings (id) {
    return http.get(`/project/GetProjectTablesWithSettings/${id}`, { headers: authHeader() })
  }

  create (data) {
    return http.post('/project/post', data, { headers: authHeader() })
  }

  update (data) {
    return http.put('/project/update', data, { headers: authHeader() })
  }

  delete (id) {
    return http.delete(`/tutorials/${id}`)
  }

  getProjectTables (id) {
    // return http.get(`/tutorials?title=${title}`)
    return http.get(`/project/GetProjectTables/${id}`, { headers: authHeader() })
  }

  getTableColumns (id, schema, table) {
    // return http.get(`/tutorials?title=${title}`)
    return http.get(`/project/GetTableColumns/${id}/${schema}/${table}`, { headers: authHeader() })
  }
}
export default new ProjectService()
