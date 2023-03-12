using System;
namespace Bootapp.Data.ViewModel
{
    public class UpdateUserModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { set; get; }
        public string phone_number { set; get; }
    }
}
