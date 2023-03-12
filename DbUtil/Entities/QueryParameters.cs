using System;
namespace DbUtil.Entities
{
    public class QueryParameters
    {
        /// <summary>
        /// The name of the table
        /// </summary>
        public string table { set; get; }
        /// <summary>
        /// Fields that you are pulling out from the table
        /// </summary>
        public string[] fields { set; get; } = null;
        /// <summary>
        /// The fields that you are querying
        /// </summary>
        public string[] args { set; get; } = null;
        /// <summary>
        /// The values of the fields that you are search, this should be same index as the fields
        /// </summary>
        public object[] values { get; set; } = null;
        /// <summary>
        /// The columns that you are ordering the result (this is for ascending)
        /// </summary>
        public string[] orderBy { set; get; } = null;
        /// <summary>
        /// The colums ordered with on descending order
        /// </summary>
        public string[] orderByDesc { set; get; } = null;
        /// <summary>
        /// Additional sql query that you want to add to the query
        /// </summary>
        public string append { get; set; } = null;
        /// <summary>
        /// Maximum number of rows to be retrieved from the table in the database
        /// </summary>
        const int maxPageSize = 50;
        /// <summary>
        /// The page number showing the number of records that has been pulled and should be skipped
        /// </summary>
        public int PageNumber { get; set; } = 0;
        private int _pageSize = 10;
        /// <summary>
        /// The size of rows to be read from the table
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

}
