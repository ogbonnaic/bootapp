using System;
using System.ComponentModel.DataAnnotations;

namespace Bootapp.Data.Model
{
    public class app_account
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
        public DateTime created_at { set; get; }
        public string updated_by { set; get; }
        public DateTime? updated_at { set; get; }
        [Required]
        public int active { set; get; }
    }
}
