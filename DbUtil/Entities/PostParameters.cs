using System;
namespace DbUtil.Entities
{
    public class PostParameters
    {
        /// <summary>
        /// The name of the table that data will be inserted into
        /// </summary>
        public string table { set; get; }
        /// <summary>
        /// The fields that data willl be inserted into
        /// </summary>
        public string[] fields { set; get; }
        /// <summary>
        /// The values to be inserted into the corresponding fields in the table
        /// </summary>
        public object[] values { set; get; }
    }
}
