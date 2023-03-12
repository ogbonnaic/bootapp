using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bootapp.Data.Model
{
    public class app_project_table_setting
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        [Required]
        public Guid project_id { set; get; }
        [Required]
        public string table_name { set; get; }
        public string table_schema { set; get; }
        public bool create { set; get; }
        public bool update { set; get; }
        public bool read { set; get; }
        public bool delete { set; get; }
        
    }
}
