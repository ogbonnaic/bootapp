using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.ViewModel
{
    public class RegisterModel
    {
        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public string email { set; get; }
        public string phone_number { set; get; }
    }
}
