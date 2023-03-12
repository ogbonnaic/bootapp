using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bootapp.Data.Model
{
    public class app_project
    {
        [Key]
        public Guid id { set; get; }
        [Required]
        public string name { set; get; }
        [Required]
        [MaxLength(10)]
        public string code { set; get; }
        [Required]
        public string created_by { set; get; }
        [Required]
        [Column(TypeName = "json")]
        public string connection_string { set; get; }
        [Required]
        public DateTime created_at { set; get; }
        public string updated_by { set; get; }
        public DateTime? updated_at { set; get; }
        [Required]
        public int active { set; get; }
        public bool enable_auth { set; get; }

        public string api_secret { set; get; }
        public string api_key { set; get; }
       

        public app_account account { set; get; }
        public app_datasource datasource { set; get; }

    }
}
