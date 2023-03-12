using System;
namespace DbUtil.Entities
{
    public class PutParameters
    {
        /// <summary>
        /// The table that is been updated
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// The columns in the table to be updated
        /// </summary>
        public string[] toAlter { get; set; }
        /// <summary>
        /// The values of the of the columns to the altered (this should be in the same order with the toAlter)
        /// </summary>
        public object[] alValues { get; set; }
        /// <summary>
        /// The columns with search the criteria 
        /// </summary>
        public string[] args { get; set; }
        /// <summary>
        /// The values to search the criteria columns with
        /// </summary>
        public object[] values { get; set; }
    }
}
