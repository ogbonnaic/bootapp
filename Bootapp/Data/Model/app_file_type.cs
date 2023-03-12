using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_file_type
    {
        [Key]
        public int id { set; get; }
        [Required]
        public string format { set; get; }
        [Required]
        public string extension { set; get; }

        public ICollection<app_file> files { set; get; }
    }
}
