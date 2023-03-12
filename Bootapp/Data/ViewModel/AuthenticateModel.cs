using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.ViewModel
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
