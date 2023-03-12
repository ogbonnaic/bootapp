using System;
using Bootapp.Data.Model;
using static Bootapp.Controllers.AccountController;

namespace Bootapp.Data.ViewModel
{
    public class AccountSetupModel
    {
        public app_account account { set; get; }
        public RegisterDto reg { set; get; }
        //public DbConfig config { set; get; }
    }

    public class DbConfig
    {
        public string server { set; get; }
        public string database { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public string port { set; get; }
    }
}
