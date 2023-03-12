using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_connection
    {
        [Key]
        public Guid id { set; get; }
        [Required]
        public string connection_id { set; get; }
        [Required]
        public string path { set; get; }
        [Required]
        public Guid project_id { set; get; }
        [Required]
        public string domain { set; get; }
        [Required]
        public string status { set; get; }
        public string scheme { set; get; }
        public double? duration { set; get; }
        [Required]
        public string method { set; get; }
        public string payload { set; get; }
        public DateTime? connection_time { set; get; }
        [Required]
        public DateTime created_at { set; get; }
    }
}
