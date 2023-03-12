using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Entities
{
    public class Role
    {
        [Key]
        public Guid id { set; get; }
        public string role_name { set; get; }
    }
}
