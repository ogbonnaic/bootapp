using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Entities
{
    public class UserRole
    {
        [Key]
        public Guid role_id { set; get; }
        [Key]
        public Guid user_id { set; get; }
    }
}
