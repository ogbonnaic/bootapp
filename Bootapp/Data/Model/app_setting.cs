using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_setting
    {
        [Key]
        public int id { set; get; }
        [Required]
        public string key { set; get; }
        public string value { set; get; }
        [Required]
        public bool active { set; get; }
    }
}
