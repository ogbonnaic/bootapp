import http from '../http-common'

class AccountService {
  create (data) {
    return http.post('/AppAccount/Post', data)
  }

  get (id) {
    return http.get(`/AppAccount/get/${id}`)
  }

  getAll () {
    return http.get('/AppAccount/getAll')
  }

  checkDbConnection () {
    return http.get('/AppAccount/IsDatabaseSetup')
  }

  IsAppSetup () {
    return http.get('/AppAccount/IsSetup')
  }
}
export default new AccountService()
