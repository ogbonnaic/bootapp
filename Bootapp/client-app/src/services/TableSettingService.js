import http from '../http-common'
import authHeader from '../utils/auth-header'
class TableSettingService {
  getProjectTableSettings (projectid) {
    return http.get(`/TableSetting/GetProjectTableSettings/${projectid}`, { headers: authHeader() })
  }

  update (data) {
    return http.put('/TableSetting/update', data)
  }
}
export default new TableSettingService()
