import moment from 'moment'

class DateFormatter {
  fromNow (date) {
    return moment.fromNow(date)
  }
}

export default new DateFormatter()
