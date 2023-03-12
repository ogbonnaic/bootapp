using System;
namespace Bootapp.Data.Model
{
    public class audit
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string type { get; set; }
        public string table_name { get; set; }
        public DateTime date_time { get; set; }
        public string old_values { get; set; }
        public string new_values { get; set; }
        public string affected_columns { get; set; }
        public string primary_key { get; set; }
    }
}
