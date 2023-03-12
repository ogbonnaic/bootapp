using System;
namespace DbUtil.Entities
{
    public class TableInfo
    {
        public string table_schema { set; get; }
        public string table_name { set; get; }
        public Single rows { set; get; }
    }
}
