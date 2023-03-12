using System;
namespace Bootapp.Data.ViewModel
{
    public class TableViewModel
    {
        public int id { set; get; }
        public string table_schema { set; get; }
        public string table_name { set; get; }
        public Single rows { set; get; }

        public bool create { set; get; }
        public bool update { set; get; }
        public bool read { set; get; }
        public bool delete { set; get; }
    }
}
