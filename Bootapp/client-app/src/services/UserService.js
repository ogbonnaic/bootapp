import http from '../http-common'
import authHeader from '../utils/auth-header'

class UserService {
  login (user) {
    return http.post('/account/login', { Email: user.Email, Password: user.Password })
      .then(response => {
        if (response.data != null && response.data != null) {
          localStorage.setItem('user', JSON.stringify(response.data))
        }
        // console.log(response.data)
        return response.data
      })
  }

  logout () {
    localStorage.removeItem('user')
  }

  register (user) {
    return http.post('/account/register', { Email: user.Email, Password: user.Password, PhoneNumber: user.PhoneNumber })
  }

  edit (user) {
    return http.post('/user/update', user, { headers: authHeader() })
  }

  getUsers () {
    return http.get('/user/GetUsers', { headers: authHeader() })
  }
}

export default new UserService()
