using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_domain
    {
        [Key]
        public Guid id { set; get; }
        [Required]
        public string domain { set; get; }
        [Required]
        public int status { set; get; }
        [Required]
        public string name { set; get; }
    }
}
