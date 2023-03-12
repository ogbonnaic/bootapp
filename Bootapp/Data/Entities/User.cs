using System;
namespace Bootapp.Data.Entities
{
    public class User
    {
     
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public string email { set; get; }
        public string phone_number { set; get; }
        
        
    }
}
