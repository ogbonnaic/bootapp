class Validator {
  validateEmail3 (email) {
    if (email) {
      var re = /(.+)@(.+){2,}\.(.+){2,}/
      return re.test(email.toLowerCase())
    } else {
      return false
    }
  }

  validate (formId) {
    var valid = true
    var elements = document.getElementById(formId).elements
    for (var i = 0; i < elements.length; i++) {
      var element = elements[i]
      if (element.required) {
        valid = this.validateText(element)
        switch (element.type) {
          case 'email':
            valid = this.validateEmail(element)
            break
          case 'url':
            valid = this.validateUrl(element)
            break
          default:
            break
        }
      }
    }
    console.log(valid)
    return valid
  }

  validateEmail (element) {
    var result = false
    if (element.value) {
      var re = /(.+)@(.+){2,}\.(.+){2,}/
      result = re.test(element.value.toLowerCase())
    }

    if (result) {
      this.validateElement(element)
    } else {
      this.invalidateElement(element)
    }

    return result
  }

  validateUrl (element) {
    var result = false
    if (element.value) {
      var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
    '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
    '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
    '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
    '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
    '(\\#[-a-z\\d_]*)?$', 'i') // fragment locator
      result = pattern.test(element.value.toLowerCase())

      // var re = /(.+)@(.+){2,}\.(.+){2,}/
      // result = re.test(element.value.toLowerCase())
    }

    if (result) {
      this.validateElement(element)
    } else {
      this.invalidateElement(element)
    }

    return result
  }

  validateText (element) {
    if (element.value) {
      this.validateElement(element)
      return true
    }
    this.invalidateElement(element)
    return false
  }

   invalidateElement = function (element) {
     element.classList.add('is-invalid')
   }

  validateElement =function (element) {
    element.classList.remove('is-invalid')
  }
}

export default new Validator()
