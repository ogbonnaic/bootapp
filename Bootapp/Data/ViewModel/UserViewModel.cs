using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Bootapp.Data.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(IdentityUser user,List<string> roles)
        {
            email = user.Email;
            user_name = user.UserName;
            phone_number = user.PhoneNumber;
            this.roles = roles;
            active = user.LockoutEnabled;
            id = user.Id;
        }

        public string id { set; get; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public bool active { get; set; }
        public List<string> roles { get; set; }
    }
}
