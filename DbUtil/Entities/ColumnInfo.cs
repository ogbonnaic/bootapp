using System;
namespace DbUtil.Entities
{
    public class ColumnInfo
    {
        public string column_name { set; get; }
        public string data_type { set; get; }
        public string is_nullable { set; get; }
        public string constraint_type { set; get; }

    }
}
