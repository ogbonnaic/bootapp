using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_file
    {
        [Key]
        public Guid id { set; get; }
        [Required]
        public string file_name { set; get; }
        [Required]
        public string original_file_name { set; get; }
        [Required]
        public string short_url { set; get; }
        [Required]
        public decimal file_size { set; get; }
        [Required]
        public Guid project_id { set; get; }
        [Required]
        public DateTime created_at { set; get; }
        public DateTime updated_at { set; get; }

        [Required]
        public int file_type_id { set; get; }
        public app_file_type file_type { set; get; }


    }
}
