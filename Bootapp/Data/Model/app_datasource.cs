using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_datasource
    {
        [Key]
        public int id { set; get; }
        [Required]
        public string name { set; get; }
        [Required]
        public int active { set; get; }
        public string icon { set; get; }
    }
}
